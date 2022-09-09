using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportRaodbReportAsyncCommand : AsyncBaseCommand
    {
        public override async Task AsyncExecute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new() { Filter = "RAODB | *.raodb" };
            saveFileDialog.FileName = "export";
            bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (saveExcel)
            {
                Report? report = parameter as Report;

                //var myTaskRep = Task.Factory.StartNew(async () => await ReportsStorge.GetReport(report!.Id, null));
                //await myTaskRep;

                var date = DateTime.Now.Date;
                var dateDay = date.Day.ToString();
                var dateMonth = date.Month.ToString();
                if (dateDay.Length < 2) dateDay = "0" + dateDay;
                if (dateMonth.Length < 2) dateMonth = "0" + dateMonth;

                report = ReportsStorge.Local_Reports.Report_Collection.FirstOrDefault(x => x.Id == report!.Id);
                //report!.ExportDate.Value = dateDay + "." + dateMonth + "." + date.Year;


                var findReports = ReportsStorge.Local_Reports.Reports_Collection.FirstOrDefault(x => x.Report_Collection.Contains(report!));

                string path = saveFileDialog.FileName.Replace(saveFileDialog.SafeFileName, "");
                saveFileDialog.Reset();
                string filename2 = "";
                if (findReports!.Master_DB.FormNum_DB == "1.0")
                {
                    filename2 += findReports.Master.RegNoRep.Value;
                    filename2 += "_" + findReports.Master.OkpoRep.Value;

                    filename2 += "_" + report.FormNum_DB;
                    filename2 += "_" + report.StartPeriod_DB;
                    filename2 += "_" + report.EndPeriod_DB;
                    filename2 += "_" + report.CorrectionNumber_DB;
                }
                else
                {
                    if (findReports.Master.Rows20.Count > 0)
                    {
                        filename2 += findReports.Master.RegNoRep.Value;
                        filename2 += "_" + findReports.Master.OkpoRep.Value;

                        filename2 += "_" + report.FormNum_DB;
                        filename2 += "_" + report.Year_DB;
                        filename2 += "_" + report.CorrectionNumber_DB;
                    }
                }

                path = Path.Combine(path, filename2 + ".raodb");

                var myTaskDb = Task.Factory.StartNew(async () => await ReportsStorge.ExportReport(path, findReports, report));
                await myTaskDb;

                #region MessageFormMissing
                string msg;
                MessageBoxResult result = MessageBox.Show(
                    msg = $"Выгрузка \"Экспорт RAODB\" завршена!",
                    "Выгрузка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                #endregion
            }
        }
    }
}
