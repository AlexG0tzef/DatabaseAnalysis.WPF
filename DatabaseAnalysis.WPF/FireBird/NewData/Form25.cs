using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form25
    {
        public int Id { get; set; }
        public string? StoragePlaceNameDb { get; set; }
        public string? CodeOyatDb { get; set; }
        public string? StoragePlaceCodeDb { get; set; }
        public string? FcpNumberDb { get; set; }
        public string? FuelMassDb { get; set; }
        public string? CellMassDb { get; set; }
        public int? QuantityDb { get; set; }
        public string? BetaGammaActivityDb { get; set; }
        public string? AlphaActivityDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
