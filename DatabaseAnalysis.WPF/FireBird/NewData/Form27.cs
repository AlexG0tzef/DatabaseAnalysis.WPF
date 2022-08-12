namespace Reports.Domain
{
    public partial class Form27
    {
        public int Id { get; set; }
        public string? ObservedSourceNumberDb { get; set; }
        public string? RadionuclidNameDb { get; set; }
        public string? AllowedWasteValueDb { get; set; }
        public string? FactedWasteValueDb { get; set; }
        public string? WasteOutbreakPreviousYearDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
