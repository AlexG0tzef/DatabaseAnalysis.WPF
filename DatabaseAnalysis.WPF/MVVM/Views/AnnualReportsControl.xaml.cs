using DatabaseAnalysis.WPF.Utility;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using static DatabaseAnalysis.WPF.Utility.MyComparer;

namespace DatabaseAnalysis.WPF.MVVM.Views
{
    public partial class AnnualReportsControl : UserControl
    {
        public AnnualReportsControl()
        {
            InitializeComponent();
        }

        private void CustomSorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;

            if (column.SortMemberPath.Equals("ExportDate_DB"))
            {
                // Prevent auto sorting
                e.Handled = true;

                column.SortDirection = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(AnnualFormsDataGrid.ItemsSource);
                lcv.CustomSort = new MyComparer(column.SortDirection.Value, TypeToCompare.Date);
            }
        }
    }
}