using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

                var myTaskRep = Task.Factory.StartNew(async () => await ReportsStorge.GetReport(report!.Id, null));
                await myTaskRep;

                var date = DateTime.Now.Date;
                var dateDay = date.Day.ToString();
                var dateMonth = date.Month.ToString();
                if (dateDay.Length < 2) dateDay = "0" + dateDay;
                if (dateMonth.Length < 2) dateMonth = "0" + dateMonth;

                report = ReportsStorge.Local_Reports.Report_Collection.FirstOrDefault(x => x.Id == report!.Id);
                report!.ExportDate.Value = dateDay + "." + dateMonth + "." + date.Year;


                var findReports = ReportsStorge.Local_Reports.Reports_Collection.FirstOrDefault(x => x.Report_Collection.Contains(report));

                var api = new EssanceMethods.APIFactory<FireBird.Reports>();

                var myTaskReps = Task.Factory.StartNew(async () => findReports = await api.GetAsync(findReports!.Id));
                await myTaskReps;


                string path = saveFileDialog.FileName.Replace(saveFileDialog.SafeFileName, "");

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

                var myTaskDb = Task.Factory.StartNew(async () =>
                {
                    using (DBModel db = new(path))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        await ProcessDataBaseFillEmpty(db);
                        try
                        {
                            FireBird.Reports rp = new();
                            rp.Master = findReports!.Master;
                            rp.Report_Collection.Add(report);

                            //await RestoreReportsOrders(rp);
                            //rp.CleanIds();
                            //await ProcessIfNoteOrder0(rp);

                            db.DBObservableDbSet.Local.First().Reports_Collection.Add(rp);
                            await db.SaveChangesAsync(ReportsStorge.cancellationToken);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                });
                await myTaskDb;

                string msg;
                #region MessageFormMissing
                MessageBoxResult result = MessageBox.Show(
                    msg = $"Выгрузка \"Экспорт RAODB\" завршена!",
                    "Выгрузка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                #endregion
            }
        }
        private async Task ProcessDataBaseFillEmpty(DBModel dbm)
        {
            if (dbm.DBObservableDbSet.Count() == 0) dbm.DBObservableDbSet.Add(new DBObservable());
            foreach (var item in dbm.DBObservableDbSet)
            {
                foreach (FireBird.Reports it in item.Reports_Collection)
                {
                    if (it.Master_DB.FormNum_DB != "")
                    {
                        if (it.Master_DB.Rows10.Count == 0)
                        {
                            var ty1 = (FireBird.Form10)Create("1.0");
                            ty1.NumberInOrder_DB = 1;
                            var ty2 = (FireBird.Form10)Create("1.0");
                            ty2.NumberInOrder_DB = 2;
                            it.Master_DB.Rows10.Add(ty1);
                            it.Master_DB.Rows10.Add(ty2);
                        }
                        if (it.Master_DB.Rows20.Count == 0)
                        {
                            var ty1 = (FireBird.Form20)Create("2.0");
                            ty1.NumberInOrder_DB = 1;
                            var ty2 = (FireBird.Form20)Create("2.0");
                            ty2.NumberInOrder_DB = 2;
                            it.Master_DB.Rows20.Add(ty1);
                            it.Master_DB.Rows20.Add(ty2);
                        }
                    }
                }
            }
        }
        private async Task RestoreReportsOrders(FireBird.Reports item)
        {
            if (item.Master_DB.FormNum_DB == "1.0")
            {
                if (item.Master_DB.Rows10[0].Id > item.Master_DB.Rows10[1].Id)
                {
                    if (item.Master_DB.Rows10[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows10[0].NumberInOrder_DB = 2;
                    }
                    if (item.Master_DB.Rows10[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows10[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 2;
                        }
                    }
                }
                else
                {
                    if (item.Master_DB.Rows10[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows10[0].NumberInOrder_DB = 1;
                    }
                    if (item.Master_DB.Rows10[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows10[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 2;
                        }
                    }
                }
            }
            if (item.Master_DB.FormNum_DB == "2.0")
            {
                if (item.Master_DB.Rows20[0].Id > item.Master_DB.Rows20[1].Id)
                {
                    if (item.Master_DB.Rows20[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows20[0].NumberInOrder_DB = 2;
                    }
                    if (item.Master_DB.Rows20[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows20[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 2;
                        }
                    }
                }
                else
                {
                    if (item.Master_DB.Rows20[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows20[0].NumberInOrder_DB = 1;
                    }
                    if (item.Master_DB.Rows20[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows20[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 2;
                        }
                    }
                }
            }
        }
        private async Task ProcessIfNoteOrder0(FireBird.Reports item)
        {
            foreach (FireBird.Report form in item.Report_Collection)
            {
                foreach (FireBird.Note note in form.Notes)
                {
                    if (note.Order == 0)
                    {
                        note.Order = GetNumberInOrder(form.Notes);
                    }
                }
            }
        }
        private int GetNumberInOrder(IEnumerable lst)
        {
            int maxNum = 0;

            foreach (var item in lst)
            {
                var frm = (INumberInOrder)item;
                if (frm.Order >= maxNum)
                {
                    maxNum++;
                }
            }
            return maxNum + 1;
        }
        public Form Create(string Param)
        {
            Form tmp = null;
            switch (Param)
            {
                case "1.0": tmp = new FireBird.Form10(); break;
                case "1.1": tmp = new FireBird.Form11(); break;
                case "1.2": tmp = new FireBird.Form12(); break;
                case "1.3": tmp = new FireBird.Form13(); break;
                case "1.4": tmp = new FireBird.Form14(); break;
                case "1.5": tmp = new FireBird.Form15(); break;
                case "1.6": tmp = new FireBird.Form16(); break;
                case "1.7": tmp = new FireBird.Form17(); break;
                case "1.8": tmp = new FireBird.Form18(); break;
                case "1.9": tmp = new FireBird.Form19(); break;

                case "2.0": tmp = new FireBird.Form20(); break;
                case "2.1": tmp = new FireBird.Form21(); break;
                case "2.2": tmp = new FireBird.Form22(); break;
                case "2.3": tmp = new FireBird.Form23(); break;
                case "2.4": tmp = new FireBird.Form24(); break;
                case "2.5": tmp = new FireBird.Form25(); break;
                case "2.6": tmp = new FireBird.Form26(); break;
                case "2.7": tmp = new FireBird.Form27(); break;
                case "2.8": tmp = new FireBird.Form28(); break;
                case "2.9": tmp = new FireBird.Form29(); break;
                case "2.10": tmp = new FireBird.Form210(); break;
                case "2.11": tmp = new FireBird.Form211(); break;
                case "2.12": tmp = new FireBird.Form212(); break;
            }

            if (tmp != null)
            {
                tmp.FormNum.Value = Param;
            }

            return tmp;
        }

    }
}
