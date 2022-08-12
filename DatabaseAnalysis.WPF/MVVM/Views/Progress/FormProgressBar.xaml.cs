using DatabaseAnalysis.WPF.Interfaces;
using DatabaseAnalysis.WPF.MVVM.ViewModels.Progress;
using System;
using System.Windows;

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
