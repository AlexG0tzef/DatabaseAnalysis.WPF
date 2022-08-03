using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form28
    {
        public int Id { get; set; }
        public string? WasteSourceNameDb { get; set; }
        public string? WasteRecieverNameDb { get; set; }
        public string? RecieverTypeCodeDb { get; set; }
        public string? PoolDistrictNameDb { get; set; }
        public string? AllowedWasteRemovalVolumeDb { get; set; }
        public string? RemovedWasteVolumeDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
