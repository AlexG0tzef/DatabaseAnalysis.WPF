using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form15
    {
        public int Id { get; set; }
        public string? PassportNumberDb { get; set; }
        public string? TypeDb { get; set; }
        public string? RadionuclidsDb { get; set; }
        public string? FactoryNumberDb { get; set; }
        public int? QuantityDb { get; set; }
        public string? ActivityDb { get; set; }
        public string? CreationDateDb { get; set; }
        public string? StatusRaoDb { get; set; }
        public string? ProviderOrRecieverOkpoDb { get; set; }
        public string? TransporterOkpoDb { get; set; }
        public string? PackNameDb { get; set; }
        public string? PackTypeDb { get; set; }
        public string? PackNumberDb { get; set; }
        public string? StoragePlaceNameDb { get; set; }
        public string? StoragePlaceCodeDb { get; set; }
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
