using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace arendi
{
    public sealed partial class Page5_Help : Page
    {
        public Page5_Help()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += OnBackKeyPress;
        }

        void OnBackKeyPress(object sender, BackPressedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
