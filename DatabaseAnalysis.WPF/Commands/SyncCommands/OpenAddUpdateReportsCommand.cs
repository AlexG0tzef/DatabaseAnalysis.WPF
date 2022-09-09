using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.MVVM.Views;
using DatabaseAnalysis.WPF.State.Navigation;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenAddUpdateReportsCommand : BaseCommand
    {
        private readonly BaseViewModel currentViewModel;
        private readonly Navigator navigator;
        private readonly MainWindowViewModel mainWindowViewModel;

        public OpenAddUpdateReportsCommand(BaseViewModel _currentViewModel, Navigator _navigator, MainWindowViewModel _mainWindowViewModel)
        {
            currentViewModel = _currentViewModel;
            navigator = _navigator;
            mainWindowViewModel = _mainWindowViewModel;
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
                _ = new AddUpdateReportsView(repsNew, currentViewModel, navigator, mainWindowViewModel);
            }
            else
            {
                FireBird.Reports repsNew = new FireBird.Reports();
                if (navigator.CurrentViewModel is OperReportsViewModel)
                {
                    repsNew.Master = new FireBird.Report() { FormNum_DB = "1.0" };
                    repsNew.Master.Rows10.Add(new FireBird.Form10() { NumberInOrder_DB = 1 });
                    repsNew.Master.Rows10.Add(new FireBird.Form10() { NumberInOrder_DB = 2 });
                }
                if (navigator.CurrentViewModel is AnnualReportsViewModel)
                {
                    repsNew.Master = new FireBird.Report() { FormNum_DB = "2.0" };
                    repsNew.Master.Rows20.Add(new FireBird.Form20() { NumberInOrder_DB = 1 });
                    repsNew.Master.Rows20.Add(new FireBird.Form20() { NumberInOrder_DB = 2 });
                }

                _ = new AddUpdateReportsView(repsNew, currentViewModel, navigator, mainWindowViewModel);
            }
        }
    }
}
