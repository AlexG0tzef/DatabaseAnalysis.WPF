using DatabaseAnalysis.WPF.State.Navigation;

namespace DatabaseAnalysis.WPF.Resourses
{
    public static class StaticResourses
    {
        public enum DB_Type
        {
            AnnualDB,
            OperDB
        }

        public static FireBird.Reports? SelectedReports;
    }
}