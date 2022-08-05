using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.ViewModels.FormViewModel;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class UpdateCurrentFormViewModelCommand : ICommand
    {
        private NavigatorForm _navigator;
        public UpdateCurrentFormViewModelCommand(NavigatorForm navigator)
        {
            _navigator = navigator;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var repId = Convert.ToInt32(parameter);
            switch (_navigator.FormNumber)
            {
                case "1.1":
                    _navigator.CurrentFormViewModel = new Form11ViewModel(_navigator, repId);
                    break;
                default:
                    break;
            }
        }
    }
}
