using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class DeleteReportsCommand : BaseCommand
    {
        public DeleteReportsCommand(Navigator navigator, BaseViewModel baseViewModel)
        {

        }

        public override bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
