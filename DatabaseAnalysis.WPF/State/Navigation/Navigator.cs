using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.State.Navigation
{
    public class Navigator : INotifyPropertyChanged, INavigator
    {

        #region INotifyPropertyChanged
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region CurrentViewModel
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {

                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));

            }
        }
        #endregion


        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);

        #region Reports Storage
        private List<FireBird.Reports>? _reportsStorage = new();
        public List<FireBird.Reports>? ReportsStorage 
        {
            get => _reportsStorage;
            set
            {
                if (value != null && value != _reportsStorage)
                {
                    _reportsStorage = value;
                    OnPropertyChanged(nameof(ReportsStorage));
                }
            }
        }
        #endregion

        #region Report Storage
        private List<Report> _reportStorage;
        public List<Report>? ReportStorage 
        {
            get => _reportStorage;
            set
            {
                if (value != null && value != _reportStorage)
                {
                    _reportStorage = value;
                    OnPropertyChanged(nameof(ReportStorage));
                }
            }
        }
        #endregion
    }
}
