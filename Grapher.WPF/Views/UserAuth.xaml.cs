using System.Windows;
using System.Windows.Controls;

using Grapher.Services.Interfaces;

namespace Grapher.WPF.Views
{
    /// <summary>
    /// Interaction logic for UserAuth.xaml
    /// </summary>
    public partial class UserAuth : Page
    {
		private readonly IRequestService? _requestService;

		public UserAuth()
		{
			_requestService = MainWindow?.RequestService;
			InitializeComponent();
			RenderEntity();
		}

		private MainWindow? MainWindow
		{
			get => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
		}

		private void RenderEntity()
		{
			var entity = _requestService?.GetGraphEntity();
			AppId.Text = entity?.Id;
			ODataContext.Text = entity?.ODataContext;
		}

		private void RenderApplication()
		{
			var application = _requestService?.GetGraphApplication();
			DisplayName.Text = application?.DisplayName;
			AppId.Text = application?.AppId;
			ODataContext.Text = application?.ODataContext;
			CreatedDateTime.Text = application?.CreatedDateTime;
		}
	}
}
