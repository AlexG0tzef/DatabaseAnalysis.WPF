using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.State.NavigationForm
{
    public class NavigatorForm : INavigatorForm, INotifyPropertyChanged
    {
        private BaseFormViewModel _currentFormViewModel;
        public BaseFormViewModel CurrentFormViewModel
        {
            get => _currentFormViewModel;
            set
            {

                _currentFormViewModel = value;
                OnPropertyChanged(nameof(CurrentFormViewModel));

            }
        }

        private string _formNumber;
        public string FormNumber
        {
            get => _formNumber;
            set
            {
                _formNumber = value;
                OnPropertyChanged(nameof(FormNumber));
            }
        }

        public ICommand UpdateCurrentFormViewModelCommand => new UpdateCurrentFormViewModelCommand(this, null);

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
