using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form18
    {
        public int Id { get; set; }
        public bool SumDb { get; set; }
        public string? IndividualNumberZhroDb { get; set; }
        public bool IndividualNumberZhroHiddenPr { get; set; }
        public string? PassportNumberDb { get; set; }
        public bool PassportNumberHiddenPriv { get; set; }
        public string? Volume6Db { get; set; }
        public bool Volume6HiddenPriv { get; set; }
        public string? Mass7Db { get; set; }
        public bool Mass7HiddenPriv { get; set; }
        public string? SaltConcentrationDb { get; set; }
        public bool SaltConcentrationHiddenPriv { get; set; }
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
        public string? CodeRaoDb { get; set; }
        public string? StatusRaoDb { get; set; }
        public string? Volume20Db { get; set; }
        public string? Mass21Db { get; set; }
        public string? TritiumActivityDb { get; set; }
        public string? BetaGammaActivityDb { get; set; }
        public string? AlphaActivityDb { get; set; }
        public string? TransuraniumActivityDb { get; set; }
        public string? RefineOrSortRaocodeDb { get; set; }
        public string? SubsidyDb { get; set; }
        public string? FcpNumberDb { get; set; }
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
