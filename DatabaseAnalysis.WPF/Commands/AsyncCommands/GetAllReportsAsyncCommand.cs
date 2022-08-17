﻿using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class GetAllReportsAsyncCommand : AsyncBaseCommand
    {
        private readonly Navigator _navigator;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public GetAllReportsAsyncCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
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
            if (parameter is OperReportsViewModel)
            {
                StaticConfiguration.TpmDb = "OPER";
                if (ReportsStorge.Local_Reports.Reports_Collection10!.Count == 0)
                {
                    var reps = new ObservableCollectionWithItemPropertyChanged<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync()).Where(x => x.Master_DB.FormNum_DB.Equals("1.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }

                ((OperReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10);
            }
            if (parameter is AnnualReportsViewModel)
            {
                StaticConfiguration.TpmDb = "YEAR";
                if (ReportsStorge.Local_Reports.Reports_Collection20!.Count == 0)
                {
                    var reps = new ObservableCollectionWithItemPropertyChanged<DatabaseAnalysis.WPF.FireBird.Reports>(await api.GetAllAsync()).Where(x => x.Master_DB.FormNum_DB.Equals("2.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }
                ((AnnualReportsViewModel)parameter!).Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20);
            }

        }
    }
}
