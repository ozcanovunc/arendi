using Arendi.Common;
using Arendi.Data;
using Arendi.DataModel;
using Arendi.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel.Resources;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls.Primitives;

namespace Arendi
{
    public sealed partial class HubPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        private HubComment selected_comment_of_mine = null;
        private HubIdea selected_idea_of_mine = null;

        public HubPage()
        {
            this.InitializeComponent();

            // Initialize contexts and collections
            DataContext = App.viewModel;
            IdeaCollection.Source = App.viewModel.Ideas;
            MyIdeaCollection.Source = App.viewModel.MyIdeas;
            MyCommentCollection.Source = App.viewModel.MyComments;

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            
            // Hide the status bar
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            statusBar.HideAsync();
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        private void HubPage_MyIdeas_Grid_Hold(object sender, HoldingRoutedEventArgs e)
        {
            string my_username = App.RoamingSettings.Values["Username"].ToString()
                + " " + App.RoamingSettings.Values["Surname"].ToString();
            FrameworkElement element = (FrameworkElement)e.OriginalSource;
            if (element.DataContext != null && element.DataContext is HubIdea)
            {
                selected_idea_of_mine = (HubIdea)element.DataContext;
                if (selected_idea_of_mine.iUsername == my_username)
                {
                    FrameworkElement senderElement = sender as FrameworkElement;
                    FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
                    flyoutBase.ShowAt(senderElement);
                }
                else
                {
                    selected_idea_of_mine = null;
                }
            }
        }
        
        private async void HubPage_MyIdeas_FlyOut_Delete(object sender, RoutedEventArgs e)
        {
            if (selected_idea_of_mine != null)
            {
                MessageDialog msgbox =
                    new MessageDialog("Fikri silmek istediğinizden emin misiniz?", "Fikri sil");
                msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
                msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
                var res = await msgbox.ShowAsync();
                if ((int)res.Id == 0)
                {
                    await Controllers.IdeaController.DeleteIdeaById(selected_idea_of_mine.id);
                    // TODO: Delete each comment corresponding idea
                    BindIdeasToCollection();
                    selected_idea_of_mine = null;
                }
            }
        }

        private void HubPage_MyIdeas_FlyOut_Edit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditIdeaPage), selected_idea_of_mine);
        }

        private void HubPage_MyComments_Grid_Hold(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.OriginalSource;
            if (element.DataContext != null && element.DataContext is HubComment)
            {
                selected_comment_of_mine = (HubComment)element.DataContext;
            }

            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            AppBarButton_Refresh_Click(null, null);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            this.navigationHelper.OnNavigatedFrom(e);
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Application.Current.Exit();
        }

        #endregion

        private async void BindIdeasToCollection()
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            HubPage_ProcessRing.IsActive = true;

            string company = App.RoamingSettings.Values["Company"].ToString();
            List<Idea> ideas = await Controllers.IdeaController.GetIdeas();

            App.viewModel.Ideas.Clear();
            App.viewModel.MyIdeas.Clear();

            foreach (var idea in ideas)
            {
                User user = await Controllers.UserController.GetUserById(idea.UserID);

                // If that idea belongs to our company then show it
                if (user != null && user.Type == "w" + company)
                {
                    HubIdea hub_idea = new HubIdea
                    {
                        iUsername = user.Username,
                        iHeader = idea.Content.Split(new string[] { "///" }, StringSplitOptions.None)[0],
                        iContent = idea.Content.Split(new string[] { "///" }, StringSplitOptions.None)[1],
                        id = idea.ID,
                        uid = user.ID
                    };

                    // hub_idea is my idea
                    if (user.ID == (int)App.RoamingSettings.Values["UserID"])
                    {
                        App.viewModel.MyIdeas.Add(hub_idea);
                    }
                    // hub_idea is someone else's idea
                    else
                    {
                        App.viewModel.Ideas.Add(hub_idea);
                    }
                }
            }

            HubPage_ProcessRing.IsActive = false; 
            this.IsEnabled = true;
            appBar.IsEnabled = true;
        }

        private async void BindMyCommentsToCollection()
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            HubPage_ProcessRing.IsActive = true;
            App.viewModel.MyComments.Clear();

            string username = App.RoamingSettings.Values["Username"] 
                + " " + App.RoamingSettings.Values["Surname"];
            List<Comment> comments = await Controllers.CommentController.GetCommentsByUserId
                ((int)App.RoamingSettings.Values["UserID"]);

            foreach (var comment in comments)
            {
                HubComment hub_comment = new HubComment
                {
                    cContent = comment.Content,
                    cDate = comment.Date,
                    cUsername = username,
                    id = comment.ID
                };

                App.viewModel.MyComments.Add(hub_comment);               
            }

            HubPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
            appBar.IsEnabled = true;
        }

        private void AppBarButton_AddIdea_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddIdeaPage));
        }

        private void AppBarButton_Refresh_Click(object sender, RoutedEventArgs e)
        {
            BindIdeasToCollection();
            BindMyCommentsToCollection();
        }

        private void AppBarButton_Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AppBarButton_Help_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AppBarButton_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox =
                new MessageDialog("Uygulamadan çıkmak istediğinizden emin misiniz?", "Çıkış");
            msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
            msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
            var res = await msgbox.ShowAsync();

            if ((int)res.Id == 0)
            {
                Application.Current.Exit();
            }
        }

        // XXX: Grave performance issue
        private void IdeaSelectionChanged_Event(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Pass selected idea as parameter
                Frame.Navigate(typeof(IdeaPage), (HubIdea)e.AddedItems[0]);
            }
            catch
            {

            }
        }

        private async void HubPage_FlyOut_Delete(object sender, RoutedEventArgs e)
        {
            if (selected_comment_of_mine != null)
            {
                MessageDialog msgbox =
                    new MessageDialog("Yorumu silmek istediğinizden emin misiniz?", "Yorumu sil");
                msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
                msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
                var res = await msgbox.ShowAsync();
                if ((int)res.Id == 0)
                {
                    await Controllers.CommentController.DeleteCommentById(selected_comment_of_mine.id);
                    BindMyCommentsToCollection();
                    selected_comment_of_mine = null;
                }
            }
        }

        private void HubPage_FlyOut_Edit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCommentPage), selected_comment_of_mine);
        }
    }
}