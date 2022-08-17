using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using DatabaseAnalysis.WPF.State.Navigation;
using System;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenFormCommand : BaseCommand
    {
        private readonly BaseViewModel _operReportsViewModel;
        private readonly Navigator _navigator;
        private readonly string _formNum;

        public OpenFormCommand(BaseViewModel operReportsViewModel, Navigator navigator)
        {
            _operReportsViewModel = operReportsViewModel;
            _navigator = navigator;

            _operReportsViewModel.PropertyChanged += OperReportsViewModelPropertyChanged;
        }

        private void OperReportsViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedReport))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (_operReportsViewModel is OperReportsViewModel)
            {
                var operReportsViewModel = (OperReportsViewModel)_operReportsViewModel;
                return operReportsViewModel.SelectedReport != null;

            }
            if (_operReportsViewModel is AnnualReportsViewModel)
            {
                var annualReportsViewModel = (AnnualReportsViewModel)_operReportsViewModel;
                return annualReportsViewModel.SelectedReport != null;
            }
            return false;
        }

        public override void Execute(object? parameter)
        {
            if (_operReportsViewModel is OperReportsViewModel)
            {
                var operReportsViewModel = (OperReportsViewModel)_operReportsViewModel;

                var progBar = new FormProgressBar(operReportsViewModel.SelectedReport.FormNum_DB, Convert.ToInt32(parameter));

            }
            if (_operReportsViewModel is AnnualReportsViewModel)
            {
                var annualReportsViewModel = (AnnualReportsViewModel)_operReportsViewModel;

                var progBar = new FormProgressBar(annualReportsViewModel.SelectedReport.FormNum_DB, Convert.ToInt32(parameter));
            }
        }
    }
}
