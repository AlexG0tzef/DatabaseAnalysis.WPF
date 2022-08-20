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

        private string _formName;
        public string FormName
        {
            get => _formName;
            set 
            { 
                _formName = value;
                OnPropertyChanged(nameof(FormName));
            }
        }

        public NavigatorForm Navigator { get; set; } = new NavigatorForm();
        public ICommand UpdateForm { get; set; }

        public FormsViewModel(string frm, int id)
        {
            Navigator.FormNumber = frm;
            UpdateForm = new UpdateCurrentFormViewModelCommand(Navigator, this);
            FormName = "Окно формы " + Navigator.FormNumber;
        }
    }
}
