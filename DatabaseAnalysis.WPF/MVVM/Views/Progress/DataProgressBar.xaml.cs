using DatabaseAnalysis.WPF.MVVM.ViewModels.Progress;
using System.Windows;

namespace DatabaseAnalysis.WPF.MVVM.Views.Progress
{
    /// <summary>
    /// Логика взаимодействия для DataProgressBar.xaml
    /// </summary>
    public partial class DataProgressBar : Window
    {
        public DataProgressBar(string attLoad)
        {
            InitializeComponent();
            DataContext = new DataProgressViewModel(attLoad, this);
            //this.Closed += DataProgressBar_Closed;
            this.ShowDialog();
        }

        //private void DataProgressBar_Closed(object? sender, EventArgs e)
        //{

        //}
    }
}
