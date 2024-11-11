using DataAccessLibrary;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFDBwithDI.ViewModel;

namespace WPFDBwithDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureService(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            var MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        private void ConfigureService(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ViewModelMainWindow>();
            services.AddSingleton<DbConfiguration>();
            
        }
    }
}
