using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.Views.Progress;
using System.ComponentModel;

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

        private IBackgroundLoader _backgroundWorker;
        public FormsViewModel FormViewModel { get; set; } = null;
        public FormProgressBar _formProgressBar { get; set; }
        public FormProgressBarViewModel(string frm, int id, FormProgressBar formProgressBar, IBackgroundLoader backgroundWorker)
        {
            _formProgressBar = formProgressBar;
            _backgroundWorker = backgroundWorker;
            _backgroundWorker.BackgroundWorker(() =>
            {
                FormViewModel = new FormsViewModel(frm, id);
                FormViewModel.PropertyChanged += FormViewModelPropertyChanged;
                FormViewModel.UpdateForm?.Execute(id);

            }, () => _formProgressBar.Close());
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
