using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.ViewModels.FormViewModel;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class UpdateCurrentFormViewModelCommand : ICommand
    {
        private NavigatorForm _navigator;
        private FormsViewModel _formsViewModel;
        public UpdateCurrentFormViewModelCommand(NavigatorForm navigator, FormsViewModel formsViewModel)
        {
            _navigator = navigator;
            _formsViewModel = formsViewModel;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var repId = Convert.ToInt32(parameter);
            _formsViewModel.ValueBar = 30;
            switch (_navigator.FormNumber)
            {
                case "1.1":
                    _navigator.CurrentFormViewModel = new Form11ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.2":
                    _navigator.CurrentFormViewModel = new Form12ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.3":
                    _navigator.CurrentFormViewModel = new Form13ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.4":
                    _navigator.CurrentFormViewModel = new Form14ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.5":
                    _navigator.CurrentFormViewModel = new Form15ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.6":
                    _navigator.CurrentFormViewModel = new Form16ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.7":
                    _navigator.CurrentFormViewModel = new Form17ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.8":
                    _navigator.CurrentFormViewModel = new Form18ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "1.9":
                    _navigator.CurrentFormViewModel = new Form19ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.1":
                    _navigator.CurrentFormViewModel = new Form21ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.2":
                    _navigator.CurrentFormViewModel = new Form22ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.3":
                    _navigator.CurrentFormViewModel = new Form23ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.4":
                    _navigator.CurrentFormViewModel = new Form24ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.5":
                    _navigator.CurrentFormViewModel = new Form25ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.6":
                    _navigator.CurrentFormViewModel = new Form26ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.7":
                    _navigator.CurrentFormViewModel = new Form27ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.8":
                    _navigator.CurrentFormViewModel = new Form28ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.9":
                    _navigator.CurrentFormViewModel = new Form29ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.10":
                    _navigator.CurrentFormViewModel = new Form210ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.11":
                    _navigator.CurrentFormViewModel = new Form211ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                case "2.12":
                    _navigator.CurrentFormViewModel = new Form212ViewModel(_navigator, repId, _formsViewModel);
                    _formsViewModel.ValueBar = 100;
                    break;
                default:
                    break;
            }
        }
    }
}
