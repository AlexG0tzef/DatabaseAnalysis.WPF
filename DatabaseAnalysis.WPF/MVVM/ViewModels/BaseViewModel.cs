using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
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
    }
}
