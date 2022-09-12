using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.InnerLogger;
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
    public class ExportExcelReportPrintAsyncCommand : AsyncBaseCommand
    {
        public override async Task AsyncExecute(object? parameter)
        {
            await ReportsStorage.GetReport(Convert.ToInt32(parameter));
            var rep = ReportsStorage.Local_Reports.Report_Collection.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
            var reps = ReportsStorage.Local_Reports.Reports_Collection.FirstOrDefault(x => x.Report_Collection.Contains(rep));
#if DEBUG
            string templatePath = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\")), Path.Combine("data", Path.Combine("Excel", rep.FormNum_DB + ".xlsx")));
#else
            string templatePath = Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), Path.Combine("data" , Path.Combine("Excel", rep.FormNum_DB + ".xlsx")));
#endif
            SaveFileDialog saveFileDialog = new() { Filter = "Excel | *.xlsx", FileName = $"" };
            bool saveFile = (bool)saveFileDialog.ShowDialog(Application.Current.MainWindow)!;
            if (saveFile)
            {
                string newPath = saveFileDialog.FileName;
                if (!newPath.EndsWith(".xlsx"))
                    newPath += ".xlsx";
                if (File.Exists(newPath))
                {
                    try
                    {
                        File.Delete(newPath);
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
                using (ExcelPackage excelPackage = new(new FileInfo(newPath), new FileInfo(templatePath)))
                {
                    excelPackage.Workbook.Properties.Author = "RAO_APP";
                    excelPackage.Workbook.Properties.Title = $"ReportByForm_{parameter}_Org_{reps.Master_DB.ShortJurLicoRep.Value}_Start_{rep.StartPeriod_DB}_End_{rep.EndPeriod_DB}";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    var worksheetTitle = excelPackage.Workbook.Worksheets[0];
                    var worksheetForm = excelPackage.Workbook.Worksheets[1];

                    ExcelPrintTitleExport(worksheetTitle, rep, reps);
                    ExcelPrintSubMainExport(worksheetForm, rep);
                    ExcelPrintNotesExport(worksheetForm, rep);
                    ExcelPrintRowsExport(worksheetForm, rep);
                    excelPackage.Save();
                    #region MessageOpenExcel
                    string messageBoxText = $"Выгрузка отчетов по форме {rep.FormNum_DB} {reps.Master_DB.ShortJurLicoRep.Value} " +
                        $"с {rep.StartPeriod_DB} по {rep.EndPeriod_DB}{Environment.NewLine}сохранена по пути {newPath}.{Environment.NewLine}Вы хотите её открыть?";
                    string caption = "Выгрузка данных";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Information;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    if (result == MessageBoxResult.Yes)
                        Process.Start("explorer.exe", newPath);
                    #endregion
                    ServiceExtension.LoggerManager.Info($"Выгрузка отчетов по форме {rep.FormNum_DB} {reps.Master_DB.ShortJurLicoRep.Value} " +
                        $"с {rep.StartPeriod_DB} по {rep.EndPeriod_DB}{Environment.NewLine}сохранена по пути {newPath}.");
                }
            }
        }
        #region TitleExport
        private void ExcelPrintTitleExport(ExcelWorksheet worksheet, Report rep, FireBird.Reports reps)
        {
            Report master = reps.Master_DB;

            if (rep.FormNum_DB.StartsWith("2"))
            {
                var frmYur = master.Rows20[0];
                var frmObosob = master.Rows20[1];
                worksheet.Cells["G10"].Value = rep.Year_DB;

                worksheet.Cells["F6"].Value = frmYur.RegNo_DB;
                worksheet.Cells["F15"].Value = frmYur.OrganUprav_DB;
                worksheet.Cells["F16"].Value = frmYur.SubjectRF_DB;
                worksheet.Cells["F17"].Value = frmYur.JurLico_DB;
                worksheet.Cells["F18"].Value = frmYur.ShortJurLico_DB;
                worksheet.Cells["F19"].Value = frmYur.JurLicoAddress_DB;
                worksheet.Cells["F20"].Value = frmYur.JurLicoFactAddress_DB;
                worksheet.Cells["F21"].Value = frmYur.GradeFIO_DB;
                worksheet.Cells["F22"].Value = frmYur.Telephone_DB;
                worksheet.Cells["F23"].Value = frmYur.Fax_DB;
                worksheet.Cells["F24"].Value = frmYur.Email_DB;

                worksheet.Cells["F25"].Value = frmObosob.SubjectRF_DB;
                worksheet.Cells["F26"].Value = frmObosob.JurLico_DB;
                worksheet.Cells["F27"].Value = frmObosob.ShortJurLico_DB;
                worksheet.Cells["F28"].Value = frmObosob.JurLicoAddress_DB;
                worksheet.Cells["F29"].Value = frmObosob.GradeFIO_DB;
                worksheet.Cells["F30"].Value = frmObosob.Telephone_DB;
                worksheet.Cells["F31"].Value = frmObosob.Fax_DB;
                worksheet.Cells["F32"].Value = frmObosob.Email_DB;

                worksheet.Cells["B36"].Value = frmYur.Okpo_DB;
                worksheet.Cells["C36"].Value = frmYur.Okved_DB;
                worksheet.Cells["D36"].Value = frmYur.Okogu_DB;
                worksheet.Cells["E36"].Value = frmYur.Oktmo_DB;
                worksheet.Cells["F36"].Value = frmYur.Inn_DB;
                worksheet.Cells["G36"].Value = frmYur.Kpp_DB;
                worksheet.Cells["H36"].Value = frmYur.Okopf_DB;
                worksheet.Cells["I36"].Value = frmYur.Okfs_DB;

                worksheet.Cells["B37"].Value = frmObosob.Okpo_DB;
                worksheet.Cells["C37"].Value = frmObosob.Okved_DB;
                worksheet.Cells["D37"].Value = frmObosob.Okogu_DB;
                worksheet.Cells["E37"].Value = frmObosob.Oktmo_DB;
                worksheet.Cells["F37"].Value = frmObosob.Inn_DB;
                worksheet.Cells["G37"].Value = frmObosob.Kpp_DB;
                worksheet.Cells["H37"].Value = frmObosob.Okopf_DB;
                worksheet.Cells["I37"].Value = frmObosob.Okfs_DB;
            }
            else
            {
                var frmYur = master.Rows10[0];
                var frmObosob = master.Rows10[1];

                worksheet.Cells["F6"].Value = frmYur.RegNo_DB;
                worksheet.Cells["F15"].Value = frmYur.OrganUprav_DB;
                worksheet.Cells["F16"].Value = frmYur.SubjectRF_DB;
                worksheet.Cells["F17"].Value = frmYur.JurLico_DB;
                worksheet.Cells["F18"].Value = frmYur.ShortJurLico_DB;
                worksheet.Cells["F19"].Value = frmYur.JurLicoAddress_DB;
                worksheet.Cells["F20"].Value = frmYur.JurLicoFactAddress_DB;
                worksheet.Cells["F21"].Value = frmYur.GradeFIO_DB;
                worksheet.Cells["F22"].Value = frmYur.Telephone_DB;
                worksheet.Cells["F23"].Value = frmYur.Fax_DB;
                worksheet.Cells["F24"].Value = frmYur.Email_DB;

                worksheet.Cells["F25"].Value = frmObosob.SubjectRF_DB;
                worksheet.Cells["F26"].Value = frmObosob.JurLico_DB;
                worksheet.Cells["F27"].Value = frmObosob.ShortJurLico_DB;
                worksheet.Cells["F28"].Value = frmObosob.JurLicoAddress_DB;
                worksheet.Cells["F29"].Value = frmObosob.GradeFIO_DB;
                worksheet.Cells["F30"].Value = frmObosob.Telephone_DB;
                worksheet.Cells["F31"].Value = frmObosob.Fax_DB;
                worksheet.Cells["F32"].Value = frmObosob.Email_DB;

                worksheet.Cells["B36"].Value = frmYur.Okpo_DB;
                worksheet.Cells["C36"].Value = frmYur.Okved_DB;
                worksheet.Cells["D36"].Value = frmYur.Okogu_DB;
                worksheet.Cells["E36"].Value = frmYur.Oktmo_DB;
                worksheet.Cells["F36"].Value = frmYur.Inn_DB;
                worksheet.Cells["G36"].Value = frmYur.Kpp_DB;
                worksheet.Cells["H36"].Value = frmYur.Okopf_DB;
                worksheet.Cells["I36"].Value = frmYur.Okfs_DB;

                worksheet.Cells["B37"].Value = frmObosob.Okpo_DB;
                worksheet.Cells["C37"].Value = frmObosob.Okved_DB;
                worksheet.Cells["D37"].Value = frmObosob.Okogu_DB;
                worksheet.Cells["E37"].Value = frmObosob.Oktmo_DB;
                worksheet.Cells["F37"].Value = frmObosob.Inn_DB;
                worksheet.Cells["G37"].Value = frmObosob.Kpp_DB;
                worksheet.Cells["H37"].Value = frmObosob.Okopf_DB;
                worksheet.Cells["I37"].Value = frmObosob.Okfs_DB;
            }
        }
        #endregion

        #region SubMainExport
        private void ExcelPrintSubMainExport(ExcelWorksheet worksheet, Report rep)
        {
            if (rep.FormNum_DB.StartsWith("1"))
            {
                worksheet.Cells["G3"].Value = rep.StartPeriod_DB;
                worksheet.Cells["G4"].Value = rep.EndPeriod_DB;
                worksheet.Cells["G5"].Value = rep.CorrectionNumber_DB;
            }
            else
            {
                switch (rep.FormNum_DB)
                {
                    case "2.6":
                        {
                            worksheet.Cells["G4"].Value = rep.CorrectionNumber_DB;
                            worksheet.Cells["G5"].Value = rep.SourcesQuantity26_DB;
                            break;
                        }
                    case "2.7":
                        {
                            worksheet.Cells["G3"].Value = rep.CorrectionNumber_DB;
                            worksheet.Cells["G4"].Value = rep.PermissionNumber27_DB;
                            worksheet.Cells["G5"].Value = rep.ValidBegin27_DB;
                            worksheet.Cells["J5"].Value = rep.ValidThru27_DB;
                            worksheet.Cells["G6"].Value = rep.PermissionDocumentName27_DB;
                            break;
                        }
                    case "2.8":
                        {
                            worksheet.Cells["G3"].Value = rep.CorrectionNumber_DB;
                            worksheet.Cells["G4"].Value = rep.PermissionNumber_28_DB;
                            worksheet.Cells["K4"].Value = rep.ValidBegin_28_DB;
                            worksheet.Cells["N4"].Value = rep.ValidThru_28_DB;
                            worksheet.Cells["G5"].Value = rep.PermissionDocumentName_28_DB;

                            worksheet.Cells["G6"].Value = rep.PermissionNumber1_28_DB;
                            worksheet.Cells["K6"].Value = rep.ValidBegin1_28_DB;
                            worksheet.Cells["N6"].Value = rep.ValidThru1_28_DB;
                            worksheet.Cells["G7"].Value = rep.PermissionDocumentName1_28_DB;

                            worksheet.Cells["G8"].Value = rep.ContractNumber_28_DB;
                            worksheet.Cells["K8"].Value = rep.ValidBegin2_28_DB;
                            worksheet.Cells["N8"].Value = rep.ValidThru2_28_DB;
                            worksheet.Cells["G9"].Value = rep.OrganisationReciever_28_DB;

                            worksheet.Cells["D21"].Value = rep.GradeExecutor_DB;
                            worksheet.Cells["F21"].Value = rep.FIOexecutor_DB;
                            worksheet.Cells["I21"].Value = rep.ExecPhone_DB;
                            worksheet.Cells["K21"].Value = rep.ExecEmail_DB;
                            return;
                        }
                    default:
                        {
                            worksheet.Cells["G4"].Value = rep.CorrectionNumber_DB;
                            break;
                        }
                }
            }
            worksheet.Cells["D18"].Value = rep.GradeExecutor_DB;
            worksheet.Cells["F18"].Value = rep.FIOexecutor_DB;
            worksheet.Cells["I18"].Value = rep.ExecPhone_DB;
            worksheet.Cells["K18"].Value = rep.ExecEmail_DB;
        }
        #endregion

        #region NotesExport
        private void ExcelPrintNotesExport(ExcelWorksheet worksheet, Report rep)
        {
            int Start = rep.FormNum_DB.Equals("2.8") ? 18 : 15;
            for (int i = 0; i < rep.Notes.Count - 1; i++)
            {
                worksheet.InsertRow(Start + 1, 1, Start);
                var cells = worksheet.Cells["A" + (Start + 1) + ":B" + (Start + 1)];
                foreach (var cell in cells)
                {
                    var btm = cell.Style.Border.Bottom;
                    var lft = cell.Style.Border.Left;
                    var rgt = cell.Style.Border.Right;
                    var top = cell.Style.Border.Top;
                    btm.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    btm.Color.SetColor(255, 0, 0, 0);
                    lft.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    lft.Color.SetColor(255, 0, 0, 0);
                    rgt.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    rgt.Color.SetColor(255, 0, 0, 0);
                    top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    top.Color.SetColor(255, 0, 0, 0);
                }
                var cellCL = worksheet.Cells["C" + (Start + 1) + ":L" + (Start + 1)];
                cellCL.Merge = true;
                var btmCL = cellCL.Style.Border.Bottom;
                var lftCL = cellCL.Style.Border.Left;
                var rgtCL = cellCL.Style.Border.Right;
                var topCL = cellCL.Style.Border.Top;
                btmCL.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                btmCL.Color.SetColor(255, 0, 0, 0);
                lftCL.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                lftCL.Color.SetColor(255, 0, 0, 0);
                rgtCL.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rgtCL.Color.SetColor(255, 0, 0, 0);
                topCL.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                topCL.Color.SetColor(255, 0, 0, 0);
            }
            foreach (Note note in rep.Notes)
            {
                worksheet.Cells[Start, 1].Value = note.RowNumber_DB;
                worksheet.Cells[Start, 2].Value = note.GraphNumber_DB;
                worksheet.Cells[Start, 3].Value = note.Comment_DB;
                Start++;
            }
        }
        #endregion

        #region RowsExport
        private void ExcelPrintRowsExport(ExcelWorksheet worksheet, Report rep)
        {
            int Start = rep.FormNum_DB.Equals("2.8") ? 14 : 11;
            for (int i = 0; i < rep[rep.FormNum_DB].Count - 1; i++)
            {
                worksheet.InsertRow(Start + 1, 1, Start);
                var cells = worksheet.Cells["A" + (Start + 1) + ":B" + (Start + 1)];
                foreach (var cell in cells)
                {
                    var btm = cell.Style.Border.Bottom;
                    var lft = cell.Style.Border.Left;
                    var rgt = cell.Style.Border.Right;
                    var top = cell.Style.Border.Top;
                    btm.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    btm.Color.SetColor(255, 0, 0, 0);
                    lft.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    lft.Color.SetColor(255, 0, 0, 0);
                    rgt.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    rgt.Color.SetColor(255, 0, 0, 0);
                    top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    top.Color.SetColor(255, 0, 0, 0);
                }
            }
            foreach (var form in rep[rep.FormNum_DB])
            {
                if (form is Form11)
                {
                    ExportForm11Data(worksheet, rep, Start);
                }
                if (form is Form12)
                {
                    ExportForm12Data(worksheet, rep, Start);
                }
                if (form is Form13)
                {
                    ExportForm13Data(worksheet, rep, Start);
                }
                if (form is Form14)
                {
                    ExportForm14Data(worksheet, rep, Start);
                }
                if (form is Form15)
                {
                    ExportForm15Data(worksheet, rep, Start);
                }
                if (form is Form16)
                {
                    ExportForm16Data(worksheet, rep, Start);
                }
                if (form is Form17)
                {
                    ExportForm17Data(worksheet, rep, Start);
                }
                if (form is Form18)
                {
                    ExportForm18Data(worksheet, rep, Start);
                }
                if (form is Form19)
                {
                    ExportForm19Data(worksheet, rep, Start);
                }

                if (form is Form21)
                {
                    ExportForm21Data(worksheet, rep, Start);
                }
                if (form is Form22)
                {
                    ExportForm22Data(worksheet, rep, Start);
                }
                if (form is Form23)
                {
                    ExportForm23Data(worksheet, rep, Start);
                }
                if (form is Form24)
                {
                    ExportForm24Data(worksheet, rep, Start);
                }
                if (form is Form25)
                {
                    ExportForm25Data(worksheet, rep, Start);
                }
                if (form is Form26)
                {
                    ExportForm26Data(worksheet, rep, Start);
                }
                if (form is Form27)
                {
                    ExportForm27Data(worksheet, rep, Start);
                }
                if (form is Form28)
                {
                    ExportForm28Data(worksheet, rep, Start);
                }
                if (form is Form29)
                {
                    ExportForm29Data(worksheet, rep, Start);
                }
                if (form is Form210)
                {
                    ExportForm210Data(worksheet, rep, Start);
                }
                if (form is Form211)
                {
                    ExportForm211Data(worksheet, rep, Start);
                }
                if (form is Form212)
                {
                    ExportForm212Data(worksheet, rep, Start);
                }
            }
        }
        #endregion

        #region ExportForm
        #region ExportForm_11
        private void ExportForm11Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            foreach (Form11 repForm in rep.Rows11)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.Type_DB;
                worksheet.Cells[Start, 6].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 7].Value = repForm.FactoryNumber_DB;
                worksheet.Cells[Start, 8].Value = repForm.Quantity_DB;
                worksheet.Cells[Start, 9].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out double val) ? val : repForm.Activity_DB;
                worksheet.Cells[Start, 10].Value = repForm.CreatorOKPO_DB;
                worksheet.Cells[Start, 11].Value = repForm.CreationDate_DB;
                worksheet.Cells[Start, 12].Value = repForm.Category_DB;
                worksheet.Cells[Start, 13].Value = repForm.SignedServicePeriod_DB;
                worksheet.Cells[Start, 14].Value = repForm.PropertyCode_DB;
                worksheet.Cells[Start, 15].Value = repForm.Owner_DB;
                worksheet.Cells[Start, 16].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 17].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 18].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 19].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 20].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 21].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 22].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 23].Value = repForm.PackNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_12
        private void ExportForm12Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            foreach (Form12 repForm in rep.Rows12)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.NameIOU_DB;
                worksheet.Cells[Start, 6].Value = repForm.FactoryNumber_DB;
                worksheet.Cells[Start, 7].Value = repForm.Mass_DB == "" || repForm.Mass_DB == "-" || repForm.Mass_DB == null ? 0 : double.TryParse(repForm.Mass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out double val) ? val : repForm.Mass_DB;
                worksheet.Cells[Start, 8].Value = repForm.CreatorOKPO_DB;
                worksheet.Cells[Start, 9].Value = repForm.CreationDate_DB;
                worksheet.Cells[Start, 10].Value = repForm.SignedServicePeriod_DB == "" || repForm.SignedServicePeriod_DB == "-" || repForm.SignedServicePeriod_DB == null ? 0 : int.TryParse(repForm.SignedServicePeriod_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out int valInt) ? valInt : repForm.SignedServicePeriod_DB;
                worksheet.Cells[Start, 11].Value = repForm.PropertyCode_DB;
                worksheet.Cells[Start, 12].Value = repForm.Owner_DB;
                worksheet.Cells[Start, 13].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 14].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 15].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 16].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 17].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 18].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 19].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 20].Value = repForm.PackNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_13
        private void ExportForm13Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form13 repForm in rep.Rows13)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.Type_DB;
                worksheet.Cells[Start, 6].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 7].Value = repForm.FactoryNumber_DB;
                worksheet.Cells[Start, 8].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Activity_DB;
                worksheet.Cells[Start, 9].Value = repForm.CreatorOKPO_DB;
                worksheet.Cells[Start, 10].Value = repForm.CreationDate_DB;
                worksheet.Cells[Start, 11].Value = repForm.AggregateState_DB;
                worksheet.Cells[Start, 12].Value = repForm.PropertyCode_DB;
                worksheet.Cells[Start, 13].Value = repForm.Owner_DB;
                worksheet.Cells[Start, 14].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 15].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 16].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 17].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 18].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 19].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 20].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 21].Value = repForm.PackNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_14
        private void ExportForm14Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form14 repForm in rep.Rows14)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.Name_DB;
                worksheet.Cells[Start, 6].Value = repForm.Sort_DB;
                worksheet.Cells[Start, 7].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 8].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Activity_DB;
                worksheet.Cells[Start, 9].Value = repForm.ActivityMeasurementDate_DB;
                worksheet.Cells[Start, 10].Value = repForm.Volume_DB == "" || repForm.Volume_DB == "-" || repForm.Volume_DB == null ? 0 : double.TryParse(repForm.Volume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume_DB;
                worksheet.Cells[Start, 11].Value = repForm.Mass_DB == "" || repForm.Mass_DB == "-" || repForm.Mass_DB == null ? 0 : double.TryParse(repForm.Mass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass_DB;
                worksheet.Cells[Start, 12].Value = repForm.AggregateState_DB;
                worksheet.Cells[Start, 13].Value = repForm.PropertyCode_DB;
                worksheet.Cells[Start, 14].Value = repForm.Owner_DB;
                worksheet.Cells[Start, 15].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 16].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 17].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 18].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 19].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 20].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 21].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 22].Value = repForm.PackNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_15
        private void ExportForm15Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form15 repForm in rep.Rows15)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.Type_DB;
                worksheet.Cells[Start, 6].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 7].Value = repForm.FactoryNumber_DB;
                worksheet.Cells[Start, 8].Value = repForm.Quantity_DB;
                worksheet.Cells[Start, 9].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Activity_DB;
                worksheet.Cells[Start, 10].Value = repForm.StatusRAO_DB;
                worksheet.Cells[Start, 11].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 12].Value = repForm.CreationDate_DB;
                worksheet.Cells[Start, 13].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 14].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 15].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 16].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 17].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 18].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 19].Value = repForm.PackNumber_DB;
                worksheet.Cells[Start, 20].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 21].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 22].Value = repForm.RefineOrSortRAOCode_DB;
                worksheet.Cells[Start, 23].Value = repForm.Subsidy_DB;
                worksheet.Cells[Start, 24].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_16
        private void ExportForm16Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form16 repForm in rep.Rows16)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.CodeRAO_DB;
                worksheet.Cells[Start, 5].Value = repForm.StatusRAO_DB;
                worksheet.Cells[Start, 6].Value = repForm.Volume_DB == "" || repForm.Volume_DB == "-" || repForm.Volume_DB == null ? 0 : double.TryParse(repForm.Volume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume_DB;
                worksheet.Cells[Start, 7].Value = repForm.Mass_DB == "" || repForm.Mass_DB == "-" || repForm.Mass_DB == null ? 0 : double.TryParse(repForm.Mass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass_DB;
                worksheet.Cells[Start, 8].Value = repForm.QuantityOZIII_DB == "" || repForm.QuantityOZIII_DB == "-" || repForm.QuantityOZIII_DB == null ? 0 : int.TryParse(repForm.QuantityOZIII_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out int valInt) ? valInt : repForm.QuantityOZIII_DB;
                worksheet.Cells[Start, 9].Value = repForm.MainRadionuclids_DB;
                worksheet.Cells[Start, 10].Value = repForm.TritiumActivity_DB == "" || repForm.TritiumActivity_DB == "-" || repForm.TritiumActivity_DB == null ? 0 : double.TryParse(repForm.TritiumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivity_DB;
                worksheet.Cells[Start, 11].Value = repForm.BetaGammaActivity_DB == "" || repForm.BetaGammaActivity_DB == "-" || repForm.BetaGammaActivity_DB == null ? 0 : double.TryParse(repForm.BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivity_DB;
                worksheet.Cells[Start, 12].Value = repForm.AlphaActivity_DB == "" || repForm.AlphaActivity_DB == "-" || repForm.AlphaActivity_DB == null ? 0 : double.TryParse(repForm.AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivity_DB;
                worksheet.Cells[Start, 13].Value = repForm.TransuraniumActivity_DB == "" || repForm.TransuraniumActivity_DB == "-" || repForm.TransuraniumActivity_DB == null ? 0 : double.TryParse(repForm.TransuraniumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TransuraniumActivity_DB;
                worksheet.Cells[Start, 14].Value = repForm.ActivityMeasurementDate_DB;
                worksheet.Cells[Start, 15].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 16].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 17].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 18].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 19].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 20].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 21].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 22].Value = repForm.RefineOrSortRAOCode_DB;
                worksheet.Cells[Start, 23].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 24].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 25].Value = repForm.PackNumber_DB;
                worksheet.Cells[Start, 26].Value = repForm.Subsidy_DB;
                worksheet.Cells[Start, 27].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_17
        private void ExportForm17Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form17 repForm in rep.Rows17)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.PackName_DB;
                worksheet.Cells[Start, 5].Value = repForm.PackType_DB;
                worksheet.Cells[Start, 6].Value = repForm.PackFactoryNumber_DB;
                worksheet.Cells[Start, 7].Value = repForm.PackFactoryNumber_DB;
                worksheet.Cells[Start, 8].Value = repForm.FormingDate_DB;
                worksheet.Cells[Start, 9].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 10].Value = repForm.Volume_DB == "" || repForm.Volume_DB == "-" || repForm.Volume_DB == null ? 0 : double.TryParse(repForm.Volume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume_DB;
                worksheet.Cells[Start, 11].Value = repForm.Mass_DB == "" || repForm.Mass_DB == "-" || repForm.Mass_DB == null ? 0 : double.TryParse(repForm.Mass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass_DB;
                worksheet.Cells[Start, 12].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 13].Value = repForm.SpecificActivity_DB == "" || repForm.SpecificActivity_DB == "-" || repForm.SpecificActivity_DB == null ? 0 : double.TryParse(repForm.SpecificActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SpecificActivity_DB;
                worksheet.Cells[Start, 14].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 15].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 16].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 17].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 18].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 19].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 20].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 21].Value = repForm.CodeRAO_DB;
                worksheet.Cells[Start, 22].Value = repForm.StatusRAO_DB;
                worksheet.Cells[Start, 23].Value = repForm.VolumeOutOfPack_DB;
                worksheet.Cells[Start, 24].Value = repForm.MassOutOfPack_DB;
                worksheet.Cells[Start, 25].Value = repForm.Quantity_DB;
                worksheet.Cells[Start, 26].Value = repForm.TritiumActivity_DB;
                worksheet.Cells[Start, 27].Value = repForm.BetaGammaActivity_DB;
                worksheet.Cells[Start, 28].Value = repForm.AlphaActivity_DB;
                worksheet.Cells[Start, 29].Value = repForm.TransuraniumActivity_DB;
                worksheet.Cells[Start, 30].Value = repForm.RefineOrSortRAOCode_DB;
                worksheet.Cells[Start, 31].Value = repForm.Subsidy_DB;
                worksheet.Cells[Start, 32].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_18
        private void ExportForm18Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form18 repForm in rep.Rows18)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.IndividualNumberZHRO_DB;
                worksheet.Cells[Start, 5].Value = repForm.PassportNumber_DB;
                worksheet.Cells[Start, 6].Value = repForm.Volume6_DB == "" || repForm.Volume6_DB == "-" || repForm.Volume6_DB == null ? 0 : double.TryParse(repForm.Volume6_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume6_DB;
                worksheet.Cells[Start, 7].Value = repForm.Mass7_DB == "" || repForm.Mass7_DB == "-" || repForm.Mass7_DB == null ? 0 : double.TryParse(repForm.Mass7_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass7_DB;
                worksheet.Cells[Start, 8].Value = repForm.SaltConcentration_DB == "" || repForm.SaltConcentration_DB == "-" || repForm.SaltConcentration_DB == null ? 0 : int.TryParse(repForm.SaltConcentration_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out int valInt) ? valInt : repForm.SaltConcentration_DB;
                worksheet.Cells[Start, 9].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 10].Value = repForm.SpecificActivity_DB == "" || repForm.SpecificActivity_DB == "-" || repForm.SpecificActivity_DB == null ? 0 : double.TryParse(repForm.SpecificActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SpecificActivity_DB;
                worksheet.Cells[Start, 11].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 12].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 13].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 14].Value = repForm.ProviderOrRecieverOKPO_DB;
                worksheet.Cells[Start, 15].Value = repForm.TransporterOKPO_DB;
                worksheet.Cells[Start, 16].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 17].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 18].Value = repForm.CodeRAO_DB;
                worksheet.Cells[Start, 19].Value = repForm.StatusRAO_DB;
                worksheet.Cells[Start, 20].Value = repForm.Volume20_DB == "" || repForm.Volume20_DB == "-" || repForm.Volume20_DB == null ? 0 : double.TryParse(repForm.Volume20_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume20_DB;
                worksheet.Cells[Start, 21].Value = repForm.Mass21_DB == "" || repForm.Mass21_DB == "-" || repForm.Mass21_DB == null ? 0 : double.TryParse(repForm.Mass21_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass21_DB;
                worksheet.Cells[Start, 22].Value = repForm.TritiumActivity_DB == "" || repForm.TritiumActivity_DB == "-" || repForm.TritiumActivity_DB == null ? 0 : double.TryParse(repForm.TritiumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivity_DB;
                worksheet.Cells[Start, 23].Value = repForm.BetaGammaActivity_DB == "" || repForm.BetaGammaActivity_DB == "-" || repForm.BetaGammaActivity_DB == null ? 0 : double.TryParse(repForm.BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivity_DB;
                worksheet.Cells[Start, 24].Value = repForm.AlphaActivity_DB == "" || repForm.AlphaActivity_DB == "-" || repForm.AlphaActivity_DB == null ? 0 : double.TryParse(repForm.AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivity_DB;
                worksheet.Cells[Start, 25].Value = repForm.TransuraniumActivity_DB == "" || repForm.TransuraniumActivity_DB == "-" || repForm.TransuraniumActivity_DB == null ? 0 : double.TryParse(repForm.TransuraniumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TransuraniumActivity_DB;
                worksheet.Cells[Start, 26].Value = repForm.RefineOrSortRAOCode_DB;
                worksheet.Cells[Start, 27].Value = repForm.Subsidy_DB == "" || repForm.Subsidy_DB == "-" || repForm.Subsidy_DB == null ? 0 : int.TryParse(repForm.Subsidy_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.Subsidy_DB;
                worksheet.Cells[Start, 28].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_19
        private void ExportForm19Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            foreach (Form19 repForm in rep.Rows19)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.OperationDate_DB;
                worksheet.Cells[Start, 4].Value = repForm.DocumentVid_DB;
                worksheet.Cells[Start, 5].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 6].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 7].Value = repForm.CodeTypeAccObject_DB;
                worksheet.Cells[Start, 8].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 9].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out double val) ? val : repForm.Activity_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_21
        private void ExportForm21Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form21 repForm in rep.Rows21)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.RefineMachineName.Value == null ? "" : repForm.RefineMachineName.Value;
                worksheet.Cells[Start, 3].Value = repForm.MachineCode.Value == null ? "" : repForm.MachineCode.Value;
                worksheet.Cells[Start, 4].Value = repForm.MachinePower.Value == null || repForm.MachinePower.Value == "" || repForm.MachinePower.Value == "-" ? 0 : double.TryParse(repForm.MachinePower.Value.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MachinePower.Value;
                worksheet.Cells[Start, 5].Value = repForm.NumberOfHoursPerYear.Value == null || repForm.NumberOfHoursPerYear.Value == "" || repForm.NumberOfHoursPerYear.Value == "-" ? 0 : int.TryParse(repForm.NumberOfHoursPerYear.Value.Replace("(", "").Replace(")", "").Replace(".", ","), out int valInt) ? valInt : repForm.NumberOfHoursPerYear.Value;
                worksheet.Cells[Start, 6].Value = repForm.CodeRAOIn_DB;
                worksheet.Cells[Start, 7].Value = repForm.StatusRAOIn_DB;
                worksheet.Cells[Start, 8].Value = repForm.VolumeIn_DB == null || repForm.VolumeIn_DB == "" || repForm.VolumeIn_DB == "-" ? 0 : double.TryParse(repForm.VolumeIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.VolumeIn_DB;
                worksheet.Cells[Start, 9].Value = repForm.MassIn_DB == null || repForm.MassIn_DB == "" || repForm.MassIn_DB == "-" ? 0 : double.TryParse(repForm.MassIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassIn_DB;
                worksheet.Cells[Start, 10].Value = repForm.QuantityIn_DB;
                worksheet.Cells[Start, 11].Value = repForm.TritiumActivityIn_DB == null || repForm.TritiumActivityIn_DB == "" || repForm.TritiumActivityIn_DB == "-" ? 0 : double.TryParse(repForm.TritiumActivityIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivityIn_DB;
                worksheet.Cells[Start, 12].Value = repForm.BetaGammaActivityIn_DB == null || repForm.BetaGammaActivityIn_DB == "" || repForm.BetaGammaActivityIn_DB == "-" ? 0 : double.TryParse(repForm.BetaGammaActivityIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivityIn_DB;
                worksheet.Cells[Start, 13].Value = repForm.AlphaActivityIn_DB == null || repForm.AlphaActivityIn_DB == "" || repForm.AlphaActivityIn_DB == "-" ? 0 : double.TryParse(repForm.AlphaActivityIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivityIn_DB;
                worksheet.Cells[Start, 14].Value = repForm.TritiumActivityIn_DB == null || repForm.TritiumActivityIn_DB == "" || repForm.TritiumActivityIn_DB == "-" ? 0 : double.TryParse(repForm.TritiumActivityIn_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivityIn_DB;
                worksheet.Cells[Start, 15].Value = repForm.CodeRAOout_DB;
                worksheet.Cells[Start, 16].Value = repForm.StatusRAOout_DB;
                worksheet.Cells[Start, 17].Value = repForm.VolumeOut_DB == null || repForm.VolumeOut_DB == "" || repForm.VolumeOut_DB == "-" ? 0 : double.TryParse(repForm.VolumeOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.VolumeOut_DB;
                worksheet.Cells[Start, 18].Value = repForm.MassOut_DB == null || repForm.MassOut_DB == "" || repForm.MassOut_DB == "-" ? 0 : double.TryParse(repForm.MassOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassOut_DB;
                worksheet.Cells[Start, 19].Value = repForm.QuantityOZIIIout_DB;
                worksheet.Cells[Start, 20].Value = repForm.TritiumActivityOut_DB == null || repForm.TritiumActivityOut_DB == "" || repForm.TritiumActivityOut_DB == "-" ? 0 : double.TryParse(repForm.TritiumActivityOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivityOut_DB;
                worksheet.Cells[Start, 21].Value = repForm.BetaGammaActivityOut_DB == null || repForm.BetaGammaActivityOut_DB == "" || repForm.BetaGammaActivityOut_DB == "-" ? 0 : double.TryParse(repForm.BetaGammaActivityOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivityOut_DB;
                worksheet.Cells[Start, 22].Value = repForm.AlphaActivityOut_DB == null || repForm.AlphaActivityOut_DB == "" || repForm.AlphaActivityOut_DB == "-" ? 0 : double.TryParse(repForm.AlphaActivityOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivityOut_DB;
                worksheet.Cells[Start, 23].Value = repForm.TransuraniumActivityOut_DB == null || repForm.TransuraniumActivityOut_DB == "" || repForm.TransuraniumActivityOut_DB == "-" ? 0 : double.TryParse(repForm.TransuraniumActivityOut_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TransuraniumActivityOut_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_22
        private void ExportForm22Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            int valInt;
            foreach (Form22 repForm in rep.Rows22)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.StoragePlaceName.Value == null ? "" : repForm.StoragePlaceName.Value;
                worksheet.Cells[Start, 3].Value = repForm.StoragePlaceCode.Value == null ? "" : repForm.StoragePlaceCode.Value;
                worksheet.Cells[Start, 4].Value = repForm.PackName.Value == null ? "" : repForm.PackName.Value;
                worksheet.Cells[Start, 5].Value = repForm.PackType.Value == null ? "" : repForm.PackType.Value;
                worksheet.Cells[Start, 6].Value = repForm.PackQuantity_DB == "" || repForm.PackQuantity_DB == null || repForm.PackQuantity_DB == "-" ? 0 : int.TryParse(repForm.PackQuantity_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.PackQuantity_DB;
                worksheet.Cells[Start, 7].Value = repForm.CodeRAO_DB;
                worksheet.Cells[Start, 8].Value = repForm.StatusRAO_DB;
                worksheet.Cells[Start, 9].Value = repForm.VolumeOutOfPack_DB == "" || repForm.VolumeOutOfPack_DB == null || repForm.VolumeOutOfPack_DB == "-" ? 0 : double.TryParse(repForm.VolumeOutOfPack_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.VolumeOutOfPack_DB;
                worksheet.Cells[Start, 10].Value = repForm.VolumeInPack_DB == "" || repForm.VolumeInPack_DB == null || repForm.VolumeInPack_DB == "-" ? 0 : double.TryParse(repForm.VolumeInPack_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.VolumeInPack_DB;
                worksheet.Cells[Start, 11].Value = repForm.MassOutOfPack_DB == "" || repForm.MassOutOfPack_DB == null || repForm.MassOutOfPack_DB == "-" ? 0 : double.TryParse(repForm.MassOutOfPack_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassOutOfPack_DB;
                worksheet.Cells[Start, 12].Value = repForm.MassInPack_DB == "" || repForm.MassInPack_DB == null || repForm.MassInPack_DB == "-" ? 0 : double.TryParse(repForm.MassInPack_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassInPack_DB;
                worksheet.Cells[Start, 13].Value = repForm.QuantityOZIII_DB == "" || repForm.QuantityOZIII_DB == null || repForm.QuantityOZIII_DB == "-" ? 0 : int.TryParse(repForm.QuantityOZIII_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityOZIII_DB;
                worksheet.Cells[Start, 14].Value = repForm.TritiumActivity_DB == "" || repForm.TritiumActivity_DB == null || repForm.TritiumActivity_DB == "-" ? 0 : double.TryParse(repForm.TritiumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TritiumActivity_DB;
                worksheet.Cells[Start, 15].Value = repForm.BetaGammaActivity_DB == "" || repForm.BetaGammaActivity_DB == null || repForm.BetaGammaActivity_DB == "-" ? 0 : double.TryParse(repForm.BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivity_DB;
                worksheet.Cells[Start, 16].Value = repForm.AlphaActivity_DB == "" || repForm.AlphaActivity_DB == null || repForm.AlphaActivity_DB == "-" ? 0 : double.TryParse(repForm.AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivity_DB;
                worksheet.Cells[Start, 17].Value = repForm.TransuraniumActivity_DB == "" || repForm.TransuraniumActivity_DB == null || repForm.TransuraniumActivity_DB == "-" ? 0 : double.TryParse(repForm.TransuraniumActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TransuraniumActivity_DB;
                worksheet.Cells[Start, 18].Value = repForm.MainRadionuclids_DB;
                worksheet.Cells[Start, 19].Value = repForm.Subsidy_DB;
                worksheet.Cells[Start, 20].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_23
        private void ExportForm23Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            int valInt;
            foreach (Form23 repForm in rep.Rows23)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 3].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 4].Value = repForm.ProjectVolume_DB == "" || repForm.ProjectVolume_DB == "-" || repForm.ProjectVolume_DB == null ? 0 : double.TryParse(repForm.ProjectVolume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.ProjectVolume_DB;
                worksheet.Cells[Start, 5].Value = repForm.CodeRAO_DB;
                worksheet.Cells[Start, 6].Value = repForm.Volume_DB == "" || repForm.Volume_DB == "-" || repForm.Volume_DB == null ? 0 : double.TryParse(repForm.Volume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Volume_DB;
                worksheet.Cells[Start, 7].Value = repForm.Mass_DB == "" || repForm.Mass_DB == "-" || repForm.Mass_DB == null ? 0 : double.TryParse(repForm.Mass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Mass_DB;
                worksheet.Cells[Start, 8].Value = repForm.QuantityOZIII_DB == "" || repForm.QuantityOZIII_DB == "-" || repForm.QuantityOZIII_DB == null ? 0 : int.TryParse(repForm.QuantityOZIII_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityOZIII_DB;
                worksheet.Cells[Start, 9].Value = repForm.SummaryActivity_DB == "" || repForm.SummaryActivity_DB == "-" || repForm.SummaryActivity_DB == null ? 0 : double.TryParse(repForm.SummaryActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SummaryActivity_DB;
                worksheet.Cells[Start, 10].Value = repForm.DocumentNumber_DB;
                worksheet.Cells[Start, 11].Value = repForm.DocumentDate_DB;
                worksheet.Cells[Start, 12].Value = repForm.ExpirationDate_DB;
                worksheet.Cells[Start, 13].Value = repForm.DocumentName_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_24
        private void ExportForm24Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            int valInt;
            double val;
            foreach (Form24 repForm in rep.Rows24)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.CodeOYAT_DB;
                worksheet.Cells[Start, 3].Value = repForm.FcpNumber_DB;
                worksheet.Cells[Start, 4].Value = repForm.MassCreated_DB == "" || repForm.MassCreated_DB == "-" || repForm.MassCreated_DB == null ? 0 : double.TryParse(repForm.MassCreated_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassCreated_DB;
                worksheet.Cells[Start, 5].Value = repForm.QuantityCreated_DB == "" || repForm.QuantityCreated_DB == "-" || repForm.QuantityCreated_DB == null ? 0 : int.TryParse(repForm.QuantityCreated_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityCreated_DB;
                worksheet.Cells[Start, 6].Value = repForm.MassFromAnothers_DB == "" || repForm.MassFromAnothers_DB == "-" || repForm.MassFromAnothers_DB == null ? 0 : double.TryParse(repForm.MassFromAnothers_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassFromAnothers_DB;
                worksheet.Cells[Start, 7].Value = repForm.QuantityFromAnothers_DB == "" || repForm.QuantityFromAnothers_DB == "-" || repForm.QuantityFromAnothers_DB == null ? 0 : int.TryParse(repForm.QuantityFromAnothers_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityFromAnothers_DB;
                worksheet.Cells[Start, 8].Value = repForm.MassFromAnothersImported_DB == "" || repForm.MassFromAnothersImported_DB == "-" || repForm.MassFromAnothersImported_DB == null ? 0 : double.TryParse(repForm.MassFromAnothersImported_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassFromAnothersImported_DB;
                worksheet.Cells[Start, 9].Value = repForm.QuantityFromAnothersImported_DB == "" || repForm.QuantityFromAnothersImported_DB == "-" || repForm.QuantityFromAnothersImported_DB == null ? 0 : int.TryParse(repForm.QuantityFromAnothersImported_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityFromAnothersImported_DB;
                worksheet.Cells[Start, 10].Value = repForm.MassAnotherReasons_DB == "" || repForm.MassAnotherReasons_DB == "-" || repForm.MassAnotherReasons_DB == null ? 0 : double.TryParse(repForm.MassAnotherReasons_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassAnotherReasons_DB;
                worksheet.Cells[Start, 11].Value = repForm.QuantityAnotherReasons_DB == "" || repForm.QuantityAnotherReasons_DB == "-" || repForm.QuantityAnotherReasons_DB == null ? 0 : int.TryParse(repForm.QuantityAnotherReasons_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityAnotherReasons_DB;
                worksheet.Cells[Start, 12].Value = repForm.MassTransferredToAnother_DB == "" || repForm.MassTransferredToAnother_DB == "-" || repForm.MassTransferredToAnother_DB == null ? 0 : double.TryParse(repForm.MassTransferredToAnother_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassTransferredToAnother_DB;
                worksheet.Cells[Start, 13].Value = repForm.QuantityTransferredToAnother_DB == "" || repForm.QuantityTransferredToAnother_DB == "-" || repForm.QuantityTransferredToAnother_DB == null ? 0 : int.TryParse(repForm.QuantityTransferredToAnother_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityTransferredToAnother_DB;
                worksheet.Cells[Start, 14].Value = repForm.MassRefined_DB == "" || repForm.MassRefined_DB == "-" || repForm.MassRefined_DB == null ? 0 : double.TryParse(repForm.MassRefined_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassRefined_DB;
                worksheet.Cells[Start, 15].Value = repForm.QuantityRefined_DB == "" || repForm.QuantityRefined_DB == "-" || repForm.QuantityRefined_DB == null ? 0 : int.TryParse(repForm.QuantityRefined_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityRefined_DB;
                worksheet.Cells[Start, 16].Value = repForm.MassRemovedFromAccount_DB == "" || repForm.MassRemovedFromAccount_DB == "-" || repForm.MassRemovedFromAccount_DB == null ? 0 : double.TryParse(repForm.MassRemovedFromAccount_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MassRemovedFromAccount_DB;
                worksheet.Cells[Start, 17].Value = repForm.QuantityRemovedFromAccount_DB == "" || repForm.QuantityRemovedFromAccount_DB == "-" || repForm.QuantityRemovedFromAccount_DB == null ? 0 : int.TryParse(repForm.QuantityRemovedFromAccount_DB.Replace("(", "").Replace(")", "").Replace(".", ","), out valInt) ? valInt : repForm.QuantityRemovedFromAccount_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_25
        private void ExportForm25Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (Form25 repForm in rep.Rows25)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.StoragePlaceName_DB;
                worksheet.Cells[Start, 3].Value = repForm.StoragePlaceCode_DB;
                worksheet.Cells[Start, 4].Value = repForm.CodeOYAT_DB;
                worksheet.Cells[Start, 5].Value = repForm.FcpNumber_DB;
                worksheet.Cells[Start, 6].Value = repForm.FuelMass_DB == "" || repForm.FuelMass_DB == "-" || repForm.FuelMass_DB == null ? 0 : double.TryParse(repForm.FuelMass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.FuelMass_DB;
                worksheet.Cells[Start, 7].Value = repForm.CellMass_DB == "" || repForm.CellMass_DB == "-" || repForm.CellMass_DB == null ? 0 : double.TryParse(repForm.CellMass_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.CellMass_DB;
                worksheet.Cells[Start, 8].Value = repForm.Quantity_DB;
                worksheet.Cells[Start, 9].Value = repForm.AlphaActivity_DB == "" || repForm.AlphaActivity_DB == "-" || repForm.AlphaActivity_DB == null ? 0 : double.TryParse(repForm.AlphaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AlphaActivity_DB;
                worksheet.Cells[Start, 10].Value = repForm.BetaGammaActivity_DB == "" || repForm.BetaGammaActivity_DB == "-" || repForm.BetaGammaActivity_DB == null ? 0 : double.TryParse(repForm.BetaGammaActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.BetaGammaActivity_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_26
        private void ExportForm26Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (Form26 repForm in rep.Rows26)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.ObservedSourceNumber_DB;
                worksheet.Cells[Start, 3].Value = repForm.ControlledAreaName_DB;
                worksheet.Cells[Start, 4].Value = repForm.SupposedWasteSource_DB;
                worksheet.Cells[Start, 5].Value = repForm.DistanceToWasteSource_DB == "" || repForm.DistanceToWasteSource_DB == "-" || repForm.DistanceToWasteSource_DB == null ? 0 : double.TryParse(repForm.DistanceToWasteSource_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.DistanceToWasteSource_DB;
                worksheet.Cells[Start, 6].Value = repForm.TestDepth_DB == "" || repForm.TestDepth_DB == "-" || repForm.TestDepth_DB == null ? 0 : double.TryParse(repForm.TestDepth_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.TestDepth_DB;
                worksheet.Cells[Start, 7].Value = repForm.RadionuclidName_DB;
                worksheet.Cells[Start, 8].Value = repForm.AverageYearConcentration_DB == "" || repForm.AverageYearConcentration_DB == "-" || repForm.AverageYearConcentration_DB == null ? 0 : double.TryParse(repForm.AverageYearConcentration_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AverageYearConcentration_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_27
        private void ExportForm27Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (FireBird.Form27 repForm in rep.Rows27)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.ObservedSourceNumber_DB;
                worksheet.Cells[Start, 3].Value = repForm.RadionuclidName_DB;
                worksheet.Cells[Start, 4].Value = repForm.AllowedWasteValue_DB == "" || repForm.AllowedWasteValue_DB == "-" || repForm.AllowedWasteValue_DB == null ? 0 : double.TryParse(repForm.AllowedWasteValue_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AllowedWasteValue_DB;
                worksheet.Cells[Start, 5].Value = repForm.FactedWasteValue_DB == "" || repForm.FactedWasteValue_DB == "-" || repForm.FactedWasteValue_DB == null ? 0 : double.TryParse(repForm.FactedWasteValue_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.FactedWasteValue_DB;
                worksheet.Cells[Start, 6].Value = repForm.WasteOutbreakPreviousYear_DB == "" || repForm.WasteOutbreakPreviousYear_DB == "-" || repForm.WasteOutbreakPreviousYear_DB == null ? 0 : double.TryParse(repForm.WasteOutbreakPreviousYear_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.WasteOutbreakPreviousYear_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_28
        private void ExportForm28Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (Form28 repForm in rep.Rows28)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.WasteSourceName_DB;
                worksheet.Cells[Start, 3].Value = repForm.WasteRecieverName_DB;
                worksheet.Cells[Start, 4].Value = repForm.RecieverTypeCode_DB;
                worksheet.Cells[Start, 5].Value = repForm.PoolDistrictName_DB;
                worksheet.Cells[Start, 6].Value = repForm.AllowedWasteRemovalVolume_DB == "" || repForm.AllowedWasteRemovalVolume_DB == "-" || repForm.AllowedWasteRemovalVolume_DB == null ? 0 : double.TryParse(repForm.AllowedWasteRemovalVolume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AllowedWasteRemovalVolume_DB;
                worksheet.Cells[Start, 7].Value = repForm.RemovedWasteVolume_DB == "" || repForm.RemovedWasteVolume_DB == "-" || repForm.RemovedWasteVolume_DB == null ? 0 : double.TryParse(repForm.RemovedWasteVolume_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.RemovedWasteVolume_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_29
        private void ExportForm29Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (Form29 repForm in rep.Rows29)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.WasteSourceName_DB;
                worksheet.Cells[Start, 3].Value = repForm.RadionuclidName_DB;
                worksheet.Cells[Start, 4].Value = repForm.AllowedActivity_DB == "" || repForm.AllowedActivity_DB == "-" || repForm.AllowedActivity_DB == null ? 0 : double.TryParse(repForm.AllowedActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AllowedActivity_DB;
                worksheet.Cells[Start, 5].Value = repForm.FactedActivity_DB == "" || repForm.FactedActivity_DB == "-" || repForm.FactedActivity_DB == null ? 0 : double.TryParse(repForm.FactedActivity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.FactedActivity_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_210
        private void ExportForm210Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val;
            foreach (Form210 repForm in rep.Rows210)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.IndicatorName_DB;
                worksheet.Cells[Start, 3].Value = repForm.PlotName_DB;
                worksheet.Cells[Start, 4].Value = repForm.PlotKadastrNumber_DB;
                worksheet.Cells[Start, 5].Value = repForm.PlotCode_DB;
                worksheet.Cells[Start, 6].Value = repForm.InfectedArea_DB == "" || repForm.InfectedArea_DB == "-" || repForm.InfectedArea_DB == null ? 0 : double.TryParse(repForm.InfectedArea_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.InfectedArea_DB;
                worksheet.Cells[Start, 7].Value = repForm.AvgGammaRaysDosePower_DB == "" || repForm.AvgGammaRaysDosePower_DB == "-" || repForm.AvgGammaRaysDosePower_DB == null ? 0 : double.TryParse(repForm.AvgGammaRaysDosePower_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.AvgGammaRaysDosePower_DB;
                worksheet.Cells[Start, 8].Value = repForm.MaxGammaRaysDosePower_DB == "" || repForm.MaxGammaRaysDosePower_DB == "-" || repForm.MaxGammaRaysDosePower_DB == null ? 0 : double.TryParse(repForm.MaxGammaRaysDosePower_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.MaxGammaRaysDosePower_DB;
                worksheet.Cells[Start, 9].Value = repForm.WasteDensityAlpha_DB == "" || repForm.WasteDensityAlpha_DB == "-" || repForm.WasteDensityAlpha_DB == null ? 0 : double.TryParse(repForm.WasteDensityAlpha_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.WasteDensityAlpha_DB;
                worksheet.Cells[Start, 10].Value = repForm.WasteDensityBeta_DB == "" || repForm.WasteDensityBeta_DB == "-" || repForm.WasteDensityBeta_DB == null ? 0 : double.TryParse(repForm.WasteDensityBeta_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.WasteDensityBeta_DB;
                worksheet.Cells[Start, 11].Value = repForm.FcpNumber_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_211
        private void ExportForm211Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (FireBird.Form211 repForm in rep.Rows211)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.PlotName_DB;
                worksheet.Cells[Start, 3].Value = repForm.PlotKadastrNumber_DB;
                worksheet.Cells[Start, 4].Value = repForm.PlotCode_DB;
                worksheet.Cells[Start, 5].Value = repForm.InfectedArea_DB == "" || repForm.InfectedArea_DB == "-" || repForm.InfectedArea_DB == null ? 0 : double.TryParse(repForm.InfectedArea_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.InfectedArea_DB;
                worksheet.Cells[Start, 6].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 7].Value = repForm.SpecificActivityOfPlot_DB == "" || repForm.SpecificActivityOfPlot_DB == "-" || repForm.SpecificActivityOfPlot_DB == null ? 0 : double.TryParse(repForm.SpecificActivityOfPlot_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SpecificActivityOfPlot_DB;
                worksheet.Cells[Start, 8].Value = repForm.SpecificActivityOfLiquidPart_DB == "" || repForm.SpecificActivityOfLiquidPart_DB == "-" || repForm.SpecificActivityOfLiquidPart_DB == null ? 0 : double.TryParse(repForm.SpecificActivityOfLiquidPart_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SpecificActivityOfLiquidPart_DB;
                worksheet.Cells[Start, 9].Value = repForm.SpecificActivityOfDensePart_DB == "" || repForm.SpecificActivityOfDensePart_DB == "-" || repForm.SpecificActivityOfDensePart_DB == null ? 0 : double.TryParse(repForm.SpecificActivityOfDensePart_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.SpecificActivityOfDensePart_DB;
                Start++;
            }
        }
        #endregion

        #region ExportForm_212
        private void ExportForm212Data(ExcelWorksheet worksheet, Report rep, int Start)
        {
            double val = 0;
            foreach (Form212 repForm in rep.Rows212)
            {
                worksheet.Cells[Start, 1].Value = repForm.NumberInOrder_DB;
                worksheet.Cells[Start, 2].Value = repForm.OperationCode_DB;
                worksheet.Cells[Start, 3].Value = repForm.ObjectTypeCode_DB;
                worksheet.Cells[Start, 4].Value = repForm.Radionuclids_DB;
                worksheet.Cells[Start, 5].Value = repForm.Activity_DB == "" || repForm.Activity_DB == "-" || repForm.Activity_DB == null ? 0 : double.TryParse(repForm.Activity_DB.Replace("е", "E").Replace("(", "").Replace(")", "").Replace("Е", "E").Replace(".", ","), out val) ? val : repForm.Activity_DB;
                worksheet.Cells[Start, 6].Value = repForm.ProviderOrRecieverOKPO_DB;
                Start++;
            }
        }
        #endregion 
        #endregion
    }
}