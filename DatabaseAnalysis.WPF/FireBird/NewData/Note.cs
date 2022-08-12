namespace Reports.Domain
{
    public partial class Note
    {
        public int Id { get; set; }
        public string? RowNumberDb { get; set; }
        public string? GraphNumberDb { get; set; }
        public string? CommentDb { get; set; }
        public int? ReportId { get; set; }
        public int Order { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
