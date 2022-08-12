using System;
using System.Collections.Generic;

namespace DatabaseAnalysis.WPF.DBAPIFactory
{
    public static partial class EssanceMethods
    {
        private static Dictionary<string, Func<IEssenceMethods>> MethodsList { get; set; } = new Dictionary<string, Func<IEssenceMethods>>()
        {
            {nameof(DatabaseAnalysis.WPF.FireBird.Report), ReportEssenceMethods.GetMethods},
            {nameof(DatabaseAnalysis.WPF.FireBird.Reports), ReportsEssenceMethods.GetMethods}
        };
    }
}
