namespace DatabaseAnalysis.WPF.FireBird
{
    public class DBModel : DataContext
    {
        public DBModel(string Path = "") : base(Path)
        {

        }
    }
}
