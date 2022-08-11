using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public abstract class BaseFormViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Реализация интерфейса INotifyPropertyChanged для оповещения системы об изменениях параметров
        /// </summary>
        /// <parametr name="propertyName"></parametr>
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private FireBird.Report _report;
        public FireBird.Report CurrentReport
        {
            get => _report;
            set
            {
                _report = value;
                OnPropertyChanged(nameof(CurrentReport));
            }
        }

    }
}
