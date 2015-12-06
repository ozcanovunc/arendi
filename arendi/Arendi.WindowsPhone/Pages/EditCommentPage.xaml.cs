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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arendi.Pages
{
    public sealed partial class EditCommentPage : Page
    {
        private HubComment selectedComment = null;

        public EditCommentPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            HardwareButtons.BackPressed += this.HardwareButtons_BackPressed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedComment = e.Parameter as HubComment;
            EditCommentPage_Comment_Text.Text = selectedComment.cContent;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private async void AppBarButton_SubmitComment_Click(object sender, RoutedEventArgs e)
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            EditCommentPage_ProcessRing.IsActive = true;

            string content = EditCommentPage_Comment_Text.Text.ToString();
            int idea_id = -1;
            int user_id = (int)App.RoamingSettings.Values["UserID"];

            if (content != string.Empty)
            {
                List<Comment> comments = await Controllers.CommentController.GetCommentsByUserId(user_id);

                foreach (var comment in comments)
                {
                    if (comment.Content == selectedComment.cContent)
                        idea_id = comment.IdeaID;
                }

                if (idea_id != -1)
                {
                    await Controllers.CommentController.DeleteCommentById(selectedComment.id);
                    await Controllers.CommentController.AddComment(
                        content, 
                        DateTime.Now.ToString("dd/MM HH:mm"), 
                        idea_id, 
                        user_id);
                }
                Frame.GoBack();
            }

            EditCommentPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
            appBar.IsEnabled = true;
        }
    }
}
