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
			var graphEntity = _requestService.GetGraphEntity();
			Id.Text = graphEntity.Id;
			ODataContext.Text = graphEntity.ODataContext;
		}
	}
}
