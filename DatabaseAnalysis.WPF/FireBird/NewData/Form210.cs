using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form210
    {
        public int Id { get; set; }
        public string? IndicatorNameDb { get; set; }
        public string? PlotNameDb { get; set; }
        public string? PlotKadastrNumberDb { get; set; }
        public string? PlotCodeDb { get; set; }
        public string? InfectedAreaDb { get; set; }
        public string? AvgGammaRaysDosePowerDb { get; set; }
        public string? MaxGammaRaysDosePowerDb { get; set; }
        public string? WasteDensityAlphaDb { get; set; }
        public string? WasteDensityBetaDb { get; set; }
        public string? FcpNumberDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
