using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace arendi
{
    public sealed partial class Menu : Page
    {
        public Menu()
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

        private void Menu1_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Menu2_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Menu3_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Menu4_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Menu5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page5_Help));
        }

        private void Menu6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Logout mechanism
        }
    }
}
