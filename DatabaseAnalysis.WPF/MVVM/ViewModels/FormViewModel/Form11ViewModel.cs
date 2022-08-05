using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.State.NavigationForm;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.FormViewModel
{
    public class Form11ViewModel : BaseFormViewModel
    {

        private FireBird.Reports _reports;
        public FireBird.Reports Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged(nameof(Reports));
            }
        }

        public Form11ViewModel(NavigatorForm navigation, int id)
        {
            ICommand GetReport = new GetReportAsyncCommand(this);
            GetReport.Execute(id);
        }
    }
}
