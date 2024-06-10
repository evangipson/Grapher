using System.Windows;
using System.Windows.Controls;

namespace Grapher.WPF.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage() => InitializeComponent();

		private void UserButton_Click(object sender, RoutedEventArgs e) =>
			MainWindowNavigationExtension.ChangePage(new Uri("/Views/UserAuth.xaml", UriKind.Relative));
	}
}
