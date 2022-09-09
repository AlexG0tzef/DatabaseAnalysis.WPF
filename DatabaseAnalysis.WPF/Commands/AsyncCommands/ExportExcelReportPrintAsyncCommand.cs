using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
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

        //#region NotesExport
        //private int ExcelExportNotes(ExcelWorksheet worksheet, Report rep, FireBird.Reports reps, int StartRow, int StartColumn, bool printID = false)
        //{
        //    foreach (Report item in reps)
        //    {
        //        if (reps != null)
        //        {
        //            var cnty = StartRow;
        //            foreach (Note i in item.Notes)
        //            {
        //                var mstrep = reps.Master_DB;
        //                i.ExcelRow(worksheetPrim, cnty, StartColumn + 1);
        //                var yu = 0;
        //                if (printID)
        //                {
        //                    if (param.Split('.')[0] == "1")
        //                    {
        //                        if (mstrep.Rows10[1].RegNo_DB != "" && mstrep.Rows10[1].Okpo_DB != "")
        //                        {
        //                            yu = reps.Master_DB.Rows10[1].ExcelRow(worksheetPrim, cnty, 1, SumNumber: reps.Master_DB.Rows20[1].Id.ToString()) + 1;
        //                        }
        //                        else
        //                        {
        //                            yu = reps.Master_DB.Rows10[0].ExcelRow(worksheetPrim, cnty, 1, SumNumber: reps.Master_DB.Rows20[1].Id.ToString()) + 1;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (mstrep.Rows20[1].RegNo_DB != "" && mstrep.Rows20[1].Okpo_DB != "")
        //                        {
        //                            yu = reps.Master_DB.Rows20[1].ExcelRow(worksheetPrim, cnty, 1, SumNumber: reps.Master_DB.Rows20[1].Id.ToString()) + 1;
        //                        }
        //                        else
        //                        {
        //                            yu = reps.Master_DB.Rows20[0].ExcelRow(worksheetPrim, cnty, 1, SumNumber: reps.Master_DB.Rows20[1].Id.ToString()) + 1;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (param.Split('.')[0] == "1")
        //                    {
        //                        if (mstrep.Rows10[1].RegNo_DB != "" && mstrep.Rows10[1].Okpo_DB != "")
        //                        {
        //                            yu = reps.Master_DB.Rows10[1].ExcelRow(worksheetPrim, cnty, 1) + 1;
        //                        }
        //                        else
        //                        {
        //                            yu = reps.Master_DB.Rows10[0].ExcelRow(worksheetPrim, cnty, 1) + 1;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (mstrep.Rows20[1].RegNo_DB != "" && mstrep.Rows20[1].Okpo_DB != "")
        //                        {
        //                            yu = reps.Master_DB.Rows20[1].ExcelRow(worksheetPrim, cnty, 1) + 1;
        //                        }
        //                        else
        //                        {
        //                            yu = reps.Master_DB.Rows20[0].ExcelRow(worksheetPrim, cnty, 1) + 1;
        //                        }
        //                    }
        //                }

        //                item.ExcelRow(worksheetPrim, cnty, yu);
        //                count++;
        //            }
        //            StartRow = cnty;
        //        }
        //    }
        //    return StartRow;
        //} 
        //#endregion
    }
}