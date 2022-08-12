using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class SearchReportsAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public SearchReportsAsyncCommand(INavigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
            _mainWindowViewModel.PropertyChanged += MainWindowViewModelPropertyChanged;
        }

        private void MainWindowViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowViewModel.StringSearch))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if (_navigator.CurrentViewModel is OperReportsViewModel)
            {
                OperReportsViewModel operReportsViewModel = (OperReportsViewModel)_navigator.CurrentViewModel;
                if (!string.IsNullOrEmpty(_mainWindowViewModel.StringSearch) && _mainWindowViewModel.SelectedSearch != null)
                {
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("Рег.№"))
                    {
                        operReportsViewModel.Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Master_DB.Rows10[0].RegNo_DB.Contains(_mainWindowViewModel.StringSearch)));
                    }
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("Сокр.наименование"))
                    {
                        operReportsViewModel.Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Master_DB.Rows10[0].ShortJurLico_DB.Contains(_mainWindowViewModel.StringSearch)));

                    }
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("ОКПО"))
                    {
                        operReportsViewModel.Reports = new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10.Where(x => x.Master_DB.Rows10[0].Okpo_DB.Contains(_mainWindowViewModel.StringSearch)));

                    }
                }
                else
                {
                    operReportsViewModel.Reports =
                            new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10);
                }
            }
            if (_navigator.CurrentViewModel is AnnualReportsViewModel)
            {
                AnnualReportsViewModel annualReportsViewModel = (AnnualReportsViewModel)_navigator.CurrentViewModel;
                if (!string.IsNullOrEmpty(_mainWindowViewModel.StringSearch) && _mainWindowViewModel.SelectedSearch != null)
                {
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("Рег.№"))
                    {
                        annualReportsViewModel.Reports =
                            new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Master_DB.Rows20[0].RegNo_DB.Contains(_mainWindowViewModel.StringSearch)));
                    }
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("Сокр.наименование"))
                    {
                        annualReportsViewModel.Reports =
                            new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Master_DB.Rows20[0].ShortJurLico_DB.Contains(_mainWindowViewModel.StringSearch)));
                    }
                    if (_mainWindowViewModel.SelectedSearch.Text.Equals("ОКПО"))
                    {
                        annualReportsViewModel.Reports =
                            new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20.Where(x => x.Master_DB.Rows20[0].Okpo_DB.Contains(_mainWindowViewModel.StringSearch)));
                    }
                }
                else
                {
                    annualReportsViewModel.Reports =
                            new ObservableCollection<DatabaseAnalysis.WPF.FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20);
                }
            }
        }


    }
}
