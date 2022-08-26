using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.Storages;

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