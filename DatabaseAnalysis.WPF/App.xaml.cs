﻿using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.Resourses;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace DatabaseAnalysis.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            StaticConfiguration.DBOperPath = GetLocalCopy(@"W:\Оперативная отчётность\1-13", DB_Type.OperDB);
            StaticConfiguration.DBYearPath = GetLocalCopy(@"W:\Годовая отчётность\1-13\БД", DB_Type.AnnualDB);

#else
            StaticConfiguration.DBOperPath = GetLastDBPath(@"C:\RAO\t\OPER", DB_Type.OperDB);
            StaticConfiguration.DBYearPath = GetLastDBPath(@"C:\RAO\t\YEAR", DB_Type.AnnualDB);
#endif

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
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
            Directory.CreateDirectory(localDBPath!);
            DirectoryInfo originDirectoryInfo = new(originDBPath);
            var LastDBFile = originDirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where(x => x.Name.EndsWith(".RAODB"))
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();
            if (LastDBFile is not null)
            {
                if (File.Exists(localDBFullPath))
                    File.Delete(localDBFullPath);
                File.Copy(LastDBFile.FullName, localDBFullPath);
            }
            else
            {
                string messageBoxText = $"{msg} {originDBPath}";
                string caption = "Ошибка доступа к базе данных";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                if (result == MessageBoxResult.OK)
                    Environment.Exit(0);
            }
            return localDBFullPath;
        }
    }
}