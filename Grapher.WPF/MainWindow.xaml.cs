using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using Grapher.Services.Interfaces;

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
		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

		private readonly IRequestService _requestService;

		public MainWindow(IRequestService requestService)
		{
			InitializeComponent();
			_requestService = requestService;

			RenderEntity();
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

		private void RenderEntity()
		{
			var entity = _requestService.GetGraphEntity();
			AppId.Text = entity.Id;
			ODataContext.Text = entity.ODataContext;
		}

		private void RenderApplication()
		{
			var application = _requestService.GetGraphApplication();
			DisplayName.Text = application.DisplayName;
			AppId.Text = application.AppId;
			ODataContext.Text = application.ODataContext;
			CreatedDateTime.Text = application.CreatedDateTime;
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
