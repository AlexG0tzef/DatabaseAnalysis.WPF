using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.ViewModels.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseAnalysis.WPF.MVVM.Views.Progress
{
    /// <summary>
    /// Логика взаимодействия для FormProgressBar.xaml
    /// </summary>
    public partial class FormProgressBar : Window
    {
        public FormProgressBar(string frm, int id)
        {
            InitializeComponent();
            DataContext = new FormProgressBarViewModel(frm, id, this, new BackgroundLoader());

            this.Closed += FormProgressBar_Closed;
            this.ShowDialog();
        }

        private void FormProgressBar_Closed(object? sender, EventArgs e)
        {
            var form = new FormView() { DataContext = ((FormProgressBarViewModel)this.DataContext).FormViewModel };
            form.ShowDialog();
        }
    }
}
