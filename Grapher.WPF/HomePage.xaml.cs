using System.Windows;
using System.Windows.Controls;

namespace Grapher.WPF
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

		private void UserButton_Click(object sender, RoutedEventArgs e)
		{
            var userAuthUri = new Uri("UserAuth.xaml", UriKind.Relative);
			MainWindowNavigation.ChangePage(userAuthUri);
		}
	}
}
