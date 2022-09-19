using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views;
using DatabaseAnalysis.WPF.State.Navigation;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class AddUpdateReportsCommand : BaseCommand
    {
        private readonly BaseViewModel _currentViewModel;
        private readonly Navigator _navigator;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public AddUpdateReportsCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _currentViewModel = navigator.CurrentViewModel;
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                var repsNew = (FireBird.Reports)parameter;
                _ = new AddUpdateReportsView(repsNew, _currentViewModel, _navigator, _mainWindowViewModel);
            }
            else
            {
                FireBird.Reports repsNew = new FireBird.Reports();
                if (_navigator.CurrentViewModel is OperReportsViewModel)
                {
                    repsNew.Master = new FireBird.Report() { FormNum_DB = "1.0" };
                    repsNew.Master.Rows10.Add(new FireBird.Form10() { NumberInOrder_DB = 1 });
                    repsNew.Master.Rows10.Add(new FireBird.Form10() { NumberInOrder_DB = 2 });
                }
                if (_navigator.CurrentViewModel is AnnualReportsViewModel)
                {
                    repsNew.Master = new FireBird.Report() { FormNum_DB = "2.0" };
                    repsNew.Master.Rows20.Add(new FireBird.Form20() { NumberInOrder_DB = 1 });
                    repsNew.Master.Rows20.Add(new FireBird.Form20() { NumberInOrder_DB = 2 });
                }
                _ = new AddUpdateReportsView(repsNew, _currentViewModel, _navigator, _mainWindowViewModel);
            }
        }
    }
}