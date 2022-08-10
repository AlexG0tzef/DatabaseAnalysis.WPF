using DatabaseAnalysis.WPF.State.Navigation;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelFormAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private readonly ICommand _getData;

        public ExportExcelFormAsyncCommand(INavigator navigator)
        {
            _navigator = navigator;
            _getData = new GetDataAsyncCommand();
        }

        public override async Task AsyncExecute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Excel | *.xlsx";
            bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (saveExcel)
            {
                string path = saveFileDialog.FileName;
                if (!path.EndsWith(".xlsx"))
                    path += ".xlsx";
                if (File.Exists(path))
                    File.Delete(path);


            }
            switch (parameter)
            {
                case "1.1":
                    _getData?.Execute(1);
                    break;
            }

        }
    }
}