using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Storages
{
    public class ReportsStorge
    {
        private static  EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report> api => new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report>();

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

        public static async Task GetDataReports(object? parameter)
        {
            var attLoad = parameter!.ToString();

            switch (attLoad)
            {
                case "1":
                    var emptyRep1 = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('1') && x.Rows == null).ToList();
                    var repsWith1 = ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "OPER";

                    await Parallel.ForEachAsync(repsWith1, async (updateReports, token) =>
                    {

                        foreach (var rep in emptyRep1)
                        {
                            if (updateReports.Report_Collection.Contains(rep))
                            {
                                var repFromDb = await api.GetAsync(rep.Id);
                                updateReports.Report_Collection.Remove(rep);
                                updateReports.Report_Collection.Add(repFromDb);
                            }

                        }
                    });

                    break;
                case "2":
                    var emptyRep2 = ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2') && x.Rows == null).ToList();
                    var repsWith2 = ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Report_Collection.Count != 0).ToList();
                    StaticConfiguration.TpmDb = "YEAR";

                    await Parallel.ForEachAsync(repsWith2, async (updateReports, token) =>
                    {

                        foreach (var rep in emptyRep2)
                        {
                            if (updateReports.Report_Collection.Contains(rep))
                            {
                                var repFromDb = await api.GetAsync(rep.Id);
                                updateReports.Report_Collection.Remove(rep);
                                updateReports.Report_Collection.Add(repFromDb);
                            }

                        }
                    });
                    break;
            }
        }
    }
}
