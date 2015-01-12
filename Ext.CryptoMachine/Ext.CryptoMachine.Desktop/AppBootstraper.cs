using System.Windows;
using Caliburn.Micro;
using Ext.CryptoMachine.Desktop.ViewModels;

namespace Ext.CryptoMachine.Desktop
{
    public class AppBootstraper : BootstrapperBase
    {
        public AppBootstraper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();

            base.OnStartup(sender, e);
        }
    }
}
