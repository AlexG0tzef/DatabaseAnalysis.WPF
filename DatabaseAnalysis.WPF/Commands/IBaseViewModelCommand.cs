using System;
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
        public ICommand ExportExcelReport { get; set; }
    }
}
