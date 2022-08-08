using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.MVVM.ViewModels.Progress
{
    public class FormProgressBarViewModel : BaseViewModel
    {
        public Task MainTask { get; set; }

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

        public FormsViewModel FormViewModel { get; set; }

        public void OnFormPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(FormsViewModel.ValueBar))
            {
                ValueBar = FormViewModel.ValueBar;
            }
        }


        public FormProgressBarViewModel(string frm, int id)
        {
            FormViewModel = new FormsViewModel(frm, id);
            FormViewModel.PropertyChanged += OnFormPropertyChanged;
        }
    }
}
