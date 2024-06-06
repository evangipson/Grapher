using System.Reflection;
using System.Text;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

using Grapher.Base.DependencyInjection;
using Grapher.Services;

namespace Grapher
{
	/// <summary>
	/// The entry point for the Grapher application.
	/// </summary>
	public partial class App : Application
	{
		public IServiceProvider? ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs eventArgs)
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			ServiceProvider = serviceCollection.BuildServiceProvider();
			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}

		private void ConfigureServices(IServiceCollection serviceCollection)
		{
			serviceCollection.AddHttpClient();
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(RequestService)));
			serviceCollection.AddTransient(typeof(MainWindow));
		}
	}
}
