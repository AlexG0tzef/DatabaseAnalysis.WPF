using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.ViewModels.Progress;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Storages
{
    public class ReportsStorge
    {
        private static EssanceMethods.APIFactory<Report> api => new();

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

        public static async Task GetDataReports(object? parameter, MainWindowViewModel mainWindowViewModel)
        {
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
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2") && x.Rows12 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.3":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3") && x.Rows13 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.4":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4") && x.Rows14 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.5":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5") && x.Rows15 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.6":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6") && x.Rows16 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.7":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7") && x.Rows17 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.8":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8") && x.Rows18 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "1.9":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9") && x.Rows19 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";
                    break;
                case "2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2') && x.Rows == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.1":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1") && x.Rows21 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.2":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2") && x.Rows22 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.3":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3") && x.Rows23 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.4":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4") && x.Rows24 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.5":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5") && x.Rows25 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.6":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6") && x.Rows26 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.7":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7") && x.Rows27 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.8":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8") && x.Rows28 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.9":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9") && x.Rows29 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.10":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10") && x.Rows210 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.11":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11") && x.Rows211 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
                case "2.12":
                    emptyRep = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12") && x.Rows212 == null).ToList();
                    repsWith = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";
                    break;
            }
            #endregion

            await Parallel.ForEachAsync(repsWith, async (updateReports, token) =>
            {
                foreach (var rep in emptyRep)
                {
                    if (updateReports.Report_Collection.Contains(rep))
                    {
                        var repFromDb = await api.GetAsync(rep.Id);
                        updateReports.Report_Collection.Remove(rep);
                        updateReports.Report_Collection.Add(repFromDb);
                        mainWindowViewModel.ValueBar += 100 / emptyRep.Count;
                    }
                }
            });
        }
    }
}