using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.InnerLogger
{
    public class ServiceExtension
    {
        public static ServiceProvider serviceProvider = new ServiceCollection()
            .AddTransient<IFileManager, BaseFileManager>()
            .AddTransient<ILogFactory, BaseLoggerFactory>()
            .BuildServiceProvider();
        public static IFileManager FileManager = serviceProvider.GetService<IFileManager>();
        public static ILogFactory LoggerManager = serviceProvider.GetService<ILogFactory>();
    }
}
