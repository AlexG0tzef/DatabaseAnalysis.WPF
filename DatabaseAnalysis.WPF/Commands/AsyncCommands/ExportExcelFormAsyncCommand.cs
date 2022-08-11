using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelFormAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;
        private readonly ICommand _getData;

        private ExcelWorksheet _worksheet { get; set; }

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
                            _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.1");
                            ExportForm11Data();
                            excelPackege.Save();
                            MessageBox.Show($"Выгрузка \"Всех форм 1.1\", сохранена по пути {path}");
                            break;
                        case "1.2":
                            _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.2");
                            ExportForm12Data();
                            excelPackege.Save();
                            MessageBox.Show($"Выгрузка \"Всех форм 1.2\", сохранена по пути {path}");
                            break;
                        case "1.3":
                            _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.3");
                            ExportForm13Data();
                            excelPackege.Save();
                            MessageBox.Show($"Выгрузка \"Всех форм 1.3\", сохранена по пути {path}");
                            break;
                        case "1.4":
                            _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.4");
                            ExportForm14Data();
                            excelPackege.Save();
                            MessageBox.Show($"Выгрузка \"Всех форм 1.4\", сохранена по пути {path}");
                            break;
                        //case "1.5":
                        //    _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.5");
                        //    ExportForm15Data();
                        //    excelPackege.Save();
                        //    MessageBox.Show($"Выгрузка \"Всех форм 1.5\", сохранена по пути {path}");
                        //    break;
                            //case "1.6":
                            //    _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.6");
                            //    ExportForm16Data();
                            //    excelPackege.Save();
                            //    MessageBox.Show($"Выгрузка \"Всех форм 1.6\", сохранена по пути {path}");
                            //    break;
                            //case "1.7":
                            //    _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.7");
                            //    ExportForm17Data();
                            //    excelPackege.Save();
                            //    MessageBox.Show($"Выгрузка \"Всех форм 1.7\", сохранена по пути {path}");
                            //    break;
                            //case "1.8":
                            //    _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.8");
                            //    ExportForm18Data();
                            //    excelPackege.Save();
                            //    MessageBox.Show($"Выгрузка \"Всех форм 1.8\", сохранена по пути {path}");
                            //    break;
                            //case "1.9":
                            //    _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.9");
                            //    ExportForm19Data();
                            //    excelPackege.Save();
                            //    MessageBox.Show($"Выгрузка \"Всех форм 1.9\", сохранена по пути {path}");
                            //    break;
                    }
                }

            }
        }

        private void ExportForm11Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "номер паспорта" + Environment.NewLine + "(сертификата)";
            _worksheet.Cells[1, 13].Value = "тип";
            _worksheet.Cells[1, 14].Value = "радионуклиды";
            _worksheet.Cells[1, 15].Value = "номер";
            _worksheet.Cells[1, 16].Value = "количество, шт";
            _worksheet.Cells[1, 17].Value = "суммарная активность, Бк";
            _worksheet.Cells[1, 18].Value = "код ОКПО изготовителя";
            _worksheet.Cells[1, 19].Value = "дата выпуска";
            _worksheet.Cells[1, 20].Value = "категория";
            _worksheet.Cells[1, 21].Value = "НСС, мес";
            _worksheet.Cells[1, 22].Value = "код формы собственности";
            _worksheet.Cells[1, 23].Value = "код ОКПО правообладателя";
            _worksheet.Cells[1, 24].Value = "вид";
            _worksheet.Cells[1, 25].Value = "номер";
            _worksheet.Cells[1, 26].Value = "дата";
            _worksheet.Cells[1, 27].Value = "поставщика или получателя";
            _worksheet.Cells[1, 28].Value = "перевозчика";
            _worksheet.Cells[1, 29].Value = "наименование";
            _worksheet.Cells[1, 30].Value = "тип";
            _worksheet.Cells[1, 31].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1") && x.Rows11 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form11 repForm in rep.Rows11)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Quantity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.CreatorOKPO_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.CreationDate_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.Category_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.SignedServicePeriod_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 31].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm12Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "номер паспорта";
            _worksheet.Cells[1, 13].Value = "наименование";
            _worksheet.Cells[1, 14].Value = "номер";
            _worksheet.Cells[1, 15].Value = "масса объединенного урана, кг";
            _worksheet.Cells[1, 16].Value = "код ОКПО изготовителя";
            _worksheet.Cells[1, 17].Value = "дата выпуска";
            _worksheet.Cells[1, 18].Value = "НСС, мес";
            _worksheet.Cells[1, 19].Value = "код формы собственности";
            _worksheet.Cells[1, 20].Value = "код ОКПО правообладателя";
            _worksheet.Cells[1, 21].Value = "вид";
            _worksheet.Cells[1, 22].Value = "номер";
            _worksheet.Cells[1, 23].Value = "дата";
            _worksheet.Cells[1, 24].Value = "поставщика или получателя";
            _worksheet.Cells[1, 25].Value = "перевозчика";
            _worksheet.Cells[1, 26].Value = "наименование";
            _worksheet.Cells[1, 27].Value = "тип";
            _worksheet.Cells[1, 28].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2") && x.Rows12 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form12 repForm in rep.Rows12)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.NameIOU_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.FactoryNumber_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.CreatorOKPO_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.CreationDate_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.SignedServicePeriod_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm13Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "номер паспорта";
            _worksheet.Cells[1, 13].Value = "тип";
            _worksheet.Cells[1, 14].Value = "радионуклиды";
            _worksheet.Cells[1, 15].Value = "номер";
            _worksheet.Cells[1, 16].Value = "активность, Бк";
            _worksheet.Cells[1, 17].Value = "код ОКПО изготовителя";
            _worksheet.Cells[1, 18].Value = "дата выпуска";
            _worksheet.Cells[1, 19].Value = "агрегатное состояние";
            _worksheet.Cells[1, 20].Value = "код формы собственности";
            _worksheet.Cells[1, 21].Value = "код ОКПО правообладателя";
            _worksheet.Cells[1, 22].Value = "вид";
            _worksheet.Cells[1, 23].Value = "номер";
            _worksheet.Cells[1, 24].Value = "дата";
            _worksheet.Cells[1, 25].Value = "поставщика или получателя";
            _worksheet.Cells[1, 26].Value = "перевозчика";
            _worksheet.Cells[1, 27].Value = "наименование";
            _worksheet.Cells[1, 28].Value = "тип";
            _worksheet.Cells[1, 29].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3") && x.Rows13 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form13 repForm in rep.Rows13)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.CreatorOKPO_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.CreationDate_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.AggregateState_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm14Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "номер паспорта";
            _worksheet.Cells[1, 13].Value = "наименование";
            _worksheet.Cells[1, 14].Value = "вид";
            _worksheet.Cells[1, 15].Value = "радионуклиды";
            _worksheet.Cells[1, 16].Value = "активность, Бк";
            _worksheet.Cells[1, 17].Value = "дата измерения активности";
            _worksheet.Cells[1, 18].Value = "объем, куб.м";
            _worksheet.Cells[1, 19].Value = "масса, кг";
            _worksheet.Cells[1, 20].Value = "агрегатное состояние";
            _worksheet.Cells[1, 21].Value = "код формы собственности";
            _worksheet.Cells[1, 22].Value = "код ОКПО правообладателя";
            _worksheet.Cells[1, 23].Value = "вид";
            _worksheet.Cells[1, 24].Value = "номер";
            _worksheet.Cells[1, 25].Value = "дата";
            _worksheet.Cells[1, 26].Value = "поставщика или получателя";
            _worksheet.Cells[1, 27].Value = "перевозчика";
            _worksheet.Cells[1, 28].Value = "наименование";
            _worksheet.Cells[1, 29].Value = "тип";
            _worksheet.Cells[1, 30].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4") && x.Rows14 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form14 repForm in rep.Rows14)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Name_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Sort_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.ActivityMeasurementDate_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.AggregateState_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm15Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "номер паспорта (сертификата) Эри, акта определения характеристик ОЗИИ";
            _worksheet.Cells[1, 13].Value = "тип";
            _worksheet.Cells[1, 14].Value = "радионуклиды";
            _worksheet.Cells[1, 15].Value = "номер";
            _worksheet.Cells[1, 16].Value = "количество, шт";
            _worksheet.Cells[1, 17].Value = "суммарная активность, Бк";
            _worksheet.Cells[1, 18].Value = "дата выпуска";
            _worksheet.Cells[1, 19].Value = "статус РАО";
            _worksheet.Cells[1, 20].Value = "вид";
            _worksheet.Cells[1, 21].Value = "номер";
            _worksheet.Cells[1, 22].Value = "дата";
            _worksheet.Cells[1, 23].Value = "поставщика или получателя";
            _worksheet.Cells[1, 24].Value = "перевозчика";
            _worksheet.Cells[1, 25].Value = "наименование";
            _worksheet.Cells[1, 26].Value = "тип";
            _worksheet.Cells[1, 27].Value = "заводской номер";
            _worksheet.Cells[1, 28].Value = "наименование";
            _worksheet.Cells[1, 29].Value = "код";
            _worksheet.Cells[1, 30].Value = "Код переработки / сортировки РАО";
            _worksheet.Cells[1, 31].Value = "Субсидия, %";
            _worksheet.Cells[1, 32].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5") && x.Rows15 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form15 repForm in rep.Rows15)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Quantity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.CreationDate_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.StatusRAO_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.PackNumber_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceName_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.StoragePlaceCode_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.RefineOrSortRAOCode_DB;
                        _worksheet.Cells[currentRow, 31].Value = repForm.Subsidy_DB;
                        _worksheet.Cells[currentRow, 32].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm16Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "Код РАО";
            _worksheet.Cells[1, 13].Value = "Статус РАО";
            _worksheet.Cells[1, 14].Value = "объем без упаковки, куб.";
            _worksheet.Cells[1, 15].Value = "масса без упаковки";
            _worksheet.Cells[1, 16].Value = "количество ОЗИИИ";
            _worksheet.Cells[1, 17].Value = "Основные радионуклиды";
            _worksheet.Cells[1, 18].Value = "тритий";
            _worksheet.Cells[1, 19].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 20].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 21].Value = "траансурановые радионуклиды";
            _worksheet.Cells[1, 22].Value = "Дата измерения активности";
            _worksheet.Cells[1, 23].Value = "вид";
            _worksheet.Cells[1, 24].Value = "номер";
            _worksheet.Cells[1, 25].Value = "дата";
            _worksheet.Cells[1, 26].Value = "поставщика или получателя";
            _worksheet.Cells[1, 27].Value = "перевозчика";
            _worksheet.Cells[1, 28].Value = "наименование";
            _worksheet.Cells[1, 29].Value = "код";
            _worksheet.Cells[1, 30].Value = "Код переработки /";
            _worksheet.Cells[1, 31].Value = "наименование";
            _worksheet.Cells[1, 32].Value = "тип";
            _worksheet.Cells[1, 33].Value = "номер упаковки";
            _worksheet.Cells[1, 34].Value = "Субсидия, %";
            _worksheet.Cells[1, 35].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6") && x.Rows16 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form16 repForm in rep.Rows16)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.CodeRAO_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.StatusRAO_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Volume_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.QuantityOZIII_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.MainRadionuclids_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.TritiumActivity_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.BetaGammaActivity_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.AlphaActivity_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.TransuraniumActivity_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.ActivityMeasurementDate_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceName_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.StoragePlaceCode_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.RefineOrSortRAOCode_DB;
                        _worksheet.Cells[currentRow, 31].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 32].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 33].Value = repForm.PackNumber_DB;
                        _worksheet.Cells[currentRow, 34].Value = repForm.Subsidy_DB;
                        _worksheet.Cells[currentRow, 35].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm17Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "наименование";
            _worksheet.Cells[1, 13].Value = "тип";
            _worksheet.Cells[1, 14].Value = "заводской номер";
            _worksheet.Cells[1, 15].Value = "номер упаковки (идентификационный код)";
            _worksheet.Cells[1, 16].Value = "дата формирования";
            _worksheet.Cells[1, 17].Value = "номер паспорта";
            _worksheet.Cells[1, 18].Value = "объем, куб.м";
            _worksheet.Cells[1, 19].Value = "масса брутто, т";
            _worksheet.Cells[1, 20].Value = "наименования радионуклида";
            _worksheet.Cells[1, 21].Value = "удельная активность, Бк/г";
            _worksheet.Cells[1, 22].Value = "вид";
            _worksheet.Cells[1, 23].Value = "номер";
            _worksheet.Cells[1, 24].Value = "дата";
            _worksheet.Cells[1, 25].Value = "поставщика или получателя";
            _worksheet.Cells[1, 26].Value = "перевозчика";
            _worksheet.Cells[1, 27].Value = "наименование";
            _worksheet.Cells[1, 28].Value = "код";
            _worksheet.Cells[1, 29].Value = "код";
            _worksheet.Cells[1, 30].Value = "статус";
            _worksheet.Cells[1, 31].Value = "объем без упаковки, куб.м";
            _worksheet.Cells[1, 32].Value = "масса без упаковки (нетто), т";
            _worksheet.Cells[1, 33].Value = "количество ОЗИИИ, шт";
            _worksheet.Cells[1, 34].Value = "тритий";
            _worksheet.Cells[1, 35].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 36].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 37].Value = "трансурановые радионуклиды";
            _worksheet.Cells[1, 38].Value = "Код переработки/сортировки РАО";
            _worksheet.Cells[1, 39].Value = "Субсидия, %";
            _worksheet.Cells[1, 40].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7") && x.Rows17 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form17 repForm in rep.Rows17)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.PackFactoryNumber_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.PackFactoryNumber_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.FormingDate_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.SpecificActivity_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.StoragePlaceName_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceCode_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.CodeRAO_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.StatusRAO_DB;
                        _worksheet.Cells[currentRow, 31].Value = repForm.VolumeOutOfPack_DB;
                        _worksheet.Cells[currentRow, 32].Value = repForm.MassOutOfPack_DB;
                        _worksheet.Cells[currentRow, 33].Value = repForm.Quantity_DB;
                        _worksheet.Cells[currentRow, 34].Value = repForm.TritiumActivity_DB;
                        _worksheet.Cells[currentRow, 35].Value = repForm.BetaGammaActivity_DB;
                        _worksheet.Cells[currentRow, 36].Value = repForm.AlphaActivity_DB;
                        _worksheet.Cells[currentRow, 37].Value = repForm.TransuraniumActivity_DB;
                        _worksheet.Cells[currentRow, 38].Value = repForm.RefineOrSortRAOCode_DB;
                        _worksheet.Cells[currentRow, 39].Value = repForm.Subsidy_DB;
                        _worksheet.Cells[currentRow, 40].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm18Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "индивидуальный номер (идентификационный код) партии ЖРО";
            _worksheet.Cells[1, 13].Value = "номер паспорта";
            _worksheet.Cells[1, 14].Value = "объем, куб.м";
            _worksheet.Cells[1, 15].Value = "масса, т";
            _worksheet.Cells[1, 16].Value = "солесодержание, г/л";
            _worksheet.Cells[1, 17].Value = "наименование радионуклида";
            _worksheet.Cells[1, 18].Value = "удельная активность, Бк/г";
            _worksheet.Cells[1, 19].Value = "вид";
            _worksheet.Cells[1, 20].Value = "номер";
            _worksheet.Cells[1, 21].Value = "дата";
            _worksheet.Cells[1, 22].Value = "поставщика или получателя";
            _worksheet.Cells[1, 23].Value = "перевозчика";
            _worksheet.Cells[1, 24].Value = "наименование";
            _worksheet.Cells[1, 25].Value = "код";
            _worksheet.Cells[1, 26].Value = "код";
            _worksheet.Cells[1, 27].Value = "статус";
            _worksheet.Cells[1, 28].Value = "объем, куб.м";
            _worksheet.Cells[1, 29].Value = "масса, т";
            _worksheet.Cells[1, 30].Value = "тритий";
            _worksheet.Cells[1, 31].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 32].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 33].Value = "трансурановые радионуклиды";
            _worksheet.Cells[1, 34].Value = "Код переработки/сортировки РАО";
            _worksheet.Cells[1, 35].Value = "Субсидия, %";
            _worksheet.Cells[1, 36].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8") && x.Rows18 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form18 repForm in rep.Rows18)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Name_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Sort_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.ActivityMeasurementDate_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.AggregateState_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }

        private void ExportForm19Data()
        {
            _getData?.Execute(1);
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Форма";
            _worksheet.Cells[1, 5].Value = "Дата начала периода";
            _worksheet.Cells[1, 6].Value = "Дата конца периода";
            _worksheet.Cells[1, 7].Value = "Номер корректировки";
            _worksheet.Cells[1, 8].Value = "Количество строк";
            _worksheet.Cells[1, 9].Value = "№ п/п";
            _worksheet.Cells[1, 10].Value = "код";
            _worksheet.Cells[1, 11].Value = "дата";
            _worksheet.Cells[1, 12].Value = "вид";
            _worksheet.Cells[1, 13].Value = "номер";
            _worksheet.Cells[1, 14].Value = "дата";
            _worksheet.Cells[1, 15].Value = "Код типа объектов учета";
            _worksheet.Cells[1, 16].Value = "радионуклиды";
            _worksheet.Cells[1, 17].Value = "активность, Бк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9") && x.Rows19 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form19 repForm in rep.Rows19)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        _worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        _worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        _worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.Name_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.Sort_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.ActivityMeasurementDate_DB;
                        _worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        _worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        _worksheet.Cells[currentRow, 20].Value = repForm.AggregateState_DB;
                        _worksheet.Cells[currentRow, 21].Value = repForm.PropertyCode_DB;
                        _worksheet.Cells[currentRow, 22].Value = repForm.Owner_DB;
                        _worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        _worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        _worksheet.Cells[currentRow, 28].Value = repForm.PackName_DB;
                        _worksheet.Cells[currentRow, 29].Value = repForm.PackType_DB;
                        _worksheet.Cells[currentRow, 30].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                }
            }
        }


    }
}
