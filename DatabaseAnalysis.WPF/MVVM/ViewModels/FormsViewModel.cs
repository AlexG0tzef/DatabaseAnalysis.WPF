using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class FormsViewModel : BaseViewModel
    {

        private int _valueBar = 1;
        public int ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        public NavigatorForm Navigator { get; set; } = new NavigatorForm();
        public ICommand UpdateForm { get; set; }

        public FormsViewModel(string frm, int id)
        {
            Navigator.FormNumber = frm;
            UpdateForm = new UpdateCurrentFormViewModelCommand(Navigator, this);
        }
    }
}
