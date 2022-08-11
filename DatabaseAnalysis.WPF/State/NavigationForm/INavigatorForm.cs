using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.State.NavigationForm
{

    public interface INavigatorForm
    {
        BaseFormViewModel CurrentFormViewModel { get; set; }
        ICommand UpdateCurrentFormViewModelCommand { get; }
        string FormNumber { get; set; }
    }
}
