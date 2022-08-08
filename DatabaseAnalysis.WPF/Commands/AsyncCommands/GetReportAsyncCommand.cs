﻿using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.Storages;
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

            FireBird.Report? rep;
            var reps = ReportsStorge.ReportsStorage.Where(x => x.Report_Collection.Where(x => x.Id == Convert.ToInt32(parameter)).Count() != 0).FirstOrDefault();
            var checkedRep = reps.Report_Collection.Where(x => x.Id == Convert.ToInt32(parameter)).FirstOrDefault();
            if (checkedRep.Rows == null)
            {
                var api = new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Report>();
                rep = await api.GetAsync(Convert.ToInt32(parameter));
                reps.Report_Collection.Remove(reps.Report_Collection.Where(x => x.Order == rep.Order).FirstOrDefault());
                reps.Report_Collection.Add(rep);
            }
            else
            {
                rep = checkedRep;
            }

            if (rep != null)
            {
                _formViewModel.CurrentReport = rep;
            }
        }
    }
}