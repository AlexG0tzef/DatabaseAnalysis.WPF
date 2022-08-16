using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using DatabaseAnalysis.WPF.Storages;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.Progress
{
    public class DataProgressViewModel : BaseViewModel
    {
        private int _valueBar = 10;
        public int ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                _valueBar = _valueBar > 100 ? 100 : _valueBar;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        private IBackgroundLoader _backgroundWorker = new BackgroundLoader();
        public DataProgressBar _dataProgressBar { get; set; }

        public DataProgressViewModel(string attLoad, DataProgressBar dataProgressBar, IBackgroundLoader backgroundWorker)
        {
            _dataProgressBar = dataProgressBar;
            _backgroundWorker = backgroundWorker;
            _backgroundWorker.BackgroundWorker(() =>
            {
                ReportsStorge.GetDataReports(attLoad, this);
            }, () => _dataProgressBar.Close());
        }
    }
}
