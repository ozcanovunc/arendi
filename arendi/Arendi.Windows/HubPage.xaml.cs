using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Arendi.Data;
using Arendi.Common;
using Arendi.DataModel;
using Windows.UI.Popups;
using System.Diagnostics;

namespace Arendi
{
    public sealed partial class HubPage : Page
    {
        private HubIdea selected_idea_of_mine = null;
        private User selected_user = null;
       
        public HubPage()
        {
            this.InitializeComponent();
            InitNameAndSurname();
            DataContext = App.viewModel;
            IdeaCollection.Source = App.viewModel.Ideas;
            UserCollection.Source = App.viewModel.Users;
            BindIdeasToCollection();
            BindUsersToCollection();
        }

        private void InitNameAndSurname()
        {
            string name = App.RoamingSettings.Values["Username"].ToString();
            string surname = App.RoamingSettings.Values["Surname"].ToString();
            Text_Name.Text = name;
            Text_Surname.Text = surname;
        }

        private async void HubPage_MyIdeas_FlyOut_Delete(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox =
                new MessageDialog("Fikri silmek istediğinizden emin misiniz?", "Fikri sil");
            msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
            msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
            var res = await msgbox.ShowAsync();
            if ((int)res.Id == 0)
            {
                // Delete idea
                await Controllers.IdeaController.DeleteIdeaById(selected_idea_of_mine.id);

                // Delete each comment corresponding idea
                List<Comment> comments =
                    await Controllers.CommentController.GetCommentsByIdeaId(selected_idea_of_mine.id);
                foreach (var comment in comments)
                {
                    await Controllers.CommentController.DeleteCommentById(comment.ID);
                }

                BindIdeasToCollection();
                selected_idea_of_mine = null;
            }
        }

        private async void DeleteWorker_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selected_user != null)
            {
                MessageDialog msgbox =
                    new MessageDialog("Çalışanı silmek istediğinizden emin misiniz?", "Çalışanı sil");
                msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
                msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
                var res = await msgbox.ShowAsync();
                if ((int)res.Id == 0)
                {
                    // Delete idea
                    await Controllers.UserController.DeleteUserByEmail(selected_user.Email);

                    // Delete each idea corresponding user
                    List<Idea> ideas = await Controllers.IdeaController.GetIdeas();

                    foreach (var idea in ideas)
                    {
                        // Delete the idea and all it's comments
                        if (idea.UserID == selected_user.ID)
                        {
                            await Controllers.IdeaController.DeleteIdeaById(idea.ID);
                            List<Comment> comments =
                                await Controllers.CommentController.GetCommentsByIdeaId(idea.ID);
                            foreach (var comment in comments)
                            {
                                await Controllers.CommentController.DeleteCommentById(comment.ID);
                            }
                        }
                    }

                    BindIdeasToCollection();
                    BindUsersToCollection();
                    selected_idea_of_mine = null;
                }
            }
        }

        private void DeleteIdea_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selected_idea_of_mine != null)
                HubPage_MyIdeas_FlyOut_Delete(sender, e);
        }

        private async void BindUsersToCollection()
        {
            this.IsEnabled = false;
            HubPage_ProcessRing.IsActive = true;

            string company = App.RoamingSettings.Values["Company"].ToString();
            List<User> user_list = await Controllers.UserController.GetAllUsers();

            App.viewModel.Users.Clear();

            foreach (var user in user_list)
            {
                if (user.Type == "w" + company)
                    App.viewModel.Users.Add(user);
            }

            HubPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
        }

        private async void BindIdeasToCollection()
        {
            this.IsEnabled = false;
            HubPage_ProcessRing.IsActive = true;

            string company = App.RoamingSettings.Values["Company"].ToString();
            List<Idea> ideas = await Controllers.IdeaController.GetIdeas();

            App.viewModel.Ideas.Clear();

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
                        iDate = idea.Date,
                        id = idea.ID,
                        uid = user.ID
                    };
                                       
                    App.viewModel.Ideas.Add(hub_idea);                   
                }
            }

            HubPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            BindIdeasToCollection();
            BindUsersToCollection();
        }

        private void IdeaSelectionChanged_Event(object sender, SelectionChangedEventArgs e)
        {
            ListView list = (ListView)GetChildren(HubSection1).ElementAt(12);
            selected_idea_of_mine = list.SelectedItem as HubIdea;
        }

        private void UserSelectionChanged_Event(object sender, SelectionChangedEventArgs e)
        {
            ListView list = (ListView)GetChildren(HubSection2).ElementAt(12);
            selected_user = list.SelectedItem as User;
        }

        private List<FrameworkElement> GetChildren(DependencyObject parent)
        {
            List<FrameworkElement> controls = new List<FrameworkElement>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement)
                {
                    controls.Add(child as FrameworkElement);
                }
                controls.AddRange(GetChildren(child));
            }

            return controls;
        }

        private async void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox =
                new MessageDialog("Oturumu kapatmak istediğinize emin misiniz?", "Oturumu kapat");
            msgbox.Commands.Add(new UICommand { Label = "Evet", Id = 0 });
            msgbox.Commands.Add(new UICommand { Label = "Hayır", Id = 1 });
            var res = await msgbox.ShowAsync();

            if ((int)res.Id == 0)
            {
                // Log out
                App.RoamingSettings.Values["Loggedin"] = false;
                App.Current.Exit();
            }
        }
    }
}
