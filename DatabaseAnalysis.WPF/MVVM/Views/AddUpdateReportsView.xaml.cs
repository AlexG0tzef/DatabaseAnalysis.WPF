using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using System.Windows;

namespace DatabaseAnalysis.WPF.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateReportsView.xaml
    /// </summary>
    public partial class AddUpdateReportsView : Window
    {
        public AddUpdateReportsView(FireBird.Reports reports, BaseViewModel baseViewModel, Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = new AddUpdateReportsViewModel(reports, this, navigator, mainWindowViewModel);
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
