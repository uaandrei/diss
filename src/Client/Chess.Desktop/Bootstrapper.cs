using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System.Windows;

namespace Chess.Desktop
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var loginWindow = new LoginWindow();
            loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var dialogResult = loginWindow.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                App.Current.MainWindow = (Window)this.Shell;
                App.Current.MainWindow.Show();
            }
            else
            {
                Application.Current.Shutdown(0);
            }
        }
    }
}
