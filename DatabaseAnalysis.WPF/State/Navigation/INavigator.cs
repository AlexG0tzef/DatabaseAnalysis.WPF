using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.State.Navigation
{
    public enum ViewType
    {
        Annual,
        Oper,
        Form
    }
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        List<DatabaseAnalysis.WPF.FireBird.Report>? ReportStorage { get; set; }
    }
}
