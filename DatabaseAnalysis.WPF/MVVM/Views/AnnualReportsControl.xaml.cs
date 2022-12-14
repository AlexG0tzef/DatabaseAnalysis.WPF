using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using DatabaseAnalysis.WPF.Resourses;

namespace DatabaseAnalysis.WPF.MVVM.Views
{
    public partial class AnnualReportsControl : UserControl
    {
        public AnnualReportsControl()
        {
            InitializeComponent();
            AnnualFormsDataGrid.Sorting += new DataGridSortingEventHandler(CustomSorting);
        }

        private void CustomSorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            string columnToSort;
            if (column.SortMemberPath.Equals(columnToSort = "FormNum_DB")
                || column.SortMemberPath.Equals(columnToSort = "ExportDate_DB"))
            {
                // Prevent auto sorting
                e.Handled = true;

                column.SortDirection = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(AnnualFormsDataGrid.ItemsSource);
                lcv.CustomSort = new CompareColumnDataGrid(column.SortDirection.Value, columnToSort);
            }
        }
    }
}