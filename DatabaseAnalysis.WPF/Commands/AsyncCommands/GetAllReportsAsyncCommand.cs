using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
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
            if (e.PropertyName == nameof(Navigator.ReportsStorage))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task AsyncExecute(object? parameter)
        {
            var api = new EssanceMethods.APIFactory<DatabaseAnalysis.WPF.FireBird.Reports>();
            List<DatabaseAnalysis.WPF.FireBird.Reports>? reports;
            if (parameter is OperReportsViewModel)
            {
                StaticConfiguration.TpmDb = "OPER";
                if (_navigator.ReportsStorage!.Count == 0)
                {
                    reports = await api.GetAllAsync();
                    _navigator.ReportsStorage!.AddRange(reports);
                }
                else
                {
                    var rep1 = _navigator.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")).ToList();
                    if (rep1.Count != 0)
                        reports = _navigator.ReportsStorage;
                    else
                    {
                        var reps = await api.GetAllAsync();
                        _navigator.ReportsStorage.AddRange(reps.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")));
                        reports = _navigator.ReportsStorage;
                    }
                }
               
                ((OperReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(_navigator.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("1.0")));
            }
            if (parameter is AnnualReportsViewModel)
            {
                StaticConfiguration.TpmDb = "YEAR";
                if (_navigator.ReportsStorage!.Count == 0)
                {
                    reports = await api.GetAllAsync();
                    _navigator.ReportsStorage!.AddRange(reports);
                }
                else
                {
                    var rep2 = _navigator.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0") && x.Order != 0).ToList();
                    if (rep2.Count != 0)
                        reports = _navigator.ReportsStorage;
                    else
                    {
                        var reps = await api.GetAllAsync();
                        _navigator.ReportsStorage.AddRange(reps.Where(x => x.Master_DB.FormNum_DB.Equals("2.0")));
                        reports = _navigator.ReportsStorage;
                    }
                }
                ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(_navigator.ReportsStorage.Where(x => x.Master_DB.FormNum_DB.Equals("2.0")));
            }

        }
    }
}
