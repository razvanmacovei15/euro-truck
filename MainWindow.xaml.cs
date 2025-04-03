using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using EuroTruck.AuthFeature.Views;

namespace EuroTruck
{
    public sealed partial class MainWindow : Window
    {
        public static Frame AppFrame { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            AppFrame = MainFrame;
            AppFrame.Navigate(typeof(LoginPage));
        }
    }
}
