using DatabaseAnalysis.WPF.Commands.SyncCommands;
using DatabaseAnalysis.WPF.Resourses;
using DatabaseAnalysis.WPF.State.NavigationForm;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels
{
    public class FormsViewModel : BaseViewModel
    {
        private int _valueBar = 1;
        public int ValueBar
        {
            get => _valueBar;
            set
            {
                _valueBar = value;
                OnPropertyChanged(nameof(ValueBar));
            }
        }

        private string _formName;
        public string FormName
        {
            get => _formName;
            set 
            { 
                _formName = value;
                OnPropertyChanged(nameof(FormName));
            }
        }

        public NavigatorForm Navigator { get; set; } = new NavigatorForm();
        public ICommand UpdateForm { get; set; }

        public FormsViewModel(string frm, int id)
        {
            Navigator.FormNumber = frm;
            UpdateForm = new UpdateCurrentFormViewModelCommand(Navigator, this);
            FormName = frm switch
            {
                "1.1" => $"Форма 1.1 Сведения о ЗРИ {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.2" => $"Форма 1.2 Сведения об изделия из обедненного урана {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.3" => $"Форма 1.3 Сведения об ОРИ в виде отдельных изделий {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.4" => $"Форма 1.4 Сведения об ОРИ, кроме отдельных изделий {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.5" => $"Форма 1.5 Сведения о РАО в виде отработавших ЗРИ {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.6" => $"Форма 1.6 Сведения о некондиционированных РАО {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.7" => $"Форма 1.7 Сведения о твердых кондиционированных РАО {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.8" => $"Форма 1.8 Сведения о жидких кондиционированных РАО {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "1.9" => $"Форма 1.9 Сведения о результатах инвенторизации РВ не в составе ЗРИ {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.1" => $"Форма 2.1 Сортировка, переработка и кондиционирование РАО на установках {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.2" => $"Форма 2.2 Наличие РАО  в пунктах хранения, местах сбора и/или временного хранения {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.3" => $"Форма 2.3 Разрешение на размещения РАО в пунктах хранения, местах сбора и/или временного хранения {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.4" => $"Форма 2.4 Постановка на учет и снятие с учета РВ, содержащихся в отработанном ядерном топливе {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.5" => $"Форма 2.5 Наличие РВ, содержащихся в отработанном ядерном топливе, в пунктах хранения {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.6" => $"Форма 2.6 Контроль загрязнения подземных вод РВ {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.7" => $"Форма 2.7 Поступление радионуклидов в атмосферный воздух {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.8" => $"Форма 2.8 Отведение сточных вод, содержащих радионуклиды {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.9" => $"Форма 2.9 Активность радионуклидов, отведенных со сточными водами {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.10" => $"Форма 2.10 Территории, загрязненные радионуклидами {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.11" => $"Форма 2.11 Радионуклидный состав загрязненных участков территорий {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                "2.12" => $"Форма 2.12 Суммарные сведения о РВ не в составе ЗРИ {StaticResourses.SelectedReports.Master_DB.RegNoRep.Value} {StaticResourses.SelectedReports.Master_DB.ShortJurLicoRep.Value} {StaticResourses.SelectedReports.Master_DB.OkpoRep.Value}",
                _ => ""
            };
        }
    }
}