namespace Reports.Domain
{
    public partial class Form22
    {
        public int Id { get; set; }
        public bool PackNameHiddenGet { get; set; }
        public bool PackNameHiddenSet { get; set; }
        public bool PackTypeHiddenGet { get; set; }
        public bool PackTypeHiddenSet { get; set; }
        public bool StoragePlaceCodeHiddenGet { get; set; }
        public bool StoragePlaceCodeHiddenSet { get; set; }
        public bool StoragePlaceNameHiddenGet { get; set; }
        public bool StoragePlaceNameHiddenSet { get; set; }
        public bool MassInPackHiddenPriv { get; set; }
        public bool MassInPackHiddenPriv2 { get; set; }
        public bool VolumeInPackHiddenPriv { get; set; }
        public bool VolumeInPackHiddenPriv2 { get; set; }
        public bool FcpNumberHiddenPriv { get; set; }
        public bool SubsidyHiddenPriv { get; set; }
        public bool SumDb { get; set; }
        public string? StoragePlaceNameDb { get; set; }
        public string? StoragePlaceCodeDb { get; set; }
        public string? PackNameDb { get; set; }
        public string? PackTypeDb { get; set; }
        public string? PackQuantityDb { get; set; }
        public string? CodeRaoDb { get; set; }
        public bool CodeRaoHiddenPriv { get; set; }
        public string? StatusRaoDb { get; set; }
        public bool StatusRaoHiddenPriv { get; set; }
        public string? VolumeInPackDb { get; set; }
        public string? MassInPackDb { get; set; }
        public string? VolumeOutOfPackDb { get; set; }
        public string? MassOutOfPackDb { get; set; }
        public string? QuantityOziiiDb { get; set; }
        public string? TritiumActivityDb { get; set; }
        public string? BetaGammaActivityDb { get; set; }
        public string? AlphaActivityDb { get; set; }
        public string? TransuraniumActivityDb { get; set; }
        public string? MainRadionuclidsDb { get; set; }
        public bool MainRadionuclidsHiddenPriv { get; set; }
        public string? SubsidyDb { get; set; }
        public string? FcpNumberDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }
        public bool SumGroupDb { get; set; }
        public int BaseColor { get; set; }
        public string? NumberInOrderSumDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
