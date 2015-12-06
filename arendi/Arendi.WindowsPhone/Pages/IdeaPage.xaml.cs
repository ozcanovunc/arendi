using Arendi.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arendi.Pages
{
    public sealed partial class IdeaPage : Page
    {
        private HubComment selected_comment_of_mine = null;
        private HubIdea selectedIdea = null;

        public IdeaPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            HardwareButtons.BackPressed += this.HardwareButtons_BackPressed;

            DataContext = App.viewModel;
            CommentCollection.Source = App.viewModel.Comments;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                App.viewModel.Comments.Clear();
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedIdea = e.Parameter as HubIdea;
            IdeaPage_IdeaHeader_TextBlock.Text = selectedIdea.iHeader;
            IdeaPage_IdeaContent_TextBlock.Text = selectedIdea.iContent;
            BindCommentsToCollection();
        }

        private async void AppBarButton_SubmitComment_Click(object sender, RoutedEventArgs e)
        {
            string comment_content = IdeaPage_Comment_TextBox.Text.ToString();

            IdeaPage_Comment_TextBox.Text = "";

            if (comment_content != string.Empty)
            {
                await Controllers.CommentController.AddComment
                    (comment_content, 
                    DateTime.Now.ToString("dd/MM HH:mm"), 
                    selectedIdea.id, 
                    (int)App.RoamingSettings.Values["UserID"]);
            }

            BindCommentsToCollection();
        }

        private void IdeaPage_MyComments_Grid_Hold(object sender, HoldingRoutedEventArgs e)
        {
            string my_username = App.RoamingSettings.Values["Username"].ToString()
                + " " + App.RoamingSettings.Values["Surname"].ToString();
            FrameworkElement element = (FrameworkElement)e.OriginalSource;
            if (element.DataContext != null && element.DataContext is HubComment)
            {
                selected_comment_of_mine = (HubComment)element.DataContext;
                if (selected_comment_of_mine.cUsername == my_username)
                {
                    FrameworkElement senderElement = sender as FrameworkElement;
                    FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
                    flyoutBase.ShowAt(senderElement);
                }
                else
                {
                    selected_comment_of_mine = null;
                }
            }
        }

        private async void IdeaPage_FlyOut_Delete(object sender, RoutedEventArgs e)
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
                    BindCommentsToCollection();
                    selected_comment_of_mine = null;
                }
            }
        }

        private void IdeaPage_FlyOut_Edit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCommentPage), selected_comment_of_mine);
        }

        private async void BindCommentsToCollection()
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            IdeaPage_ProcessRing.IsActive = true;

            App.viewModel.Comments.Clear();
            List<Comment> comments = await Controllers.CommentController.GetCommentsByIdeaId(selectedIdea.id);

            foreach(var comment in comments)
            {
                User user = await Controllers.UserController.GetUserById(comment.UserID);

                if (user != null)
                {
                    HubComment hub_comment = new HubComment
                    {
                        cContent = comment.Content,
                        cDate = comment.Date,
                        cUsername = user.Username,
                        id = comment.ID
                    };

                    App.viewModel.Comments.Add(hub_comment);
                }
            }

            IdeaPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
            appBar.IsEnabled = true;
        }

        private void AppBarButton_Refresh_Click(object sender, RoutedEventArgs e)
        {
            App.viewModel.Comments.Clear();
            BindCommentsToCollection();
        }
    }
}
