using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class GetAllReportsAsyncCommand : AsyncBaseCommand
    {
        private readonly Navigator _navigator;

        public GetAllReportsAsyncCommand(Navigator navigator)
        { 
            _navigator = navigator;
            _navigator.PropertyChanged += NavigatorPropertyChanged;
        }

        private void NavigatorPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(Navigator.ReportsStorage))
            //{
            //    OnCanExecuteChanged();
            //}
        }

        public override async Task AsyncExecute(object? parameter)
        {
            var api = new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Reports>();
            List<DatabaseAnalysis.WPF.FireBird.Reports>? reports;
            if (parameter is OperReportsViewModel)
            {
                StaticConfiguration.TpmDb = "OPER";
                if (ReportsStorge.ReportsStorage!.Count == 0)
                {
                    reports = await api.GetAllAsync();
                    ReportsStorge.ReportsStorage!.AddRange(reports);
                }
                else
                {
                    var rep1 = ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")).ToList();
                    if (rep1.Count != 0)
                        reports = ReportsStorge.ReportsStorage;
                    else
                    {
                        var reps = await api.GetAllAsync();
                        ReportsStorge.ReportsStorage.AddRange(reps.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")));
                        reports = ReportsStorge.ReportsStorage;
                    }
                }
               
                ((OperReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")));
            }
            if (parameter is AnnualReportsViewModel)
            {
                StaticConfiguration.TpmDb = "YEAR";
                if (ReportsStorge.ReportsStorage!.Count == 0)
                {
                    reports = await api.GetAllAsync();
                    ReportsStorge.ReportsStorage!.AddRange(reports);
                }
                else
                {
                    var rep2 = ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0") && x.Order != 0).ToList();
                    if (rep2.Count != 0)
                        reports = ReportsStorge.ReportsStorage;
                    else
                    {
                        var reps = await api.GetAllAsync();
                        ReportsStorge.ReportsStorage.AddRange(reps.Where(x => x.Master_DB.FormNum_DB.Equals("2.0")));
                        reports = ReportsStorge.ReportsStorage;
                    }
                }
                ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0")));
            }

        }
    }
}
