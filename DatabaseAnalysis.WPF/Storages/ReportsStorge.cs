using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Storages
{
    public class ReportsStorge
    {
        public static bool isBusy = false;
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

        public static async Task FillEmptyReports(object? parameter, MainWindowViewModel mainWindowViewModel)
        {
            isBusy = true;
            mainWindowViewModel.ValueBar = 0;
            mainWindowViewModel.ValueBarVisible = Visibility.Visible;

            List<Report> emptyRep = new();
            List<FireBird.Reports> repsWith = new();

            #region SwitchFormToLoad
            switch (parameter)
            {
                case "1":
                    emptyRep = Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('1') && x.Rows == null).ToList();
                    repsWith = Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.1":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1") && x.Rows11 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2") && x.Rows12 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.3":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3") && x.Rows13 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.4":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4") && x.Rows14 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.5":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5") && x.Rows15 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.6":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6") && x.Rows16 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.7":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7") && x.Rows17 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.8":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8") && x.Rows18 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.9":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9") && x.Rows19 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2') && x.Rows == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.1":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1") && x.Rows21 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2") && x.Rows22 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.3":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3") && x.Rows23 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.4":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4") && x.Rows24 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.5":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5") && x.Rows25 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.6":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6") && x.Rows26 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.7":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7") && x.Rows27 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.8":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8") && x.Rows28 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.9":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9") && x.Rows29 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.10":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10") && x.Rows210 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.11":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11") && x.Rows211 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.12":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12") && x.Rows212 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12")).Count() != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
            }
            #endregion

            bool breakFlag = false;
            await Parallel.ForEachAsync(repsWith.TakeWhile(_ => !Volatile.Read(ref breakFlag)), async (updateReports, token) =>
            {
                var emptyPerInupdateReports = emptyRep.Where(x => updateReports.Report_Collection.Contains(x));
                foreach (var rep in emptyPerInupdateReports)
                {
                    if (cancellationToken.IsCancellationRequested)
                        Volatile.Write(ref breakFlag, true);
                    var repFromDb = await api.GetAsync(rep.Id);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        updateReports.Report_Collection.Remove(rep);
                        updateReports.Report_Collection.Add(repFromDb);
                        mainWindowViewModel.ValueBar += (double)100 / emptyRep.Count;
                    }
                }
            });
            isBusy = false;
        }

        public static async Task GetAllReports(object? parameter, MainWindowViewModel _mainWindowViewModel)
        {
            isBusy = true;
            var api = new EssanceMethods.APIFactory<FireBird.Reports>();
            List<FireBird.Reports> repList = new();
            _mainWindowViewModel.ValueBar = 0;
            _mainWindowViewModel.ValueBarVisible = Visibility.Visible;
            if (parameter is OperReportsViewModel)
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка оперативной базы: ";
                StaticConfiguration.TpmDb = "OPER";
                if (ReportsStorge.Local_Reports.Reports_Collection10!.Count == 0)
                {
                    var myTask = Task.Factory.StartNew(async () => repList = await api.GetAllAsync());
                    while (!myTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repList).Where(x => x.Master_DB.FormNum_DB.Equals("1.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }
                if (parameter is not null)
                    ((OperReportsViewModel)parameter!).Reports = new ObservableCollection<FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10);
            }
            if (StaticConfiguration.TpmDb == "YEAR")
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка годовой базы: ";
                //StaticConfiguration.TpmDb = "YEAR";
                if (ReportsStorge.Local_Reports.Reports_Collection20!.Count == 0)
                {
                    var myTask = Task.Factory.StartNew(async () => repList = await api.GetAllAsync());
                    while (!myTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repList).Where(x => x.Master_DB.FormNum_DB.Equals("2.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }
                if (parameter is not null)
                    ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20);
            }
            _mainWindowViewModel.ValueBar = 100;
            _mainWindowViewModel.ValueBarStatus = "";
            _mainWindowViewModel.AmountReports = null;
            isBusy = false;
        }
    }
}