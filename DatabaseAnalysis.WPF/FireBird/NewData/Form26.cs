using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form26
    {
        public int Id { get; set; }
        public string? ObservedSourceNumberDb { get; set; }
        public string? ControlledAreaNameDb { get; set; }
        public string? SupposedWasteSourceDb { get; set; }
        public string? DistanceToWasteSourceDb { get; set; }
        public string? TestDepthDb { get; set; }
        public string? RadionuclidNameDb { get; set; }
        public string? AverageYearConcentrationDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
