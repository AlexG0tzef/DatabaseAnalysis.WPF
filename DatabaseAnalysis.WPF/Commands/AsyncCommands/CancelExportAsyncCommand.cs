using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class CancelExportAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private CancellationTokenSource _tokenSource;

        public CancelExportAsyncCommand(INavigator navigator, MainWindowViewModel mainWindowViewModel, CancellationTokenSource tokenSource)
        {
            _navigator = navigator;
            _tokenSource = tokenSource;
        }

        public override async Task AsyncExecute(object? parameter)
        {
            _tokenSource.Cancel();
        }
    }
}