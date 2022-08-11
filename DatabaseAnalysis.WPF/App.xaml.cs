using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xaml;
using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.Resourses;

namespace DatabaseAnalysis.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StaticConfiguration.DBOperPath = GetLastDBPath(@"C:\RAO\t", DB_Type.OperDB);
            StaticConfiguration.DBPath = GetLastDBPath(@"C:\RAO\t\Annual", DB_Type.AnnualDB);
            MainWindow = new MainWindow() 
            {
                DataContext = new MainWindowViewModel() 
            };
            MainWindow.Show();
        }

        private string GetLastDBPath(string path, DB_Type dbType)
        {
            string DBDirPath = path;
            string msg = dbType switch
            {
                DB_Type.OperDB => "Файл оперативной отчетности отсутствует в директории",
                DB_Type.AnnualDB => "Файл годовой отчетности отсутствует в директории"
            };
            DirectoryInfo directoryInfo = new(DBDirPath);
            var LastDBFile = directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(x => x.Name.EndsWith(".RAODB"))
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();
            if (LastDBFile is null)
            {
                string messageBoxText = $"{msg} {DBDirPath}";
                string caption = "Ошибка доступа к базе данных";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                if (result == MessageBoxResult.OK)
                    Environment.Exit(0);
            }
            return LastDBFile!.FullName;
        }
    }
}