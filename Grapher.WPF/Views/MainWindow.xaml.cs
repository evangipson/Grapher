using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using Grapher.Services.Interfaces;

namespace Grapher.WPF.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IWindowBlurService _windowBlurService;

		/// <summary>
		/// A dependency-injected constructor for the main window
		/// of the application. Any dependencies that are injected
		/// here can be accessed by pages within the app, as long
		/// as there is a public property exposing it.
		/// </summary>
		/// <param name="_requestService"></param>
		public MainWindow(IRequestService _requestService, IWindowBlurService windowBlurService)
		{
			_windowBlurService = windowBlurService;
			RequestService = _requestService;
			InitializeComponent();
			GrapherNavigationFrame.Navigate(new HomePage());
		}

		/// <summary>
		/// A pointer to <c>this</c> window's
		/// <see cref="WindowInteropHelper"/>, which
		/// is used when enabling the blurred background
		/// when the application loads.
		/// </summary>
		private IntPtr WindowInteropHelperPointer
		{
			get => new WindowInteropHelper(this).Handle;
		}

		/// <summary>
		/// A service to make requests which will be
		/// accessed from child pages.
		/// </summary>
		public IRequestService RequestService { get; set; }

		private void Window_Loaded(object sender, RoutedEventArgs e) => _windowBlurService.EnableBlur(WindowInteropHelperPointer);

		private void Window_Close(object sender, RoutedEventArgs e) => Close();

		private void Window_Minimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

		private void Window_Maximize(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				return;
			}
			WindowState = WindowState.Maximized;
		}

		private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				DragMove();
			}
		}
	}
}
