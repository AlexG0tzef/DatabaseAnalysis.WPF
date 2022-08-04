﻿using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private Navigator _navigator;
        public UpdateCurrentViewModelCommand(Navigator navigator)
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
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Annual:
                        _navigator.CurrentViewModel = new AnnualReportsViewModel(_navigator);
                        break;
                    case ViewType.Oper:
                        _navigator.CurrentViewModel = new OperReportsViewModel(_navigator);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}