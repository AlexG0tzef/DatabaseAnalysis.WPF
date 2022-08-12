using System;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Interfaces
{
    public class BackgroundLoader : IBackgroundLoader
    {
        public async void BackgroundWorker(Action action, Action onCompleted)
        {
            var task = Task.Factory.StartNew(action);
            await task;
            onCompleted();
        }
    }
}
