using DatabaseAnalysis.WPF.DBAPIFactory;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
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
    public class ExportExcelAsyncCommand : AsyncBaseCommand
    {
        private readonly Navigator _navigator;
        private ExcelWorksheet worksheet { get; set; }
        private ExcelWorksheet worksheetComment { get; set; }
        private MainWindowViewModel _mainWindowViewModel { get; set; }

        public ExportExcelAsyncCommand(Navigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override async Task AsyncExecute(object? parameter)
        {
            if (parameter.ToString()[0].Equals('1'))
            {
                StaticConfiguration.TpmDb = "OPER";
            }
            else if (parameter.ToString()[0].Equals('2'))
            {
                StaticConfiguration.TpmDb = "YEAR";
            }
            try
            {
                var getReportsTask = Task.Factory.StartNew(async () => await ReportsStorge.GetAllReports(null, _mainWindowViewModel));
                await getReportsTask;
            }
            catch (Exception ex)
            {
                string msg;
                #region MessageException
                MessageBox.Show(
                    msg = $"Не удалось получить список организаций оперативной отчетности, экспорт данных в Excel не выполнен.",
                    "Ошибка при получении данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                #endregion
                ServiceExtension.LoggerManager.Warning($"{msg}{Environment.NewLine}{ex}");
                return;
            }

            if (ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals(parameter)).Count() != 0 || parameter.ToString().Length == 1)
            {
                SaveFileDialog saveFileDialog = new() { Filter = "Excel | *.xlsx" };
                bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
                if (saveExcel)
                {

                    _mainWindowViewModel.CloseButtonVisible = Visibility.Visible;
                    _mainWindowViewModel.ValueBarStatus = $"Идёт выгрузка форм {parameter} ";

                    try
                    {
                        var myTask = Task.Factory.StartNew(async () => await ReportsStorge.FillEmptyReports(parameter, _mainWindowViewModel));
                        await myTask;
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        #region MessageException
                        MessageBox.Show(
                            msg = $"Не удалось получить список отчетов по форме {parameter}, экспорт данных в Excel не выполнен.",
                            "Ошибка при получении данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        #endregion
                        ServiceExtension.LoggerManager.Warning($"{msg}{Environment.NewLine}{ex}");
                        return;
                    }
                    if (!ReportsStorge.cancellationToken.IsCancellationRequested)
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
                        using (ExcelPackage excelPackage = new(new FileInfo(path)))
                        {
                            excelPackage.Workbook.Properties.Author = "RAO_APP";
                            excelPackage.Workbook.Properties.Title = $"ReportsByForm_{parameter}";
                            excelPackage.Workbook.Properties.Created = DateTime.Now;
                            worksheet = excelPackage.Workbook.Worksheets.Add($"Список всех форм {parameter}");
                            #region SwitchForm
                            switch (parameter)
                            {
                                case "1":
                                    ExportForm1Data();
                                    break;
                                case "1.1":
                                    worksheetComment = excelPackage.Workbook.Worksheets.Add($"Примечания");
                                    ExportForm11Data();
                                    break;
                                case "1.2":
                                    ExportForm12Data();
                                    break;
                                case "1.3":
                                    ExportForm13Data();
                                    break;
                                case "1.4":
                                    ExportForm14Data();
                                    break;
                                case "1.5":
                                    ExportForm15Data();
                                    break;
                                case "1.6":
                                    ExportForm16Data();
                                    break;
                                case "1.7":
                                    ExportForm17Data();
                                    break;
                                case "1.8":
                                    ExportForm18Data();
                                    break;
                                case "1.9":
                                    ExportForm19Data();
                                    break;
                                case "2":
                                    ExportForm2Data();
                                    break;
                                case "2.1":
                                    ExportForm21Data();
                                    break;
                                case "2.2":
                                    ExportForm22Data();
                                    break;
                                case "2.3":
                                    ExportForm23Data();
                                    break;
                                case "2.4":
                                    ExportForm24Data();
                                    break;
                                case "2.5":
                                    ExportForm25Data();
                                    break;
                                case "2.6":
                                    ExportForm26Data();
                                    break;
                                case "2.7":
                                    ExportForm27Data();
                                    break;
                                case "2.8":
                                    ExportForm28Data();
                                    break;
                                case "2.9":
                                    ExportForm29Data();
                                    break;
                                case "2.10":
                                    ExportForm210Data();
                                    break;
                                case "2.11":
                                    ExportForm211Data();
                                    break;
                                case "2.12":
                                    ExportForm212Data();
                                    break;
                                default:
                                    try
                                    {
                                        throw new Exception();
                                    }
                                    catch (Exception)
                                    {
                                        #region MessageWrongParam
                                        MessageBox.Show(
                                            $"Не удалось сохранить файл по указанному пути. Форма {parameter} отстутствует в списке форм.",
                                            "Ошибка при сохранении файла",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                                        #endregion
                                        return;
                                    }
                            }
                            #endregion
                            try
                            {
                                excelPackage.Save();
                                #region MessageOpenExcel
                                string messageBoxText = $"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}. Вы хотите её открыть?";
                                string caption = "Выгрузка данных";
                                MessageBoxButton button = MessageBoxButton.YesNo;
                                MessageBoxImage icon = MessageBoxImage.Information;
                                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                                if (result == MessageBoxResult.Yes)
                                    Process.Start("explorer.exe", path);
                                #endregion
                                ServiceExtension.LoggerManager.Info($"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}.");
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
                            finally
                            {
                                _mainWindowViewModel.CloseButtonVisible = Visibility.Hidden;
                                _mainWindowViewModel.ValueBarVisible = Visibility.Hidden;
                            }
                        }
                    }
                    else
                    {
                        ReportsStorge._cancellationTokenSource.Dispose();
                        ReportsStorge._cancellationTokenSource = new();
                        ReportsStorge.cancellationToken = ReportsStorge._cancellationTokenSource.Token;
                        _mainWindowViewModel.ValueBar = 100;
                        _mainWindowViewModel.CloseButtonVisible = Visibility.Hidden;
                        _mainWindowViewModel.ValueBarVisible = Visibility.Hidden;
                    }
                }
            }
            else
            {
                string msg;
                #region MessageFormMissing
                MessageBoxResult result = MessageBox.Show(
                    msg = $"Выгрузка \"Всех форм {parameter}\" не выполнена, формы {parameter} отсутствуют в базе.",
                    "Выгрузка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                #endregion
                ServiceExtension.LoggerManager.Warning(msg);
            }
        }
        #region ExportForm
        #region ExportForm_1
        private void ExportForm1Data()
        {
            worksheet.Cells[1, 2].Value = "ОКПО";
            worksheet.Cells[1, 1].Value = "Рег.№";
            worksheet.Cells[1, 3].Value = "Форма";
            worksheet.Cells[1, 4].Value = "Дата начала";
            worksheet.Cells[1, 5].Value = "Дата конца";
            worksheet.Cells[1, 6].Value = "Номер кор.";
            worksheet.Cells[1, 7].Value = "Колличество строк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {

                foreach (FireBird.Report rep in reps.Report_Collection)
                {
                    worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                    worksheet.Cells[currentRow, 2].Value = reps.Master.OkpoRep.Value;
                    worksheet.Cells[currentRow, 3].Value = rep.FormNum_DB;
                    worksheet.Cells[currentRow, 4].Value = rep.StartPeriod_DB;
                    worksheet.Cells[currentRow, 5].Value = rep.EndPeriod_DB;
                    worksheet.Cells[currentRow, 6].Value = rep.CorrectionNumber_DB;
                    worksheet.Cells[currentRow, 7].Value = rep.Rows.Count;
                    currentRow++;
                }
            }
            for (int i = 1; i <= 7; i++)
            {
                worksheet.Column(i).AutoFit();
            }
        }
        #endregion

        #region ExportForm_11
        private void ExportForm11Data()
        {
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
            worksheet.Cells[1, 12].Value = "номер паспорта (сертификата)";
            worksheet.Cells[1, 13].Value = "тип";
            worksheet.Cells[1, 14].Value = "радионуклиды";
            worksheet.Cells[1, 15].Value = "номер";
            worksheet.Cells[1, 16].Value = "количество, шт";
            worksheet.Cells[1, 17].Value = "суммарная активность, Бк";
            worksheet.Cells[1, 18].Value = "код ОКПО изготовителя";
            worksheet.Cells[1, 19].Value = "дата выпуска";
            worksheet.Cells[1, 20].Value = "категория";
            worksheet.Cells[1, 21].Value = "НСС, мес";
            worksheet.Cells[1, 22].Value = "код формы собственности";
            worksheet.Cells[1, 23].Value = "код ОКПО правообладателя";
            worksheet.Cells[1, 24].Value = "вид";
            worksheet.Cells[1, 25].Value = "номер";
            worksheet.Cells[1, 26].Value = "дата";
            worksheet.Cells[1, 27].Value = "поставщика или получателя";
            worksheet.Cells[1, 28].Value = "перевозчика";
            worksheet.Cells[1, 29].Value = "наименование";
            worksheet.Cells[1, 30].Value = "тип";
            worksheet.Cells[1, 31].Value = "номер";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.1") && x.Rows11 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form11 repForm in rep.Rows11)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.Quantity_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.CreatorOKPO_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.CreationDate_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.Category_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.SignedServicePeriod_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.PropertyCode_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.Owner_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 31].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_12
        private void ExportForm12Data()
        {
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
            worksheet.Cells[1, 12].Value = "номер паспорта";
            worksheet.Cells[1, 13].Value = "наименование";
            worksheet.Cells[1, 14].Value = "номер";
            worksheet.Cells[1, 15].Value = "масса объединенного урана, кг";
            worksheet.Cells[1, 16].Value = "код ОКПО изготовителя";
            worksheet.Cells[1, 17].Value = "дата выпуска";
            worksheet.Cells[1, 18].Value = "НСС, мес";
            worksheet.Cells[1, 19].Value = "код формы собственности";
            worksheet.Cells[1, 20].Value = "код ОКПО правообладателя";
            worksheet.Cells[1, 21].Value = "вид";
            worksheet.Cells[1, 22].Value = "номер";
            worksheet.Cells[1, 23].Value = "дата";
            worksheet.Cells[1, 24].Value = "поставщика или получателя";
            worksheet.Cells[1, 25].Value = "перевозчика";
            worksheet.Cells[1, 26].Value = "наименование";
            worksheet.Cells[1, 27].Value = "тип";
            worksheet.Cells[1, 28].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.2") && x.Rows12 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form12 repForm in rep.Rows12)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.NameIOU_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.FactoryNumber_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.Mass_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.CreatorOKPO_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.CreationDate_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.SignedServicePeriod_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.PropertyCode_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.Owner_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_13
        private void ExportForm13Data()
        {
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
            worksheet.Cells[1, 12].Value = "номер паспорта";
            worksheet.Cells[1, 13].Value = "тип";
            worksheet.Cells[1, 14].Value = "радионуклиды";
            worksheet.Cells[1, 15].Value = "номер";
            worksheet.Cells[1, 16].Value = "активность, Бк";
            worksheet.Cells[1, 17].Value = "код ОКПО изготовителя";
            worksheet.Cells[1, 18].Value = "дата выпуска";
            worksheet.Cells[1, 19].Value = "агрегатное состояние";
            worksheet.Cells[1, 20].Value = "код формы собственности";
            worksheet.Cells[1, 21].Value = "код ОКПО правообладателя";
            worksheet.Cells[1, 22].Value = "вид";
            worksheet.Cells[1, 23].Value = "номер";
            worksheet.Cells[1, 24].Value = "дата";
            worksheet.Cells[1, 25].Value = "поставщика или получателя";
            worksheet.Cells[1, 26].Value = "перевозчика";
            worksheet.Cells[1, 27].Value = "наименование";
            worksheet.Cells[1, 28].Value = "тип";
            worksheet.Cells[1, 29].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.3") && x.Rows13 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form13 repForm in rep.Rows13)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.CreatorOKPO_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.CreationDate_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.AggregateState_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.PropertyCode_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.Owner_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_14
        private void ExportForm14Data()
        {
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
            worksheet.Cells[1, 12].Value = "номер паспорта";
            worksheet.Cells[1, 13].Value = "наименование";
            worksheet.Cells[1, 14].Value = "вид";
            worksheet.Cells[1, 15].Value = "радионуклиды";
            worksheet.Cells[1, 16].Value = "активность, Бк";
            worksheet.Cells[1, 17].Value = "дата измерения активности";
            worksheet.Cells[1, 18].Value = "объем, куб.м";
            worksheet.Cells[1, 19].Value = "масса, кг";
            worksheet.Cells[1, 20].Value = "агрегатное состояние";
            worksheet.Cells[1, 21].Value = "код формы собственности";
            worksheet.Cells[1, 22].Value = "код ОКПО правообладателя";
            worksheet.Cells[1, 23].Value = "вид";
            worksheet.Cells[1, 24].Value = "номер";
            worksheet.Cells[1, 25].Value = "дата";
            worksheet.Cells[1, 26].Value = "поставщика или получателя";
            worksheet.Cells[1, 27].Value = "перевозчика";
            worksheet.Cells[1, 28].Value = "наименование";
            worksheet.Cells[1, 29].Value = "тип";
            worksheet.Cells[1, 30].Value = "номер";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.4") && x.Rows14 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form14 repForm in rep.Rows14)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.Name_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Sort_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.Activity_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.ActivityMeasurementDate_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.AggregateState_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.PropertyCode_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.Owner_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.PackNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_15
        private void ExportForm15Data()
        {
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
            worksheet.Cells[1, 12].Value = "номер паспорта (сертификата) Эри, акта определения характеристик ОЗИИ";
            worksheet.Cells[1, 13].Value = "тип";
            worksheet.Cells[1, 14].Value = "радионуклиды";
            worksheet.Cells[1, 15].Value = "номер";
            worksheet.Cells[1, 16].Value = "количество, шт";
            worksheet.Cells[1, 17].Value = "суммарная активность, Бк";
            worksheet.Cells[1, 18].Value = "дата выпуска";
            worksheet.Cells[1, 19].Value = "статус РАО";
            worksheet.Cells[1, 20].Value = "вид";
            worksheet.Cells[1, 21].Value = "номер";
            worksheet.Cells[1, 22].Value = "дата";
            worksheet.Cells[1, 23].Value = "поставщика или получателя";
            worksheet.Cells[1, 24].Value = "перевозчика";
            worksheet.Cells[1, 25].Value = "наименование";
            worksheet.Cells[1, 26].Value = "тип";
            worksheet.Cells[1, 27].Value = "заводской номер";
            worksheet.Cells[1, 28].Value = "наименование";
            worksheet.Cells[1, 29].Value = "код";
            worksheet.Cells[1, 30].Value = "Код переработки / сортировки РАО";
            worksheet.Cells[1, 31].Value = "Субсидия, %";
            worksheet.Cells[1, 32].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.5") && x.Rows15 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form15 repForm in rep.Rows15)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.Type_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.FactoryNumber_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.Quantity_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.CreationDate_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.StatusRAO_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.PackNumber_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.RefineOrSortRAOCode_DB;
                        worksheet.Cells[currentRow, 31].Value = repForm.Subsidy_DB;
                        worksheet.Cells[currentRow, 32].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_16
        private void ExportForm16Data()
        {
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
            worksheet.Cells[1, 12].Value = "Код РАО";
            worksheet.Cells[1, 13].Value = "Статус РАО";
            worksheet.Cells[1, 14].Value = "объем без упаковки, куб.";
            worksheet.Cells[1, 15].Value = "масса без упаковки";
            worksheet.Cells[1, 16].Value = "количество ОЗИИИ";
            worksheet.Cells[1, 17].Value = "Основные радионуклиды";
            worksheet.Cells[1, 18].Value = "тритий";
            worksheet.Cells[1, 19].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 20].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 21].Value = "траансурановые радионуклиды";
            worksheet.Cells[1, 22].Value = "Дата измерения активности";
            worksheet.Cells[1, 23].Value = "вид";
            worksheet.Cells[1, 24].Value = "номер";
            worksheet.Cells[1, 25].Value = "дата";
            worksheet.Cells[1, 26].Value = "поставщика или получателя";
            worksheet.Cells[1, 27].Value = "перевозчика";
            worksheet.Cells[1, 28].Value = "наименование";
            worksheet.Cells[1, 29].Value = "код";
            worksheet.Cells[1, 30].Value = "Код переработки /";
            worksheet.Cells[1, 31].Value = "наименование";
            worksheet.Cells[1, 32].Value = "тип";
            worksheet.Cells[1, 33].Value = "номер упаковки";
            worksheet.Cells[1, 34].Value = "Субсидия, %";
            worksheet.Cells[1, 35].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.6") && x.Rows16 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form16 repForm in rep.Rows16)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.CodeRAO_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.StatusRAO_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Volume_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.Mass_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.QuantityOZIII_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.MainRadionuclids_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.TritiumActivity_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.BetaGammaActivity_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.AlphaActivity_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.TransuraniumActivity_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.ActivityMeasurementDate_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.RefineOrSortRAOCode_DB;
                        worksheet.Cells[currentRow, 31].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 32].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 33].Value = repForm.PackNumber_DB;
                        worksheet.Cells[currentRow, 34].Value = repForm.Subsidy_DB;
                        worksheet.Cells[currentRow, 35].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_17
        private void ExportForm17Data()
        {
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
            worksheet.Cells[1, 12].Value = "наименование";
            worksheet.Cells[1, 13].Value = "тип";
            worksheet.Cells[1, 14].Value = "заводской номер";
            worksheet.Cells[1, 15].Value = "номер упаковки (идентификационный код)";
            worksheet.Cells[1, 16].Value = "дата формирования";
            worksheet.Cells[1, 17].Value = "номер паспорта";
            worksheet.Cells[1, 18].Value = "объем, куб.м";
            worksheet.Cells[1, 19].Value = "масса брутто, т";
            worksheet.Cells[1, 20].Value = "наименования радионуклида";
            worksheet.Cells[1, 21].Value = "удельная активность, Бк/г";
            worksheet.Cells[1, 22].Value = "вид";
            worksheet.Cells[1, 23].Value = "номер";
            worksheet.Cells[1, 24].Value = "дата";
            worksheet.Cells[1, 25].Value = "поставщика или получателя";
            worksheet.Cells[1, 26].Value = "перевозчика";
            worksheet.Cells[1, 27].Value = "наименование";
            worksheet.Cells[1, 28].Value = "код";
            worksheet.Cells[1, 29].Value = "код";
            worksheet.Cells[1, 30].Value = "статус";
            worksheet.Cells[1, 31].Value = "объем без упаковки, куб.м";
            worksheet.Cells[1, 32].Value = "масса без упаковки (нетто), т";
            worksheet.Cells[1, 33].Value = "количество ОЗИИИ, шт";
            worksheet.Cells[1, 34].Value = "тритий";
            worksheet.Cells[1, 35].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 36].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 37].Value = "трансурановые радионуклиды";
            worksheet.Cells[1, 38].Value = "Код переработки/сортировки РАО";
            worksheet.Cells[1, 39].Value = "Субсидия, %";
            worksheet.Cells[1, 40].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.7") && x.Rows17 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form17 repForm in rep.Rows17)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.PackFactoryNumber_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.PackFactoryNumber_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.FormingDate_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.Volume_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.Mass_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.SpecificActivity_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.CodeRAO_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.StatusRAO_DB;
                        worksheet.Cells[currentRow, 31].Value = repForm.VolumeOutOfPack_DB;
                        worksheet.Cells[currentRow, 32].Value = repForm.MassOutOfPack_DB;
                        worksheet.Cells[currentRow, 33].Value = repForm.Quantity_DB;
                        worksheet.Cells[currentRow, 34].Value = repForm.TritiumActivity_DB;
                        worksheet.Cells[currentRow, 35].Value = repForm.BetaGammaActivity_DB;
                        worksheet.Cells[currentRow, 36].Value = repForm.AlphaActivity_DB;
                        worksheet.Cells[currentRow, 37].Value = repForm.TransuraniumActivity_DB;
                        worksheet.Cells[currentRow, 38].Value = repForm.RefineOrSortRAOCode_DB;
                        worksheet.Cells[currentRow, 39].Value = repForm.Subsidy_DB;
                        worksheet.Cells[currentRow, 40].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_18
        private void ExportForm18Data()
        {
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
            worksheet.Cells[1, 12].Value = "индивидуальный номер (идентификационный код) партии ЖРО";
            worksheet.Cells[1, 13].Value = "номер паспорта";
            worksheet.Cells[1, 14].Value = "объем, куб.м";
            worksheet.Cells[1, 15].Value = "масса, т";
            worksheet.Cells[1, 16].Value = "солесодержание, г/л";
            worksheet.Cells[1, 17].Value = "наименование радионуклида";
            worksheet.Cells[1, 18].Value = "удельная активность, Бк/г";
            worksheet.Cells[1, 19].Value = "вид";
            worksheet.Cells[1, 20].Value = "номер";
            worksheet.Cells[1, 21].Value = "дата";
            worksheet.Cells[1, 22].Value = "поставщика или получателя";
            worksheet.Cells[1, 23].Value = "перевозчика";
            worksheet.Cells[1, 24].Value = "наименование";
            worksheet.Cells[1, 25].Value = "код";
            worksheet.Cells[1, 26].Value = "код";
            worksheet.Cells[1, 27].Value = "статус";
            worksheet.Cells[1, 28].Value = "объем, куб.м";
            worksheet.Cells[1, 29].Value = "масса, т";
            worksheet.Cells[1, 30].Value = "тритий";
            worksheet.Cells[1, 31].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 32].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 33].Value = "трансурановые радионуклиды";
            worksheet.Cells[1, 34].Value = "Код переработки/сортировки РАО";
            worksheet.Cells[1, 35].Value = "Субсидия, %";
            worksheet.Cells[1, 36].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.8") && x.Rows18 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form18 repForm in rep.Rows18)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.IndividualNumberZHRO_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.PassportNumber_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.Volume6_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.Mass7_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.SaltConcentration_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.SpecificActivity_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.ProviderOrRecieverOKPO_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.TransporterOKPO_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.CodeRAO_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.StatusRAO_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.Volume20_DB;
                        worksheet.Cells[currentRow, 29].Value = repForm.Mass21_DB;
                        worksheet.Cells[currentRow, 30].Value = repForm.TritiumActivity_DB;
                        worksheet.Cells[currentRow, 31].Value = repForm.BetaGammaActivity_DB;
                        worksheet.Cells[currentRow, 32].Value = repForm.AlphaActivity_DB;
                        worksheet.Cells[currentRow, 33].Value = repForm.TransuraniumActivity_DB;
                        worksheet.Cells[currentRow, 34].Value = repForm.RefineOrSortRAOCode_DB;
                        worksheet.Cells[currentRow, 35].Value = repForm.Subsidy_DB;
                        worksheet.Cells[currentRow, 36].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_19
        private void ExportForm19Data()
        {
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
            worksheet.Cells[1, 12].Value = "вид";
            worksheet.Cells[1, 13].Value = "номер";
            worksheet.Cells[1, 14].Value = "дата";
            worksheet.Cells[1, 15].Value = "Код типа объектов учета";
            worksheet.Cells[1, 16].Value = "радионуклиды";
            worksheet.Cells[1, 17].Value = "активность, Бк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("1.9") && x.Rows19 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form19 repForm in rep.Rows19)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.FormNum_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheet.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheet.Cells[currentRow, 7].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = rep.Rows.Count;
                        worksheet.Cells[currentRow, 9].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.OperationDate_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.DocumentVid_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.CodeTypeAccObject_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.StartPeriod_DB;
                        worksheetComment.Cells[currentRow, 6].Value = rep.EndPeriod_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 9].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_2
        private void ExportForm2Data()
        {
            worksheet.Cells[1, 1].Value = "Рег.№";
            worksheet.Cells[1, 2].Value = "ОКПО";
            worksheet.Cells[1, 3].Value = "Форма";
            worksheet.Cells[1, 4].Value = "Отчетный год";
            worksheet.Cells[1, 5].Value = "Номер кор.";
            worksheet.Cells[1, 6].Value = "Колличество строк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                foreach (FireBird.Report rep in reps.Report_Collection)
                {
                    worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                    worksheet.Cells[currentRow, 2].Value = reps.Master.OkpoRep.Value;
                    worksheet.Cells[currentRow, 3].Value = rep.FormNum_DB;
                    worksheet.Cells[currentRow, 4].Value = rep.Year_DB;
                    worksheet.Cells[currentRow, 5].Value = rep.CorrectionNumber_DB;
                    worksheet.Cells[currentRow, 6].Value = rep.Rows.Count;
                    currentRow++;
                }
            }
        }
        #endregion

        #region ExportForm_21
        private void ExportForm21Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "наименование";
            worksheet.Cells[1, 8].Value = "код";
            worksheet.Cells[1, 9].Value = "мощность куб.м/год";
            worksheet.Cells[1, 10].Value = "количество часов работы за год";
            worksheet.Cells[1, 11].Value = "код РАО";
            worksheet.Cells[1, 12].Value = "статус РАО";
            worksheet.Cells[1, 13].Value = "куб.м";
            worksheet.Cells[1, 14].Value = "т";
            worksheet.Cells[1, 15].Value = "ОЗИИИ, шт";
            worksheet.Cells[1, 16].Value = "тритий";
            worksheet.Cells[1, 17].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 18].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 19].Value = "трансурановые радионуклиды";
            worksheet.Cells[1, 20].Value = "код РАО";
            worksheet.Cells[1, 21].Value = "статус РАО";
            worksheet.Cells[1, 22].Value = "куб.м";
            worksheet.Cells[1, 23].Value = "т";
            worksheet.Cells[1, 24].Value = "ОЗИИИ, шт";
            worksheet.Cells[1, 25].Value = "тритий";
            worksheet.Cells[1, 26].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 27].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 28].Value = "трансурановые радионуклиды";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1") && x.Rows21 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form21 repForm in rep.Rows21)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.RefineMachineName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.MachineCode_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.MachinePower_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.NumberOfHoursPerYear_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.CodeRAOIn_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.StatusRAOIn_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.VolumeIn_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.MassIn_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.QuantityIn_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.TritiumActivityIn_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.BetaGammaActivityIn_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.AlphaActivityIn_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.TransuraniumActivityIn_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.CodeRAOout_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.StatusRAOout_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.VolumeOut_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.MassOut_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.QuantityOZIIIout_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.TritiumActivityOut_DB;
                        worksheet.Cells[currentRow, 26].Value = repForm.BetaGammaActivityOut_DB;
                        worksheet.Cells[currentRow, 27].Value = repForm.AlphaActivityOut_DB;
                        worksheet.Cells[currentRow, 28].Value = repForm.TransuraniumActivityOut_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_22
        private void ExportForm22Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "наименование";
            worksheet.Cells[1, 8].Value = "код";
            worksheet.Cells[1, 9].Value = "наименование";
            worksheet.Cells[1, 10].Value = "тип";
            worksheet.Cells[1, 11].Value = "количество, шт";
            worksheet.Cells[1, 12].Value = "код РАО";
            worksheet.Cells[1, 13].Value = "статус РАО";
            worksheet.Cells[1, 14].Value = "РАО без упаковки";
            worksheet.Cells[1, 15].Value = "РАО с упаковкой";
            worksheet.Cells[1, 16].Value = "РАО без упаковки (нетто)";
            worksheet.Cells[1, 17].Value = "РАО с упаковкой (брутто)";
            worksheet.Cells[1, 18].Value = "Количество ОЗИИИ, шт";
            worksheet.Cells[1, 19].Value = "тритий";
            worksheet.Cells[1, 20].Value = "бета-, гамма-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 21].Value = "альфа-излучающие радионуклиды (исключая";
            worksheet.Cells[1, 22].Value = "трансурановые радионуклиды";
            worksheet.Cells[1, 23].Value = "Основные радионуклиды";
            worksheet.Cells[1, 24].Value = "Субсидия, %";
            worksheet.Cells[1, 25].Value = "Номер мероприятия ФЦП";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2") && x.Rows22 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form22 repForm in rep.Rows22)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.PackName_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.PackType_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.PackQuantity_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.CodeRAO_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.StatusRAO_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.VolumeOutOfPack_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.VolumeInPack_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.MassOutOfPack_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.MassInPack_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.QuantityOZIII_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.TritiumActivity_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.BetaGammaActivity_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.AlphaActivity_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.TransuraniumActivity_DB;
                        worksheet.Cells[currentRow, 23].Value = repForm.MainRadionuclids_DB;
                        worksheet.Cells[currentRow, 24].Value = repForm.Subsidy_DB;
                        worksheet.Cells[currentRow, 25].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_23
        private void ExportForm23Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "наименование";
            worksheet.Cells[1, 8].Value = "код";
            worksheet.Cells[1, 9].Value = "проектный объем, куб.м";
            worksheet.Cells[1, 10].Value = "код РАО";
            worksheet.Cells[1, 11].Value = "объем, куб.м";
            worksheet.Cells[1, 12].Value = "масса, т";
            worksheet.Cells[1, 13].Value = "количество ОЗИИИ, шт";
            worksheet.Cells[1, 14].Value = "суммарная активность, Бк";
            worksheet.Cells[1, 15].Value = "номер";
            worksheet.Cells[1, 16].Value = "дата";
            worksheet.Cells[1, 17].Value = "срок действия";
            worksheet.Cells[1, 18].Value = "наименование документа";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3") && x.Rows23 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form23 repForm in rep.Rows23)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.ProjectVolume_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.CodeRAO_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.Volume_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.Mass_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.QuantityOZIII_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.SummaryActivity_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.DocumentNumber_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.DocumentDate_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.ExpirationDate_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.DocumentName_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_24
        private void ExportForm24Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Код ОЯТ";
            worksheet.Cells[1, 8].Value = "Номер мероприятия ФЦП";
            worksheet.Cells[1, 9].Value = "масса образованного, т";
            worksheet.Cells[1, 10].Value = "количество образованного, шт";
            worksheet.Cells[1, 11].Value = "масса поступивших от сторонних, т";
            worksheet.Cells[1, 12].Value = "количество поступивших от сторонних, шт";
            worksheet.Cells[1, 13].Value = "масса импортированных от сторонних, т";
            worksheet.Cells[1, 14].Value = "количество импортированных от сторонних, шт";
            worksheet.Cells[1, 15].Value = "масса учтенных по другим причинам, т";
            worksheet.Cells[1, 16].Value = "количество учтенных по другим причинам, шт";
            worksheet.Cells[1, 17].Value = "масса переданных сторонним, т";
            worksheet.Cells[1, 18].Value = "количество переданных сторонним, шт";
            worksheet.Cells[1, 19].Value = "масса переработанных, т";
            worksheet.Cells[1, 20].Value = "количество переработанных, шт";
            worksheet.Cells[1, 21].Value = "масса снятия с учета, т";
            worksheet.Cells[1, 22].Value = "количество снятых с учета, шт";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4") && x.Rows24 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form24 repForm in rep.Rows24)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.CodeOYAT_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.FcpNumber_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.MassCreated_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.QuantityCreated_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.MassFromAnothers_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.QuantityFromAnothers_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.MassFromAnothersImported_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.QuantityFromAnothersImported_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.MassAnotherReasons_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.QuantityAnotherReasons_DB;
                        worksheet.Cells[currentRow, 17].Value = repForm.MassTransferredToAnother_DB;
                        worksheet.Cells[currentRow, 18].Value = repForm.QuantityTransferredToAnother_DB;
                        worksheet.Cells[currentRow, 19].Value = repForm.MassRefined_DB;
                        worksheet.Cells[currentRow, 20].Value = repForm.QuantityRefined_DB;
                        worksheet.Cells[currentRow, 21].Value = repForm.MassRemovedFromAccount_DB;
                        worksheet.Cells[currentRow, 22].Value = repForm.QuantityRemovedFromAccount_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_25
        private void ExportForm25Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "наименование, номер";
            worksheet.Cells[1, 8].Value = "код";
            worksheet.Cells[1, 9].Value = "код ОЯТ";
            worksheet.Cells[1, 10].Value = "номер мероприятия ФЦП";
            worksheet.Cells[1, 11].Value = "топливо (нетто)";
            worksheet.Cells[1, 12].Value = "ОТВС(ТВЭЛ, выемной части реактора) брутто";
            worksheet.Cells[1, 13].Value = "количество, шт";
            worksheet.Cells[1, 14].Value = "альфа-излучающих нуклидов";
            worksheet.Cells[1, 15].Value = "бета-, гамма-излучающих нуклидов";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5") && x.Rows25 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form25 repForm in rep.Rows25)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.StoragePlaceName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.StoragePlaceCode_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.CodeOYAT_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.FcpNumber_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.FuelMass_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.CellMass_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.Quantity_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.AlphaActivity_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.BetaGammaActivity_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_26
        private void ExportForm26Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Номер наблюдательной скважины";
            worksheet.Cells[1, 8].Value = "Наименование зоны контроля";
            worksheet.Cells[1, 9].Value = "Предполагаемый источник поступления радиоактивных веществ";
            worksheet.Cells[1, 10].Value = "Расстояние от источника поступления радиоактивных веществ до наблюдательной скважины, м";
            worksheet.Cells[1, 11].Value = "Глубина отбора проб, м";
            worksheet.Cells[1, 12].Value = "Наименование радионуклида";
            worksheet.Cells[1, 13].Value = "Среднегодовое содержание радионуклида, Бк/кг";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6") && x.Rows26 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form26 repForm in rep.Rows26)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.ObservedSourceNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.ControlledAreaName_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.SupposedWasteSource_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.DistanceToWasteSource_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.TestDepth_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.RadionuclidName_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.AverageYearConcentration_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_27
        private void ExportForm27Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Наименование, номер источника выбросов";
            worksheet.Cells[1, 8].Value = "Наименование радионуклида";
            worksheet.Cells[1, 9].Value = "разрешенный";
            worksheet.Cells[1, 10].Value = "фактический";
            worksheet.Cells[1, 11].Value = "фактический";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7") && x.Rows27 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form27 repForm in rep.Rows27)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.ObservedSourceNumber_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.RadionuclidName_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.AllowedWasteValue_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.FactedWasteValue_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.WasteOutbreakPreviousYear_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_28
        private void ExportForm28Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Наименование, номер выпуска сточных вод";
            worksheet.Cells[1, 8].Value = "наименование";
            worksheet.Cells[1, 9].Value = "код типа документа";
            worksheet.Cells[1, 10].Value = "Наименование бассейнового округа";
            worksheet.Cells[1, 11].Value = "Допустимый объем водоотведения за год, тыс.куб.м";
            worksheet.Cells[1, 12].Value = "Отведено за отчетный период, тыс.куб.м";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8") && x.Rows28 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form28 repForm in rep.Rows28)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.WasteSourceName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.WasteRecieverName_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.RecieverTypeCode_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.PoolDistrictName_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.AllowedWasteRemovalVolume_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.RemovedWasteVolume_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_29
        private void ExportForm29Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Наименование, номер выпуска сточных вод";
            worksheet.Cells[1, 8].Value = "Наименование радионуклада";
            worksheet.Cells[1, 9].Value = "допустимая";
            worksheet.Cells[1, 10].Value = "фактическая";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9") && x.Rows29 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form29 repForm in rep.Rows29)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.WasteSourceName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.RadionuclidName_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.AllowedActivity_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.FactedActivity_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_210
        private void ExportForm210Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Наименование показателя";
            worksheet.Cells[1, 8].Value = "Наименование участка";
            worksheet.Cells[1, 9].Value = "Кадастровый номер участка";
            worksheet.Cells[1, 10].Value = "Код участка";
            worksheet.Cells[1, 11].Value = "Площадь загряжненной территории, кв.м";
            worksheet.Cells[1, 12].Value = "средняя";
            worksheet.Cells[1, 13].Value = "максимальная";
            worksheet.Cells[1, 14].Value = "альфа-узлучающие радионуклиды";
            worksheet.Cells[1, 15].Value = "бета-узлучающие радионуклиды";
            worksheet.Cells[1, 16].Value = "Номер мероприятия ФЦП";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10") && x.Rows210 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form210 repForm in rep.Rows210)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.IndicatorName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.PlotName_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.PlotKadastrNumber_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.PlotCode_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.InfectedArea_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.AvgGammaRaysDosePower_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.MaxGammaRaysDosePower_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.WasteDensityAlpha_DB;
                        worksheet.Cells[currentRow, 15].Value = repForm.WasteDensityBeta_DB;
                        worksheet.Cells[currentRow, 16].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_211
        private void ExportForm211Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Наименование участка";
            worksheet.Cells[1, 8].Value = "Кадастровый номер участка";
            worksheet.Cells[1, 9].Value = "Код участка";
            worksheet.Cells[1, 10].Value = "Площадь загрязненной территории, кв.м";
            worksheet.Cells[1, 11].Value = "Наименование радионуклидов";
            worksheet.Cells[1, 12].Value = "земельный участок";
            worksheet.Cells[1, 13].Value = "жидкая фаза";
            worksheet.Cells[1, 14].Value = "донные отложения";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11") && x.Rows211 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form211 repForm in rep.Rows211)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.PlotName_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.PlotKadastrNumber_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.PlotCode_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.InfectedArea_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 12].Value = repForm.SpecificActivityOfPlot_DB;
                        worksheet.Cells[currentRow, 13].Value = repForm.SpecificActivityOfLiquidPart_DB;
                        worksheet.Cells[currentRow, 14].Value = repForm.SpecificActivityOfDensePart_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_212
        private void ExportForm212Data()
        {
            worksheet.Cells[1, 1].Value = "Рег. №";
            worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            worksheet.Cells[1, 3].Value = "ОКПО";
            worksheet.Cells[1, 4].Value = "Номер корректировки";
            worksheet.Cells[1, 5].Value = "отчетный год";
            worksheet.Cells[1, 6].Value = "№ п/п";
            worksheet.Cells[1, 7].Value = "Код операции";
            worksheet.Cells[1, 8].Value = "Код типа объектов учета";
            worksheet.Cells[1, 9].Value = "радионуклиды";
            worksheet.Cells[1, 10].Value = "активность, Бк";
            worksheet.Cells[1, 11].Value = "ОКПО поставщика/получателя";

            worksheetComment.Cells[1, 1].Value = "ОКПО";
            worksheetComment.Cells[1, 2].Value = "Сокращенное наименование";
            worksheetComment.Cells[1, 3].Value = "Рег. №";
            worksheetComment.Cells[1, 4].Value = "Номер корректировки";
            worksheetComment.Cells[1, 5].Value = "Дата начала периода";
            worksheetComment.Cells[1, 6].Value = "Дата конца периода";
            worksheetComment.Cells[1, 7].Value = "№ строки";
            worksheetComment.Cells[1, 8].Value = "№ графы";
            worksheetComment.Cells[1, 9].Value = "Пояснение";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12") && x.Rows212 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form212 repForm in rep.Rows212)
                    {
                        worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        worksheet.Cells[currentRow, 2].Value = reps.Master.Rows20[0].ShortJurLico_DB;
                        worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        worksheet.Cells[currentRow, 7].Value = repForm.OperationCode_DB;
                        worksheet.Cells[currentRow, 8].Value = repForm.ObjectTypeCode_DB;
                        worksheet.Cells[currentRow, 9].Value = repForm.Radionuclids_DB;
                        worksheet.Cells[currentRow, 10].Value = repForm.Activity_DB;
                        worksheet.Cells[currentRow, 11].Value = repForm.ProviderOrRecieverOKPO_DB;
                        currentRow++;
                    }
                    currentRow = 2;
                    foreach (FireBird.Note comment in rep.Notes)
                    {
                        worksheetComment.Cells[currentRow, 1].Value = reps.Master.OkpoRep.Value;
                        worksheetComment.Cells[currentRow, 2].Value = reps.Master.ShortJurLicoRep.Value;
                        worksheetComment.Cells[currentRow, 3].Value = reps.Master.RegNoRep.Value;
                        worksheetComment.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        worksheetComment.Cells[currentRow, 5].Value = rep.Year_DB;
                        worksheetComment.Cells[currentRow, 6].Value = comment.RowNumber_DB;
                        worksheetComment.Cells[currentRow, 7].Value = comment.GraphNumber_DB;
                        worksheetComment.Cells[currentRow, 8].Value = comment.Comment_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}