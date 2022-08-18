using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class CancelExportCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            ReportsStorge._cancellationTokenSource.Cancel();
        }
    }
}