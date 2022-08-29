using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace DatabaseAnalysis.WPF.InnerLogger
{
    public interface ILogFactory
    {
        public void CreateFile(string path);
        event Action<(string msg, ErrorCodeLogger code)> NewLog;
        public bool IncludeOriginalDetails { get; set; }
        public void AddLogger(IInnerLogger innerLogger);
        public void RemoveLogger(IInnerLogger innerLogger);
        public void Info(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true);
        public void Debug(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true);
        public void Warning(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true);
        public void Error(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true);
    }
    public class BaseLoggerFactory : ILogFactory
    {
        protected static List<IInnerLogger> Loggers = new List<IInnerLogger>();
        protected object Lock = new();
        public bool IncludeOriginalDetails { get; set; }
        public BaseLoggerFactory(IInnerLogger[]? loggers = null)
        {
            if (loggers != null)
            {
                foreach (var log in loggers)
                {
                    AddLogger(log);
                }
            }
        }

        public event Action<(string msg, ErrorCodeLogger code)> NewLog = (details) => { };

        public void AddLogger(IInnerLogger innerLogger)
        {
            lock (Lock)
            {
                if (!Loggers.Contains(innerLogger))
                {
                    Loggers.Add(innerLogger);
                }
            }
        }
        public void RemoveLogger(IInnerLogger innerLogger)
        {
            lock (Lock)
            {
                if (Loggers.Contains(innerLogger))
                {
                    Loggers.Remove(innerLogger);
                }
            }
        }

        public void CreateFile(string path)
        {
            new BaseLoggerFactory(new[] { new BaseFileLogger(path) });
        }

        public void Debug(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true)
        {
            if (isIncludeOriginDetails)
                msg = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - " +
                    $"Line {lineNumber}] -" +
                    $"Message: {msg}";
            Loggers.ForEach(log => log.Debug(msg, code));
            NewLog.Invoke((msg, code));
        }
        public void Error(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true)
        {
            if (isIncludeOriginDetails)
                msg = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - " +
                    $"Line {lineNumber}] -" +
                    $"Message: {msg}";
            Loggers.ForEach(log => log.Error(msg, code));
            NewLog.Invoke((msg, code));
        }
        public void Info(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true)
        {
            if (isIncludeOriginDetails)
                msg = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - " +
                    $"Line {lineNumber}] -" +
                    $"Message: {msg}";
            Loggers.ForEach(log => log.Info(msg, code));
            NewLog.Invoke((msg, code));
        }
        public void Warning(string msg, ErrorCodeLogger code = ErrorCodeLogger.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool isIncludeOriginDetails = true)
        {
            if (isIncludeOriginDetails)
                msg = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - " +
                    $"Line {lineNumber}] -" +
                    $"Message: {msg}";
            Loggers.ForEach(log => log.Warning(msg, code));
            NewLog.Invoke((msg, code));
        }
    }
}
