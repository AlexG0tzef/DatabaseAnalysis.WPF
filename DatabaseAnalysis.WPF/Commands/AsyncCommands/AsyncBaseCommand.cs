using DatabaseAnalysis.WPF.Commands.SyncCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public abstract class AsyncBaseCommand : BaseCommand
    {
        private bool _isExecute = false;
        public bool IsExecute
        {
            get => _isExecute;
            set
            {
                _isExecute = value;
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecute;
        }
        public override  void Execute(object? parameter)
        {
            IsExecute = true;

            var tFact = Task.Factory.StartNew(() => AsyncExecute(parameter));
            //tFact.Wait();

            IsExecute = false;
        }

        public abstract Task AsyncExecute(object? parameter);
    }
}
