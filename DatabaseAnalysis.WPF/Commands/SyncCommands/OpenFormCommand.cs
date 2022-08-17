using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using DatabaseAnalysis.WPF.State.Navigation;
using System;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenFormCommand : BaseCommand
    {
        private readonly BaseViewModel _operReportsViewModel;

        public OpenFormCommand(BaseViewModel operReportsViewModel)
        {
            _operReportsViewModel = operReportsViewModel;
            _operReportsViewModel.PropertyChanged += OperReportsViewModelPropertyChanged;
        }

        private void OperReportsViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedForm))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.Reports))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.ReportCollection))
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
            return true;
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
