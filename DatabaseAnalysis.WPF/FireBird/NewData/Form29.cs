using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form29
    {
        public int Id { get; set; }
        public string? WasteSourceNameDb { get; set; }
        public string? RadionuclidNameDb { get; set; }
        public string? AllowedActivityDb { get; set; }
        public string? FactedActivityDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
