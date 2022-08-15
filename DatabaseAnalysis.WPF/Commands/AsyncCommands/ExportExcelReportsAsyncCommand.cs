using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelReportsAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;

        public ExportExcelReportsAsyncCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        public override async Task AsyncExecute(object? parameter)
        {
            //await ReportsStorge.GetDataReports(parameter);
            SaveFileDialog dial = new();
            dial.Filter = "Excel | *.xlsx";
            switch (parameter.ToString())
            {
                case "1":
                    var saveExcel1 = dial.ShowDialog(Application.Current.MainWindow);
                    if ((bool)saveExcel1!)
                    {
                        var path = dial.FileName;
                        FileInfo fileInfo = new(path);
                        if (!path.Contains(".xlsx"))
                        {
                            path += ".xlsx";
                        }
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                        {
                            excelPackage.Workbook.Properties.Author = "RAO_APP";
                            excelPackage.Workbook.Properties.Title = "Report";
                            excelPackage.Workbook.Properties.Created = DateTime.Now;
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Список всех форм 1");
                            worksheet.Cells[1, 1].Value = "Рег.№";
                            worksheet.Cells[1, 2].Value = "ОКПО";
                            worksheet.Cells[1, 3].Value = "Форма";
                            worksheet.Cells[1, 4].Value = "Дата начала";
                            worksheet.Cells[1, 5].Value = "Дата конца";
                            worksheet.Cells[1, 6].Value = "Номер кор.";
                            worksheet.Cells[1, 7].Value = "Колличество строк";

                            var row = 2;
                            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
                            {

                                foreach (FireBird.Report rep in reps.Report_Collection)
                                {
                                    worksheet.Cells[row, 1].Value = reps.Master.RegNoRep.Value;
                                    worksheet.Cells[row, 2].Value = reps.Master.OkpoRep.Value;
                                    worksheet.Cells[row, 3].Value = rep.FormNum_DB;
                                    worksheet.Cells[row, 4].Value = rep.StartPeriod_DB;
                                    worksheet.Cells[row, 5].Value = rep.EndPeriod_DB;
                                    worksheet.Cells[row, 6].Value = rep.CorrectionNumber_DB;
                                    worksheet.Cells[row, 7].Value = rep.Rows.Count;
                                    row++;
                                }
                            }

                            excelPackage.Save();

                            string messageBoxText = $"Выгрузка \"Всех форм 1\", сохранена по пути {path}. Вы хотите открыть расположение файла?";
                            string caption = "Выгрузка данных";
                            MessageBoxButton button = MessageBoxButton.YesNo;
                            MessageBoxImage icon = MessageBoxImage.Information;
                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                            if (result == MessageBoxResult.Yes)
                                Process.Start("explorer.exe", $"{fileInfo.DirectoryName}");
                        }
                    }
                    break;
                case "2":
                    var saveExcel2 = dial.ShowDialog(Application.Current.MainWindow);
                    if ((bool)saveExcel2!)
                    {
                        var path = dial.FileName;
                        FileInfo fileInfo = new(path);
                        if (!path.Contains(".xlsx"))
                        {
                            path += ".xlsx";
                        }
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                        {
                            excelPackage.Workbook.Properties.Author = "RAO_APP";
                            excelPackage.Workbook.Properties.Title = "Report";
                            excelPackage.Workbook.Properties.Created = DateTime.Now;


                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Список всех форм 2");
                            worksheet.Cells[1, 1].Value = "Рег.№";
                            worksheet.Cells[1, 2].Value = "ОКПО";
                            worksheet.Cells[1, 3].Value = "Форма";
                            worksheet.Cells[1, 4].Value = "Отчетный год";
                            worksheet.Cells[1, 5].Value = "Номер кор.";
                            worksheet.Cells[1, 6].Value = "Колличество строк";

                            var row = 2;
                            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
                            {
                                foreach (FireBird.Report rep in reps.Report_Collection)
                                {
                                    worksheet.Cells[row, 1].Value = reps.Master.RegNoRep.Value;
                                    worksheet.Cells[row, 2].Value = reps.Master.OkpoRep.Value;
                                    worksheet.Cells[row, 3].Value = rep.FormNum_DB;
                                    worksheet.Cells[row, 4].Value = rep.Year_DB;
                                    worksheet.Cells[row, 5].Value = rep.CorrectionNumber_DB;
                                    worksheet.Cells[row, 6].Value = rep.Rows.Count;
                                    row++;
                                }
                            }
                            excelPackage.Save();

                            string messageBoxText = $"Выгрузка \"Всех форм 2\", сохранена по пути {path}. Вы хотите открыть расположение файла?";
                            string caption = "Выгрузка данных";
                            MessageBoxButton button = MessageBoxButton.YesNo;
                            MessageBoxImage icon = MessageBoxImage.Information;
                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                            if (result == MessageBoxResult.Yes)
                                Process.Start("explorer.exe", $"{fileInfo.DirectoryName}");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}