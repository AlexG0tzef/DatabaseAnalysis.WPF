namespace Reports.Domain
{
    public partial class Form24
    {
        public int Id { get; set; }
        public string? CodeOyatDb { get; set; }
        public string? FcpNumberDb { get; set; }
        public string? MassCreatedDb { get; set; }
        public string? QuantityCreatedDb { get; set; }
        public string? MassFromAnothersDb { get; set; }
        public string? QuantityFromAnothersDb { get; set; }
        public string? MassFromAnothersImportedDb { get; set; }
        public string? QuantityFromAnothersImportedDb { get; set; }
        public string? MassAnotherReasonsDb { get; set; }
        public string? QuantityAnotherReasonsDb { get; set; }
        public string? MassTransferredToAnotherDb { get; set; }
        public string? QuantityTransferredToAnotherDb { get; set; }
        public string? MassRefinedDb { get; set; }
        public string? QuantityRefinedDb { get; set; }
        public string? MassRemovedFromAccountDb { get; set; }
        public string? QuantityRemovedFromAccountDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
