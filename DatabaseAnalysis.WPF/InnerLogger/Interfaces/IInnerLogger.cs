namespace DatabaseAnalysis.WPF.InnerLogger
{
    public interface IInnerLogger
    {
        public void Info(string msg, ErrorCodeLogger code);
        public void Error(string msg, ErrorCodeLogger code);
        public void Debug(string msg, ErrorCodeLogger code);
        public void Warning(string msg, ErrorCodeLogger code);
    }
}
