using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.MVVM.Views;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class AddUpdateReportsViewModel : BaseViewModel
    {
        #region Properties
        private FireBird.Reports _reports;
        public FireBird.Reports Reports
        {
            get => _reports;
            set
            {
                if (value != null && value != _reports)
                {
                    _reports = value;
                    OnPropertyChanged(nameof(Reports));
                }
            }
        }
        #endregion

        public ICommand ApplyCommand { get; set; }

        #region Constructure
        public AddUpdateReportsViewModel(FireBird.Reports reports, AddUpdateReportsView addUpdateReportsView, Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            Reports = reports;
            ApplyCommand = new ApplyAsyncCommand(addUpdateReportsView, navigator, mainWindowViewModel);
        }
        #endregion
    }
}
