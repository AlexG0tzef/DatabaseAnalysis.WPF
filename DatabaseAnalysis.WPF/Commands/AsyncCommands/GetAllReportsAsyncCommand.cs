using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class GetAllReportsAsyncCommand : AsyncBaseCommand
    {
        private readonly Navigator _navigator;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public GetAllReportsAsyncCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
            _navigator.PropertyChanged += NavigatorPropertyChanged;
        }

        private void NavigatorPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(Navigator.ReportsStorage))
            //{
            //    OnCanExecuteChanged();
            //}
        }

        public override async Task AsyncExecute(object? parameter)
        {
            var api = new EssanceMethods.APIFactory<FireBird.Reports>();
            List<FireBird.Reports>? repListQ = null;
            _mainWindowViewModel.ValueBar = 0;
            _mainWindowViewModel.ValueBarVisible = Visibility.Visible;
            if (parameter is OperReportsViewModel operReportsViewModel)
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка оперативной базы: ";
                StaticConfiguration.TpmDb = "OPER";
                if (ReportsStorge.Local_Reports.Reports_Collection10!.Count == 0)
                {
                    Task getOperTask;
                    try
                    {
                        getOperTask = Task.Factory.StartNew(async () => repListQ = await api.GetAllAsync());
                    }
                    catch (Exception)
                    {
                        #region MessageException
                        MessageBox.Show(
                            $"Не удалось загрузить оперативную базу, экспорт данных в Excel не выполнен.",
                            "Ошибка при получении данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        return;
                    }
                    while (!getOperTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100 + (int)_mainWindowViewModel.ValueBar);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repListQ!).Where(x => x.Master_DB.FormNum_DB.Equals("1.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }
                operReportsViewModel.Reports = new ObservableCollection<FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection10);
            }
            else if (parameter is AnnualReportsViewModel annualReportsViewModel)
            {
                _mainWindowViewModel.ValueBarStatus = "Идёт загрузка годовой базы: ";
                StaticConfiguration.TpmDb = "YEAR";
                if (ReportsStorge.Local_Reports.Reports_Collection20!.Count == 0)
                {
                    Task getAnnualTask;
                    try
                    {
                        getAnnualTask = Task.Factory.StartNew(async () => repListQ = await api.GetAllAsync());
                    }
                    catch (Exception)
                    {
                        #region MessageException
                        MessageBox.Show(
                            $"Не удалось загрузить годовую базу, экспорт данных в Excel не выполнен.",
                            "Ошибка при получении данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        return;
                    }
                    while (!getAnnualTask.IsCompleted)
                    {
                        if (_mainWindowViewModel.ValueBar < 99)
                        {
                            Thread.Sleep(100);
                            _mainWindowViewModel.ValueBar++;
                        }
                    }
                    var reps = new ObservableCollectionWithItemPropertyChanged<FireBird.Reports>(repListQ!).Where(x => x.Master_DB.FormNum_DB.Equals("2.0"));
                    ReportsStorge.Local_Reports.Reports_Collection.AddRange(reps);
                }
                annualReportsViewModel.Reports = new ObservableCollection<FireBird.Reports>(ReportsStorge.Local_Reports.Reports_Collection20);
            }
            _mainWindowViewModel.ValueBar = 100;
            _mainWindowViewModel.ValueBarStatus = "";
            _mainWindowViewModel.AmountReports = null;
        }
    }
}