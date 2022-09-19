using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using DatabaseAnalysis.WPF.State.Navigation;
using System;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenFormCommand : BaseCommand
    {
        private readonly Navigator _navigator;

        public OpenFormCommand(Navigator navigator)
        {
            _navigator = navigator;
            _navigator.PropertyChanged += NavigatorPropertyChanged;
        }

        private void NavigatorPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedForm) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedForm) 
                || e.PropertyName == nameof(OperReportsViewModel.Reports) || e.PropertyName == nameof(AnnualReportsViewModel.Reports) 
                || e.PropertyName == nameof(OperReportsViewModel.ReportCollection) || e.PropertyName == nameof(AnnualReportsViewModel.ReportCollection) 
                || e.PropertyName == nameof(Navigator.CurrentViewModel))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (_navigator.CurrentViewModel is OperReportsViewModel operReportsViewModel)
            {
                return operReportsViewModel.SelectedReport != null;

            }
            if (_navigator.CurrentViewModel is AnnualReportsViewModel annualReportsViewModel)
            {
                return annualReportsViewModel.SelectedReport != null;
            }
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (_navigator.CurrentViewModel is OperReportsViewModel operReportsViewModel)
            {
                _ = new FormProgressBar(operReportsViewModel.SelectedReport!.FormNum_DB, Convert.ToInt32(parameter));
            }
            if (_navigator.CurrentViewModel is AnnualReportsViewModel annualReportsViewModel)
            {
                _ = new FormProgressBar(annualReportsViewModel.SelectedReport!.FormNum_DB, Convert.ToInt32(parameter));
            }
        }
    }
}