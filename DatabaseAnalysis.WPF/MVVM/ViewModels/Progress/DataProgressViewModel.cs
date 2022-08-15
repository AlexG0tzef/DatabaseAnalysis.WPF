using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using DatabaseAnalysis.WPF.Storages;
using System.ComponentModel;
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

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();
        public DataProgressBar _dataProgressBar { get; set; }

        public DataProgressViewModel(string attLoad, DataProgressBar progressBar)
        {
            _backgroundWorker.DoWork += async (s, e) =>
            {
                _dataProgressBar = progressBar;
                await ReportsStorge.GetDataReports(attLoad, this);
            };

            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            _dataProgressBar.Close();
            _backgroundWorker.Dispose();
        }
    }
}
