using DatabaseAnalysis.WPF.MVVM.ViewModels;
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
