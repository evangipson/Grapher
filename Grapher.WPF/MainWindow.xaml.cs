using System.Windows;
using Grapher.Services.Interfaces;

namespace Grapher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IRequestService _requestService;

		public MainWindow(IRequestService requestService)
		{
			InitializeComponent();
			_requestService = requestService;

			RenderEntity();
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
	}
}
