using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form17
    {
        public int Id { get; set; }
        public bool SumDb { get; set; }
        public string? PackNameDb { get; set; }
        public bool PackNameHiddenPriv { get; set; }
        public string? PackTypeDb { get; set; }
        public bool PackTypeHiddenPriv { get; set; }
        public string? PackNumberDb { get; set; }
        public bool PackNumberHiddenPriv { get; set; }
        public string? PackFactoryNumberDb { get; set; }
        public bool PackFactoryNumberHiddenPriv { get; set; }
        public string? FormingDateDb { get; set; }
        public bool FormingDateHiddenPriv { get; set; }
        public string? VolumeDb { get; set; }
        public bool VolumeHiddenPriv { get; set; }
        public string? MassDb { get; set; }
        public bool MassHiddenPriv { get; set; }
        public string? PassportNumberDb { get; set; }
        public bool PassportNumberHiddenPriv { get; set; }
        public string? RadionuclidsDb { get; set; }
        public string? SpecificActivityDb { get; set; }
        public string? ProviderOrRecieverOkpoDb { get; set; }
        public bool ProviderOrRecieverOkpoHidden { get; set; }
        public string? TransporterOkpoDb { get; set; }
        public bool TransporterOkpoHiddenPriv { get; set; }
        public string? StoragePlaceNameDb { get; set; }
        public bool StoragePlaceNameHiddenPriv { get; set; }
        public string? StoragePlaceCodeDb { get; set; }
        public bool StoragePlaceCodeHiddenPriv { get; set; }
        public string? SubsidyDb { get; set; }
        public string? FcpNumberDb { get; set; }
        public string? CodeRaoDb { get; set; }
        public string? StatusRaoDb { get; set; }
        public string? VolumeOutOfPackDb { get; set; }
        public string? MassOutOfPackDb { get; set; }
        public string? QuantityDb { get; set; }
        public string? TritiumActivityDb { get; set; }
        public string? BetaGammaActivityDb { get; set; }
        public string? AlphaActivityDb { get; set; }
        public string? TransuraniumActivityDb { get; set; }
        public string? RefineOrSortRaocodeDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public string? OperationCodeDb { get; set; }
        public bool OperationCodeHiddenPriv { get; set; }
        public string? OperationDateDb { get; set; }
        public bool OperationDateHiddenPriv { get; set; }
        public short? DocumentVidDb { get; set; }
        public bool DocumentVidHiddenPriv { get; set; }
        public string? DocumentNumberDb { get; set; }
        public bool DocumentNumberHiddenPriv { get; set; }
        public string? DocumentDateDb { get; set; }
        public bool DocumentDateHiddenPriv { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
