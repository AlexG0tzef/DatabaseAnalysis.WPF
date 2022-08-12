using DatabaseAnalysis.WPF.Commands.SyncCommands;
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
        public override async void Execute(object? parameter)
        {
            IsExecute = true;
            await AsyncExecute(parameter);
            //var tFact = new Task(()=> AsyncExecute(parameter));
            //tFact.Start();
            IsExecute = false;
        }

        public abstract Task AsyncExecute(object? parameter);
    }
}
