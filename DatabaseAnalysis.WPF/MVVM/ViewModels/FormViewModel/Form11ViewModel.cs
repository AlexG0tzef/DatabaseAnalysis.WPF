using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.State.NavigationForm;
using DatabaseAnalysis.WPF.Storages;
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

        public Form11ViewModel(NavigatorForm navigation, int id, FormsViewModel _formsViewModel)
        {
            _formsViewModel.ValueBar = 50;
            //var myTask = Task.Factory.StartNew(async () => await ReportsStorge.GetReport(id, this));
            //await myTask;
            ICommand GetReport = new GetReportAsyncCommand(this);
            GetReport.Execute(id);
            _formsViewModel.ValueBar = 80;
        }
    }
}
