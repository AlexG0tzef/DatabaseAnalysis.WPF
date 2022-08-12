using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Interfaces
{
    public interface IBackgroundLoader
    {
        public void BackgroundWorker(Action action, Action onCompleted);
    }
}
