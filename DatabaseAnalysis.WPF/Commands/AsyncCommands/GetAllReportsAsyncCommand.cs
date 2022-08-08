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
            LinkedList<DatabaseAnalysis.WPF.FireBird.Reports>? reports;
            if (parameter is OperReportsViewModel)
            {
                StaticConfiguration.TpmDb = "OPER";
                if (ReportsStorge.ReportsStorage!.Count == 0)
                {
                    reports = new LinkedList<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync());
                    foreach (var report in reports)
                        ReportsStorge.ReportsStorage!.AddLast(report);
                }
                else
                {
                    var rep1 = ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")).ToList();
                    if (rep1.Count != 0)
                        reports = ReportsStorge.ReportsStorage;
                    else
                    {
                        var reps = new LinkedList<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync()).Where(x => x.Master_DB.FormNum_DB.Equals("1.0"));
                        foreach (var report in reps)
                            ReportsStorge.ReportsStorage!.AddLast(report);
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
                    reports = new LinkedList<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync());
                    foreach (var report in reports)
                        ReportsStorge.ReportsStorage!.AddLast(report);
                }
                else
                {
                    var rep2 = ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0") && x.Order != 0).ToList();
                    if (rep2.Count != 0)
                        reports = ReportsStorge.ReportsStorage;
                    else
                    {
                        var reps = new LinkedList<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync()).Where(x => x.Master_DB.FormNum_DB.Equals("2.0"));
                        foreach (var report in reps)
                            ReportsStorge.ReportsStorage!.AddLast(report);
                        reports = ReportsStorge.ReportsStorage;
                    }
                }
                ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0")));
            }

        }
    }
}
