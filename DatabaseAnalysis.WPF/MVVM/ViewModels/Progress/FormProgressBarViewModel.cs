using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.Progress
{
    public class FormProgressBarViewModel : BaseViewModel
    {
        private int _valueBar;
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
        public FormsViewModel FormViewModel { get; set; } = null;
        public FormProgressBar _formProgressBar { get; set; }
        public FormProgressBarViewModel(string frm, int id, FormProgressBar formProgressBar)
        {
            _formProgressBar = formProgressBar;
            _backgroundWorker.DoWork += (s, e) =>
                {
                    FormViewModel = new FormsViewModel(frm, id);
                    FormViewModel.PropertyChanged += FormViewModelPropertyChanged;
                    FormViewModel.UpdateForm?.Execute(id);
                };

            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
            
        }

        private void BackgroundWorkerRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            _formProgressBar.Close();
            _backgroundWorker.Dispose();
        }

        private void FormViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FormsViewModel.ValueBar))
            {
                ValueBar = FormViewModel.ValueBar;
            }
        }
    }
}
