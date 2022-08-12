using Microsoft.EntityFrameworkCore;

namespace DatabaseAnalysis.WPF.MVVM.Models
{
    [Keyless]
    public class ReportsEs
    {
        public string ReNo { get; set; }
        public string Name { get; set; }
        public string OKPO { get; set; }
    }
}
