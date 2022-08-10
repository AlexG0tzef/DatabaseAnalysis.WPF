using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                OnPropertyChanged(nameof(ValueBar));

            }
        }

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();
        public DataProgressBar _dataProgressBar { get; set; }

        public DataProgressViewModel(string attLoad, DataProgressBar dataProgressBar)
        {
            _dataProgressBar = dataProgressBar;
            _backgroundWorker.DoWork += (s, e) =>
            {
                ICommand getData = new GetDataAsyncCommand(this);
                getData?.Execute(attLoad);
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
