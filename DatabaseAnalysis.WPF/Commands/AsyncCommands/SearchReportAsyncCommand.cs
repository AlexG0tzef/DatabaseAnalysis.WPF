using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class SearchReportAsyncCommand : AsyncBaseCommand
    {
        private readonly BaseViewModel _baseViewModel;

        public SearchReportAsyncCommand(BaseViewModel baseViewModel)
        {
            _baseViewModel = baseViewModel;
            _baseViewModel.PropertyChanged += BaseViewModelPropertyChanged;
        }

        private void BaseViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedCorrectionNumber) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedCorrectionNumber))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedEndPeriod))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedExportDate) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedExportDate))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedStartPeriod))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedForm) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedForm))
            {
                OnCanExecuteChanged();
            }
            if (e.PropertyName == nameof(AnnualReportsViewModel.YearPeriod))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if (_baseViewModel is OperReportsViewModel)
            {
                OperReportsViewModel operReportsViewModel = (OperReportsViewModel)_baseViewModel;
                if (operReportsViewModel.ReportStorage != null)
                {
                    var repCollection = operReportsViewModel.ReportStorage.Where(
                        x => x.FormNum_DB.Contains(operReportsViewModel.SelectedForm) &&
                        x.StartPeriod_DB.Contains(operReportsViewModel.SelectedStartPeriod) &&
                        x.EndPeriod_DB.Contains(operReportsViewModel.SelectedEndPeriod) &&
                        x.ExportDate_DB.Contains(operReportsViewModel.SelectedExportDate) &&
                        x.CorrectionNumber_DB.ToString().Contains(operReportsViewModel.SelectedCorrectionNumber));
                    operReportsViewModel.ReportCollection = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Report>(repCollection);
                }
            }
            if (_baseViewModel is AnnualReportsViewModel)
            {
                AnnualReportsViewModel annualReportsViewModel = (AnnualReportsViewModel)_baseViewModel;
                if (annualReportsViewModel.ReportStorage != null)
                {
                    var repCollection = annualReportsViewModel.ReportStorage.Where(
                        x => x.FormNum_DB.Contains(annualReportsViewModel.SelectedForm) &&
                        x.StartPeriod_DB.Contains(annualReportsViewModel.SelectedYearPeriod) &&
                        x.ExportDate_DB.Contains(annualReportsViewModel.SelectedExportDate) &&
                        x.CorrectionNumber_DB.ToString().Contains(annualReportsViewModel.SelectedCorrectionNumber));
                    annualReportsViewModel.ReportCollection = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Report>(repCollection);
                }
            }
        }
    }
}
