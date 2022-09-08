using DatabaseAnalysis.WPF.State.NavigationForm;
using DatabaseAnalysis.WPF.Storages;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.FormViewModel
{
    public class Form23ViewModel : BaseFormViewModel
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

        public Form23ViewModel(NavigatorForm navigation, int id, FormsViewModel _formsViewModel)
        {
            _formsViewModel.ValueBar = 50;
            Task myTask = Task.Factory.StartNew(async () => await ReportsStorage.GetReport(id, this));
            myTask.Wait();
            _formsViewModel.ValueBar = 80;
        }
    }
}