namespace Reports.Domain
{
    public partial class Form10
    {
        public int Id { get; set; }
        public string? RegNoDb { get; set; }
        public string? OrganUpravDb { get; set; }
        public string? SubjectRfDb { get; set; }
        public string? JurLicoDb { get; set; }
        public string? ShortJurLicoDb { get; set; }
        public string? JurLicoAddressDb { get; set; }
        public string? JurLicoFactAddressDb { get; set; }
        public string? GradeFioDb { get; set; }
        public string? TelephoneDb { get; set; }
        public string? FaxDb { get; set; }
        public string? EmailDb { get; set; }
        public string? OkpoDb { get; set; }
        public string? OkvedDb { get; set; }
        public string? OkoguDb { get; set; }
        public string? OktmoDb { get; set; }
        public string? InnDb { get; set; }
        public string? KppDb { get; set; }
        public string? OkopfDb { get; set; }
        public string? OkfsDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
