using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form19
    {
        public int Id { get; set; }
        public short? CodeTypeAccObjectDb { get; set; }
        public string? RadionuclidsDb { get; set; }
        public string? ActivityDb { get; set; }
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
