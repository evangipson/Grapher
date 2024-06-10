using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Grapher.Services.Interfaces;
using Grapher.WPF;

namespace Grapher
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public IntPtr Data;
		public int SizeOfData;
	}

	internal enum WindowCompositionAttribute
	{
		WCA_ACCENT_POLICY = 19
	}

	internal enum AccentState
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_INVALID_STATE = 4
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public IRequestService RequestService { get; set; }

		public static MainWindow? AppContext { get; set; }

		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

		public MainWindow(IRequestService _requestService)
		{
			// allow IRequestService access from child pages
			// by passing context as a static reference.
			AppContext = this;
			RequestService = _requestService;

			InitializeComponent();
			// show the initial page of the app
			GrapherNavigationFrame.Navigate(new HomePage());
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) => EnableBlur();

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

		internal void EnableBlur()
		{
			var windowHelper = new WindowInteropHelper(this);

			var accent = new AccentPolicy();
			var accentStructSize = Marshal.SizeOf(accent);
			accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var data = new WindowCompositionAttributeData();
			data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
			data.SizeOfData = accentStructSize;
			data.Data = accentPtr;

			SetWindowCompositionAttribute(windowHelper.Handle, ref data);

			Marshal.FreeHGlobal(accentPtr);
		}
	}
}
