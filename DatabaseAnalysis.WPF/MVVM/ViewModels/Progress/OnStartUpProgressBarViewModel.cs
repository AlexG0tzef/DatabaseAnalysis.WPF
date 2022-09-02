using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using static DatabaseAnalysis.WPF.Resourses.StaticResourses;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.Progress
{
    public class OnStartUpProgressBarViewModel : BaseProgressBarViewModel
    {
        public OnStartUpProgressBarViewModel(OnStartUpProgressBar startProgressBar, IBackgroundLoader backgroundWorker)
        {
            backgroundWorker.BackgroundWorker(() =>
            {
                ServiceExtension.LoggerManager.CreateFile("ApplicationLogs.log");
#if DEBUG
                StaticConfiguration.DBOperPath = GetLocalCopy(@"C:\RAO\t\OPER", DB_Type.OperDB);
                StaticConfiguration.DBYearPath = GetLocalCopy(@"C:\RAO\t\YEAR", DB_Type.AnnualDB);
#else
                StaticConfiguration.DBOperPath = GetLocalCopy(@"W:\Оперативная отчётность\1-13\Архив базы", DB_Type.OperDB);
                StaticConfiguration.DBYearPath = GetLocalCopy(@"W:\Годовая отчётность\1-13\БД", DB_Type.AnnualDB);
#endif
            }, () =>
            {
                Application.Current.MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
                Application.Current.MainWindow.Show();
                startProgressBar.Close();
            });
        }

        #region GetLocalCopy
        private static string GetLocalCopy(string originDBPath, DB_Type dbType)
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
            catch (Exception ex)
            {
                ServiceExtension.LoggerManager.Error($"{msg} {originDBPath}\n{ex}");
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
        #endregion
    }
}