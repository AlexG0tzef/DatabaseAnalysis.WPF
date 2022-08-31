using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedCorrectionNumber) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedCorrectionNumber) 
                || e.PropertyName == nameof(OperReportsViewModel.SelectedEndPeriod)
                || e.PropertyName == nameof(OperReportsViewModel.SelectedExportDate) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedExportDate)
                || e.PropertyName == nameof(OperReportsViewModel.SelectedStartPeriod)
                || e.PropertyName == nameof(OperReportsViewModel.SelectedForm) || e.PropertyName == nameof(AnnualReportsViewModel.SelectedForm)
                || e.PropertyName == nameof(AnnualReportsViewModel.SelectedYearPeriod))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if (_baseViewModel is OperReportsViewModel operReportsViewModel)
            {
                if (operReportsViewModel.ReportStorage != null)
                {
                    var repCollection = operReportsViewModel.ReportStorage.Where(
                        x => x.FormNum_DB.Contains(operReportsViewModel.SelectedForm) &&
                        IsDateInRange(operReportsViewModel.SelectedStartPeriod, operReportsViewModel.SelectedEndPeriod, x.StartPeriod_DB, x.EndPeriod_DB) &&
                        //x.StartPeriod_DB.Contains(operReportsViewModel.SelectedStartPeriod) &&
                        //x.EndPeriod_DB.Contains(operReportsViewModel.SelectedEndPeriod) &&
                        x.ExportDate_DB.Contains(operReportsViewModel.SelectedExportDate) &&
                        x.CorrectionNumber_DB.ToString().Contains(operReportsViewModel.SelectedCorrectionNumber));
                    operReportsViewModel.ReportCollection = new ObservableCollection<FireBird.Report>(repCollection);
                }
            }
            if (_baseViewModel is AnnualReportsViewModel annualReportsViewModel)
            {
                if (annualReportsViewModel.ReportStorage != null)
                {
                    var repCollection = annualReportsViewModel.ReportStorage.Where(
                        x => x.FormNum_DB.Contains(annualReportsViewModel.SelectedForm) &&
                        x.Year_DB.Contains(annualReportsViewModel.SelectedYearPeriod) &&
                        x.ExportDate_DB.Contains(annualReportsViewModel.SelectedExportDate) &&
                        x.CorrectionNumber_DB.ToString().Contains(annualReportsViewModel.SelectedCorrectionNumber));
                    annualReportsViewModel.ReportCollection = new ObservableCollection<FireBird.Report>(repCollection);
                }
            }
        }

        private bool IsDateInRange(string? rangeStart, string? rangeEnd, string dateStart, string dateEnd) 
        {
            if (rangeStart is null && rangeEnd is null)
                return true;

            if (rangeEnd is null)
                return compareDate(dateEnd, rangeStart) >= 0;

            if (rangeStart is null)
                return compareDate(dateStart, rangeEnd) <= 0;

            return compareDate(dateStart, rangeEnd) <=0 && compareDate(dateEnd, rangeStart) >= 0;
        }

        private int compareDate(string date1, string date2)
        {
            var date1Arr = date1.Replace("_", "0").Replace("/", ".").Split(".");
            short year1 = short.Parse(date1Arr[2]);
            byte month1 = byte.Parse(date1Arr[1]);
            byte day1 = byte.Parse(date1Arr[0]);
            var date2Arr = date2.Replace("_", "0").Replace("/", ".").Split(".");
            short year2 = short.Parse(date2Arr[2]);
            byte month2 = byte.Parse(date2Arr[1]);
            byte day2 = byte.Parse(date2Arr[0]);

            if (string.IsNullOrEmpty(date1) || string.IsNullOrEmpty(date2))
                return 0;

            if (year1 != year2)
                return year1.CompareTo(year2);

            if (month1 != month2)
                return month1.CompareTo(month2);

            if (day1 != day2)
                return day1.CompareTo(day2);

            return 0;
        }
    }
}