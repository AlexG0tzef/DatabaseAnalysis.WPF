using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ApplyAsyncCommand : AsyncBaseCommand
    {
        private AddUpdateReportsView addUpdateReportsView;
        private Navigator navigator;
        private MainWindowViewModel mainWindowViewModel;

        public ApplyAsyncCommand(AddUpdateReportsView _addUpdateReportsView, Navigator _navigator, MainWindowViewModel _mainWindowViewModel)
        {
            addUpdateReportsView = _addUpdateReportsView;
            navigator = _navigator;
            mainWindowViewModel = _mainWindowViewModel;
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if(parameter is FireBird.Reports reps)
                ReportsStorage.Local_Reports.Reports_Collection.Add(reps);
            if (parameter is FireBird.Report rep)
                ReportsStorage.Local_Reports.Report_Collection.Add(rep);
            var _ = new UpdateCurrentViewModelCommand(navigator, mainWindowViewModel);
            _?.Execute(ViewType.UpdateOrg);
            addUpdateReportsView.Close();
        }
    }
}
