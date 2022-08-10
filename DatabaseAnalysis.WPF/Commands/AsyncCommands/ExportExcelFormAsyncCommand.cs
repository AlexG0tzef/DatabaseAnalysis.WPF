using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
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
                using (ExcelPackage excelPackege = new(new FileInfo(path)))
                {
                    excelPackege.Workbook.Properties.Author = "RAO_APP";
                    excelPackege.Workbook.Properties.Title = $"ReportByForm_{parameter}";
                    excelPackege.Workbook.Properties.Created = DateTime.Now;

                    switch (parameter)
                    {
                        case "1.1":
                            OperReportsViewModel operReportsViewModel = (OperReportsViewModel)_navigator.CurrentViewModel;
                            ExcelWorksheet worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.1");
                            worksheet.Cells[1, 1].Value = "Рег. №";
                            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
                            worksheet.Cells[1, 3].Value = "ОКПО";
                            worksheet.Cells[1, 4].Value = "Форма";
                            worksheet.Cells[1, 5].Value = "Дата начала периода";
                            worksheet.Cells[1, 6].Value = "Дата конца периода";
                            worksheet.Cells[1, 7].Value = "Номер корректировки";
                            worksheet.Cells[1, 8].Value = "Количество строк";
                            worksheet.Cells[1, 9].Value = "№ п/п";
                            worksheet.Cells[1, 10].Value = "код";
                            worksheet.Cells[1, 11].Value = "дата";
                            worksheet.Cells[1, 12].Value = "номер паспорта\n(сертификата)";
                            worksheet.Cells[1, 13].Value = "тип";
                            worksheet.Cells[1, 14].Value = "радионуклиды";
                            worksheet.Cells[1, 15].Value = "номер";
                            worksheet.Cells[1, 16].Value = "количество, шт";
                            worksheet.Cells[1, 17].Value = "суммарная активность, Бк";
                            worksheet.Cells[1, 18].Value = "код ОКПО изготовителя";
                            worksheet.Cells[1, 19].Value = "дата выпуска";
                            worksheet.Cells[1, 20].Value = "категория";
                            worksheet.Cells[1, 21].Value = "НСС, мес";
                            worksheet.Cells[1, 22].Value = "код формы\nсобственности";

                            _getData?.Execute(1);
                            break;
                    }
                }
                
            }
        }
    }
}