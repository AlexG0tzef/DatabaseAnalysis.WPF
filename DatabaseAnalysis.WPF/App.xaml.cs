using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.Resourses;
using System.IO;
using System.Linq;
using System.Windows;

namespace DatabaseAnalysis.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StaticConfiguration.DBOperPath = GetLocalCopy(@"W:\Оперативная отчётность\1-13", DB_Type.OperDB);
            StaticConfiguration.DBPath = GetLocalCopy(@"W:\Годовая отчётность\1-13\БД", DB_Type.AnnualDB);
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
        }

        private string GetLocalCopy(string originDBPath, DB_Type dbType)
        {
            string localDBPath = "";
            string localDBFullPath = "";
            if (dbType == DB_Type.OperDB)
            {
                localDBPath = @"C:\RAO\t\OPER";
                localDBFullPath = localDBPath + @"\LOCAL_0.RAODB";
            }
            if (dbType == DB_Type.AnnualDB)
            {
                localDBPath = @"C:\RAO\t\YEAR";
                localDBFullPath = localDBPath + @"\YEAR.RAODB";
            }
            Directory.CreateDirectory(localDBPath!);
            DirectoryInfo originDirectoryInfo = new(originDBPath);
            var LastDBFile = originDirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(x => x.Name.EndsWith(".RAODB"))
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();
            if (LastDBFile is null)
                File.Create(localDBFullPath);
            else
            {
                if (File.Exists(localDBFullPath))
                    File.Delete(localDBFullPath);
                File.Copy(LastDBFile.FullName, localDBFullPath);
            }
            return localDBFullPath;
        }

        //private string GetLastDBPath(string path, DB_Type dbType)
        //{
        //    string DBDirPath = path;
        //    string msg = dbType switch
        //    {
        //        DB_Type.OperDB => "Файл оперативвной отчетности отсутствует в директории",
        //        DB_Type.AnnualDB => "Файл годовой отчетности отсутствует в директории",
        //        _ => "Errore!"
        //    };
        //    DirectoryInfo directoryInfo = new(DBDirPath);
        //    var LastDBFile = directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
        //        .Where(x => x.Name.EndsWith(".RAODB"))
        //        .OrderByDescending(x => x.LastWriteTime)
        //        .FirstOrDefault();
        //    if (LastDBFile is null)
        //    {
        //        string messageBoxText = $"{msg} {DBDirPath}";
        //        string caption = "Ошибка доступа к базе данных";
        //        MessageBoxButton button = MessageBoxButton.OK;
        //        MessageBoxImage icon = MessageBoxImage.Error;
        //        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
        //        if (result == MessageBoxResult.OK)
        //            Environment.Exit(0);
        //    }
        //    return LastDBFile!.FullName;
        //}
    }
}