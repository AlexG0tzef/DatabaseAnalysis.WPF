using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IBaseViewModelCommand
    {
        #region Commands
        public ICommand AddUpdateReports { get; set; }
        public ICommand DeleteReports { get; set; }
        public ICommand ExportExcelReport { get; set; }
        public ICommand ExportRaodbReport { get; set; }
        public ICommand ExportExcelReportAnalisys { get; set; }
        public ICommand ExportExcelReportPrint { get; set; }
        public ICommand ImportExcel { get; set; }
        public ICommand ImportRAODB { get; set; }
        public ICommand OpenForm { get; set; } 
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
