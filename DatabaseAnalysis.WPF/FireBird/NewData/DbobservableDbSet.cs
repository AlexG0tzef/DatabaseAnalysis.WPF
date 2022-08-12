using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class DbobservableDbSet
    {
        public DbobservableDbSet()
        {
            ReportsCollectionDbSets = new HashSet<ReportsCollectionDbSet>();
        }

        public int Id { get; set; }

        public virtual ICollection<ReportsCollectionDbSet> ReportsCollectionDbSets { get; set; }
    }
}
