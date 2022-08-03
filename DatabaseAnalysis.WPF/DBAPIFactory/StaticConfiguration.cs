using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.DBAPIFactory
{
    public class StaticConfiguration
    {
        public static string _dbPath { get; set; } = "";
        public static string DBPath
        {
            get => _dbPath;
            set
            {
                _dbPath = value;
            }
        }


        private static string _dbOperPath = "";
        public static string DBOperPath
        {
            get => _dbOperPath;
            set
            {
               _dbOperPath = value;
            }
        }

        private static string _tepmDb = "";
        public static string TpmDb
        {
            get => _tepmDb;
            set
            {
                _tepmDb = value;
            }
        }
    }
}
