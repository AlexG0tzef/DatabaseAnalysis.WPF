using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.Storages
{
    public class ReportsStorge
    {
        #region Reports Storage
        private static List<FireBird.Reports>? _reportsStorage = new();
        public static List<FireBird.Reports>? ReportsStorage
        {
            get => _reportsStorage;
            set
            {
                if (value != null && value != _reportsStorage)
                {
                    _reportsStorage = value;
                }
            }
        }
        #endregion
    }
}
