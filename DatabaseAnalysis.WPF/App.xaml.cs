using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            if (!InstanceCheck())
            {
                #region MessageAlreadyLaunched
                string messageBoxText = $"Программа уже была запущена";
                string caption = "Ошибка запуска";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                if (result == MessageBoxResult.OK)
                    Environment.Exit(0);
                #endregion
                ServiceExtension.LoggerManager.Error(messageBoxText);
            }
            MainWindow = new OnStartUpProgressBar();

            //MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            //MainWindow.Show();
        }

        private static Mutex? InstanceCheckMutex;
        static bool InstanceCheck()
        {
            bool isNew;
            InstanceCheckMutex = new Mutex(true, "<DatabaseAnalysis.WPF>", out isNew);
            if (!isNew)
                InstanceCheckMutex.Dispose();
            return isNew;
        }
    }
}