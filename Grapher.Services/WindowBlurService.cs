using System.Runtime.InteropServices;

using Grapher.Base.DependencyInjection;
using Grapher.Services.Interfaces;

namespace Grapher.Services
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

	[Service(typeof(IWindowBlurService))]
	public class WindowBlurService : IWindowBlurService
	{
		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr mainWindowInterloperPointer, ref WindowCompositionAttributeData windowComposition);

		public WindowBlurService() {}

		public void EnableBlur(IntPtr mainWindowInterloperPointer)
		{
			var accent = new AccentPolicy();
			var accentStructSize = Marshal.SizeOf(accent);
			accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var windowComposition = new WindowCompositionAttributeData();
			windowComposition.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
			windowComposition.SizeOfData = accentStructSize;
			windowComposition.Data = accentPtr;

			SetWindowCompositionAttribute(mainWindowInterloperPointer, ref windowComposition);

			Marshal.FreeHGlobal(accentPtr);
		}
	}
}
