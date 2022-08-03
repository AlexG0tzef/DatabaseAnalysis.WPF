using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form212
    {
        public int Id { get; set; }
        public short? OperationCodeDb { get; set; }
        public short? ObjectTypeCodeDb { get; set; }
        public string? RadionuclidsDb { get; set; }
        public string? ActivityDb { get; set; }
        public string? ProviderOrRecieverOkpoDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
