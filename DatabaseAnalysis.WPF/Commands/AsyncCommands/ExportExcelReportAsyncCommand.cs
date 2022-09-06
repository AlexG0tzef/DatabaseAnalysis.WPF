using DatabaseAnalysis.WPF.Storages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelReportAsyncCommand : AsyncBaseCommand
    {
        public ExportExcelReportAsyncCommand() { }

        public override async Task AsyncExecute(object? parameter)
        {
            //await
            var rep = ReportsStorge.Local_Reports.Report_Collection.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
        }
    }
}
