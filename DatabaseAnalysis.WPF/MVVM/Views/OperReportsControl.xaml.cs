using DatabaseAnalysis.WPF.Utility;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using static DatabaseAnalysis.WPF.Utility.MyComparer;

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

            if (column.SortMemberPath.Equals("StartPeriod_DB")
                || column.SortMemberPath.Equals("EndPeriod_DB")
                || column.SortMemberPath.Equals("ExportDate_DB"))
            {
                // Prevent auto sorting
                e.Handled = true;

                column.SortDirection = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(OperFormsDataGrid.ItemsSource);
                lcv.CustomSort = new MyComparer(column.SortDirection.Value, TypeToCompare.Date);
            }
        }
    }
}