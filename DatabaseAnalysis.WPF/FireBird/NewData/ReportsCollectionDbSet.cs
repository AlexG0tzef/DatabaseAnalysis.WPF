using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class ReportsCollectionDbSet
    {
        public ReportsCollectionDbSet()
        {
            ReportCollectionDbSets = new HashSet<ReportCollectionDbSet>();
        }

        public int Id { get; set; }
        public int? MasterDbid { get; set; }
        public int? DbobservableId { get; set; }

        public virtual DbobservableDbSet? Dbobservable { get; set; }
        public virtual ReportCollectionDbSet? MasterDb { get; set; }
        public virtual ICollection<ReportCollectionDbSet> ReportCollectionDbSets { get; set; }
    }
}
