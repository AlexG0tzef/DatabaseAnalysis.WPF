using System.Collections;
using System.ComponentModel;

namespace DatabaseAnalysis.WPF.Utility
{
    public class MyComparer : IComparer
    {
        private TypeToCompare _typeToCompare;

        public enum TypeToCompare
        {
            Date
        }

        public ListSortDirection SortDirection { get; set; }

        public MyComparer() { }

        public MyComparer(ListSortDirection sortDirection, TypeToCompare typeToCompare)
        {
            SortDirection = sortDirection;
            _typeToCompare = typeToCompare;
        }

        public int Compare(object obj1, object obj2)
        {
            if (_typeToCompare == TypeToCompare.Date)
                return CompareDate(obj1, obj2);

            return 0;
        }

        private int CompareDate(object obj1, object obj2)
        {
            string date1 = (obj1 as FireBird.Report)?.StartPeriod_DB;
            short year1 = short.Parse(date1.Substring(date1.Length - 4));
            byte month1 = byte.Parse(date1.Substring(3, 2));
            byte day1 = byte.Parse(date1.Substring(0, 2));
            string date2 = (obj2 as FireBird.Report)?.StartPeriod_DB;
            short year2 = short.Parse(date2.Substring(date2.Length - 4));
            byte month2 = byte.Parse(date2.Substring(3, 2));
            byte day2 = byte.Parse(date2.Substring(0, 2));

            if (date1 == null || date2 == null)
                return 0;

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
