using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class GetDataAsyncCommand : AsyncBaseCommand
    {
        private readonly EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report> api;
        private readonly CancellationTokenSource cancelTokenSource;
        private readonly CancellationToken token;

        public GetDataAsyncCommand()
        {
            api = new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report>();
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
        }

        public override async Task AsyncExecute(object? parameter)
        {
            var attLoad = parameter.ToString();
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
                    StaticConfiguration.TpmDb = "OPER";

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
