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
                //if (form is Form12)
                //{
                //    ((Form12)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form13)
                //{
                //    ((Form13)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form14)
                //{
                //    ((Form14)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form15)
                //{
                //    ((Form15)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form16)
                //{
                //    ((Form16)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form17)
                //{
                //    ((Form17)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form18)
                //{
                //    ((Form18)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form19)
                //{
                //    ((Form19)(form)).ExcelRow(worksheet, Start, 1);
                //}

                //if (form is Form21)
                //{
                //    ((Form21)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form22)
                //{
                //    ((Form22)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form23)
                //{
                //    ((Form23)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form24)
                //{
                //    ((Form24)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form25)
                //{
                //    ((Form25)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form26)
                //{
                //    ((Form26)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form27)
                //{
                //    ((Form27)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form28)
                //{
                //    ((Form28)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form29)
                //{
                //    ((Form29)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form210)
                //{
                //    ((Form210)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form211)
                //{
                //    ((Form211)(form)).ExcelRow(worksheet, Start, 1);
                //}
                //if (form is Form212)
                //{
                //    ((Form212)(form)).ExcelRow(worksheet, Start, 1);
                //}
            }
        }
        #endregion

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
    }
}