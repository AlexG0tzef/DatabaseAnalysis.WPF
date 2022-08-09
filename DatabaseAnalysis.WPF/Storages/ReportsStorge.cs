using DatabaseAnalysis.WPF.FireBird;
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
        #region Local_Reports
        private static DBObservable _local_Reports = new();
        public static DBObservable Local_Reports
        {
            get => _local_Reports;
            set
            {
                if (_local_Reports != value && value != null)
                {
                    _local_Reports = value;
                }
            }
        }
        #endregion

        #region Reports Storage
        private static LinkedList<FireBird.Reports>? _reportsStorage = new();
        public static LinkedList<FireBird.Reports>? ReportsStorage
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
