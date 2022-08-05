using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenFormCommand : BaseCommand
    {
        private readonly OperReportsViewModel _operReportsViewModel;
        private readonly Navigator _navigator;
        private readonly string _formNum;

        public OpenFormCommand(OperReportsViewModel operReportsViewModel, Navigator navigator)
        {
            _operReportsViewModel = operReportsViewModel;
            _navigator = navigator;

            _operReportsViewModel.PropertyChanged += OperReportsViewModelPropertyChanged;
        }

        private void OperReportsViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OperReportsViewModel.SelectedForm))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            var form = new FormView() { DataContext = new FormsViewModel(_operReportsViewModel.SelectedReport.FormNum_DB, Convert.ToInt32(parameter)) };
            form.ShowDialog();            
        }
    }
}
