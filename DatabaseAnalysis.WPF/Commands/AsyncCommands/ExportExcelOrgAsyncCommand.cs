using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelOrgAsyncCommand : AsyncBaseCommand
    {
        private readonly Navigator _navigator;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ExportExcelOrgAsyncCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if (parameter!.ToString()!.Equals("1"))
            {
                if (_mainWindowViewModel.Navigator.CurrentViewModel is AnnualReportsViewModel annualReportsViewModel)
                {
                    StaticConfiguration.TpmDb = "OPER";
                    try
                    {
                        await ReportsStorage.FillEmptyReports(null, _mainWindowViewModel);
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        #region MessageException
                        MessageBox.Show(
                            msg = $"Не удалось получить список организаций оперативной отчетности, экспорт данных в Excel не выполнен.",
                            "Ошибка при получении данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                        return;
                    }
                }
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Excel | *.xlsx";
                bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
                if (saveExcel)
                {
                    string path = saveFileDialog.FileName;
                    if (!path.EndsWith(".xlsx"))
                        path += ".xlsx";
                    if (File.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch (Exception ex)
                        {
                            string msg;
                            #region MessageException
                            MessageBox.Show(
                                msg = $"Не удалось сохранить файл по указанному пути. Файл с таким именем уже существует в этом расположении и используется другим процессом.",
                                "Ошибка при сохранении файла",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            #endregion
                            ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                            return;
                        }
                    }
                    using (ExcelPackage excelPackege = new(new FileInfo(path)))
                    {
                        excelPackege.Workbook.Properties.Author = "RAO_APP";
                        excelPackege.Workbook.Properties.Title = $"ReportsByForm_{parameter}";
                        excelPackege.Workbook.Properties.Created = DateTime.Now;

                        ExcelWorksheet worksheet = excelPackege.Workbook.Worksheets.Add("Список всех организаций");
                        worksheet.Cells[1, 1].Value = "Рег.№";
                        worksheet.Cells[1, 2].Value = "ОКПО";
                        worksheet.Cells[1, 3].Value = "Сокращенное наименование";
                        worksheet.Cells[1, 4].Value = "Фактический адрес";
                        worksheet.Cells[1, 5].Value = "ИНН";
                        worksheet.Cells[1, 6].Value = "Форма 1.1";
                        worksheet.Cells[1, 7].Value = "Форма 1.2";
                        worksheet.Cells[1, 8].Value = "Форма 1.3";
                        worksheet.Cells[1, 9].Value = "Форма 1.4";
                        worksheet.Cells[1, 10].Value = "Форма 1.5";
                        worksheet.Cells[1, 11].Value = "Форма 1.6";
                        worksheet.Cells[1, 12].Value = "Форма 1.7";
                        worksheet.Cells[1, 13].Value = "Форма 1.8";
                        worksheet.Cells[1, 14].Value = "Форма 1.9";

                        var orgCountRow = 2;
                        foreach (FireBird.Reports org in ReportsStorage.Local_Reports.Reports_Collection10)
                        {
                            worksheet.Cells[orgCountRow, 1].Value = org.Master.RegNoRep.Value;
                            worksheet.Cells[orgCountRow, 2].Value = org.Master.OkpoRep.Value;
                            worksheet.Cells[orgCountRow, 3].Value = org.Master.ShortJurLicoRep.Value;
                            var address = !string.IsNullOrEmpty(org.Master.Rows10[0].JurLicoFactAddress_DB) ? org.Master.Rows10[0].JurLicoFactAddress_DB :
                                          !string.IsNullOrEmpty(org.Master.Rows10[1].JurLicoFactAddress_DB) ? org.Master.Rows10[1].JurLicoFactAddress_DB :
                                          "";
                            worksheet.Cells[orgCountRow, 4].Value = address;
                            var inn = !string.IsNullOrEmpty(org.Master.Rows10[0].Inn_DB) ? org.Master.Rows10[0].Inn_DB :
                                      !string.IsNullOrEmpty(org.Master.Rows10[1].Inn_DB) ? org.Master.Rows10[1].Inn_DB :
                                      "";
                            worksheet.Cells[orgCountRow, 5].Value = inn;
                            worksheet.Cells[orgCountRow, 6].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1")).Count();
                            worksheet.Cells[orgCountRow, 7].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2")).Count();
                            worksheet.Cells[orgCountRow, 8].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3")).Count();
                            worksheet.Cells[orgCountRow, 9].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4")).Count();
                            worksheet.Cells[orgCountRow, 10].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5")).Count();
                            worksheet.Cells[orgCountRow, 11].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6")).Count();
                            worksheet.Cells[orgCountRow, 12].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7")).Count();
                            worksheet.Cells[orgCountRow, 13].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8")).Count();
                            worksheet.Cells[orgCountRow, 14].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9")).Count();
                            orgCountRow++;
                        }
                        try
                        {
                            excelPackege.Save();
                            #region MessageOpenExcel
                            MessageBoxResult result = MessageBox.Show(
                                $"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}. Вы хотите её открыть?",
                                "Выгрузка данных",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information);
                            if (result == MessageBoxResult.Yes)
                                Process.Start("explorer.exe", path);
                            #endregion
                            ServiceExtension.LoggerManager.Info($"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}.");
                        }
                        catch (Exception ex)
                        {
                            string msg;
                            #region MessageException
                            MessageBox.Show(
                                msg = $"Не удалось сохранить файл по указанному пути.",
                                "Ошибка при сохранении файла",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            #endregion
                            ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                            return;
                        }
                    }
                }
            }
            else if (parameter!.ToString()!.Equals("2"))
            {
                if (_mainWindowViewModel.Navigator.CurrentViewModel is OperReportsViewModel operReportsViewModel)
                {
                    StaticConfiguration.TpmDb = "YEAR";
                    try
                    {
                        var GetReportsTask = Task.Factory.StartNew(async () => await ReportsStorage.GetAllReports(null, _mainWindowViewModel));
                        await GetReportsTask;
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        #region MessageException
                        MessageBox.Show(
                            msg = $"Не удалось получить список организаций оперативной отчетности, экспорт данных в Excel не выполнен.",
                            "Ошибка при получении данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                        return;
                    }
                }
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Excel | *.xlsx";
                bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
                if (saveExcel)
                {
                    string path = saveFileDialog.FileName;
                    FileInfo fileInfo = new(path);
                    if (!path.EndsWith(".xlsx"))
                        path += ".xlsx";
                    if (File.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch (Exception ex)
                        {
                            string msg;
                            #region MessageException
                            MessageBox.Show(
                                msg = $"Не удалось сохранить файл по указанному пути. Файл с таким именем уже существует в этом расположении и используется другим процессом.",
                                "Ошибка при сохранении файла",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            #endregion
                            ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                            return;
                        }
                    }
                    using (ExcelPackage excelPackege = new(fileInfo))
                    {
                        excelPackege.Workbook.Properties.Author = "RAO_APP";
                        excelPackege.Workbook.Properties.Title = $"ReportsByForm_{parameter}";
                        excelPackege.Workbook.Properties.Created = DateTime.Now;

                        ExcelWorksheet worksheet = excelPackege.Workbook.Worksheets.Add("Список всех организаций");
                        worksheet.Cells[1, 1].Value = "Рег.№";
                        worksheet.Cells[1, 2].Value = "ОКПО";
                        worksheet.Cells[1, 3].Value = "Сокращенное наименование";
                        worksheet.Cells[1, 4].Value = "Фактический адрес";
                        worksheet.Cells[1, 5].Value = "ИНН";
                        worksheet.Cells[1, 6].Value = "Форма 2.1";
                        worksheet.Cells[1, 7].Value = "Форма 2.2";
                        worksheet.Cells[1, 8].Value = "Форма 2.3";
                        worksheet.Cells[1, 9].Value = "Форма 2.4";
                        worksheet.Cells[1, 10].Value = "Форма 2.5";
                        worksheet.Cells[1, 11].Value = "Форма 2.6";
                        worksheet.Cells[1, 12].Value = "Форма 2.7";
                        worksheet.Cells[1, 13].Value = "Форма 2.8";
                        worksheet.Cells[1, 14].Value = "Форма 2.9";
                        worksheet.Cells[1, 15].Value = "Форма 2.10";
                        worksheet.Cells[1, 16].Value = "Форма 2.11";
                        worksheet.Cells[1, 17].Value = "Форма 2.12";

                        var orgCountRow = 2;
                        foreach (FireBird.Reports org in ReportsStorage.Local_Reports.Reports_Collection20)
                        {
                            worksheet.Cells[orgCountRow, 1].Value = org.Master.RegNoRep.Value;
                            worksheet.Cells[orgCountRow, 2].Value = org.Master.OkpoRep.Value;
                            worksheet.Cells[orgCountRow, 3].Value = org.Master.ShortJurLicoRep.Value;
                            var address = !string.IsNullOrEmpty(org.Master.Rows20[0].JurLicoFactAddress_DB) ? org.Master.Rows20[0].JurLicoFactAddress_DB :
                                          !string.IsNullOrEmpty(org.Master.Rows20[1].JurLicoFactAddress_DB) ? org.Master.Rows20[1].JurLicoFactAddress_DB :
                                          "";
                            worksheet.Cells[orgCountRow, 4].Value = address;
                            var inn = !string.IsNullOrEmpty(org.Master.Rows20[0].Inn_DB) ? org.Master.Rows20[0].Inn_DB :
                                      !string.IsNullOrEmpty(org.Master.Rows20[1].Inn_DB) ? org.Master.Rows20[1].Inn_DB :
                                      "";
                            worksheet.Cells[orgCountRow, 5].Value = inn;
                            worksheet.Cells[orgCountRow, 6].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1")).Count();
                            worksheet.Cells[orgCountRow, 7].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2")).Count();
                            worksheet.Cells[orgCountRow, 8].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3")).Count();
                            worksheet.Cells[orgCountRow, 9].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4")).Count();
                            worksheet.Cells[orgCountRow, 10].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5")).Count();
                            worksheet.Cells[orgCountRow, 11].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6")).Count();
                            worksheet.Cells[orgCountRow, 12].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7")).Count();
                            worksheet.Cells[orgCountRow, 13].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8")).Count();
                            worksheet.Cells[orgCountRow, 14].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9")).Count();
                            worksheet.Cells[orgCountRow, 15].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10")).Count();
                            worksheet.Cells[orgCountRow, 16].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11")).Count();
                            worksheet.Cells[orgCountRow, 17].Value = org.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12")).Count();
                            orgCountRow++;
                        }
                        try
                        {
                            excelPackege.Save();
                            string msg;
                            #region MessageOpenExcel
                            MessageBoxResult result = MessageBox.Show(
                                $"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}. Вы хотите её открыть?",
                                "Выгрузка данных",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information);
                            if (result == MessageBoxResult.Yes)
                                Process.Start("explorer.exe", path);
                            #endregion
                            ServiceExtension.LoggerManager.Info($"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}.");
                        }
                        catch (Exception ex)
                        {
                            string msg;
                            #region MessageException
                            MessageBox.Show(
                                msg = $"Не удалось сохранить файл по указанному пути.",
                                "Ошибка при сохранении файла",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            #endregion
                            ServiceExtension.LoggerManager.Warning($"{msg}\n{ex}");
                            return;
                        }
                    }
                }
            }
        }
    }
}