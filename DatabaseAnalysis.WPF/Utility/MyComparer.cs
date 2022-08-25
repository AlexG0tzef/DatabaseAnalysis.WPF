using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DatabaseAnalysis.WPF.Utility
{
    public class MyComparer : IComparer
    {
        private string? _columnToCompare;

        public ListSortDirection SortDirection { get; set; }

        public MyComparer() { }

        public MyComparer(ListSortDirection sortDirection, string columnToCompare)
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
            return 0;
        }

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
            string date2 = _columnToCompare switch
            {
                "StartPeriod_DB" => (obj2 as FireBird.Report)?.StartPeriod_DB!,
                "EndPeriod_DB" => (obj2 as FireBird.Report)?.EndPeriod_DB!,
                "ExportDate_DB" => (obj2 as FireBird.Report)?.ExportDate_DB!,
                _ => ""
            };
            var date2Arr = date2.Replace("_", "0").Replace("/", ".").Split(".");
            if (string.IsNullOrEmpty(date1) || string.IsNullOrEmpty(date2))
                return 0;

            short year1 = short.Parse(date1Arr[2]);
            byte month1 = byte.Parse(date1Arr[1]);
            byte day1 = byte.Parse(date1Arr[0]);
            
            short year2 = short.Parse(date2Arr[2]);
            byte month2 = byte.Parse(date2Arr[1]);
            byte day2 = byte.Parse(date2Arr[0]);

            if (year1 != year2)
                return SortDirection == ListSortDirection.Ascending ? year1.CompareTo(year2) : year2.CompareTo(year1);

            if (month1 != month2)
                return SortDirection == ListSortDirection.Ascending ? month1.CompareTo(month2) : month2.CompareTo(month1);

            if (day1 != day2)
                return SortDirection == ListSortDirection.Ascending ? day1.CompareTo(day2) : day2.CompareTo(day1);

            return SortDirection == ListSortDirection.Ascending ? date1.CompareTo(date2) : date2.CompareTo(date1);
        }
    }
}