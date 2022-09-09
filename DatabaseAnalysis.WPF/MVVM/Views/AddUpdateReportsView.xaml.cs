using DatabaseAnalysis.WPF.MVVM.ViewModels;
using System.Windows;

namespace DatabaseAnalysis.WPF.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateReportsView.xaml
    /// </summary>
    public partial class AddUpdateReportsView : Window
    {
        public AddUpdateReportsView(FireBird.Reports reports)
        {
            InitializeComponent();
            DataContext = new AddUpdateReportsViewModel(reports);
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
