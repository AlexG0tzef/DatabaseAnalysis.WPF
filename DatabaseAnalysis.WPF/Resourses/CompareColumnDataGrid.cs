using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Collections;
using System.ComponentModel;

namespace DatabaseAnalysis.WPF.Resourses
{
    public class CompareColumnDataGrid : IComparer
    {
        private string? _columnToCompare;

        public ListSortDirection SortDirection { get; set; }

        public CompareColumnDataGrid() { }

        public CompareColumnDataGrid(ListSortDirection sortDirection, string columnToCompare)
        {
            SortDirection = sortDirection;
            _columnToCompare = columnToCompare;
        }

        public int Compare(object? obj1, object? obj2)
        {
            if (_columnToCompare == "StartPeriod_DB"
                || _columnToCompare == "EndPeriod_DB"
                || _columnToCompare == "ExportDate_DB")
                return CompareDate(obj1, obj2);

            if (_columnToCompare == "FormNum_DB")
                return CompareFormNum(obj1, obj2);

            return 0;
        }

        #region CompareDate
        private int CompareDate(object? obj1, object? obj2)
        {
            string date1 = _columnToCompare switch
            {
                "StartPeriod_DB" => (obj1 as FireBird.Report)?.StartPeriod_DB!,
                "EndPeriod_DB" => (obj1 as FireBird.Report)?.EndPeriod_DB!,
                "ExportDate_DB" => (obj1 as FireBird.Report)?.ExportDate_DB!,
                _ => ""
            };
            var date1Arr = date1.Replace("_", "0").Replace("/", ".").Split(".");
            short year1 = short.Parse(date1Arr[2]);
            byte month1 = byte.Parse(date1Arr[1]);
            byte day1 = byte.Parse(date1Arr[0]);

            string date2 = _columnToCompare switch
            {
                "StartPeriod_DB" => (obj2 as FireBird.Report)?.StartPeriod_DB!,
                "EndPeriod_DB" => (obj2 as FireBird.Report)?.EndPeriod_DB!,
                "ExportDate_DB" => (obj2 as FireBird.Report)?.ExportDate_DB!,
                _ => ""
            };
            var date2Arr = date2.Replace("_", "0").Replace("/", ".").Split(".");
            short year2 = short.Parse(date2Arr[2]);
            byte month2 = byte.Parse(date2Arr[1]);
            byte day2 = byte.Parse(date2Arr[0]);

            if (string.IsNullOrEmpty(date1) || string.IsNullOrEmpty(date2))
                return 0;

            if (year1 != year2)
                return SortDirection == ListSortDirection.Ascending ? year1.CompareTo(year2) : year2.CompareTo(year1);

            if (month1 != month2)
                return SortDirection == ListSortDirection.Ascending ? month1.CompareTo(month2) : month2.CompareTo(month1);

            if (day1 != day2)
                return SortDirection == ListSortDirection.Ascending ? day1.CompareTo(day2) : day2.CompareTo(day1);

            return SortDirection == ListSortDirection.Ascending ? date1.CompareTo(date2) : date2.CompareTo(date1);
        }
        #endregion

        #region CompareFormNum
        private int CompareFormNum(object? obj1, object? obj2)
        {
            string formNum1 = (obj1 as FireBird.Report)?.FormNum_DB!;
            var formNum1Arr = formNum1.Split('.');
            byte formType1 = byte.Parse(formNum1Arr[0]);
            byte fNum1 = byte.Parse(formNum1Arr[1]);

            string formNum2 = (obj2 as FireBird.Report)?.FormNum_DB!;
            var formNum2Arr = formNum2.Split('.');
            byte formType2 = byte.Parse(formNum2Arr[0]);
            byte fNum2 = byte.Parse(formNum2Arr[1]);

            if (string.IsNullOrEmpty(formNum1) || string.IsNullOrEmpty(formNum2))
                return 0;

            if (formType1 != formType2)
                return SortDirection == ListSortDirection.Ascending ? formType1.CompareTo(formType2) : formType2.CompareTo(formType1);

            if (fNum1 != fNum2)
                return SortDirection == ListSortDirection.Ascending ? fNum1.CompareTo(fNum2) : fNum2.CompareTo(fNum1);

            return SortDirection == ListSortDirection.Ascending ? formNum1.CompareTo(formNum2) : formNum2.CompareTo(formNum1); ;
        }
        #endregion
    }
}