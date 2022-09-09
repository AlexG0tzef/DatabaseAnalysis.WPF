using DatabaseAnalysis.WPF.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.SyncCommands
{
    public class OpenAddUpdateReportsCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                var repsNew = (FireBird.Reports)parameter;
                _ = new AddUpdateReportsView(repsNew);
            }
            else
            {
                var repsNew = new FireBird.Reports();
                _ = new AddUpdateReportsView(repsNew);
            }
        }
    }
}
