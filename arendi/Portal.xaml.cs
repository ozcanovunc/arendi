using System;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace arendi
{
    public sealed partial class Portal : Page
    {
        public Portal()
        {
            this.InitializeComponent();

            // Initialization of DrawerLayout menu
            DrawerLayout.InitializeDrawerLayout();
        }

        // DrawerLayout open or close
        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
            }
            else
            {
                DrawerLayout.OpenDrawer();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += OnBackKeyPress;
        }

        private async void Item1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("TEST");
            if (dialog != null) await dialog.ShowAsync();
        }

        private async void Item2_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Item3_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Item4_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Item5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Logout mechanism
        }

        void OnBackKeyPress(object sender, BackPressedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
                e.Handled = true;
            }
            else
            {
                // TODO: Add MessageDialog
                Application.Current.Exit();
            }
        }
    }
}
