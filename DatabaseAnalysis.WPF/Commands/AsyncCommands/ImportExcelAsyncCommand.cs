using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ImportExcelAsyncCommand : AsyncBaseCommand
    {
        public override async Task AsyncExecute(object? parameter)
        {
            OpenFileDialog openFileDialog = new() { Filter = "Excel | *.xlsx" };
            bool openExcel = (bool)openFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (openExcel)
            {
                string path = openFileDialog.FileName;
                if (!path.EndsWith(".xlsx"))
                    path += ".xlsx";
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorksheet worksheet0 = excelPackage.Workbook.Worksheets[0];



                    bool val = false;
                    if (worksheet0.Name == "1.0" && Convert.ToString(worksheet0.Cells["A3"].Value) == "ГОСУДАРСТВЕННЫЙ УЧЕТ И КОНТРОЛЬ РАДИОАКТИВНЫХ ВЕЩЕСТВ И РАДИОАКТИВНЫХ ОТХОДОВ")
                    {
                        val = true;
                    }
                    if (worksheet0.Name == "2.0" && Convert.ToString(worksheet0.Cells["A4"].Value) == "ГОСУДАРСТВЕННЫЙ УЧЕТ И КОНТРОЛЬ РАДИОАКТИВНЫХ ВЕЩЕСТВ И РАДИОАКТИВНЫХ ОТХОДОВ")
                    {
                        val = true;
                    }

                    if (val)
                    {
                        var timeCreate = new List<string>() { excelPackage.File.CreationTime.Day.ToString(), excelPackage.File.CreationTime.Month.ToString(), excelPackage.File.CreationTime.Year.ToString() };
                        if (timeCreate[0].Length == 1)
                        {
                            timeCreate[0] = "0" + timeCreate[0];
                        }
                        if (timeCreate[1].Length == 1)
                        {
                            timeCreate[1] = "0" + timeCreate[1];
                        }

                        FireBird.Reports newRepsFromExcel = await CheckReps(worksheet0);

                        ExcelWorksheet worksheet1 = excelPackage.Workbook.Worksheets[1];
                        var param1 = worksheet1.Name;
                        var repFromEx = new Report()
                        {
                            FormNum_DB = param1
                        };
                        repFromEx.ExportDate_DB = $"{timeCreate[0]}.{timeCreate[1]}.{timeCreate[2]}";
                        if (param1.Split('.')[0] == "1")
                        {
                            repFromEx.StartPeriod_DB = Convert.ToString(worksheet1.Cells["G3"].Text).Replace("/", ".");
                            repFromEx.EndPeriod_DB = Convert.ToString(worksheet1.Cells["G4"].Text).Replace("/", ".");
                            repFromEx.CorrectionNumber_DB = Convert.ToByte(worksheet1.Cells["G5"].Value);
                        }
                        else
                        {
                            switch (param1)
                            {
                                case "2.6":
                                    {
                                        repFromEx.CorrectionNumber_DB = Convert.ToByte(worksheet1.Cells["G4"].Value);
                                        repFromEx.SourcesQuantity26_DB = Convert.ToInt32(worksheet1.Cells["G5"].Value);
                                        repFromEx.Year_DB = Convert.ToString(worksheet0.Cells["G10"].Value);
                                        break;
                                    }
                                case "2.7":
                                    {
                                        repFromEx.CorrectionNumber_DB = Convert.ToByte(worksheet1.Cells["G3"].Value);
                                        repFromEx.PermissionNumber27_DB = Convert.ToString(worksheet1.Cells["G4"].Value);
                                        repFromEx.ValidBegin27_DB = Convert.ToString(worksheet1.Cells["G5"].Value);
                                        repFromEx.ValidThru27_DB = Convert.ToString(worksheet1.Cells["J5"].Value);
                                        repFromEx.PermissionDocumentName27_DB = Convert.ToString(worksheet1.Cells["G6"].Value);
                                        repFromEx.Year_DB = Convert.ToString(worksheet0.Cells["G10"].Value);
                                        break;
                                    }
                                case "2.8":
                                    {
                                        repFromEx.CorrectionNumber_DB = Convert.ToByte(worksheet1.Cells["G3"].Value);
                                        repFromEx.PermissionNumber_28_DB = Convert.ToString(worksheet1.Cells["G4"].Value);
                                        repFromEx.ValidBegin_28_DB = Convert.ToString(worksheet1.Cells["K4"].Value);
                                        repFromEx.ValidThru_28_DB = Convert.ToString(worksheet1.Cells["N4"].Value);
                                        repFromEx.PermissionDocumentName_28_DB = Convert.ToString(worksheet1.Cells["G5"].Value);

                                        repFromEx.PermissionNumber1_28_DB = Convert.ToString(worksheet1.Cells["G6"].Value);
                                        repFromEx.ValidBegin1_28_DB = Convert.ToString(worksheet1.Cells["K6"].Value);
                                        repFromEx.ValidThru1_28_DB = Convert.ToString(worksheet1.Cells["N6"].Value);
                                        repFromEx.PermissionDocumentName1_28_DB = Convert.ToString(worksheet1.Cells["G7"].Value);

                                        repFromEx.ContractNumber_28_DB = Convert.ToString(worksheet1.Cells["G8"].Value);
                                        repFromEx.ValidBegin2_28_DB = Convert.ToString(worksheet1.Cells["K8"].Value);
                                        repFromEx.ValidThru2_28_DB = Convert.ToString(worksheet1.Cells["N8"].Value);
                                        repFromEx.OrganisationReciever_28_DB = Convert.ToString(worksheet1.Cells["G9"].Value);

                                        repFromEx.GradeExecutor_DB = Convert.ToString(worksheet1.Cells["D21"].Value);
                                        repFromEx.FIOexecutor_DB = Convert.ToString(worksheet1.Cells["F21"].Value);
                                        repFromEx.ExecPhone_DB = Convert.ToString(worksheet1.Cells["I21"].Value);
                                        repFromEx.ExecEmail_DB = Convert.ToString(worksheet1.Cells["K21"].Value);
                                        repFromEx.Year_DB = Convert.ToString(worksheet0.Cells["G10"].Value);
                                        break;
                                    }
                                default:
                                    {
                                        repFromEx.CorrectionNumber_DB = Convert.ToByte(worksheet1.Cells["G4"].Value);
                                        repFromEx.Year_DB = Convert.ToString(worksheet0.Cells["G10"].Text);
                                        break;
                                    }
                            }
                        }
                        repFromEx.GradeExecutor_DB = (string)worksheet1.Cells[$"D{worksheet1.Dimension.Rows - 1}"].Value;
                        repFromEx.FIOexecutor_DB = (string)worksheet1.Cells[$"F{worksheet1.Dimension.Rows - 1}"].Value;
                        repFromEx.ExecPhone_DB = (string)worksheet1.Cells[$"I{worksheet1.Dimension.Rows - 1}"].Value;
                        repFromEx.ExecEmail_DB = (string)worksheet1.Cells[$"K{worksheet1.Dimension.Rows - 1}"].Value;
                        int start = 11;
                        if (param1 == "2.8")
                        {
                            start = param1 switch
                            {
                                "2.8" => 14,
                                _ => 11
                            };
                        }
                        var end = $"A{start}";
                        while (worksheet1.Cells[end].Value != null && worksheet1.Cells[end].Value.ToString().ToLower() != "примечание:")
                        {
                            await GetDataFromRow(param1, worksheet1, start, repFromEx);
                            start++;
                            end = $"A{start}";
                        }

                        if (worksheet1.Cells[end].Value == null)
                            start += 3;
                        else if (worksheet1.Cells[end].Value.ToString().ToLower() == "примечание:")
                            start += 2;

                        while (worksheet1.Cells[$"A{start}"].Value != null || worksheet1.Cells[$"B{start}"].Value != null || worksheet1.Cells[$"C{start}"].Value != null)
                        {
                            Note newNote = new Note();

                            await GetDataFromRowNote(worksheet1, start, newNote);
                            repFromEx.Notes.Add(newNote);
                            start++;
                        }

                        if (newRepsFromExcel.Report_Collection.Count != 0)
                        {
                            if (worksheet0.Name == "1.0")
                            {
                                var not_in = false;
                                var skipLess = false;
                                var skipNew = false;
                                var _skipNew = false;
                                var skipInter = false;

                                foreach (Report rep in newRepsFromExcel.Report_Collection)
                                {

                                    DateTimeOffset st_elem = DateTimeOffset.Now;
                                    DateTimeOffset en_elem = DateTimeOffset.Now;
                                    try
                                    {
                                        st_elem = DateTime.Parse(rep.StartPeriod_DB) > DateTime.Parse(rep.EndPeriod_DB) ? DateTime.Parse(rep.EndPeriod_DB) : DateTime.Parse(rep.StartPeriod_DB);
                                        en_elem = DateTime.Parse(rep.StartPeriod_DB) < DateTime.Parse(rep.EndPeriod_DB) ? DateTime.Parse(rep.EndPeriod_DB) : DateTime.Parse(rep.StartPeriod_DB);
                                    }
                                    catch (Exception ex)
                                    { }

                                    DateTimeOffset st_it = DateTimeOffset.Now;
                                    DateTimeOffset en_it = DateTimeOffset.Now;
                                    try
                                    {
                                        st_it = DateTime.Parse(repFromEx.StartPeriod_DB) > DateTime.Parse(repFromEx.EndPeriod_DB) ? DateTime.Parse(repFromEx.EndPeriod_DB) : DateTime.Parse(repFromEx.StartPeriod_DB);
                                        en_it = DateTime.Parse(repFromEx.StartPeriod_DB) < DateTime.Parse(repFromEx.EndPeriod_DB) ? DateTime.Parse(repFromEx.EndPeriod_DB) : DateTime.Parse(repFromEx.StartPeriod_DB);
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                    if (st_elem == st_it && en_elem == en_it && repFromEx.FormNum_DB == rep.FormNum_DB)
                                    {
                                        not_in = true;
                                        if (repFromEx.CorrectionNumber_DB < rep.CorrectionNumber_DB)
                                        {
                                            if (!skipLess)
                                            {
                                                var str = " Вы пытаетесь загрузить форму с наименьщим номером корректировки - " +
                                                    repFromEx.CorrectionNumber_DB + ",\n" +
                                                    "при текущем значении корректировки - " +
                                                    rep.CorrectionNumber_DB + ".\n" +
                                                    "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                    "Начало отчетного периода - " + repFromEx.StartPeriod_DB + "\n" +
                                                    "Конец отчетного периода - " + repFromEx.EndPeriod_DB + "\n" +
                                                    "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                    "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                    "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                    "Количество строк - " + repFromEx.Rows.Count;
                                                string msg;
                                                MessageBoxResult result = MessageBox.Show(
                                                    msg = $"Отчет",
                                                    "Импорт данных",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Information);
                                                //var an = await ShowMessage.Handle(new List<string>() { str, "Отчет", "OK", "Пропустить для всех" });
                                                //if (an == "Пропустить для всех")
                                                //{
                                                //    skipLess = true;
                                                //}
                                            }
                                        }
                                        else if (repFromEx.CorrectionNumber_DB == rep.CorrectionNumber_DB)
                                        {
                                            var str = "Совпадение даты в " + rep.FormNum_DB + " " +
                                                rep.StartPeriod_DB + "-" +
                                                rep.EndPeriod_DB + " .\n" +
                                                "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                newRepsFromExcel.Master.RegNoRep.Value + " " +
                                                newRepsFromExcel.Master.ShortJurLicoRep.Value + " " +
                                                newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            string msg;
                                            MessageBoxResult result = MessageBox.Show(
                                                msg = $"Отчет",
                                                "Импорт данных",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                                            //var an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //        "Заменить",
                                            //        "Дополнить",
                                            //        "Сохранить оба",
                                            //        "Отменить"
                                            //    });
                                            //await ChechAanswer(an, newRepsFromExcel, rep, repFromEx);
                                        }
                                        else
                                        {
                                            var an = "Загрузить новую";
                                            if (!skipNew)
                                            {
                                                if (newRepsFromExcel.Report_Collection.Count() > 1)
                                                {
                                                    var str = "Загрузить новую форму? \n" +
                                                        "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                        "Начало отчетного периода - " + repFromEx.StartPeriod_DB + "\n" +
                                                        "Конец отчетного периода - " + repFromEx.EndPeriod_DB + "\n" +
                                                        "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                        "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                        "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                        "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                        "Форма с предыдущим номером корректировки №" +
                                                        rep.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                                        "Сделайте резервную копию." + "\n" +
                                                        "Количество строк - " + repFromEx.Rows.Count;
                                                    string msg;
                                                    MessageBoxResult result = MessageBox.Show(
                                                        msg = $"Отчет",
                                                        "Импорт данных",
                                                        MessageBoxButton.OK,
                                                        MessageBoxImage.Information);
                                                    //an = await ShowMessage.Handle(new List<string>() {str, "Отчет",
                                                    //    "Загрузить новую",
                                                    //    "Отмена",
                                                    //    "Загрузить для все"
                                                    //    });
                                                    if (an == "Загрузить для всех") skipNew = true;
                                                    an = "Загрузить новую";
                                                }
                                                else
                                                {
                                                    var str = "Загрузить новую форму? \n" +
                                                        "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                        "Начало отчетного периода - " + repFromEx.StartPeriod_DB + "\n" +
                                                        "Конец отчетного периода - " + repFromEx.EndPeriod_DB + "\n" +
                                                        "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                        "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                        "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                        "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                        "Форма с предыдущим номером корректировки №" +
                                                        rep.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                                        "Сделайте резервную копию." + "\n" +
                                                        "Количество строк - " + repFromEx.Rows.Count;
                                                    string msg;
                                                    MessageBoxResult result = MessageBox.Show(
                                                        msg = $"Отчет",
                                                        "Импорт данных",
                                                        MessageBoxButton.OK,
                                                        MessageBoxImage.Information);
                                                    //an = await ShowMessage.Handle(new List<string>() {str, "Отчет",
                                                    //            "Загрузить новую",
                                                    //            "Отмена"
                                                    //            });
                                                }
                                            }
                                            //await ChechAanswer(an, newRepsFromExcel, rep, repFromEx);
                                        }
                                    }
                                    if ((st_elem > st_it && st_elem < en_it || en_elem > st_it && en_elem < en_it) && repFromEx.FormNum.Value == rep.FormNum.Value)
                                    {
                                        not_in = true;
                                        var an = "Отменить";
                                        if (!skipInter)
                                        {
                                            var str = "Пересечение даты в " + rep.FormNum_DB + " " +
                                                rep.StartPeriod_DB + "-" +
                                                rep.EndPeriod_DB + " \n" +
                                                newRepsFromExcel.Master.RegNoRep.Value + " " +
                                                newRepsFromExcel.Master.ShortJurLicoRep.Value + " " +
                                                newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            //an = await ShowMessage.Handle(new List<string>(){str,"Отчет",
                                            //            "Сохранить оба",
                                            //            "Отменить"
                                            //            });
                                            skipInter = true;
                                        }
                                        //await ChechAanswer(an, newRepsFromExcel, null, repFromEx);
                                    }
                                }
                                if (!not_in)
                                {
                                    var an = "Да";
                                    if (!_skipNew)
                                    {
                                        if (newRepsFromExcel.Report_Collection.Count() > 1)
                                        {
                                            var str = "Загрузить новую форму?\n" +
                                                "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                "Начало отчетного периода - " + repFromEx.StartPeriod_DB + "\n" +
                                                "Конец отчетного периода - " + repFromEx.EndPeriod_DB + "\n" +
                                                "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            string msg;
                                            MessageBoxResult result = MessageBox.Show(
                                                msg = $"Отчет",
                                                "Импорт данных",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                                            //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //            "Да",
                                            //            "Нет",
                                            //            "Загрузить для всех"
                                            //            });
                                        }
                                        else
                                        {
                                            var str = "Загрузить новую форму?\n" +
                                                "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                "Начало отчетного периода - " + repFromEx.StartPeriod_DB + "\n" +
                                                "Конец отчетного периода - " + repFromEx.EndPeriod_DB + "\n" +
                                                "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            string msg;
                                            MessageBoxResult result = MessageBox.Show(
                                                msg = $"Отчет",
                                                "Импорт данных",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                                            //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //            "Да",
                                            //            "Нет"
                                            //            });
                                        }
                                    }
                                    //await ChechAanswer(an, newRepsFromExcel, null, repFromEx);
                                }
                            }
                            if (worksheet0.Name == "2.0")
                            {
                                var not_in = false;
                                var skipLess = false;
                                var skipNew = false;
                                var _skipNew = false;
                                var skipInter = false;
                                foreach (Report rep in newRepsFromExcel.Report_Collection)
                                {

                                    if (rep.Year_DB == repFromEx.Year_DB && repFromEx.FormNum_DB == rep.FormNum_DB)
                                    {
                                        not_in = true;
                                        if (repFromEx.CorrectionNumber_DB < rep.CorrectionNumber_DB)
                                        {
                                            if (!skipLess)
                                            {
                                                var str = " Вы пытаетесь загрузить форму с наименьщим номером корректировки - " +
                                                    repFromEx.CorrectionNumber_DB + ",\n" +
                                                    "при текущем значении корректировки - " +
                                                    rep.CorrectionNumber_DB + ".\n" +
                                                    "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                    "Отчетный год - " + repFromEx.Year_DB + "\n" +
                                                    "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                    "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                    "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                    "Количество строк - " + repFromEx.Rows.Count;
                                                //var an = await ShowMessage.Handle(new List<string>() { str, "Отчет", "OK", "Пропустить для всех" });
                                                //if (an == "Пропустить для всех") skipLess = true;
                                            }
                                        }
                                        else if (repFromEx.CorrectionNumber_DB == rep.CorrectionNumber_DB)
                                        {
                                            var str = "Совпадение даты в " + rep.FormNum_DB + " " +
                                            rep.Year_DB + " .\n" +
                                            "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                            newRepsFromExcel.Master.RegNoRep.Value + " \n" +
                                            newRepsFromExcel.Master.ShortJurLicoRep.Value + " " +
                                            newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                            "Количество строк - " + repFromEx.Rows.Count;
                                            //var an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //            "Заменить",
                                            //            "Сохранить оба",
                                            //            "Отменить"
                                            //        });
                                            //await ChechAanswer(an, newRepsFromExcel, rep, repFromEx);
                                        }
                                        else
                                        {
                                            var an = "Загрузить новую";
                                            if (!skipNew)
                                            {
                                                if (newRepsFromExcel.Report_Collection.Count() > 1)
                                                {
                                                    var str = "Загрузить новую форму? \n" +
                                                    "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                    "Отчетный год - " + repFromEx.Year_DB + "\n" +
                                                    "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                    "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                    "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                    "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                    "Форма с предыдущим номером корректировки №" +
                                                    rep.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                                    "Сделайте резервную копию." + "\n" +
                                                    "Количество строк - " + repFromEx.Rows.Count;
                                                    //an = await ShowMessage.Handle(new List<string>() {
                                                    //                    str,
                                                    //                    "Отчет",
                                                    //                    "Загрузить новую",
                                                    //                    "Отмена",
                                                    //                    "Загрузить для всех"
                                                    //                    });
                                                    if (an == "Загрузить для всех") skipNew = true;
                                                    an = "Загрузить новую";
                                                }
                                                else
                                                {
                                                    var str = "Загрузить новую форму? \n" +
                                                        "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                        "Отчетный год - " + repFromEx.Year_DB + "\n" +
                                                        "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                        "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                        "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                        "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                        "Форма с предыдущим номером корректировки №" +
                                                        rep.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                                        "Сделайте резервную копию." + "\n" +
                                                        "Количество строк - " + repFromEx.Rows.Count;
                                                    //an = await ShowMessage.Handle(new List<string>() {
                                                    //                    str,
                                                    //                    "Отчет",
                                                    //                    "Загрузить новую",
                                                    //                    "Отмена"
                                                    //                    });
                                                }
                                            }
                                            //await ChechAanswer(an, newRepsFromExcel, rep, repFromEx);
                                        }
                                    }
                                }
                                if (!not_in)
                                {
                                    var an = "Да";
                                    if (!_skipNew)
                                    {
                                        if (newRepsFromExcel.Report_Collection.Count() > 1)
                                        {
                                            var str = "Загрузить новую форму? \n" +
                                                "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                "Отчетный год - " + repFromEx.Year_DB + "\n" +
                                                "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //                "Да",
                                            //                "Нет",
                                            //                "Загрузить для всех"
                                            //            });
                                            if (an == "Загрузить для всех") _skipNew = true;
                                            an = "Да";
                                        }
                                        else
                                        {
                                            var str = "Загрузить новую форму? \n" +
                                                "Номер формы - " + repFromEx.FormNum_DB + "\n" +
                                                "Отчетный год - " + repFromEx.Year_DB + "\n" +
                                                "Номер корректировки -" + repFromEx.CorrectionNumber_DB + "\n" +
                                                "Регистрационный номер - " + newRepsFromExcel.Master.RegNoRep.Value + "\n" +
                                                "Сокращенное наименование - " + newRepsFromExcel.Master.ShortJurLicoRep.Value + "\n" +
                                                "ОКПО - " + newRepsFromExcel.Master.OkpoRep.Value + "\n" +
                                                "Количество строк - " + repFromEx.Rows.Count;
                                            //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                            //                "Да",
                                            //                "Нет"
                                            //            });
                                        }
                                    }
                                    //await ChechAanswer(an, newRepsFromExcel, null, repFromEx);
                                    not_in = false;
                                }
                            }
                        }
                        else
                        {
                            newRepsFromExcel.Report_Collection.Add(repFromEx);
                        }
                        //var dbm = StaticConfiguration.DBModel;
                        //dbm.SaveChanges();
                    }
                    else
                    {
                        var str = "Не соответствует формат данных!";
                        //var an = await ShowMessage.Handle(new List<string>(){str, "Формат данных",
                        //                            "Ок"
                        //                        });
                    }

                }
            }
        }

        private async Task<FireBird.Reports> CheckReps(ExcelWorksheet worksheet0)
        {
            IEnumerable<FireBird.Reports>? reps = null;

            if (worksheet0.Name == "1.0")
            {
                reps = from FireBird.Reports t in ReportsStorage.Local_Reports.Reports_Collection10
                       where ((Convert.ToString(worksheet0.Cells["B36"].Value) == t.Master.Rows10[0].Okpo_DB &&
                       Convert.ToString(worksheet0.Cells["F6"].Value) == t.Master.Rows10[0].RegNo_DB))
                       select t;
            }
            if (worksheet0.Name == "2.0")
            {
                reps = from FireBird.Reports t in ReportsStorage.Local_Reports.Reports_Collection20
                       where ((Convert.ToString(worksheet0.Cells["B36"].Value) == t.Master.Rows20[0].Okpo_DB &&
                       Convert.ToString(worksheet0.Cells["F6"].Value) == t.Master.Rows20[0].RegNo_DB))
                       select t;
            }

            if (reps.Count() != 0)
            {
                return reps.FirstOrDefault();
            }
            else
            {
                var newRepsFromExcel = new FireBird.Reports();
                var param0 = worksheet0.Name;
                newRepsFromExcel.Master_DB = new FireBird.Report()
                {
                    FormNum_DB = param0
                };
                if (param0 == "1.0")
                {
                    var ty1 = new Form10();
                    ty1.NumberInOrder_DB = 1;
                    var ty2 = new Form10();
                    ty2.NumberInOrder_DB = 2;
                    newRepsFromExcel.Master_DB.Rows10.Add(ty1);
                    newRepsFromExcel.Master_DB.Rows10.Add(ty2);
                }
                if (param0 == "2.0")
                {
                    var ty1 = new Form20();
                    ty1.NumberInOrder_DB = 1;
                    var ty2 = new Form20();
                    ty2.NumberInOrder_DB = 2;
                    newRepsFromExcel.Master_DB.Rows20.Add(ty1);
                    newRepsFromExcel.Master_DB.Rows20.Add(ty2);
                }
                await GetDataTitleReps(newRepsFromExcel, worksheet0);
                ReportsStorage.Local_Reports.Reports_Collection.Add(newRepsFromExcel);
                return newRepsFromExcel;
            }
        }
        private async Task ChechAanswer(string an, Reports first, Report elem = null, Report it = null, bool doSomething = false)
        {
            if (an == "Сохранить оба" || an == "Да")
            {
                if (!doSomething)
                    first.Report_Collection.Add(it);
            }
            if (an == "Заменить" || an == "Загрузить новую")
            {
                first.Report_Collection.Remove(elem);
                first.Report_Collection.Add(it);
            }
            if (an == "Дополнить")
            {
                first.Report_Collection.Remove(elem);
                it.Rows.AddRange<IKey>(0, elem.Rows.GetEnumerable());
                it.Notes.AddRange<IKey>(0, elem.Notes);
                first.Report_Collection.Add(it);
            }
        }

        #region GetData
        private async Task GetDataTitleReps(Reports newRepsFromExcel, ExcelWorksheet worksheet0)
        {
            if (worksheet0.Name == "1.0")
            {
                newRepsFromExcel.Master_DB.Rows10[0].RegNo_DB = Convert.ToString(worksheet0.Cells["F6"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].OrganUprav_DB = Convert.ToString(worksheet0.Cells["F15"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].SubjectRF_DB = Convert.ToString(worksheet0.Cells["F16"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].JurLico_DB = Convert.ToString(worksheet0.Cells["F17"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].ShortJurLico_DB = worksheet0.Cells["F18"].Value == null ? "" : Convert.ToString(worksheet0.Cells["F18"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].JurLicoAddress_DB = Convert.ToString(worksheet0.Cells["F19"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].JurLicoFactAddress_DB = Convert.ToString(worksheet0.Cells["F20"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].GradeFIO_DB = Convert.ToString(worksheet0.Cells["F21"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Telephone_DB = Convert.ToString(worksheet0.Cells["F22"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Fax_DB = Convert.ToString(worksheet0.Cells["F23"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Email_DB = Convert.ToString(worksheet0.Cells["F24"].Value);

                newRepsFromExcel.Master_DB.Rows10[1].SubjectRF_DB = Convert.ToString(worksheet0.Cells["F25"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].JurLico_DB = Convert.ToString(worksheet0.Cells["F26"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].ShortJurLico_DB = worksheet0.Cells["F27"].Value == null ? "" : Convert.ToString(worksheet0.Cells["F27"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].JurLicoAddress_DB = Convert.ToString(worksheet0.Cells["F28"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].GradeFIO_DB = Convert.ToString(worksheet0.Cells["F29"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Telephone_DB = Convert.ToString(worksheet0.Cells["F30"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Fax_DB = Convert.ToString(worksheet0.Cells["F31"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Email_DB = Convert.ToString(worksheet0.Cells["F32"].Value);

                newRepsFromExcel.Master_DB.Rows10[0].Okpo_DB = worksheet0.Cells["B36"].Value == null ? "" : Convert.ToString(worksheet0.Cells["B36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Okved_DB = Convert.ToString(worksheet0.Cells["C36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Okogu_DB = Convert.ToString(worksheet0.Cells["D36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Oktmo_DB = Convert.ToString(worksheet0.Cells["E36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Inn_DB = Convert.ToString(worksheet0.Cells["F36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Kpp_DB = Convert.ToString(worksheet0.Cells["G36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Okopf_DB = Convert.ToString(worksheet0.Cells["H36"].Value);
                newRepsFromExcel.Master_DB.Rows10[0].Okfs_DB = Convert.ToString(worksheet0.Cells["I36"].Value);

                newRepsFromExcel.Master_DB.Rows10[1].Okpo_DB = worksheet0.Cells["B37"].Value == null ? "" : Convert.ToString(worksheet0.Cells["B37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Okved_DB = Convert.ToString(worksheet0.Cells["C37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Okogu_DB = Convert.ToString(worksheet0.Cells["D37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Oktmo_DB = Convert.ToString(worksheet0.Cells["E37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Inn_DB = Convert.ToString(worksheet0.Cells["F37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Kpp_DB = Convert.ToString(worksheet0.Cells["G37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Okopf_DB = Convert.ToString(worksheet0.Cells["H37"].Value);
                newRepsFromExcel.Master_DB.Rows10[1].Okfs_DB = Convert.ToString(worksheet0.Cells["I37"].Value);
            }
            if (worksheet0.Name == "2.0")
            {
                newRepsFromExcel.Master_DB.Rows20[0].RegNo.Value = Convert.ToString(worksheet0.Cells["F6"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].OrganUprav_DB = Convert.ToString(worksheet0.Cells["F15"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].SubjectRF_DB = Convert.ToString(worksheet0.Cells["F16"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].JurLico_DB = Convert.ToString(worksheet0.Cells["F17"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].ShortJurLico_DB = Convert.ToString(worksheet0.Cells["F18"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].JurLicoAddress_DB = Convert.ToString(worksheet0.Cells["F19"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].JurLicoFactAddress_DB = Convert.ToString(worksheet0.Cells["F20"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].GradeFIO_DB = Convert.ToString(worksheet0.Cells["F21"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Telephone_DB = Convert.ToString(worksheet0.Cells["F22"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Fax_DB = Convert.ToString(worksheet0.Cells["F23"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Email_DB = Convert.ToString(worksheet0.Cells["F24"].Value);

                newRepsFromExcel.Master_DB.Rows20[1].SubjectRF_DB = Convert.ToString(worksheet0.Cells["F25"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].JurLico_DB = Convert.ToString(worksheet0.Cells["F26"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].ShortJurLico_DB = Convert.ToString(worksheet0.Cells["F27"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].JurLicoAddress_DB = Convert.ToString(worksheet0.Cells["F28"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].GradeFIO_DB = Convert.ToString(worksheet0.Cells["F29"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Telephone_DB = Convert.ToString(worksheet0.Cells["F30"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Fax_DB = Convert.ToString(worksheet0.Cells["F31"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Email_DB = Convert.ToString(worksheet0.Cells["F32"].Value);

                newRepsFromExcel.Master_DB.Rows20[0].Okpo_DB = Convert.ToString(worksheet0.Cells["B36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Okved_DB = Convert.ToString(worksheet0.Cells["C36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Okogu_DB = Convert.ToString(worksheet0.Cells["D36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Oktmo_DB = Convert.ToString(worksheet0.Cells["E36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Inn_DB = Convert.ToString(worksheet0.Cells["F36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Kpp_DB = Convert.ToString(worksheet0.Cells["G36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Okopf_DB = Convert.ToString(worksheet0.Cells["H36"].Value);
                newRepsFromExcel.Master_DB.Rows20[0].Okfs_DB = Convert.ToString(worksheet0.Cells["I36"].Value);

                newRepsFromExcel.Master_DB.Rows20[1].Okpo_DB = Convert.ToString(worksheet0.Cells["B37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Okved_DB = Convert.ToString(worksheet0.Cells["C37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Okogu_DB = Convert.ToString(worksheet0.Cells["D37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Oktmo_DB = Convert.ToString(worksheet0.Cells["E37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Inn_DB = Convert.ToString(worksheet0.Cells["F37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Kpp_DB = Convert.ToString(worksheet0.Cells["G37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Okopf_DB = Convert.ToString(worksheet0.Cells["H37"].Value);
                newRepsFromExcel.Master_DB.Rows20[1].Okfs_DB = Convert.ToString(worksheet0.Cells["I37"].Value);
            }
        }
        private async Task GetDataFromRow(string param1, ExcelWorksheet worksheet1, int start, Report repFromEx)
        {
            if (param1 == "1.1")
            {
                Form11 form11 = new Form11();
                await GetDataFromRow11(worksheet1, start, form11);
                repFromEx.Rows11.Add(form11);
            }
            if (param1 == "1.2")
            {
                Form12 form12 = new Form12();
                await GetDataFromRow12(worksheet1, start, form12);
                repFromEx.Rows12.Add(form12);
            }
            if (param1 == "1.3")
            {
                Form13 form13 = new Form13();
                await GetDataFromRow13(worksheet1, start, form13);
                repFromEx.Rows13.Add(form13);
            }
            if (param1 == "1.4")
            {
                Form14 form14 = new Form14();
                await GetDataFromRow14(worksheet1, start, form14);
                repFromEx.Rows14.Add(form14);
            }
            if (param1 == "1.5")
            {
                Form15 form15 = new Form15();
                await GetDataFromRow15(worksheet1, start, form15);
                repFromEx.Rows15.Add(form15);
            }
            if (param1 == "1.6")
            {
                Form16 form16 = new Form16();
                await GetDataFromRow16(worksheet1, start, form16);
                repFromEx.Rows16.Add(form16);
            }
            if (param1 == "1.7")
            {
                Form17 form17 = new Form17();
                await GetDataFromRow17(worksheet1, start, form17);
                repFromEx.Rows17.Add(form17);
            }
            if (param1 == "1.8")
            {
                Form18 form18 = new Form18();
                await GetDataFromRow18(worksheet1, start, form18);
                repFromEx.Rows18.Add(form18);
            }
            if (param1 == "1.9")
            {
                Form19 form19 = new Form19();
                await GetDataFromRow19(worksheet1, start, form19);
                repFromEx.Rows19.Add(form19);
            }
            if (param1 == "2.1")
            {
                Form21 form21 = new Form21();
                await GetDataFromRow21(worksheet1, start, form21);
                repFromEx.Rows21.Add(form21);
            }
            if (param1 == "2.2")
            {
                Form22 form22 = new Form22();
                await GetDataFromRow22(worksheet1, start, form22);
                repFromEx.Rows22.Add(form22);
            }
            if (param1 == "2.3")
            {
                Form23 form23 = new Form23();
                await GetDataFromRow23(worksheet1, start, form23);
                repFromEx.Rows23.Add(form23);
            }
            if (param1 == "2.4")
            {
                Form24 form24 = new Form24();
                await GetDataFromRow24(worksheet1, start, form24);
                repFromEx.Rows24.Add(form24);
            }
            if (param1 == "2.5")
            {
                Form25 form25 = new Form25();
                await GetDataFromRow25(worksheet1, start, form25);
                repFromEx.Rows25.Add(form25);
            }
            if (param1 == "2.6")
            {
                Form26 form26 = new Form26();
                await GetDataFromRow26(worksheet1, start, form26);
                repFromEx.Rows26.Add(form26);
            }
            if (param1 == "2.7")
            {
                Form27 form27 = new Form27();
                await GetDataFromRow27(worksheet1, start, form27);
                repFromEx.Rows27.Add(form27);
            }
            if (param1 == "2.8")
            {
                Form28 form28 = new Form28();
                await GetDataFromRow28(worksheet1, start, form28);
                repFromEx.Rows28.Add(form28);
            }
            if (param1 == "2.9")
            {
                Form29 form29 = new Form29();
                await GetDataFromRow29(worksheet1, start, form29);
                repFromEx.Rows29.Add(form29);
            }
            if (param1 == "2.10")
            {
                Form210 form210 = new Form210();
                await GetDataFromRow210(worksheet1, start, form210);
                repFromEx.Rows210.Add(form210);
            }
            if (param1 == "2.11")
            {
                Form211 form211 = new Form211();
                await GetDataFromRow211(worksheet1, start, form211);
                repFromEx.Rows211.Add(form211);
            }
            if (param1 == "2.12")
            {
                Form212 form212 = new Form212();
                await GetDataFromRow212(worksheet1, start, form212);
                repFromEx.Rows212.Add(form212);
            }
        }

        #region GetDataFromRows
        private async Task GetDataFromRow11(ExcelWorksheet worksheet, int Row, Form11 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.Type_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.FactoryNumber_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.Quantity_DB = Convert.ToInt32(worksheet.Cells[Row, 8].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.CreatorOKPO_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.CreationDate_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.Category_DB = Convert.ToInt16(worksheet.Cells[Row, 12].Value);
            newForm.SignedServicePeriod_DB = Convert.ToInt32(worksheet.Cells[Row, 13].Value);
            newForm.PropertyCode_DB = Convert.ToByte(worksheet.Cells[Row, 14].Value);
            newForm.Owner_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 16].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);
        }
        private async Task GetDataFromRow12(ExcelWorksheet worksheet, int Row, Form12 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.NameIOU_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.FactoryNumber_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.Mass_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.CreatorOKPO_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.CreationDate_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.SignedServicePeriod_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.PropertyCode_DB = Convert.ToByte(worksheet.Cells[Row, 11].Value);
            newForm.Owner_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 13].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
        }
        private async Task GetDataFromRow13(ExcelWorksheet worksheet, int Row, Form13 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.Type_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.FactoryNumber_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.CreatorOKPO_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.CreationDate_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.AggregateState_DB = Convert.ToByte(worksheet.Cells[Row, 11].Value);
            newForm.PropertyCode_DB = Convert.ToByte(worksheet.Cells[Row, 12].Value);
            newForm.Owner_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 14].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
        }
        private async Task GetDataFromRow14(ExcelWorksheet worksheet, int Row, Form14 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.Name_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Sort_DB = Convert.ToByte(worksheet.Cells[Row, 6].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.ActivityMeasurementDate_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.Volume_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.Mass_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.AggregateState_DB = Convert.ToByte(worksheet.Cells[Row, 12].Value);
            newForm.PropertyCode_DB = Convert.ToByte(worksheet.Cells[Row, 13].Value);
            newForm.Owner_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 15].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
        }
        private async Task GetDataFromRow15(ExcelWorksheet worksheet, int Row, Form15 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.Type_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.FactoryNumber_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.Quantity_DB = Convert.ToInt32(worksheet.Cells[Row, 8].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.CreationDate_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 12].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 24].Value);
        }
        private async Task GetDataFromRow16(ExcelWorksheet worksheet, int Row, Form16 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Volume_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.Mass_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.QuantityOZIII_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.MainRadionuclids_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.TritiumActivity_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.TransuraniumActivity_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.ActivityMeasurementDate_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 15].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 24].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 25].Value);
            newForm.Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 26].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 27].Value);
        }
        private async Task GetDataFromRow17(ExcelWorksheet worksheet, int Row, Form17 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PackName_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.PackType_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.PackFactoryNumber_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.PackNumber_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.FormingDate_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.Volume_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.Mass_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.SpecificActivity_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 14].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.VolumeOutOfPack_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);
            newForm.MassOutOfPack_DB = Convert.ToString(worksheet.Cells[Row, 24].Value);
            newForm.Quantity_DB = Convert.ToString(worksheet.Cells[Row, 25].Value);
            newForm.TritiumActivity_DB = Convert.ToString(worksheet.Cells[Row, 26].Value);
            newForm.BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 27].Value);
            newForm.AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 28].Value);
            newForm.TransuraniumActivity_DB = Convert.ToString(worksheet.Cells[Row, 29].Value);
            newForm.RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[Row, 30].Value);
            newForm.Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 31].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 32].Value);
        }
        private async Task GetDataFromRow18(ExcelWorksheet worksheet, int Row, Form18 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.IndividualNumberZHRO_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.PassportNumber_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Volume6_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.Mass7_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.SaltConcentration_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.SpecificActivity_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 11].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.TransporterOKPO_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.Volume20_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.Mass21_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.TritiumActivity_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);
            newForm.AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 24].Value);
            newForm.TransuraniumActivity_DB = Convert.ToString(worksheet.Cells[Row, 25].Value);
            newForm.RefineOrSortRAOCode_DB = Convert.ToString(worksheet.Cells[Row, 26].Value);
            newForm.Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 27].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 28].Value);
        }
        private async Task GetDataFromRow19(ExcelWorksheet worksheet, int Row, Form19 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.OperationDate_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.DocumentVid_DB = Convert.ToByte(worksheet.Cells[Row, 4].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.CodeTypeAccObject_DB = Convert.ToInt16(worksheet.Cells[Row, 7].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);

        }

        private async Task GetDataFromRow21(ExcelWorksheet worksheet, int Row, Form21 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.RefineMachineName.Value = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.MachineCode.Value = Convert.ToByte(worksheet.Cells[Row, 3].Value);
            newForm.MachinePower.Value = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.NumberOfHoursPerYear.Value = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.CodeRAOIn_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.StatusRAOIn_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.VolumeIn_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.MassIn_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.QuantityIn_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.TritiumActivityIn_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.BetaGammaActivityIn_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.AlphaActivityIn_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.TritiumActivityIn_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.CodeRAOout_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.StatusRAOout_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.VolumeOut_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.MassOut_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.QuantityOZIIIout_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
            newForm.TritiumActivityOut_DB = Convert.ToString(worksheet.Cells[Row, 20].Value);
            newForm.BetaGammaActivityOut_DB = Convert.ToString(worksheet.Cells[Row, 21].Value);
            newForm.AlphaActivityOut_DB = Convert.ToString(worksheet.Cells[Row, 22].Value);
            newForm.TransuraniumActivityOut_DB = Convert.ToString(worksheet.Cells[Row, 23].Value);

        }
        private async Task GetDataFromRow22(ExcelWorksheet worksheet, int Row, Form22 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.StoragePlaceName.Value = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.StoragePlaceCode.Value = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PackName.Value = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.PackType.Value = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.PackQuantity_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.StatusRAO_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.VolumeOutOfPack_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.VolumeInPack_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.MassOutOfPack_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.MassInPack_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.QuantityOZIII_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.TritiumActivity_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.TransuraniumActivity_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);
            newForm.MainRadionuclids_DB = Convert.ToString(worksheet.Cells[Row, 18].Value);
            newForm.Subsidy_DB = Convert.ToString(worksheet.Cells[Row, 19].Value);
        }
        private async Task GetDataFromRow23(ExcelWorksheet worksheet, int Row, Form23 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.ProjectVolume_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.CodeRAO_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Volume_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.Mass_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.QuantityOZIII_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.SummaryActivity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.DocumentNumber_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.DocumentDate_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.ExpirationDate_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.DocumentName_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
        }
        private async Task GetDataFromRow24(ExcelWorksheet worksheet, int Row, Form24 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.CodeOYAT_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.MassCreated_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.QuantityCreated_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.MassFromAnothers_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.QuantityFromAnothers_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.MassFromAnothersImported_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.QuantityFromAnothersImported_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.MassAnotherReasons_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.QuantityAnotherReasons_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);
            newForm.MassTransferredToAnother_DB = Convert.ToString(worksheet.Cells[Row, 12].Value);
            newForm.QuantityTransferredToAnother_DB = Convert.ToString(worksheet.Cells[Row, 13].Value);
            newForm.MassRefined_DB = Convert.ToString(worksheet.Cells[Row, 14].Value);
            newForm.QuantityRefined_DB = Convert.ToString(worksheet.Cells[Row, 15].Value);
            newForm.MassRemovedFromAccount_DB = Convert.ToString(worksheet.Cells[Row, 16].Value);
            newForm.QuantityRemovedFromAccount_DB = Convert.ToString(worksheet.Cells[Row, 17].Value);

        }
        private async Task GetDataFromRow25(ExcelWorksheet worksheet, int Row, Form25 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.StoragePlaceCode_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newForm.StoragePlaceName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.CodeOYAT_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.FuelMass_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.CellMass_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.Quantity_DB = Convert.ToInt32(worksheet.Cells[Row, 7].Value);
            newForm.AlphaActivity_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.BetaGammaActivity_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);

        }
        private async Task GetDataFromRow26(ExcelWorksheet worksheet, int Row, Form26 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.ObservedSourceNumber_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newForm.ControlledAreaName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.SupposedWasteSource_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.DistanceToWasteSource_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.TestDepth_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.RadionuclidName_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.AverageYearConcentration_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);

        }
        private async Task GetDataFromRow27(ExcelWorksheet worksheet, int Row, Form27 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.ObservedSourceNumber_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newForm.RadionuclidName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.AllowedWasteValue_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.FactedWasteValue_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.WasteOutbreakPreviousYear_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
        }
        private async Task GetDataFromRow28(ExcelWorksheet worksheet, int Row, Form28 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.WasteSourceName_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newForm.WasteRecieverName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.RecieverTypeCode_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PoolDistrictName_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.AllowedWasteRemovalVolume_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.RemovedWasteVolume_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);

        }
        private async Task GetDataFromRow29(ExcelWorksheet worksheet, int Row, Form29 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.WasteSourceName_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newForm.RadionuclidName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.AllowedActivity_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.FactedActivity_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
        }
        private async Task GetDataFromRow210(ExcelWorksheet worksheet, int Row, Form210 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.IndicatorName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.PlotName_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PlotKadastrNumber_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.PlotCode_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.InfectedArea_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.AvgGammaRaysDosePower_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.MaxGammaRaysDosePower_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.WasteDensityAlpha_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);
            newForm.WasteDensityBeta_DB = Convert.ToString(worksheet.Cells[Row, 10].Value);
            newForm.FcpNumber_DB = Convert.ToString(worksheet.Cells[Row, 11].Value);

        }
        private async Task GetDataFromRow211(ExcelWorksheet worksheet, int Row, Form211 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.PlotName_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newForm.PlotKadastrNumber_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);
            newForm.PlotCode_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.InfectedArea_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);
            newForm.SpecificActivityOfPlot_DB = Convert.ToString(worksheet.Cells[Row, 7].Value);
            newForm.SpecificActivityOfLiquidPart_DB = Convert.ToString(worksheet.Cells[Row, 8].Value);
            newForm.SpecificActivityOfDensePart_DB = Convert.ToString(worksheet.Cells[Row, 9].Value);

        }
        private async Task GetDataFromRow212(ExcelWorksheet worksheet, int Row, Form212 newForm)
        {
            newForm.NumberInOrder_DB = Convert.ToInt32(worksheet.Cells[Row, 1].Value);
            newForm.OperationCode_DB = Convert.ToInt16(worksheet.Cells[Row, 2].Value);
            newForm.ObjectTypeCode_DB = Convert.ToInt16(worksheet.Cells[Row, 3].Value);
            newForm.Radionuclids_DB = Convert.ToString(worksheet.Cells[Row, 4].Value);
            newForm.Activity_DB = Convert.ToString(worksheet.Cells[Row, 5].Value);
            newForm.ProviderOrRecieverOKPO_DB = Convert.ToString(worksheet.Cells[Row, 6].Value);

        }

        private async Task GetDataFromRowNote(ExcelWorksheet worksheet, int Row, Note newNote)
        {
            newNote.RowNumber_DB = Convert.ToString(worksheet.Cells[Row, 1].Value);
            newNote.GraphNumber_DB = Convert.ToString(worksheet.Cells[Row, 2].Value);
            newNote.Comment_DB = Convert.ToString(worksheet.Cells[Row, 3].Value);

        }
        #endregion

        #endregion
    }
}
