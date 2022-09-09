using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class AddUpdateReportsViewModel : BaseViewModel
    {
        #region Properties
        private FireBird.Reports _reports;
        public FireBird.Reports Reports
        {
            get => _reports;
            set
            {
                if (value != null && value != _reports)
                {
                    _reports = value;
                    OnPropertyChanged(nameof(Reports));
                }
            }
        }
        #endregion

        #region Constructure
        public AddUpdateReportsViewModel(FireBird.Reports reports)
        {
            Reports = reports;
        }
        #endregion
    }
}
