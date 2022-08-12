using System;

namespace DatabaseAnalysis.WPF.Interfaces
{
    public interface IBackgroundLoader
    {
        public void BackgroundWorker(Action action, Action onCompleted);
    }
}
