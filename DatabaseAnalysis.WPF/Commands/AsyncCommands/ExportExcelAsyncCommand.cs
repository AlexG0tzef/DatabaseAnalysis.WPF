using DatabaseAnalysis.WPF.MVVM.ViewModels;
using DatabaseAnalysis.WPF.State.Navigation;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace DatabaseAnalysis.WPF.Commands.AsyncCommands
{
    public class ExportExcelAsyncCommand : AsyncBaseCommand
    {
        private readonly INavigator _navigator;

        private ExcelWorksheet _worksheet { get; set; }
        private MainWindowViewModel _mainWindowViewModel { get; set; }

        public ExportExcelAsyncCommand(INavigator navigator, MainWindowViewModel mainWindowViewModel)
        {
            _navigator = navigator;
            _mainWindowViewModel = mainWindowViewModel;

        }
        public override async Task AsyncExecute(object? parameter)
        {
            
            if (ReportsStorge.Local_Reports.Report_Collection.Where(x => x.FormNum_DB.Equals(parameter)).Count() != 0 || parameter.ToString().Length == 1)
            {
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Excel | *.xlsx";
                bool saveExcel = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;

                if (saveExcel)
                {
                    await ReportsStorge.GetDataReports(parameter, _mainWindowViewModel);
                    string path = saveFileDialog.FileName;
                    FileInfo fileInfo = new(path);
                    if (!path.EndsWith(".xlsx"))
                        path += ".xlsx";
                    if (File.Exists(path))
                        File.Delete(path);
                   
                    using (ExcelPackage excelPackege = new(fileInfo))
                    {
                        excelPackege.Workbook.Properties.Author = "RAO_APP";
                        excelPackege.Workbook.Properties.Title = $"ReportsByForm_{parameter}";
                        excelPackege.Workbook.Properties.Created = DateTime.Now;

                        #region SwitchForm
                        switch (parameter)
                        {
                            case "1":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1");
                                ExportForm1Data();
                                break;
                            case "1.1":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.1");
                                ExportForm11Data();
                                break;
                            case "1.2":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.2");
                                ExportForm12Data();
                                break;
                            case "1.3":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.3");
                                ExportForm13Data();
                                break;
                            case "1.4":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.4");
                                ExportForm14Data();
                                break;
                            case "1.5":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.5");
                                ExportForm15Data();
                                break;
                            case "1.6":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.6");
                                ExportForm16Data();
                                break;
                            case "1.7":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.7");
                                ExportForm17Data();
                                break;
                            case "1.8":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.8");
                                ExportForm18Data();
                                break;
                            case "1.9":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 1.9");
                                ExportForm19Data();
                                break;
                            case "2":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2");
                                ExportForm2Data();
                                break;
                            case "2.1":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.1");
                                ExportForm21Data();
                                break;
                            case "2.2":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.2");
                                ExportForm22Data();
                                break;
                            case "2.3":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.3");
                                ExportForm23Data();
                                break;
                            case "2.4":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.4");
                                ExportForm24Data();
                                break;
                            case "2.5":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.5");
                                ExportForm25Data();
                                break;
                            case "2.6":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.6");
                                ExportForm26Data();
                                break;
                            case "2.7":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.7");
                                ExportForm27Data();
                                break;
                            case "2.8":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.8");
                                ExportForm28Data();
                                break;
                            case "2.9":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.9");
                                ExportForm29Data();
                                break;
                            case "2.10":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.10");
                                ExportForm210Data();
                                break;
                            case "2.11":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.11");
                                ExportForm211Data();
                                break;
                            case "2.12":
                                _worksheet = excelPackege.Workbook.Worksheets.Add("Список всех форм 2.12");
                                ExportForm212Data();
                                break;
                            default:
                                break;
                        }
                        #endregion
                        excelPackege.Save();

                        string messageBoxText = $"Выгрузка \"Всех форм {parameter}\" сохранена по пути {path}. Вы хотите её открыть?";
                        string caption = "Выгрузка данных";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Information;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                        if (result == MessageBoxResult.Yes)
                            Process.Start("explorer.exe", path);
                    }

                }
            }
            else
            {
                string messageBoxText = $"Выгрузка \"Всех форм {parameter}\" не выполнена, формы {parameter} отсутствуют в базе.";
                string caption = "Выгрузка данных";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                _mainWindowViewModel.ValueBar = 100;
            }
        }

        #region ExportForm
        #region ExportForm_1
        private void ExportForm1Data()
        {
            _worksheet.Cells[1, 2].Value = "ОКПО";
            _worksheet.Cells[1, 1].Value = "Рег.№";
            _worksheet.Cells[1, 3].Value = "Форма";
            _worksheet.Cells[1, 4].Value = "Дата начала";
            _worksheet.Cells[1, 5].Value = "Дата конца";
            _worksheet.Cells[1, 6].Value = "Номер кор.";
            _worksheet.Cells[1, 7].Value = "Колличество строк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection10)
            {

                foreach (FireBird.Report rep in reps.Report_Collection)
                {
                    _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                    _worksheet.Cells[currentRow, 2].Value = reps.Master.OkpoRep.Value;
                    _worksheet.Cells[currentRow, 3].Value = rep.FormNum_DB;
                    _worksheet.Cells[currentRow, 4].Value = rep.StartPeriod_DB;
                    _worksheet.Cells[currentRow, 5].Value = rep.EndPeriod_DB;
                    _worksheet.Cells[currentRow, 6].Value = rep.CorrectionNumber_DB;
                    _worksheet.Cells[currentRow, 7].Value = rep.Rows.Count;
                    currentRow++;
                }
            }
        }
        #endregion

        #region ExportForm_11
        private void ExportForm11Data()
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
        #endregion

        #region ExportForm_12
        private void ExportForm12Data()
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
        #endregion

        #region ExportForm_13
        private void ExportForm13Data()
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
        #endregion

        #region ExportForm_14
        private void ExportForm14Data()
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
        #endregion

        #region ExportForm_15
        private void ExportForm15Data()
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
        #endregion

        #region ExportForm_16
        private void ExportForm16Data()
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
        #endregion

        #region ExportForm_17
        private void ExportForm17Data()
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
        #endregion

        #region ExportForm_18
        private void ExportForm18Data()
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
            }
        }
        #endregion

        #region ExportForm_19
        private void ExportForm19Data()
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
                        _worksheet.Cells[currentRow, 12].Value = repForm.DocumentVid_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.DocumentNumber_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.DocumentDate_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.CodeTypeAccObject_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 17].Value = repForm.Activity_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_2
        private void ExportForm2Data()
        {
            _worksheet.Cells[1, 1].Value = "Рег.№";
            _worksheet.Cells[1, 2].Value = "ОКПО";
            _worksheet.Cells[1, 3].Value = "Форма";
            _worksheet.Cells[1, 4].Value = "Отчетный год";
            _worksheet.Cells[1, 5].Value = "Номер кор.";
            _worksheet.Cells[1, 6].Value = "Колличество строк";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                foreach (FireBird.Report rep in reps.Report_Collection)
                {
                    _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                    _worksheet.Cells[currentRow, 2].Value = reps.Master.OkpoRep.Value;
                    _worksheet.Cells[currentRow, 3].Value = rep.FormNum_DB;
                    _worksheet.Cells[currentRow, 4].Value = rep.Year_DB;
                    _worksheet.Cells[currentRow, 5].Value = rep.CorrectionNumber_DB;
                    _worksheet.Cells[currentRow, 6].Value = rep.Rows.Count;
                    currentRow++;
                }
            }
        }
        #endregion

        #region ExportForm_21
        private void ExportForm21Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.1") && x.Rows21 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form21 repForm in rep.Rows21)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_22
        private void ExportForm22Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.2") && x.Rows22 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form22 repForm in rep.Rows22)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_23
        private void ExportForm23Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.3") && x.Rows23 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form23 repForm in rep.Rows23)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_24
        private void ExportForm24Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.4") && x.Rows24 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form24 repForm in rep.Rows24)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_25
        private void ExportForm25Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.5") && x.Rows25 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form25 repForm in rep.Rows25)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_26
        private void ExportForm26Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.6") && x.Rows26 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form26 repForm in rep.Rows26)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_27
        private void ExportForm27Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.7") && x.Rows27 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form27 repForm in rep.Rows27)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_28
        private void ExportForm28Data()
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
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.8") && x.Rows28 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form28 repForm in rep.Rows28)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
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
            }
        }
        #endregion

        #region ExportForm_29
        private void ExportForm29Data()
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Наименование, номер выпуска сточных вод";
            _worksheet.Cells[1, 8].Value = "Наименование радионуклада";
            _worksheet.Cells[1, 9].Value = "допустимая";
            _worksheet.Cells[1, 10].Value = "фактическая";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.9") && x.Rows29 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form29 repForm in rep.Rows29)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 7].Value = repForm.WasteSourceName_DB;
                        _worksheet.Cells[currentRow, 8].Value = repForm.RadionuclidName_DB;
                        _worksheet.Cells[currentRow, 9].Value = repForm.AllowedActivity_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.FactedActivity_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_210
        private void ExportForm210Data()
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Наименование показателя";
            _worksheet.Cells[1, 8].Value = "Наименование участка";
            _worksheet.Cells[1, 9].Value = "Кадастровый номер участка";
            _worksheet.Cells[1, 10].Value = "Код участка";
            _worksheet.Cells[1, 11].Value = "Площадь загряжненной территории, кв.м";
            _worksheet.Cells[1, 12].Value = "средняя";
            _worksheet.Cells[1, 13].Value = "максимальная";
            _worksheet.Cells[1, 14].Value = "альфа-узлучающие радионуклиды";
            _worksheet.Cells[1, 15].Value = "бета-узлучающие радионуклиды";
            _worksheet.Cells[1, 16].Value = "Номер мероприятия ФЦП";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.10") && x.Rows210 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form210 repForm in rep.Rows210)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 7].Value = repForm.IndicatorName_DB;
                        _worksheet.Cells[currentRow, 8].Value = repForm.PlotName_DB;
                        _worksheet.Cells[currentRow, 9].Value = repForm.PlotKadastrNumber_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.PlotCode_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.InfectedArea_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.AvgGammaRaysDosePower_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.MaxGammaRaysDosePower_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.WasteDensityAlpha_DB;
                        _worksheet.Cells[currentRow, 15].Value = repForm.WasteDensityBeta_DB;
                        _worksheet.Cells[currentRow, 16].Value = repForm.FcpNumber_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_211
        private void ExportForm211Data()
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Наименование участка";
            _worksheet.Cells[1, 8].Value = "Кадастровый номер участка";
            _worksheet.Cells[1, 9].Value = "Код участка";
            _worksheet.Cells[1, 10].Value = "Площадь загрязненной территории, кв.м";
            _worksheet.Cells[1, 11].Value = "Наименование радионуклидов";
            _worksheet.Cells[1, 12].Value = "земельный участок";
            _worksheet.Cells[1, 13].Value = "жидкая фаза";
            _worksheet.Cells[1, 14].Value = "донные отложения";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.11") && x.Rows211 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form211 repForm in rep.Rows211)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 7].Value = repForm.PlotName_DB;
                        _worksheet.Cells[currentRow, 8].Value = repForm.PlotKadastrNumber_DB;
                        _worksheet.Cells[currentRow, 9].Value = repForm.PlotCode_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.InfectedArea_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 12].Value = repForm.SpecificActivityOfPlot_DB;
                        _worksheet.Cells[currentRow, 13].Value = repForm.SpecificActivityOfLiquidPart_DB;
                        _worksheet.Cells[currentRow, 14].Value = repForm.SpecificActivityOfDensePart_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion

        #region ExportForm_212
        private void ExportForm212Data()
        {
            _worksheet.Cells[1, 1].Value = "Рег. №";
            _worksheet.Cells[1, 2].Value = "Сокращенное наименование";
            _worksheet.Cells[1, 3].Value = "ОКПО";
            _worksheet.Cells[1, 4].Value = "Номер корректировки";
            _worksheet.Cells[1, 5].Value = "отчетный год";
            _worksheet.Cells[1, 6].Value = "№ п/п";
            _worksheet.Cells[1, 7].Value = "Код операции";
            _worksheet.Cells[1, 8].Value = "Код типа объектов учета";
            _worksheet.Cells[1, 9].Value = "радионуклиды";
            _worksheet.Cells[1, 10].Value = "активность, Бк";
            _worksheet.Cells[1, 11].Value = "ОКПО поставщика/получателя";

            int currentRow = 2;
            foreach (FireBird.Reports reps in ReportsStorge.Local_Reports.Reports_Collection20)
            {
                var form = reps.Report_Collection.Where(x => x.FormNum_DB.Equals("2.12") && x.Rows212 != null);
                foreach (FireBird.Report rep in form)
                {
                    foreach (FireBird.Form212 repForm in rep.Rows212)
                    {
                        _worksheet.Cells[currentRow, 1].Value = reps.Master.RegNoRep.Value;
                        _worksheet.Cells[currentRow, 2].Value = reps.Master.Rows10[0].ShortJurLico_DB;
                        _worksheet.Cells[currentRow, 3].Value = reps.Master.OkpoRep.Value;
                        _worksheet.Cells[currentRow, 4].Value = rep.CorrectionNumber_DB;
                        _worksheet.Cells[currentRow, 5].Value = rep.Year_DB;
                        _worksheet.Cells[currentRow, 6].Value = repForm.NumberInOrder_DB;
                        _worksheet.Cells[currentRow, 7].Value = repForm.OperationCode_DB;
                        _worksheet.Cells[currentRow, 8].Value = repForm.ObjectTypeCode_DB;
                        _worksheet.Cells[currentRow, 9].Value = repForm.Radionuclids_DB;
                        _worksheet.Cells[currentRow, 10].Value = repForm.Activity_DB;
                        _worksheet.Cells[currentRow, 11].Value = repForm.ProviderOrRecieverOKPO_DB;
                        currentRow++;
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}