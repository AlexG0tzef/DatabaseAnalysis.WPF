using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form211
    {
        public int Id { get; set; }
        public string? PlotNameDb { get; set; }
        public string? PlotKadastrNumberDb { get; set; }
        public string? PlotCodeDb { get; set; }
        public string? InfectedAreaDb { get; set; }
        public string? RadionuclidsDb { get; set; }
        public string? SpecificActivityOfPlotDb { get; set; }
        public string? SpecificActivityOfLiquidPartDb { get; set; }
        public string? SpecificActivityOfDensePartDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
