﻿using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelReportAsyncCommand : AsyncBaseCommand
    {
        private ExcelWorksheet _worksheet { get; set; }

        public ExportExcelReportAsyncCommand() { }

        public override async Task AsyncExecute(object? parameter)
        {
            await ReportsStorge.GetReport(Convert.ToInt32(parameter));
            var rep = ReportsStorge.Local_Reports.Report_Collection.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
            var reps = ReportsStorge.Local_Reports.Reports_Collection.FirstOrDefault(x => x.Report_Collection.Contains(rep));
            SaveFileDialog saveFileDialog = new() { Filter = "Excel | *.xlsx" };
            bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (saveExcel)
            {
                string path = saveFileDialog.FileName;
                if (!path.EndsWith(".xlsx"))
                    path += ".xlsx";
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception ex)
                    {
                        #region MessageException
                        string msg;
                        MessageBox.Show(
                            msg = $"Не удалось сохранить файл по указанному пути. Файл с таким именем уже существует в этом расположении и используется другим процессом.",
                            "Ошибка при сохранении файла",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        ServiceExtension.LoggerManager.Warning($"{msg}{Environment.NewLine}{ex}");
                        return;
                    }
                }
                using (ExcelPackage excelPackege = new(new FileInfo(path)))
                {
                    excelPackege.Workbook.Properties.Author = "RAO_APP";
                    excelPackege.Workbook.Properties.Title = $"ReportId_{parameter}_{reps.Master_DB.ShortJurLicoRep}";
                    excelPackege.Workbook.Properties.Created = DateTime.Now;
                    _worksheet = excelPackege.Workbook.Worksheets.Add($"Отчеты по форме {rep.FormNum_DB} {reps.Master_DB.ShortJurLicoRep}");
                    switch (rep.FormNum_DB)
                    {
                        case "1.1":
                            ExportForm11Data(reps, rep);
                            break;
                        case "1.2":
                            ExportForm12Data(reps, rep);
                            break;
                        case "1.3":
                            ExportForm13Data(reps, rep);
                            break;
                        case "1.4":
                            ExportForm14Data(reps, rep);
                            break;
                        case "1.5":
                            ExportForm15Data(reps, rep);
                            break;
                        case "1.6":
                            ExportForm16Data(reps, rep);
                            break;
                        case "1.7":
                            ExportForm17Data(reps, rep);
                            break;
                        case "1.8":
                            ExportForm18Data(reps, rep);
                            break;
                        case "1.9":
                            ExportForm19Data(reps, rep);
                            break;
                        case "2.1":
                            ExportForm21Data(reps, rep);
                            break;
                        case "2.2":
                            ExportForm22Data(reps, rep);
                            break;
                        case "2.3":
                            ExportForm23Data(reps, rep);
                            break;
                        case "2.4":
                            ExportForm24Data(reps, rep);
                            break;
                        case "2.5":
                            ExportForm25Data(reps, rep);
                            break;
                        case "2.6":
                            ExportForm26Data(reps, rep);
                            break;
                        case "2.7":
                            ExportForm27Data(reps, rep);
                            break;
                        case "2.8":
                            ExportForm28Data(reps, rep);
                            break;
                        //case "2.9":
                        //    ExportForm29Data(reps, rep);
                        //    break;
                        //case "2.10":
                        //    ExportForm210Data(reps, rep);
                        //    break;
                        //case "2.11":
                        //    ExportForm211Data(reps, rep);
                        //    break;
                        //case "2.12":
                        //    ExportForm212Data(reps, rep);
                        //    break;
                        default:
                            try
                            {
                                throw new Exception();
                            }
                            catch (Exception)
                            {
                                #region MessageWrongParam
                                MessageBox.Show(
                                    $"Не удалось сохранить файл по указанному пути. Форма {rep.FormNum_DB} отстутствует в списке форм.",
                                    "Ошибка при сохранении файла",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                                #endregion
                                return;
                            }
                    }

                    try
                    {
                        excelPackege.Save();
                        #region MessageOpenExcel
                        string messageBoxText = $"Выгрузка \"Отчетов по форме {parameter} {reps.Master_DB.ShortJurLicoRep}\" сохранена по пути {path}. Вы хотите её открыть?";
                        string caption = "Выгрузка данных";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Information;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                        if (result == MessageBoxResult.Yes)
                            Process.Start("explorer.exe", path);
                        #endregion
                        ServiceExtension.LoggerManager.Info($"Выгрузка \"Отчетов по форме {parameter} {reps.Master_DB.ShortJurLicoRep}\" сохранена по пути {path}.");
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        #region MessageException
                        MessageBox.Show(
                            msg = $"Не удалось сохранить файл по указанному пути.",
                            "Ошибка при сохранении файла",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        ServiceExtension.LoggerManager.Warning($"{msg}{Environment.NewLine}{ex}");
                        return;
                    }
                }
            }
        }
        #region ExportForm
        #region ExportForm_11
        private void ExportForm11Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
            _worksheet.Cells[1, 12].Value = "номер паспорта (сертификата)";
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
        #endregion

        #region ExportForm_12
        private void ExportForm12Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_13
        private void ExportForm13Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_14
        private void ExportForm14Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_15
        private void ExportForm15Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_16
        private void ExportForm16Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_17
        private void ExportForm17Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
        #endregion

        #region ExportForm_18
        private void ExportForm18Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
                _worksheet.Cells[currentRow, 12].Value = repForm.IndividualNumberZHRO_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.PassportNumber_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.Volume6_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.Mass7_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.SaltConcentration_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.Radionuclids_DB;
                _worksheet.Cells[currentRow, 18].Value = repForm.SpecificActivity_DB;
                _worksheet.Cells[currentRow, 19].Value = repForm.DocumentVid_DB;
                _worksheet.Cells[currentRow, 20].Value = repForm.DocumentNumber_DB;
                _worksheet.Cells[currentRow, 21].Value = repForm.DocumentDate_DB;
                _worksheet.Cells[currentRow, 22].Value = repForm.ProviderOrRecieverOKPO_DB;
                _worksheet.Cells[currentRow, 23].Value = repForm.TransporterOKPO_DB;
                _worksheet.Cells[currentRow, 24].Value = repForm.StoragePlaceName_DB;
                _worksheet.Cells[currentRow, 25].Value = repForm.StoragePlaceCode_DB;
                _worksheet.Cells[currentRow, 26].Value = repForm.CodeRAO_DB;
                _worksheet.Cells[currentRow, 27].Value = repForm.StatusRAO_DB;
                _worksheet.Cells[currentRow, 28].Value = repForm.Volume20_DB;
                _worksheet.Cells[currentRow, 29].Value = repForm.Mass21_DB;
                _worksheet.Cells[currentRow, 30].Value = repForm.TritiumActivity_DB;
                _worksheet.Cells[currentRow, 31].Value = repForm.BetaGammaActivity_DB;
                _worksheet.Cells[currentRow, 32].Value = repForm.AlphaActivity_DB;
                _worksheet.Cells[currentRow, 33].Value = repForm.TransuraniumActivity_DB;
                _worksheet.Cells[currentRow, 34].Value = repForm.RefineOrSortRAOCode_DB;
                _worksheet.Cells[currentRow, 35].Value = repForm.Subsidy_DB;
                _worksheet.Cells[currentRow, 36].Value = repForm.FcpNumber_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_19
        private void ExportForm19Data(FireBird.Reports reps, FireBird.Report rep)
        {
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
                _worksheet.Cells[currentRow, 12].Value = repForm.DocumentVid_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.DocumentNumber_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.DocumentDate_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.CodeTypeAccObject_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.Radionuclids_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_21
        private void ExportForm21Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "наименование";
            _worksheet.Cells[1, 8].Value = "код";
            _worksheet.Cells[1, 9].Value = "мощность куб.м/год";
            _worksheet.Cells[1, 10].Value = "количество часов работы за год";
            _worksheet.Cells[1, 11].Value = "код РАО";
            _worksheet.Cells[1, 12].Value = "статус РАО";
            _worksheet.Cells[1, 13].Value = "куб.м";
            _worksheet.Cells[1, 14].Value = "т";
            _worksheet.Cells[1, 15].Value = "ОЗИИИ, шт";
            _worksheet.Cells[1, 16].Value = "тритий";
            _worksheet.Cells[1, 17].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 18].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 19].Value = "трансурановые радионуклиды";
            _worksheet.Cells[1, 20].Value = "код РАО";
            _worksheet.Cells[1, 21].Value = "статус РАО";
            _worksheet.Cells[1, 22].Value = "куб.м";
            _worksheet.Cells[1, 23].Value = "т";
            _worksheet.Cells[1, 24].Value = "ОЗИИИ, шт";
            _worksheet.Cells[1, 25].Value = "тритий";
            _worksheet.Cells[1, 26].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 27].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 28].Value = "трансурановые радионуклиды";

            int currentRow = 2;
            foreach (FireBird.Form21 repForm in rep.Rows21)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.RefineMachineName_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.MachineCode_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.MachinePower_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.NumberOfHoursPerYear_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.CodeRAOIn_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.StatusRAOIn_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.VolumeIn_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.MassIn_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.QuantityIn_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.TritiumActivityIn_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.BetaGammaActivityIn_DB;
                _worksheet.Cells[currentRow, 18].Value = repForm.AlphaActivityIn_DB;
                _worksheet.Cells[currentRow, 19].Value = repForm.TransuraniumActivityIn_DB;
                _worksheet.Cells[currentRow, 20].Value = repForm.CodeRAOout_DB;
                _worksheet.Cells[currentRow, 21].Value = repForm.StatusRAOout_DB;
                _worksheet.Cells[currentRow, 22].Value = repForm.VolumeOut_DB;
                _worksheet.Cells[currentRow, 23].Value = repForm.MassOut_DB;
                _worksheet.Cells[currentRow, 24].Value = repForm.QuantityOZIIIout_DB;
                _worksheet.Cells[currentRow, 25].Value = repForm.TritiumActivityOut_DB;
                _worksheet.Cells[currentRow, 26].Value = repForm.BetaGammaActivityOut_DB;
                _worksheet.Cells[currentRow, 27].Value = repForm.AlphaActivityOut_DB;
                _worksheet.Cells[currentRow, 28].Value = repForm.TransuraniumActivityOut_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_22
        private void ExportForm22Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "наименование";
            _worksheet.Cells[1, 8].Value = "код";
            _worksheet.Cells[1, 9].Value = "наименование";
            _worksheet.Cells[1, 10].Value = "тип";
            _worksheet.Cells[1, 11].Value = "количество, шт";
            _worksheet.Cells[1, 12].Value = "код РАО";
            _worksheet.Cells[1, 13].Value = "статус РАО";
            _worksheet.Cells[1, 14].Value = "РАО без упаковки";
            _worksheet.Cells[1, 15].Value = "РАО с упаковкой";
            _worksheet.Cells[1, 16].Value = "РАО без упаковки (нетто)";
            _worksheet.Cells[1, 17].Value = "РАО с упаковкой (брутто)";
            _worksheet.Cells[1, 18].Value = "Количество ОЗИИИ, шт";
            _worksheet.Cells[1, 19].Value = "тритий";
            _worksheet.Cells[1, 20].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 21].Value = "альфа-излучающие радионуклиды (исключая";
            _worksheet.Cells[1, 22].Value = "трансурановые радионуклиды";
            _worksheet.Cells[1, 23].Value = "Основные радионуклиды";
            _worksheet.Cells[1, 24].Value = "Субсидия, %";
            _worksheet.Cells[1, 25].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Form22 repForm in rep.Rows22)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.PackName_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.PackType_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.PackQuantity_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.CodeRAO_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.StatusRAO_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.VolumeOutOfPack_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.VolumeInPack_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.MassOutOfPack_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.MassInPack_DB;
                _worksheet.Cells[currentRow, 18].Value = repForm.QuantityOZIII_DB;
                _worksheet.Cells[currentRow, 19].Value = repForm.TritiumActivity_DB;
                _worksheet.Cells[currentRow, 20].Value = repForm.BetaGammaActivity_DB;
                _worksheet.Cells[currentRow, 21].Value = repForm.AlphaActivity_DB;
                _worksheet.Cells[currentRow, 22].Value = repForm.TransuraniumActivity_DB;
                _worksheet.Cells[currentRow, 23].Value = repForm.MainRadionuclids_DB;
                _worksheet.Cells[currentRow, 24].Value = repForm.Subsidy_DB;
                _worksheet.Cells[currentRow, 25].Value = repForm.FcpNumber_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_23
        private void ExportForm23Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "наименование";
            _worksheet.Cells[1, 8].Value = "код";
            _worksheet.Cells[1, 9].Value = "проектный объем, куб.м";
            _worksheet.Cells[1, 10].Value = "код РАО";
            _worksheet.Cells[1, 11].Value = "объем, куб.м";
            _worksheet.Cells[1, 12].Value = "масса, т";
            _worksheet.Cells[1, 13].Value = "количество ОЗИИИ, шт";
            _worksheet.Cells[1, 14].Value = "суммарная активность, Бк";
            _worksheet.Cells[1, 15].Value = "номер";
            _worksheet.Cells[1, 16].Value = "дата";
            _worksheet.Cells[1, 17].Value = "срок действия";
            _worksheet.Cells[1, 18].Value = "наименование документа";

            int currentRow = 2;
            foreach (FireBird.Form23 repForm in rep.Rows23)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.ProjectVolume_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.CodeRAO_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.Volume_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.Mass_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.QuantityOZIII_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.SummaryActivity_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.DocumentNumber_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.DocumentDate_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.ExpirationDate_DB;
                _worksheet.Cells[currentRow, 18].Value = repForm.DocumentName_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_24
        private void ExportForm24Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Код ОЯТ";
            _worksheet.Cells[1, 8].Value = "Номер мероприятия ФЦП";
            _worksheet.Cells[1, 9].Value = "масса образованного, т";
            _worksheet.Cells[1, 10].Value = "количество образованного, шт";
            _worksheet.Cells[1, 11].Value = "масса поступивших от сторонних, т";
            _worksheet.Cells[1, 12].Value = "количество поступивших от сторонних, шт";
            _worksheet.Cells[1, 13].Value = "масса импортированных от сторонних, т";
            _worksheet.Cells[1, 14].Value = "количество импортированных от сторонних, шт";
            _worksheet.Cells[1, 15].Value = "масса учтенных по другим причинам, т";
            _worksheet.Cells[1, 16].Value = "количество учтенных по другим причинам, шт";
            _worksheet.Cells[1, 17].Value = "масса переданных сторонним, т";
            _worksheet.Cells[1, 18].Value = "количество переданных сторонним, шт";
            _worksheet.Cells[1, 19].Value = "масса переработанных, т";
            _worksheet.Cells[1, 20].Value = "количество переработанных, шт";
            _worksheet.Cells[1, 21].Value = "масса снятия с учета, т";
            _worksheet.Cells[1, 22].Value = "количество снятых с учета, шт";

            int currentRow = 2;
            foreach (FireBird.Form24 repForm in rep.Rows24)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.CodeOYAT_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.FcpNumber_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.MassCreated_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.QuantityCreated_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.MassFromAnothers_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.QuantityFromAnothers_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.MassFromAnothersImported_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.QuantityFromAnothersImported_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.MassAnotherReasons_DB;
                _worksheet.Cells[currentRow, 16].Value = repForm.QuantityAnotherReasons_DB;
                _worksheet.Cells[currentRow, 17].Value = repForm.MassTransferredToAnother_DB;
                _worksheet.Cells[currentRow, 18].Value = repForm.QuantityTransferredToAnother_DB;
                _worksheet.Cells[currentRow, 19].Value = repForm.MassRefined_DB;
                _worksheet.Cells[currentRow, 20].Value = repForm.QuantityRefined_DB;
                _worksheet.Cells[currentRow, 21].Value = repForm.MassRemovedFromAccount_DB;
                _worksheet.Cells[currentRow, 22].Value = repForm.QuantityRemovedFromAccount_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_25
        private void ExportForm25Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "наименование, номер";
            _worksheet.Cells[1, 8].Value = "код";
            _worksheet.Cells[1, 9].Value = "код ОЯТ";
            _worksheet.Cells[1, 10].Value = "номер мероприятия ФЦП";
            _worksheet.Cells[1, 11].Value = "топливо (нетто)";
            _worksheet.Cells[1, 12].Value = "ОТВС(ТВЭЛ, выемной части реактора) брутто";
            _worksheet.Cells[1, 13].Value = "количество, шт";
            _worksheet.Cells[1, 14].Value = "альфа-излучающих нуклидов";
            _worksheet.Cells[1, 15].Value = "бета-, гамма-излучающих нуклидов";

            int currentRow = 2;
            foreach (FireBird.Form25 repForm in rep.Rows25)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.CodeOYAT_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.FcpNumber_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.FuelMass_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.CellMass_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.Quantity_DB;
                _worksheet.Cells[currentRow, 14].Value = repForm.AlphaActivity_DB;
                _worksheet.Cells[currentRow, 15].Value = repForm.BetaGammaActivity_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_26
        private void ExportForm26Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Номер наблюдательной скважины";
            _worksheet.Cells[1, 8].Value = "Наименование зоны контроля";
            _worksheet.Cells[1, 9].Value = "Предполагаемый источник поступления радиоактивных веществ";
            _worksheet.Cells[1, 10].Value = "Расстояние от источника поступления радиоактивных веществ до наблюдательной скважины, м";
            _worksheet.Cells[1, 11].Value = "Глубина отбора проб, м";
            _worksheet.Cells[1, 12].Value = "Наименование радионуклида";
            _worksheet.Cells[1, 13].Value = "Среднегодовое содержание радионуклида, Бк/кг";

            int currentRow = 2;
            foreach (FireBird.Form26 repForm in rep.Rows26)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.ObservedSourceNumber_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.ControlledAreaName_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.SupposedWasteSource_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.DistanceToWasteSource_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.TestDepth_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.RadionuclidName_DB;
                _worksheet.Cells[currentRow, 13].Value = repForm.AverageYearConcentration_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_27
        private void ExportForm27Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Наименование, номер источника выбросов";
            _worksheet.Cells[1, 8].Value = "Наименование радионуклида";
            _worksheet.Cells[1, 9].Value = "разрешенный";
            _worksheet.Cells[1, 10].Value = "фактический";
            _worksheet.Cells[1, 11].Value = "фактический";

            int currentRow = 2;
            foreach (FireBird.Form27 repForm in rep.Rows27)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.ObservedSourceNumber_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.RadionuclidName_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.AllowedWasteValue_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.FactedWasteValue_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.WasteOutbreakPreviousYear_DB;
                currentRow++;
            }
        }
        #endregion

        #region ExportForm_28
        private void ExportForm28Data(FireBird.Reports reps, FireBird.Report rep)
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Наименование, номер выпуска сточных вод";
            _worksheet.Cells[1, 8].Value = "наименование";
            _worksheet.Cells[1, 9].Value = "код типа документа";
            _worksheet.Cells[1, 10].Value = "Наименование бассейнового округа";
            _worksheet.Cells[1, 11].Value = "Допустимый объем водоотведения за год, тыс.куб.м";
            _worksheet.Cells[1, 12].Value = "Отведено за отчетный период, тыс.куб.м";

            int currentRow = 2;
            foreach (FireBird.Form28 repForm in rep.Rows28)
            {
                _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                _worksheet.Cells[currentRow, 7].Value = repForm.WasteSourceName_DB;
                _worksheet.Cells[currentRow, 8].Value = repForm.WasteRecieverName_DB;
                _worksheet.Cells[currentRow, 9].Value = repForm.RecieverTypeCode_DB;
                _worksheet.Cells[currentRow, 10].Value = repForm.PoolDistrictName_DB;
                _worksheet.Cells[currentRow, 11].Value = repForm.AllowedWasteRemovalVolume_DB;
                _worksheet.Cells[currentRow, 12].Value = repForm.RemovedWasteVolume_DB;
                currentRow++;
            }
        }
        #endregion
        #endregion
    }
}