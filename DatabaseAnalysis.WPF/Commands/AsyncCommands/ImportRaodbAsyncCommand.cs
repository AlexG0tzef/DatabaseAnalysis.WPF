using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ImportRaodbAsyncCommand : AsyncBaseCommand
    {
        public override async Task AsyncExecute(object? parameter)
        {
            var answ = await GetSelectedFilesFromDialog("RAODB", "raodb");
            if (answ != null)
            {
                foreach (var res in answ)
                {
                    if (res != "")
                    {
                        var file = await GetRaoFileName();
                        var sourceFile = new FileInfo(res);
                        sourceFile.CopyTo(file, true);

                        var reportsCollection = await GetReportsFromDataBase(file);
                        var skipAll = false;
                        foreach (var item in reportsCollection)
                        {
                            if (item.Master.Rows10.Count != 0)
                                item.Master.Rows10[1].RegNo_DB = item.Master.Rows10[0].RegNo_DB;
                            else
                                item.Master.Rows20[1].RegNo_DB = item.Master.Rows20[0].RegNo_DB;
                            Reports first11 = await GetReports11FromLocalEqual(item);
                            Reports first21 = await GetReports21FromLocalEqual(item);
                            await RestoreReportsOrders(item);
                            item.CleanIds();

                            await ProcessIfNoteOrder0(item);

                            if (first11 != null)
                            {
                                await ProcessIfHasReports11(first11, item);
                            }
                            else if (first21 != null)
                            {
                                await ProcessIfHasReports21(first21, item);
                            }
                            else if (first21 == null && first11 == null)
                            {
                                var rep = item.Report_Collection.FirstOrDefault();
                                string? an = null;
                                if (rep != null)
                                {
                                    if (!skipAll)
                                    {
                                        var str = "Был добавлен отчет по форме " + rep.FormNum_DB + " за период " + rep.StartPeriod_DB + "-" + rep.EndPeriod_DB + ",\n" +
                                            "номер корректировки " + rep.CorrectionNumber_DB + ", количество строк " + rep.Rows.Count + ".\n" +
                                            "Организации:" + "\n" +
                                            "   1.Регистрационный номер  " + item.Master.RegNoRep.Value + "\n" +
                                            "   2.Сокращенное наименование  " + item.Master.ShortJurLicoRep.Value + "\n" +
                                            "   3.ОКПО  " + item.Master.OkpoRep.Value + "\n"; ;
                                        //an = await ShowMessage.Handle(new List<string>() { str, "Новая организация", "Ок", "Пропустить для всех" });
                                        //if (an == "Пропустить для всех") skipAll = true;
                                    }
                                }
                                else
                                {
                                    ReportsStorage.Local_Reports.Reports_Collection.Add(item);
                                }
                                if (an == "Пропустить для всех" || an == "Ок" || skipAll)
                                {
                                    ReportsStorage.Local_Reports.Reports_Collection.Add(item);
                                }
                            }
                        }
                        await ReportsStorage.Local_Reports.Reports_Collection.QuickSortAsync();
                    }
                }
            }
            //StaticConfiguration.DBModel.SaveChanges();
        }

        private async Task ProcessDataBaseFillEmpty(DataContext dbm)
        {
            if (dbm.DBObservableDbSet.Count() == 0) dbm.DBObservableDbSet.Add(new DBObservable());
            foreach (var item in dbm.DBObservableDbSet)
            {
                foreach (Reports it in item.Reports_Collection)
                {
                    if (it.Master_DB.FormNum_DB != "")
                    {
                        if (it.Master_DB.Rows10.Count == 0)
                        {
                            var ty1 = new Form10();
                            ty1.NumberInOrder_DB = 1;
                            var ty2 = new Form10();
                            ty2.NumberInOrder_DB = 2;
                            it.Master_DB.Rows10.Add(ty1);
                            it.Master_DB.Rows10.Add(ty2);
                        }
                        if (it.Master_DB.Rows20.Count == 0)
                        {
                            var ty1 = new Form20();
                            ty1.NumberInOrder_DB = 1;
                            var ty2 = new Form20();
                            ty2.NumberInOrder_DB = 2;
                            it.Master_DB.Rows20.Add(ty1);
                            it.Master_DB.Rows20.Add(ty2);
                        }
                        it.Master_DB.Rows10.Sorted = false;
                        it.Master_DB.Rows20.Sorted = false;
                        await it.Master_DB.Rows10.QuickSortAsync();
                        await it.Master_DB.Rows20.QuickSortAsync();
                    }
                }
            }
        }
        private async Task<string[]> GetSelectedFilesFromDialog(string Name, params string[] Extensions)
        {
            string[]? answ = null;

            OpenFileDialog openFileDialog = new() { Filter = "RAODB | *.raodb" };
            bool openExcel = (bool)openFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (openExcel)
            {
                return openFileDialog.FileNames;
            }
            return null;
        }
        private async Task<string> GetSystemDirectory()
        {
            try
            {
                string system = "";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    system = Environment.GetFolderPath(Environment.SpecialFolder.System);
                }
                else
                {
                    system = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                return system;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        private async Task<string> GetTempDirectory(string systemDirectory)
        {
            var tmp = "";
            var pty = "";
            try
            {
                string path = Path.GetPathRoot(systemDirectory);
                tmp = Path.Combine(path, "RAO");
                pty = tmp;
                tmp = Path.Combine(tmp, "temp");
                Directory.CreateDirectory(tmp);
                return tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        private async Task<string> GetRaoFileName()
        {
            var tmp = await GetTempDirectory(await GetSystemDirectory());

            var file = "";
            var count = 0;
            do
            {
                file = Path.Combine(tmp, "file_imp_" + (count++) + ".raodb");
            }
            while (File.Exists(file));

            return file;
        }
        private async Task<List<FireBird.Reports>> GetReportsFromDataBase(string file)
        {
            var lst = new List<FireBird.Reports>();
            using (DBModel db = new DBModel(file))
            {
                #region Test Version
                var t = db.Database.GetPendingMigrations();
                var a = db.Database.GetMigrations();
                var b = db.Database.GetAppliedMigrations();
                #endregion

                await db.Database.MigrateAsync();
                await db.LoadTablesAsync();
                await ProcessDataBaseFillEmpty(db);

                if (db.DBObservableDbSet.Local.First().Reports_Collection.ToList().Count != 0)
                {
                    lst = db.DBObservableDbSet.Local.First().Reports_Collection.ToList();
                }
                else
                {
                    lst = await db.ReportsCollectionDbSet.ToListAsync();
                }
            }
            return lst;
        }
        private async Task<FireBird.Reports> GetReports11FromLocalEqual(FireBird.Reports item)
        {
            try
            {
                var tb1 = item.Report_Collection.Where(x => x.FormNum_DB[0].ToString().Equals("1"));
                if (tb1.Count() != 0)
                {
                    var tb11 = from FireBird.Reports t in ReportsStorage.Local_Reports.Reports_Collection10
                               where ((item.Master.Rows10[0].Okpo_DB == t.Master.Rows10[0].Okpo_DB &&
                               item.Master.Rows10[0].RegNo_DB == t.Master.Rows10[0].RegNo_DB &&
                               item.Master.Rows10[1].Okpo_DB == "") ||
                               (item.Master.Rows10[1].Okpo_DB == t.Master.Rows10[1].Okpo_DB &&
                               item.Master.Rows10[1].RegNo_DB == t.Master.Rows10[1].RegNo_DB &&
                               item.Master.Rows10[1].Okpo_DB != ""))
                               select t;
                    return tb11.FirstOrDefault();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private async Task<FireBird.Reports> GetReports21FromLocalEqual(FireBird.Reports item)
        {
            try
            {
                var tb2 = item.Report_Collection.Where(x => x.FormNum_DB[0].ToString().Equals("2"));
                if (tb2.Count() != 0)
                {
                    var tb21 = from FireBird.Reports t in ReportsStorage.Local_Reports.Reports_Collection20
                               where ((item.Master.Rows20[0].Okpo_DB == t.Master.Rows20[0].Okpo_DB &&
                               item.Master.Rows20[0].RegNo_DB == t.Master.Rows20[0].RegNo_DB &&
                               item.Master.Rows20[1].Okpo_DB == "") ||
                               (item.Master.Rows20[1].Okpo_DB == t.Master.Rows20[1].Okpo_DB &&
                               item.Master.Rows20[1].RegNo_DB == t.Master.Rows20[1].RegNo_DB &&
                               item.Master.Rows20[1].Okpo_DB != ""))
                               select t;

                    return tb21.FirstOrDefault();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private async Task RestoreReportsOrders(FireBird.Reports item)
        {
            if (item.Master_DB.FormNum_DB == "1.0")
            {
                if (item.Master_DB.Rows10[0].Id > item.Master_DB.Rows10[1].Id)
                {
                    if (item.Master_DB.Rows10[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows10[0].NumberInOrder_DB = 2;
                    }
                    if (item.Master_DB.Rows10[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows10[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 2;
                        }
                    }
                    item.Master_DB.Rows10.Sorted = false;
                    item.Master_DB.Rows10.QuickSort();
                }
                else
                {
                    if (item.Master_DB.Rows10[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows10[0].NumberInOrder_DB = 1;
                    }
                    if (item.Master_DB.Rows10[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows10[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows10[1].NumberInOrder_DB = 2;
                        }
                    }
                    item.Master_DB.Rows10.Sorted = false;
                    item.Master_DB.Rows10.QuickSort();
                }
            }
            if (item.Master_DB.FormNum_DB == "2.0")
            {
                if (item.Master_DB.Rows20[0].Id > item.Master_DB.Rows20[1].Id)
                {
                    if (item.Master_DB.Rows20[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows20[0].NumberInOrder_DB = 2;
                    }
                    if (item.Master_DB.Rows20[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows20[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 2;
                        }
                    }
                    item.Master_DB.Rows20.Sorted = false;
                    item.Master_DB.Rows20.QuickSort();
                }
                else
                {
                    if (item.Master_DB.Rows20[0].NumberInOrder_DB == 0)
                    {
                        item.Master_DB.Rows20[0].NumberInOrder_DB = 1;
                    }
                    if (item.Master_DB.Rows20[1].NumberInOrder_DB == 0)
                    {
                        if (item.Master_DB.Rows20[1].NumberInOrder_DB == 2)
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 1;
                        }
                        else
                        {
                            item.Master_DB.Rows20[1].NumberInOrder_DB = 2;
                        }
                    }
                    item.Master_DB.Rows20.Sorted = false;
                    item.Master_DB.Rows20.QuickSort();
                }
            }
        }
        private async Task ProcessIfNoteOrder0(FireBird.Reports item)
        {
            foreach (FireBird.Report form in item.Report_Collection)
            {
                foreach (FireBird.Note note in form.Notes)
                {
                    if (note.Order == 0)
                    {
                        note.Order = GetNumberInOrder(form.Notes);
                    }
                }
            }
        }
        private async Task ProcessIfHasReports21(FireBird.Reports first21, FireBird.Reports item)
        {
            var not_in = false;

            var skipLess = false;
            var skipNew = false;
            var _skipNew = false;

            foreach (Report it in item.Report_Collection)
            {
                if (first21.Report_Collection.Count != 0)
                {
                    foreach (Report elem in first21.Report_Collection)
                    {
                        if (elem.Year_DB == it.Year_DB && it.FormNum_DB == elem.FormNum_DB)
                        {
                            not_in = true;
                            if (it.CorrectionNumber_DB < elem.CorrectionNumber_DB)
                            {
                                if (!skipLess)
                                {
                                    var str = " Вы пытаетесь загрузить форму с наименьщим номером корректировки - " +
                                        it.CorrectionNumber_DB + ",\n" +
                                        "при текущем значении корректировки - " +
                                        elem.CorrectionNumber_DB + ".\n" +
                                        "Номер формы - " + it.FormNum_DB + "\n" +
                                        "Отчетный год - " + it.Year_DB + "\n" +
                                        "Регистрационный номер - " + first21.Master.RegNoRep.Value + "\n" +
                                        "Сокращенное наименование - " + first21.Master.ShortJurLicoRep.Value + "\n" +
                                        "ОКПО - " + first21.Master.OkpoRep.Value + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                    //var an = await ShowMessage.Handle(new List<string>() { str, "Отчет", "OK", "Пропустить для всех" });
                                    //if (an == "Пропустить для всех") skipLess = true;
                                }
                            }
                            else if (it.CorrectionNumber_DB == elem.CorrectionNumber_DB && it.ExportDate_DB == elem.ExportDate_DB)
                            {
                                var str = "Совпадение даты в " + elem.FormNum_DB + " " +
                                elem.Year_DB + " .\n" +
                                "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                first21.Master.RegNoRep.Value + " \n" +
                                first21.Master.ShortJurLicoRep.Value + " " +
                                first21.Master.OkpoRep.Value + "\n" +
                                "Количество строк - " + it.Rows.Count;
                                //var an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                //    "Заменить",
                                //    "Сохранить оба",
                                //    "Отменить"
                                //});
                                //await ChechAanswer(an, first21, elem, it);
                            }
                            else
                            {
                                var an = "Загрузить новую";
                                if (!skipNew)
                                {
                                    if (item.Report_Collection.Count() > 1)
                                    {
                                        var str = "Загрузить новую форму? \n" +
                                        "Номер формы - " + it.FormNum_DB + "\n" +
                                        "Отчетный год - " + it.Year_DB + "\n" +
                                        "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                        "Регистрационный номер - " + first21.Master.RegNoRep.Value + "\n" +
                                        "Сокращенное наименование - " + first21.Master.ShortJurLicoRep.Value + "\n" +
                                        "ОКПО - " + first21.Master.OkpoRep.Value + "\n" +
                                        "Форма с предыдущим номером корректировки №" +
                                        elem.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                        "Сделайте резервную копию." + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                        //an = await ShowMessage.Handle(new List<string>() {
                                        //                                str,
                                        //                                "Отчет",
                                        //                                "Загрузить новую",
                                        //                                "Отмена",
                                        //                                "Загрузить для всех"
                                        //                                });
                                        //if (an == "Загрузить для всех") skipNew = true;
                                        //an = "Загрузить новую";
                                    }
                                    else
                                    {
                                        var str = "Загрузить новую форму? \n" +
                                            "Номер формы - " + it.FormNum_DB + "\n" +
                                            "Отчетный год - " + it.Year_DB + "\n" +
                                            "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                            "Регистрационный номер - " + first21.Master.RegNoRep.Value + "\n" +
                                            "Сокращенное наименование - " + first21.Master.ShortJurLicoRep.Value + "\n" +
                                            "ОКПО - " + first21.Master.OkpoRep.Value + "\n" +
                                            "Форма с предыдущим номером корректировки №" +
                                            elem.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                            "Сделайте резервную копию." + "\n" +
                                            "Количество строк - " + it.Rows.Count;
                                        //an = await ShowMessage.Handle(new List<string>() {
                                        //                                str,
                                        //                                "Отчет",
                                        //                                "Загрузить новую",
                                        //                                "Отмена"
                                        //                                });
                                    }
                                }
                                //await ChechAanswer(an, first21, elem, it);
                            }
                        }
                    }
                    if (!not_in)
                    {
                        var an = "Да";
                        if (!_skipNew)
                        {
                            if (item.Report_Collection.Count() > 1)
                            {
                                var str = "Загрузить новую форму? \n" +
                                    "Номер формы - " + it.FormNum_DB + "\n" +
                                    "Отчетный год - " + it.Year_DB + "\n" +
                                    "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                    "Регистрационный номер - " + first21.Master.RegNoRep.Value + "\n" +
                                    "Сокращенное наименование - " + first21.Master.ShortJurLicoRep.Value + "\n" +
                                    "ОКПО - " + first21.Master.OkpoRep.Value + "\n" +
                                    "Количество строк - " + it.Rows.Count;
                                //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                //    "Да",
                                //    "Нет",
                                //    "Загрузить для всех"
                                //});
                                //if (an == "Загрузить для всех") _skipNew = true;
                                //an = "Да";
                            }
                            else
                            {
                                var str = "Загрузить новую форму? \n" +
                                    "Номер формы - " + it.FormNum_DB + "\n" +
                                    "Отчетный год - " + it.Year_DB + "\n" +
                                    "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                    "Регистрационный номер - " + first21.Master.RegNoRep.Value + "\n" +
                                    "Сокращенное наименование - " + first21.Master.ShortJurLicoRep.Value + "\n" +
                                    "ОКПО - " + first21.Master.OkpoRep.Value + "\n" +
                                    "Количество строк - " + it.Rows.Count;
                                //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                //    "Да",
                                //    "Нет"
                                //});
                            }
                        }
                        //await ChechAanswer(an, first21, null, it);
                        not_in = false;
                    }
                }
                else
                {
                    first21.Report_Collection.Add(it);
                }
                await first21.SortAsync();
            }
        }
        private async Task ChechAanswer(string an, FireBird.Reports first, FireBird.Report elem = null, FireBird.Report it = null, bool doSomething = false)
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
        private int GetNumberInOrder(IEnumerable lst)
        {
            int maxNum = 0;

            foreach (var item in lst)
            {
                var frm = (INumberInOrder)item;
                if (frm.Order >= maxNum)
                {
                    maxNum++;
                }
            }
            return maxNum + 1;
        }

        private async Task ProcessIfHasReports11(Reports first11, Reports item)
        {
            var not_in = false;

            var skipLess = false;
            var doSomething = false;
            var skipNew = false;
            var _skipNew = false;
            var skipInter = false;

            foreach (Report it in item.Report_Collection)
            {
                if (first11.Report_Collection.Count != 0)
                {
                    foreach (Report elem in first11.Report_Collection)
                    {
                        DateTime st_elem = DateTime.Parse(DateTime.Now.ToShortDateString());
                        DateTime en_elem = DateTime.Parse(DateTime.Now.ToShortDateString());
                        try
                        {
                            st_elem = DateTime.Parse(elem.StartPeriod_DB) > DateTime.Parse(elem.EndPeriod_DB) ? DateTime.Parse(elem.EndPeriod_DB) : DateTime.Parse(elem.StartPeriod_DB);
                            en_elem = DateTime.Parse(elem.StartPeriod_DB) < DateTime.Parse(elem.EndPeriod_DB) ? DateTime.Parse(elem.EndPeriod_DB) : DateTime.Parse(elem.StartPeriod_DB);
                        }
                        catch (Exception ex)
                        { }

                        DateTime st_it = DateTime.Parse(DateTime.Now.ToShortDateString());
                        DateTime en_it = DateTime.Parse(DateTime.Now.ToShortDateString());
                        try
                        {
                            st_it = DateTime.Parse(it.StartPeriod_DB) > DateTime.Parse(it.EndPeriod_DB) ? DateTime.Parse(it.EndPeriod_DB) : DateTime.Parse(it.StartPeriod_DB);
                            en_it = DateTime.Parse(it.StartPeriod_DB) < DateTime.Parse(it.EndPeriod_DB) ? DateTime.Parse(it.EndPeriod_DB) : DateTime.Parse(it.StartPeriod_DB);
                        }
                        catch (Exception ex)
                        {
                        }

                        if (st_elem == st_it && en_elem == en_it && it.FormNum_DB == elem.FormNum_DB)
                        {
                            not_in = true;
                            if (it.CorrectionNumber_DB < elem.CorrectionNumber_DB)
                            {
                                if (!skipLess)
                                {
                                    var str = " Вы пытаетесь загрузить форму с наименьщим номером корректировки - " +
                                        it.CorrectionNumber_DB + ",\n" +
                                        "при текущем значении корректировки - " +
                                        elem.CorrectionNumber_DB + ".\n" +
                                        "Номер формы - " + it.FormNum_DB + "\n" +
                                        "Начало отчетного периода - " + it.StartPeriod_DB + "\n" +
                                        "Конец отчетного периода - " + it.EndPeriod_DB + "\n" +
                                        "Регистрационный номер - " + first11.Master.RegNoRep.Value + "\n" +
                                        "Сокращенное наименование - " + first11.Master.ShortJurLicoRep.Value + "\n" +
                                        "ОКПО - " + first11.Master.OkpoRep.Value + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                    //var an = await ShowMessage.Handle(new List<string>() { str, "Отчет", "OK", "Пропустить для всех" });
                                    //if (an == "Пропустить для всех")
                                    //{
                                    //    skipLess = true;
                                    //}
                                }
                            }
                            else if (it.CorrectionNumber_DB == elem.CorrectionNumber_DB && it.ExportDate_DB == elem.ExportDate_DB)
                            {
                                var str = "Совпадение даты в " + elem.FormNum_DB + " " +
                                    elem.StartPeriod_DB + "-" +
                                    elem.EndPeriod_DB + " .\n" +
                                    "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                    first11.Master.RegNoRep.Value + " " +
                                    first11.Master.ShortJurLicoRep.Value + " " +
                                    first11.Master.OkpoRep.Value + "\n" +
                                    "Количество строк - " + it.Rows.Count;
                                doSomething = true;
                                //var an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                //    "Заменить",
                                //    "Дополнить",
                                //    "Сохранить оба",
                                //    "Отменить"
                                //});
                                //await ChechAanswer(an, first11, elem, it, doSomething);
                                doSomething = true;
                            }
                            else
                            {
                                var an = "Загрузить новую";
                                if (!skipNew)
                                {
                                    if (item.Report_Collection.Count() > 1)
                                    {
                                        var str = "Загрузить новую форму? \n" +
                                            "Номер формы - " + it.FormNum_DB + "\n" +
                                            "Начало отчетного периода - " + it.StartPeriod_DB + "\n" +
                                            "Конец отчетного периода - " + it.EndPeriod_DB + "\n" +
                                            "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                            "Регистрационный номер - " + first11.Master.RegNoRep.Value + "\n" +
                                            "Сокращенное наименование - " + first11.Master.ShortJurLicoRep.Value + "\n" +
                                            "ОКПО - " + first11.Master.OkpoRep.Value + "\n" +
                                            "Форма с предыдущим номером корректировки №" +
                                            elem.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                            "Сделайте резервную копию." + "\n" +
                                            "Количество строк - " + it.Rows.Count;
                                        doSomething = true;
                                        //an = await ShowMessage.Handle(new List<string>() {str, "Отчет",
                                        //    "Загрузить новую",
                                        //    "Отмена",
                                        //    "Загрузить для все"
                                        //    });
                                        //if (an == "Загрузить для всех") skipNew = true;
                                        //an = "Загрузить новую";
                                    }
                                    else
                                    {
                                        var str = "Загрузить новую форму? \n" +
                                            "Номер формы - " + it.FormNum_DB + "\n" +
                                            "Начало отчетного периода - " + it.StartPeriod_DB + "\n" +
                                            "Конец отчетного периода - " + it.EndPeriod_DB + "\n" +
                                            "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                            "Регистрационный номер - " + first11.Master.RegNoRep.Value + "\n" +
                                            "Сокращенное наименование - " + first11.Master.ShortJurLicoRep.Value + "\n" +
                                            "ОКПО - " + first11.Master.OkpoRep.Value + "\n" +
                                            "Форма с предыдущим номером корректировки №" +
                                            elem.CorrectionNumber_DB + " будет безвозвратно удалена.\n" +
                                            "Сделайте резервную копию." + "\n" +
                                            "Количество строк - " + it.Rows.Count;
                                        doSomething = true;
                                        //an = await ShowMessage.Handle(new List<string>() {str, "Отчет",
                                        //    "Загрузить новую",
                                        //    "Отмена"
                                        //    });
                                    }
                                }
                                //await ChechAanswer(an, first11, elem, it, doSomething);
                                doSomething = true;
                            }
                        }
                        else
                        {
                            if ((st_elem > st_it && st_elem < en_it || en_elem > st_it && en_elem < en_it) && it.FormNum.Value == elem.FormNum.Value)
                            {
                                not_in = true;
                                var an = "Отменить";
                                if (!skipInter)
                                {
                                    var str = "Пересечение даты в " + elem.FormNum_DB + " " +
                                        elem.StartPeriod_DB + "-" +
                                        elem.EndPeriod_DB + " \n" +
                                        first11.Master.RegNoRep.Value + " " +
                                        first11.Master.ShortJurLicoRep.Value + " " +
                                        first11.Master.OkpoRep.Value + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                    //    an = await ShowMessage.Handle(new List<string>(){str,"Отчет",
                                    //"Сохранить оба",
                                    //"Отменить"
                                    //});
                                    //    skipInter = true;
                                    //}
                                    //await ChechAanswer(an, first11, null, it, doSomething);
                                    doSomething = true;
                                }
                            }
                        }
                        if (!not_in)
                        {
                            var an = "Да";
                            if (!_skipNew)
                            {
                                if (item.Report_Collection.Count() > 1)
                                {
                                    var str = "Загрузить новую форму?\n" +
                                        "Номер формы - " + it.FormNum_DB + "\n" +
                                        "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                        "Начало отчетного периода - " + it.StartPeriod_DB + "\n" +
                                        "Конец отчетного периода - " + it.EndPeriod_DB + "\n" +
                                        "Регистрационный номер - " + first11.Master.RegNoRep.Value + "\n" +
                                        "Сокращенное наименование - " + first11.Master.ShortJurLicoRep.Value + "\n" +
                                        "ОКПО - " + first11.Master.OkpoRep.Value + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                    //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                    //    "Да",
                                    //    "Нет",
                                    //    "Загрузить для всех"
                                    //    });
                                    //an = "Да";
                                }
                                else
                                {
                                    var str = "Загрузить новую форму?\n" +
                                        "Номер формы - " + it.FormNum_DB + "\n" +
                                        "Номер корректировки -" + it.CorrectionNumber_DB + "\n" +
                                        "Начало отчетного периода - " + it.StartPeriod_DB + "\n" +
                                        "Конец отчетного периода - " + it.EndPeriod_DB + "\n" +
                                        "Регистрационный номер - " + first11.Master.RegNoRep.Value + "\n" +
                                        "Сокращенное наименование - " + first11.Master.ShortJurLicoRep.Value + "\n" +
                                        "ОКПО - " + first11.Master.OkpoRep.Value + "\n" +
                                        "Количество строк - " + it.Rows.Count;
                                    //an = await ShowMessage.Handle(new List<string>(){str, "Отчет",
                                    //"Да",
                                    //"Нет"
                                    //});
                                }
                            }
                            await ChechAanswer(an, first11, null, it);
                        }
                    }
                }
                else
                {
                    first11.Report_Collection.Add(it);
                    await first11.SortAsync();
                }
            }
        }
    }
}
