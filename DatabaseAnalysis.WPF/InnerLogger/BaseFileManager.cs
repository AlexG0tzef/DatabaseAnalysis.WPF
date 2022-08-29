using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.InnerLogger
{
    public interface IFileManager
    {
        public Task WriteToFile(string msg, string path, bool append = true);
        public Task WriteToConsole(string msg);
        public string NormalizePath(string path);
        public string ResolvePath(string path);
    }

    public class BaseFileManager : IFileManager
    {
        public string NormalizePath(string path)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return path.Replace("/", "\\").Trim();
            else
                return path.Replace("\\", "/").Trim();
        }

        public string ResolvePath(string path)
        {
            return Path.GetFullPath(path);
        }

        public async Task WriteToConsole(string msg)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(msg);
            });
        }

        public async Task WriteToFile(string msg, string path, bool append = true)
        {
            path = NormalizePath(path);
            path = ResolvePath(path);
            await Awaiter.Async(path, async() => 
            {
                await Task.Run(() => 
                {
                    using(var fileStream = (TextWriter)new StreamWriter(File.Open(path, append?FileMode.Append:FileMode.Create)))
                    { 
                        fileStream.Write(msg);
                    }
                });
            });
        }
    }
}
