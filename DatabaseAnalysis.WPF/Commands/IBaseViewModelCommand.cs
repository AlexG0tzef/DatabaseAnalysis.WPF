﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public interface IBaseViewModelCommand
    {
        public ICommand OpenForm { get; set; }
        public ICommand AddUpdateReports { get; set;}
        public ICommand ExportExcelReport { get; set; }
        public ICommand ExportRaodbReport { get; set; }
        public ICommand ExportExcelReportAnalisys { get; set; }
        public ICommand ExportExcelReportPrint { get; set; }
        public ICommand ImportExcel { get; set; }
        public ICommand ImportRAODB { get; set; }
    }
}
