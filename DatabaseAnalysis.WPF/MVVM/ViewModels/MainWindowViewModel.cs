using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Navigator Navigator { get; set; } = new Navigator();
        private CancellationTokenSource _cancelTokenSource = new();

        private TextBlock _selectedSearch;
        public TextBlock SelectedSearch
        {
            get => _selectedSearch;
            set
            {
                if (_selectedSearch != value)
                {
                    _selectedSearch = value;
                    OnPropertyChanged(nameof(SelectedSearch));
                    StringSearch = "";
                }
            }
        }

        private string _stringSearch;
        public string StringSearch
        {
            get => _stringSearch;
            set
            {
                _stringSearch = value;
                OnPropertyChanged(nameof(StringSearch));
                SearchCommand?.Execute(null);
            }
        }

        private string _amountReports = "Общее кол-во отчетов: 0";
        public string AmountReports
        {
            get => _amountReports;
            set
            {
                if (Navigator.CurrentViewModel is OperReportsViewModel)
                    _amountReports = "Общее кол-во отчетов: " + ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('1')).Count();
                if (Navigator.CurrentViewModel is AnnualReportsViewModel)
                    _amountReports = "Общее кол-во отчетов: " + ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2')).Count();
                OnPropertyChanged(nameof(AmountReports));
            }
        }

        #region ValueBar
        private double _valueBar = 100;
        public double ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                if (value < 100)
                    ValueBarVisible = Visibility.Visible;
                else
                    ValueBarVisible = Visibility.Hidden;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        private Visibility _valueBarVisible = Visibility.Hidden;
        public Visibility ValueBarVisible
        {
            get => _valueBarVisible;
            set
            {
                _valueBarVisible = value;
                OnPropertyChanged(nameof(ValueBarVisible));
            }
        }

        private string _valueBarStatus = "";
        public string ValueBarStatus
        {
            get => _valueBarStatus;
            set
            {
                _valueBarStatus = value;
                OnPropertyChanged(nameof(ValueBarStatus));
            }
        }
        #endregion

        private Visibility _closeButtonVisible = Visibility.Hidden;
        public Visibility CloseButtonVisible
        {
            get => _closeButtonVisible;
            set
            {
                _closeButtonVisible = value;
                OnPropertyChanged(nameof(CloseButtonVisible));
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ExportExcel { get; set; }
        public ICommand ExportExcelOrganizaations { get; set; }
        public ICommand UpdateCurrentViewModel { get; set; }
        public ICommand CancelExport { get; set; }


        public MainWindowViewModel()
        {
            Navigator.CurrentViewModel = new OperReportsViewModel(Navigator, this);
            SearchCommand = new SearchReportsAsyncCommand(Navigator, this);
            ExportExcel = new ExportExcelAsyncCommand(Navigator, this);
            CancelExport = new CancelExportCommand();
            ExportExcelOrganizaations = new ExportExcelOrgAsyncCommnad(this);
        }
    }
}