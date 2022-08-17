using DatabaseAnalysis.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.Progress
{
    public abstract class BaseProgressBarViewModel : BaseViewModel
    {
        private int _valueBar;
        public int ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        protected IBackgroundLoader _backgroundWorker;
    }
}