using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.FormViewModel
{
    public class Form13ViewModel : BaseFormViewModel
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

        public Form13ViewModel(NavigatorForm navigation, int id)
        {
            ICommand GetReport = new GetReportAsyncCommand(this);
            GetReport.Execute(id);
        }
    }
}
