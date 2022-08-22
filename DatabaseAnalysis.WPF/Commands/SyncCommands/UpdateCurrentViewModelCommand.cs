using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private Navigator _navigator;
        private MainWindowViewModel _mainWindowViewModel;
        public UpdateCurrentViewModelCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Annual:
                        if (_navigator.CurrentViewModel is not AnnualReportsViewModel)
                            _mainWindowViewModel.MainWindowName = "Аналитика отчетности RAODB v.1.0.0 Годовая отчетность";
                            _navigator.CurrentViewModel = new AnnualReportsViewModel(_navigator, _mainWindowViewModel);
                        break;
                    case ViewType.Oper:
                        if (_navigator.CurrentViewModel is not OperReportsViewModel)
                            _mainWindowViewModel.MainWindowName = "Аналитика отчетности RAODB v.1.0.0 Оперативная отчетность";
                        _navigator.CurrentViewModel = new OperReportsViewModel(_navigator, _mainWindowViewModel);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
