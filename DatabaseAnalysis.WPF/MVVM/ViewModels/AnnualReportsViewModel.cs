using DatabaseAnalysis.WPF.Commands.AsyncCommands;
using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.Resourses;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class AnnualReportsViewModel : BaseViewModel
    {
        #region Form
        private ObservableCollection<string> _formsCollection;
        public ObservableCollection<string> FormsCollection
        {
            get => _formsCollection;
            set
            {
                if (_formsCollection != value && value != null)
                {
                    _formsCollection = value;
                    OnPropertyChanged(nameof(FormsCollection));
                }
            }
        }
        private string _selectedForm;
        public string SelectedForm
        {
            get => _selectedForm;
            set
            {
                if (_selectedForm != value && value != null)
                {
                    _selectedForm = value;
                    OnPropertyChanged(nameof(SelectedForm));
                    SearchReportByFilter.Execute(value);
                }
            }
        }
        #endregion

        #region YearPeriod
        private ObservableCollection<string> _yearPeriod;
        public ObservableCollection<string> YearPeriod
        {
            get => _yearPeriod;
            set
            {
                if (_yearPeriod != value && value != null)
                {
                    _yearPeriod = value;
                    OnPropertyChanged(nameof(YearPeriod));
                }
            }
        }
        private string _selectedYearPeriod;
        public string SelectedYearPeriod
        {
            get => _selectedYearPeriod;
            set
            {
                if (value != _selectedYearPeriod && value != null)
                {
                    _selectedYearPeriod = value;
                    OnPropertyChanged(nameof(SelectedYearPeriod));
                    SearchReportByFilter.Execute(value);
                }
            }
        }
        #endregion

        #region ExportDate
        private ObservableCollection<string> _exportsDate;
        public ObservableCollection<string> ExportsDate
        {
            get => _exportsDate;
            set
            {
                if (value != _exportsDate && value != null)
                {
                    _exportsDate = value;
                    OnPropertyChanged(nameof(ExportsDate));
                }
            }
        }
        private string _selectedExportDate;
        public string SelectedExportDate
        {
            get => _selectedExportDate;
            set
            {
                if (_selectedExportDate != value && value != null)
                {
                    _selectedExportDate = value;
                    OnPropertyChanged(nameof(SelectedExportDate));
                    SearchReportByFilter.Execute(value);
                }
            }
        }
        #endregion

        #region CorretionNumber
        private ObservableCollection<string> _correctionNumber;
        public ObservableCollection<string> CorrectionNumber
        {
            get => _correctionNumber;
            set
            {
                if (_correctionNumber != value && value != null)
                {
                    _correctionNumber = value;
                    OnPropertyChanged(nameof(CorrectionNumber));
                }
            }
        }
        private string _selectedCorrectionNumber;
        public string SelectedCorrectionNumber
        {
            get => _selectedCorrectionNumber;
            set
            {
                if (value != _selectedCorrectionNumber && value != null)
                {
                    _selectedCorrectionNumber = value;
                    OnPropertyChanged(nameof(SelectedCorrectionNumber));
                    SearchReportByFilter.Execute(value);
                }
            }
        }
        #endregion

        #region Reports
        public ObservableCollection<FireBird.Reports>? ReportsStorage;

        private ObservableCollection<FireBird.Reports>? _reports;
        public ObservableCollection<FireBird.Reports>? Reports
        {
            get => _reports;
            set
            {
                if (_reports != value && value != null)
                {
                    _reports = value;
                    OnPropertyChanged(nameof(Reports));
                }
            }
        }

        private FireBird.Reports? _selectedReports;
        public FireBird.Reports? SelectedReports
        {
            get => _selectedReports;
            set
            {
                if (_selectedReports != value && value != null)
                {
                    _selectedReports = value;
                    StaticResourses.SelectedReports = value;
                    SelectedForm = "";
                    SelectedYearPeriod = "";
                    SelectedExportDate = "";
                    SelectedCorrectionNumber = "";
                    OnPropertyChanged(nameof(SelectedReports));
                    ReportCollection = new ObservableCollection<FireBird.Report>(value!.Report_Collection);
                    ReportStorage = new ObservableCollection<FireBird.Report>(SelectedReports!.Report_Collection);
                    
                }
            }
        }
        #endregion

        #region Report Collection
        public ObservableCollection<FireBird.Report>? ReportStorage;

        private ObservableCollection<FireBird.Report>? _reportCollection;
        public ObservableCollection<FireBird.Report>? ReportCollection
        {
            get => _reportCollection;
            set
            {
                if (value != null && SelectedReports != null)
                {
                    _reportCollection = value;
                    OnPropertyChanged(nameof(ReportCollection));
                    FormsCollection = new ObservableCollection<string>(new List<string>() { "" }.Union(_reportCollection.Select(x => x.FormNum_DB)));
                    YearPeriod = new ObservableCollection<string>(new List<string>() { "" }.Union(_reportCollection.Select(x => x.Year_DB)));
                    ExportsDate = new ObservableCollection<string>(new List<string>() { "" }.Union(_reportCollection.Select(x => x.ExportDate_DB)));
                    CorrectionNumber = new ObservableCollection<string>(new List<string>() { "" }.Union(_reportCollection.Select(x => x.CorrectionNumber_DB.ToString())));
                }
            }
        }
        private FireBird.Report? _selectedReport;
        public FireBird.Report? SelectedReport
        {
            get => _selectedReport;
            set
            {
                if (_selectedReport != value && value != null)
                {
                    _selectedReport = value;
                    OnPropertyChanged(nameof(SelectedReport));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand SearchReportByFilter { get; set; }
        #endregion

        #region Constructor
        public AnnualReportsViewModel(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            Task.Factory.StartNew(() => Init(navigator, mainWindowViewModel));
            SearchReportByFilter = new SearchReportAsyncCommand(this);
        }

        private async Task Init(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            StaticConfiguration.TpmDb = "YEAR";
            await Storages.ReportsStorage.GetAllReports(this, mainWindowViewModel);
        }
        #endregion
    }
}