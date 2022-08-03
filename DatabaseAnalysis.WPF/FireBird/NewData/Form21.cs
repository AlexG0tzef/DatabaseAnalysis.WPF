using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class Form21
    {
        public int Id { get; set; }
        public bool SumDb { get; set; }
        public string? RefineMachineNameDb { get; set; }
        public bool RefineMachineNameHiddenGet { get; set; }
        public short? MachineCodeDb { get; set; }
        public bool MachineCodeHiddenGet { get; set; }
        public string? MachinePowerDb { get; set; }
        public bool MachinePowerHiddenGet { get; set; }
        public string? NumberOfHoursPerYearDb { get; set; }
        public bool NumberOfHoursPerYearHiddenG { get; set; }
        public string? CodeRaoinDb { get; set; }
        public bool CodeRaoinHiddenPriv { get; set; }
        public string? StatusRaoinDb { get; set; }
        public string? VolumeInDb { get; set; }
        public string? MassInDb { get; set; }
        public string? QuantityInDb { get; set; }
        public string? TritiumActivityInDb { get; set; }
        public string? BetaGammaActivityInDb { get; set; }
        public string? AlphaActivityInDb { get; set; }
        public string? TransuraniumActivityInDb { get; set; }
        public string? CodeRaooutDb { get; set; }
        public string? StatusRaooutDb { get; set; }
        public string? VolumeOutDb { get; set; }
        public string? MassOutDb { get; set; }
        public string? QuantityOziiioutDb { get; set; }
        public string? TritiumActivityOutDb { get; set; }
        public string? BetaGammaActivityOutDb { get; set; }
        public string? AlphaActivityOutDb { get; set; }
        public string? TransuraniumActivityOutDb { get; set; }
        public int? ReportId { get; set; }
        public string? FormNumDb { get; set; }
        public int NumberInOrderDb { get; set; }
        public int NumberOfFieldsDb { get; set; }
        public short CorrectionNumberDb { get; set; }
        public bool CodeRaooutHiddenPriv { get; set; }
        public bool StatusRaoinHiddenPriv { get; set; }
        public bool StatusRaooutHiddenPriv { get; set; }
        public bool MachineCodeHiddenSet { get; set; }
        public bool MachinePowerHiddenSet { get; set; }
        public bool NumberOfHoursPerYearHiddenS { get; set; }
        public bool RefineMachineNameHiddenSet { get; set; }
        public bool SumGroupDb { get; set; }
        public int BaseColor { get; set; }
        public string? NumberInOrderSumDb { get; set; }

        public virtual ReportCollectionDbSet? Report { get; set; }
    }
}
