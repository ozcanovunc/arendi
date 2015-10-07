using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace arendi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                DrawerLayout.CloseDrawer();
            else
                DrawerLayout.OpenDrawer();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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
            Application.Current.Exit();
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (DrawerLayout.IsDrawerOpen)
            {
                DrawerLayout.CloseDrawer();
                e.Handled = true;
            }
        }
    }
}
