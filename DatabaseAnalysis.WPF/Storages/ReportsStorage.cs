using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Storages
{
    public class ReportsStorage
    {
        private static EssanceMethods.APIFactory<Report> api => new();
        public static CancellationTokenSource _cancellationTokenSource = new();
        public static CancellationToken cancellationToken = _cancellationTokenSource.Token;

        #region Local_Reports
        private static DBObservable _local_Reports = new();
        public static DBObservable Local_Reports
        {
            get => _local_Reports;
            set
            {
                if (_local_Reports != value && value != null)
                {
                    _local_Reports = value;
                }
            }
        }
        #endregion

        #region FillEmptyReports
        public static async Task FillEmptyReports(object? parameter, MainWindowViewModel mainWindowViewModel)
        {
            mainWindowViewModel.IsBusy = false;
            mainWindowViewModel.ValueBar = 0;
            mainWindowViewModel.ValueBarVisible = Visibility.Visible;

            List<Report> emptyRep = new();
            List<FireBird.Reports> repsWith = new();
            List<Report> repFromDbN = new();

            #region SwitchFormToLoad
            switch (parameter)
            {
                case "1":
                    emptyRep = Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('1') && x.Rows == null).ToList();
                    repsWith = Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.1":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1") && x.Rows11 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.2":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2") && x.Rows12 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.3":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3") && x.Rows13 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.4":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4") && x.Rows14 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.5":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5") && x.Rows15 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.6":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6") && x.Rows16 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.7":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7") && x.Rows17 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.8":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8") && x.Rows18 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.9":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9") && x.Rows19 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "2":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2') && x.Rows == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.1":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1") && x.Rows21 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.2":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2") && x.Rows22 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.3":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3") && x.Rows23 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.4":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4") && x.Rows24 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.5":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5") && x.Rows25 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.6":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6") && x.Rows26 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.7":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7") && x.Rows27 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.8":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8") && x.Rows28 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.9":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9") && x.Rows29 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.10":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10") && x.Rows210 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.11":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11") && x.Rows211 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.12":
                    emptyRep = ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12") && x.Rows212 == null).ToList();
                    repsWith = ReportsStorage.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
            }
            #endregion

            if (emptyRep.Count != 0)
            {
                var myTask = Task.Factory.StartNew(async () => repFromDbN = await api.GetAllAsync(parameter.ToString()));
                while (!myTask.IsCompleted)
                {
                    if (mainWindowViewModel.ValueBar < 99)
                    {
                        Thread.Sleep(50);
                        mainWindowViewModel.ValueBar += (double)100 / emptyRep.Count;
                    }
                }
                foreach (var org in repsWith)
                {
                    var emptyRepInUpdateReports = emptyRep.Where(x => org.Report_Collection.Contains(x));
                    foreach (var rep in emptyRepInUpdateReports)
                    {
                        var repFromDb = repFromDbN.FirstOrDefault(x => x.Id == rep.Id);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            org.Report_Collection.Remove(rep);
                            org.Report_Collection.Add(repFromDb);
                            mainWindowViewModel.ValueBar += (double)100 / emptyRep.Count;
                        }

                    }
                }
            }
            mainWindowViewModel.IsBusy = true;
        }
        #endregion

        #region GetAllReports
        public static async Task GetAllReports(object? parameter, MainWindowViewModel _mainWindowViewModel)
        {
            _mainWindowViewModel.IsBusy = false;

            var api = new EssanceMethods.APIFactory<FireBird.Reports>();
            List<FireBird.Reports> repListQ = null;
            _mainWindowViewModel.ValueBar = 0;
            _mainWindowViewModel.ValueBarVisible = Visibility.Visible;

            if (StaticConfiguration.TpmDb == "OPER")
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка оперативной базы: ";
                if (ReportsStorage.Local_Reports.Reports_Collection10!.Count == 0)
                {
                    var myTask = Task.Factory.StartNew(async () => repListQ = await api.GetAllAsync());
                    while (!myTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repListQ!).Where(x => x.Master_DB.FormNum_DB.Equals("1.0"));
                    ReportsStorage.Local_Reports.Reports_Collection.AddRange(reps);
                }
                if (parameter is not null)
                    ((OperReportsViewModel)parameter!).Reports = new ObservableCollection<FireBird.Reports>(ReportsStorage.Local_Reports.Reports_Collection10);
            }
            if (StaticConfiguration.TpmDb == "YEAR")
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка годовой базы: ";
                if (ReportsStorage.Local_Reports.Reports_Collection20!.Count == 0)
                {
                    var myTask = Task.Factory.StartNew(async () => repListQ = await api.GetAllAsync());
                    while (!myTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repListQ!).Where(x => x.Master_DB.FormNum_DB.Equals("2.0"));
                    ReportsStorage.Local_Reports.Reports_Collection.AddRange(reps);
                }
                if (parameter is not null)
                    ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<FireBird.Reports>(ReportsStorage.Local_Reports.Reports_Collection20);
            }
            _mainWindowViewModel.ValueBar = 100;
            _mainWindowViewModel.ValueBarStatus = "";
            _mainWindowViewModel.AmountReports = null;
            _mainWindowViewModel.AmountOrgs = null;

            _mainWindowViewModel.IsBusy = true;
        }
        #endregion

        #region GetReport
        public static async Task GetReport(int id, BaseFormViewModel? _formViewModel = null)
        {
            Report? rep;
            var reps = Local_Reports.Reports_Collection.FirstOrDefault(x => x.Report_Collection.Where(x => x.Id == Convert.ToInt32(id)).Count() != 0);
            var checkedRep = reps.Report_Collection.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();
            if (checkedRep.Rows == null)
            {
                var api = new EssanceMethods.APIFactory<Report>();
                rep = await api.GetAsync(Convert.ToInt32(id));
                reps.Report_Collection.Remove(checkedRep);
                reps.Report_Collection.Add(rep);
            }
            else
                rep = checkedRep;

            if (rep != null && _formViewModel != null)
                _formViewModel.CurrentReport = rep;
        }
        #endregion

        #region ExportReport
        public static async Task ExportReport(string path, FireBird.Reports reports, FireBird.Report report)
        {
            using (DBModel db = new(path))
            {
                await db.Database.MigrateAsync(ReportsStorge.cancellationToken);

                try
                {
                    FireBird.Reports rp = new();
                    rp.Master = reports.Master;
                    rp.Report_Collection.Add(report);

                    db.ReportsCollectionDbSet.Add(reports);

                    await db.SaveChangesAsync(ReportsStorge.cancellationToken);
                }
                catch (DbUpdateException ex)
                {
                    var t = ex.Entries;
                }
            }
        }

        private static async Task ProcessDataBaseFillEmpty(DBModel dbm)
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
        private static async Task ProcessDataBaseFillNullOrder(DBObservable Local_Reports)
        {
            foreach (FireBird.Reports item in Local_Reports.Reports_Collection)
            {
                foreach (FireBird.Report it in item.Report_Collection)
                {
                    foreach (FireBird.Note _i in it.Notes)
                    {
                        if (_i.Order == 0)
                        {
                            _i.Order = GetNumberInOrder(it.Notes);
                        }
                    }
                }
            }
        }
        private static async Task RestoreReportsOrders(FireBird.Reports item)
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
        private static async Task ProcessIfNoteOrder0(FireBird.Reports item)
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
        private static int GetNumberInOrder(IEnumerable lst)
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
        public static Form Create(string Param)
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
        #endregion
    }
}