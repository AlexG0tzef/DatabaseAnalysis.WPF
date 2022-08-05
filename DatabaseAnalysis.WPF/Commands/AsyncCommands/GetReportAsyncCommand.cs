using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class GetReportAsyncCommand : AsyncBaseCommand
    {
        private readonly BaseFormViewModel _formViewModel;
        public GetReportAsyncCommand(BaseFormViewModel formViewModel)
        { 
            _formViewModel = formViewModel;
        }
        public override async Task AsyncExecute(object? parameter)
        {
            var api = new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report>();
            var rep = await api.GetAsync(Convert.ToInt32(parameter));

            if (rep != null)
            {
                _formViewModel.CurrentReport = rep;
            }
        }
    }
}
