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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += ((App)Application.Current).HardwareButtons_BackPressed;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= ((App)Application.Current).HardwareButtons_BackPressed;
        }

        private void MenuIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Navigate to menu
            Frame.Navigate(typeof(Menu));
        }
    }
}
