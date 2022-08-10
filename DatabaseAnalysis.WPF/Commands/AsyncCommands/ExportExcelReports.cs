using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelReports : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private readonly ICommand _getData;

        public ExportExcelReports(INavigator navigator)
        {
            _navigator = navigator;
            _getData = new GetDataAsyncCommand();
        }
        public override async Task AsyncExecute(object? parameter)
        {

            if (Convert.ToInt32(parameter) == 1)
            {

                _getData?.Execute(parameter);

                #region Test List
                //foreach (DatabaseAnalysis.WPF.FireBird.Reports updateReports in repsWith)
                //{
                //    foreach (var rep in emptyRep)
                //    {
                //        var repFromDb = await api.GetAsync(rep.Id);
                //        updateReports.Report_Collection.Remove(updateReports.Report_Collection.Where(x => x.Order == repFromDb.Order).FirstOrDefault());
                //        updateReports.Report_Collection.Add(repFromDb);
                //    }
                //}
                #endregion

                SaveFileDialog dial = new();
                dial.Filter = "Excel | *.xlsx";
                var saveExcel = dial.ShowDialog(Application.Current.MainWindow);
                if ((bool)saveExcel!)
                {
                    var path = dial.FileName;
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

                        if (_navigator.CurrentViewModel is OperReportsViewModel)
                        {
                            OperReportsViewModel operReportsViewModel = (OperReportsViewModel)_navigator.CurrentViewModel;
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Список всех форм 1");
                            worksheet.Cells[1, 1].Value = "Рег.№";
                            worksheet.Cells[1, 2].Value = "ОКПО";
                            worksheet.Cells[1, 3].Value = "Форма";
                            worksheet.Cells[1, 4].Value = "Дата начала";
                            worksheet.Cells[1, 5].Value = "Дата конца";
                            worksheet.Cells[1, 6].Value = "Номер кор.";
                            worksheet.Cells[1, 7].Value = "Колличество строк";

                            var row = 2;
                            foreach (DatabaseAnalysis.WPF.FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
                            {
                                foreach (DatabaseAnalysis.WPF.FireBird.Report rep in reps.Report_Collection)
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
                        }
                        excelPackage.Save();
                        MessageBox.Show($"Выгрузка \"Всех форм 1\", сохранена по пути {path}");
                    }
                }
            }
            if (Convert.ToInt32(parameter) == 2)
            {
                _getData?.Execute(parameter);

                SaveFileDialog dial = new();
                dial.Filter = "Excel | *.xlsx";
                var saveExcel = dial.ShowDialog(Application.Current.MainWindow);
                if ((bool)saveExcel!)
                {
                    var path = dial.FileName;
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

                        if (_navigator.CurrentViewModel is AnnualReportsViewModel)
                        {
                            AnnualReportsViewModel annualReportsViewModel = (AnnualReportsViewModel)_navigator.CurrentViewModel;
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Список всех форм 2");
                            worksheet.Cells[1, 1].Value = "Рег.№";
                            worksheet.Cells[1, 2].Value = "ОКПО";
                            worksheet.Cells[1, 3].Value = "Форма";
                            worksheet.Cells[1, 4].Value = "Отчетный год";
                            worksheet.Cells[1, 5].Value = "Номер кор.";
                            worksheet.Cells[1, 6].Value = "Колличество строк";

                            var row = 2;
                            foreach (DatabaseAnalysis.WPF.FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
                            {
                                foreach (DatabaseAnalysis.WPF.FireBird.Report rep in reps.Report_Collection)
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
                        }
                        excelPackage.Save();
                        MessageBox.Show($"Выгрузка \"Всех форм 2\", сохранена по пути {path}");
                    }
                }
            }
        }
    }
}
