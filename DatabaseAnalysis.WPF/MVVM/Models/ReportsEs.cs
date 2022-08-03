using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
