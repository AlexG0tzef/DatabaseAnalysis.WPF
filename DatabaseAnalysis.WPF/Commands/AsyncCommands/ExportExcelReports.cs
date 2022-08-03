using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelReports : AsyncBaseCommand
    {
        private readonly INavigator _navigator;

        public ExportExcelReports(INavigator navigator)
        {
            _navigator = navigator;
        }
        public override async Task AsyncExecute(object? parameter)
        {
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

                        var lst = new List<DatabaseAnalysis.WPF.FireBird.Reports>();

                        foreach (DatabaseAnalysis.WPF.FireBird.Reports item in operReportsViewModel.Reports!)
                        {
                            if (item.Master_DB.FormNum_DB.Split('.')[0] == "1")
                            {
                                lst.Add(item);
                            }
                        }

                        var row = 2;
                        foreach (DatabaseAnalysis.WPF.FireBird.Reports reps in lst)
                        {
                            foreach (DatabaseAnalysis.WPF.FireBird.Report rep in reps.Report_Collection)
                            {
                                worksheet.Cells[row, 1].Value = reps.Master.RegNoRep.Value;
                                worksheet.Cells[row, 2].Value = reps.Master.OkpoRep.Value;
                                worksheet.Cells[row, 3].Value = rep.FormNum_DB;
                                worksheet.Cells[row, 4].Value = rep.StartPeriod_DB;
                                worksheet.Cells[row, 5].Value = rep.EndPeriod_DB;
                                worksheet.Cells[row, 6].Value = rep.CorrectionNumber_DB;
                                row++;
                            }
                        }
                    }
                    excelPackage.Save();
                    MessageBox.Show($"Выгрузка \"Всех форм 1\", сохранена по пути {path}");
                }
            }
        }
    }
}
