using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Navigator Navigator { get; set; } = new Navigator();

        #region Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #region Search
        private TextBlock _selectedSearch;
        public TextBlock SelectedSearch
        {
            get => _selectedSearch;
            set
            {
                StringSearchEditable = true;
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

        private bool _stringSearchEditable = false;
        public bool StringSearchEditable
        {
            get => _stringSearchEditable;
            set
            {
                _stringSearchEditable = value;
                OnPropertyChanged(nameof(StringSearchEditable));
            }
        }

        #endregion

        private string _amountReports = "Общее кол-во отчетов: 0";
        public string AmountReports
        {
            get => _amountReports;
            set
            {
                if (Navigator.CurrentViewModel is OperReportsViewModel)
                    _amountReports = "Общее кол-во отчетов: " + ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('1')).Count();
                if (Navigator.CurrentViewModel is AnnualReportsViewModel)
                    _amountReports = "Общее кол-во отчетов: " + ReportsStorage.Local_Reports.Report_Collection.Where(x => x.FormNum_DB[0].Equals('2')).Count();
                OnPropertyChanged(nameof(AmountReports));
            }
        }

        private string _amountOrgs = "Общее кол-во орг-ций: 0";
        public string AmountOrgs
        {
            get => _amountOrgs;
            set
            {
                if (Navigator.CurrentViewModel is OperReportsViewModel)
                    _amountOrgs = "Общее кол-во орг-ций: " + ReportsStorage.Local_Reports.Reports_Collection10.Count();
                if (Navigator.CurrentViewModel is AnnualReportsViewModel)
                    _amountOrgs = "Общее кол-во орг-ций: " + ReportsStorage.Local_Reports.Reports_Collection20.Count();
                OnPropertyChanged(nameof(AmountOrgs));
            }
        }

        private string _mainWindowName = "Аналитика отчетности RAODB v.1.0.1 Оперативная отчетность";
        public string MainWindowName
        {
            get => _mainWindowName;
            set
            {
                _mainWindowName = value;
                OnPropertyChanged(nameof(MainWindowName));
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
        #endregion 
        #endregion

        #region Commands
        public ICommand SearchCommand { get; set; }
        public ICommand ExcelStatistic { get; set; }
        public ICommand ExportExcel { get; set; }
        public ICommand ExportExcelOrg { get; set; }
        public ICommand UpdateCurrentViewModel { get; set; }
        public ICommand CancelExport { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Navigator.CurrentViewModel = new OperReportsViewModel(Navigator, this);
            SearchCommand = new SearchReportsAsyncCommand(Navigator, this);
            ExcelStatistic = new ExcelStatisticAsyncCommand();
            ExportExcel = new ExportExcelAsyncCommand(Navigator, this);
            CancelExport = new CancelExportCommand();
            ExportExcelOrg = new ExportExcelOrgAsyncCommand(Navigator, this);

            OpenForm = new OpenFormCommand(Navigator);
            ExportExcelReportAnalisys = new ExportExcelReportAnalisysAsyncCommand();
            ExportExcelReportPrint = new ExportExcelReportPrintAsyncCommand();
            ExportRaodbReport = new ExportRaodbReportAsyncCommand();
            ImportExcel = new ImportExcelAsyncCommand();
            ImportRAODB = new ImportRaodbAsyncCommand();

            AddUpdateReports = new AddUpdateReportsCommand(Navigator, this);
            DeleteReports = new DeleteReportsCommand(Navigator, this);
        }
        #endregion
    }
}