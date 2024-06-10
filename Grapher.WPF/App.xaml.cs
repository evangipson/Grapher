using System.Reflection;
using System.Text;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Grapher.Base.DependencyInjection;
using Grapher.Services;
using Grapher.WPF.Views;

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
			// configure ILogger service
			serviceCollection.AddLogging();

			// configure user secrets service
			var userSecretsConfig = new ConfigurationBuilder().AddUserSecrets<App>().Build();
			serviceCollection.AddScoped<IConfiguration>(_ => userSecretsConfig);

			// add the IHttpClientFactory service
			serviceCollection.AddHttpClient();

			// add Grapher services from Grapher.Base.Services and Grapher.Services
			serviceCollection.AddServicesFromAssembly(Assembly.GetAssembly(typeof(RequestService)));

			// lastly, add the MainWindow transient application service
			serviceCollection.AddTransient(typeof(MainWindow));
		}
	}
}
