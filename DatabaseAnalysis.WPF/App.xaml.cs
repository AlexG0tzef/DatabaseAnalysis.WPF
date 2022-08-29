using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using static DatabaseAnalysis.WPF.Resourses.StaticResourses;

namespace DatabaseAnalysis.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceExtension.LoggerManager.CreateFile("ApplicationLogs.log");
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
            }

#if DEBUG
            StaticConfiguration.DBOperPath = GetLocalCopy(@"C:\RAO\t\OPER", DB_Type.OperDB);
            StaticConfiguration.DBYearPath = GetLocalCopy(@"C:\RAO\t\YEAR", DB_Type.AnnualDB);
#else
            
            StaticConfiguration.DBOperPath = GetLocalCopy(@"W:\Оперативная отчётность\1-13", DB_Type.OperDB);
            StaticConfiguration.DBYearPath = GetLocalCopy(@"W:\Годовая отчётность\1-13\БД", DB_Type.AnnualDB);
#endif

            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
        }

        private string GetLocalCopy(string originDBPath, DB_Type dbType)
        {
            string localDBPath = "";
            string localDBFullPath = "";
            string msg = "";
            if (dbType == DB_Type.OperDB)
            {
                localDBPath = @"C:\RAO\t\OPER";
                localDBFullPath = localDBPath + @"\OPER.RAODB";
                msg = "Файл оперативной отчетности отсутствует в директории";
            }
            if (dbType == DB_Type.AnnualDB)
            {
                localDBPath = @"C:\RAO\t\YEAR";
                localDBFullPath = localDBPath + @"\YEAR.RAODB";
                msg = "Файл годовой отчетности отсутствует в директории";
            }
            if (localDBPath.Equals(originDBPath) && File.Exists(localDBFullPath))
                return localDBFullPath;
            Directory.CreateDirectory(localDBPath!);
            DirectoryInfo originDirectoryInfo = new(originDBPath);
            try
            {
                var LastDBFile = originDirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                    .Where(x => x.Name.EndsWith(".RAODB"))
                    .OrderByDescending(x => x.LastWriteTime)
                    .FirstOrDefault();
                if (File.Exists(localDBFullPath))
                    File.Delete(localDBFullPath);
                File.Copy(LastDBFile!.FullName, localDBFullPath);
            }
            catch (Exception)
            {
                #region MessageDBMissing
                MessageBoxResult result = MessageBox.Show(
                    $"{msg} {originDBPath}",
                    "Ошибка доступа к базе данных",
                    MessageBoxButton.OK,
                     MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    Environment.Exit(0); 
                #endregion
            }
            return localDBFullPath;
        }

        static Mutex? InstanceCheckMutex;
        static bool InstanceCheck()
        {
            bool isNew;
            var mutex = new Mutex(true, "<DatabaseAnalysis.WPF>", out isNew);
            if (isNew)
                InstanceCheckMutex = mutex;
            else
                mutex.Dispose();
            return isNew;
        }
    }
}