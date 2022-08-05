using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class FormsViewModel : BaseViewModel
    {
        public NavigatorForm Navigator { get; set; } = new NavigatorForm();

        public FormsViewModel(string frm, int id)
        {
            Navigator.FormNumber = frm;
            ICommand UpdateForm = new UpdateCurrentFormViewModelCommand(Navigator);
            UpdateForm?.Execute(id);
        }
    }
}
