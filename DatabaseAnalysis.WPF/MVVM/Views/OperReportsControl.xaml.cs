using DatabaseAnalysis.WPF.Utility;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace DatabaseAnalysis.WPF.MVVM.Views
{
    public partial class OperReportsControl : UserControl
    {
        public OperReportsControl()
        {
            InitializeComponent();
            OperFormsDataGrid.Sorting += new DataGridSortingEventHandler(CustomSorting);
        }

        private void CustomSorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            string columnToSort;
            if (column.SortMemberPath.Equals(columnToSort = "StartPeriod_DB")
                || column.SortMemberPath.Equals(columnToSort = "EndPeriod_DB")
                || column.SortMemberPath.Equals(columnToSort = "ExportDate_DB"))
            {
                // Prevent auto sorting
                e.Handled = true;

                column.SortDirection = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(OperFormsDataGrid.ItemsSource);
                lcv.CustomSort = new MyComparer(column.SortDirection.Value, columnToSort);
            }
        }
    }
}