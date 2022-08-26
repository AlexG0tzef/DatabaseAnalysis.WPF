using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExcelStatisticAsyncCommand : AsyncBaseCommand
    {
        public override async Task AsyncExecute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Excel | *.xlsx";
            bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow);
            await Task.Factory.StartNew(() => ExcelStatisticExport(parameter, saveFileDialog, saveExcel));
        }

        private void ExcelStatisticExport(object? parameter, SaveFileDialog saveFileDialog, bool saveExcel)
        {
            var find_rep = 0;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection)
            {
                foreach (Report rep in reps.Report_Collection)
                {
                    if (rep.FormNum_DB[0] == '1')
                    {
                        find_rep += 1;
                    }
                }
            }
            
            if (saveExcel)
            {
                var path = saveFileDialog.FileName;
                if (!path.Contains(".xlsx"))
                {
                    path += ".xlsx";
                }
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception)
                    {
                        #region MessageException
                        MessageBox.Show(
                            $"Не удалось сохранить файл по указанному пути. Файл с таким именем уже существует в этом расположении и используется другим процессом.",
                            "Ошибка при сохранении файла",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                        #endregion
                    }
                }
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                {
                    excelPackage.Workbook.Properties.Author = "RAO_APP";
                    excelPackage.Workbook.Properties.Title = "Report";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    if (ReportsStorge.Local_Reports.Reports_Collection.Count > 0)
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Статистика");
                        worksheet.Cells[1, 1].Value = "Рег.№";
                        worksheet.Cells[1, 2].Value = "ОКПО";
                        worksheet.Cells[1, 3].Value = "Сокращенное наименование";
                        worksheet.Cells[1, 4].Value = "Форма";
                        worksheet.Cells[1, 5].Value = "Начало предыдущего периода";
                        worksheet.Cells[1, 6].Value = "Конец предыдущего периода";
                        worksheet.Cells[1, 7].Value = "Начало периода";
                        worksheet.Cells[1, 8].Value = "Конец периода";
                        worksheet.Cells[1, 9].Value = "Зона разрыва";
                        worksheet.Cells[1, 10].Value = "Вид несоответствия";

                        var lst = new List<FireBird.Reports>();
                        var listSotrRep = new List<ReportForSort>();

                        foreach (FireBird.Reports item in ReportsStorge.Local_Reports.Reports_Collection10)
                        {
                            lst.Add(item);
                            foreach (Report rep in item.Report_Collection)
                            {
                                var start = StringReverse(rep.StartPeriod_DB);
                                var end = StringReverse(rep.EndPeriod_DB);

                                listSotrRep.Add(new ReportForSort()
                                {
                                    RegNoRep = item.Master_DB.RegNoRep.Value ?? "",
                                    OkpoRep = item.Master_DB.OkpoRep.Value ?? "",
                                    FormNum = rep.FormNum_DB,
                                    StartPeriod = start.Count() < 6 ? 0 : Convert.ToInt32(start),
                                    EndPeriod = end.Count() < 6 ? 0 : Convert.ToInt32(end),
                                    ShortYr = item.Master_DB.ShortJurLicoRep.Value
                                });
                            }
                        }
                        var newGen = listSotrRep.GroupBy(x => x.RegNoRep)
                            .ToDictionary(gr => gr.Key, gr => gr.ToList().GroupBy(x => x.FormNum).ToDictionary(gr => gr.Key, gr => gr.ToList().OrderBy(elem => elem.EndPeriod)));
                        var row = 2;
                        foreach (var grp in newGen)
                        {
                            foreach (var gr in grp.Value)
                            {
                                var prev_end = gr.Value.FirstOrDefault().EndPeriod;
                                var prev_start = gr.Value.FirstOrDefault().StartPeriod;
                                var newGr = gr.Value.Skip(1).ToList();
                                foreach (var g in newGr)
                                {
                                    if (g.StartPeriod != prev_end && g.StartPeriod != prev_start && g.EndPeriod != prev_end)
                                    {
                                        string prev_end_n = "";
                                        string prev_start_n = "";
                                        string st_per = "";
                                        string end_per = "";
                                        try
                                        {
                                            prev_end_n = prev_end.ToString().Length == 8 ?
                                                prev_end.ToString() : prev_end == 0 ? "нет даты конца периода" : prev_end.ToString().Insert(6, "0");
                                            prev_start_n = prev_start.ToString().Length == 8 ?
                                                prev_start.ToString() : prev_start == 0 ? "нет даты начала периода" : prev_start.ToString().Insert(6, "0");
                                            st_per = g.StartPeriod.ToString().Length == 8 ?
                                                g.StartPeriod.ToString() : g.StartPeriod.ToString().Insert(6, "0");
                                            end_per = g.EndPeriod.ToString().Length == 8 ?
                                                g.EndPeriod.ToString() : g.EndPeriod.ToString().Insert(6, "0");
                                        }
                                        catch (Exception)
                                        {
                                            #region MessageException
                                            MessageBox.Show(
                                                $"Не удалось преобразовать дату, файл не сохранён. Неверный формат данных",
                                                "Ошибка формата данных",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                                            #endregion
                                            return;
                                        }
                                        worksheet.Cells[row, 1].Value = g.RegNoRep;
                                        worksheet.Cells[row, 2].Value = g.OkpoRep;
                                        worksheet.Cells[row, 3].Value = g.ShortYr;
                                        worksheet.Cells[row, 4].Value = g.FormNum;
                                        worksheet.Cells[row, 5].Value = prev_start_n.Equals("нет даты начала периода") ?
                                            prev_start_n : $"{prev_start_n[6..8]}.{prev_start_n[4..6]}.{prev_start_n[0..4]}";
                                        worksheet.Cells[row, 6].Value = prev_end_n.Equals("нет даты конца периода") ?
                                            prev_end_n : $"{prev_end_n[6..8]}.{prev_end_n[4..6]}.{prev_end_n[0..4]}";
                                        worksheet.Cells[row, 7].Value = $"{st_per[6..8]}.{st_per[4..6]}.{st_per[0..4]}";
                                        worksheet.Cells[row, 8].Value = $"{end_per[6..8]}.{end_per[4..6]}.{end_per[0..4]}";
                                        worksheet.Cells[row, 9].Value = $"{worksheet.Cells[row, 6].Value}-{worksheet.Cells[row, 7].Value}";
                                        worksheet.Cells[row, 10].Value = g.StartPeriod < prev_end ? "пересечение" : "разрыв";
                                        row++;
                                    }
                                    prev_end = g.EndPeriod;
                                    prev_start = g.StartPeriod;
                                }
                            }
                        }
                        for (int i = 1; i <= 10; i++)
                        {
                            worksheet.Column(i).AutoFit();
                        }
                        try
                        {
                            excelPackage.Save();
                            #region MessageOpenExcel
                            MessageBoxResult result = MessageBox.Show(
                                $"Выгрузка \"Разрывов и пересечений\" сохранена по пути {path}. Вы хотите её открыть?",
                                "Выгрузка данных",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information);
                            if (result == MessageBoxResult.Yes)
                                Process.Start("explorer.exe", path);
                            #endregion
                        }
                        catch (Exception)
                        {
                            #region MessageException
                            MessageBox.Show(
                                $"Не удалось сохранить файл по указанному пути.",
                                "Ошибка при сохранении файла",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            #endregion
                        }
                    }
                }
            }
        }

        private string StringReverse(string _string)
        {
            var charArray = _string.Replace("_", "0").Replace("/", ".").Split(".");
            Array.Reverse(charArray);
            return string.Join("", charArray);
        }
    }
}