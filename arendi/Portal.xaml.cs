using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

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
        private void MenuIcon_Tapped(object sender, TappedRoutedEventArgs e)
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

        private async void Menu1_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Menu2_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Menu3_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Menu4_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private async void Menu5_Tapped(object sender, TappedRoutedEventArgs e)
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
