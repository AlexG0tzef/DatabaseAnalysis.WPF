using System.Windows;
using System.Xaml;
using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;

namespace DatabaseAnalysis.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StaticConfiguration.DBOperPath = "C:\\RAO\\t\\OPER.RAODB";
            StaticConfiguration.DBPath = "C:\\RAO\\t\\YEAR.RAODB";
            MainWindow = new MainWindow() 
            {
                DataContext = new MainWindowViewModel() 
            };
            MainWindow.Show();
        }
    }
}
