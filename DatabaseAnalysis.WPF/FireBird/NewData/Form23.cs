namespace Reports.Domain
{
    public partial class Form23
    {
        public int Id { get; set; }
        public string? StoragePlaceNameDb { get; set; }
        public string? StoragePlaceCodeDb { get; set; }
        public string? ProjectVolumeDb { get; set; }
        public string? CodeRaoDb { get; set; }
        public string? VolumeDb { get; set; }
        public string? MassDb { get; set; }
        public string? QuantityOziiiDb { get; set; }
        public string? SummaryActivityDb { get; set; }
        public string? DocumentNumberDb { get; set; }
        public string? DocumentDateDb { get; set; }
        public string? ExpirationDateDb { get; set; }
        public string? DocumentNameDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
