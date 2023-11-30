using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using FastReport;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class ValidateDataMigration
    {
        public async Task<(bool isOk, string message, List<List<string>> errorByRowList)> ValidateDataAndExport(FinishingSystemFactory factory, Stream stream, string pathFileName, int userId)
        {
            XLWorkbook workbook = null;
            IXLWorksheet workSheet = null;

            try
            {
                workbook = new XLWorkbook(stream, XLEventTracking.Disabled);
                workSheet = workbook.Worksheet(1);
            }
            catch (Exception)
            {
                return (false, "Error reading Excel file", null);
            }

            int rowIni = 4;
            int colIni = 1;
            int row = 0;
            int rowSheet = workSheet.LastRowUsed().RowNumber();
            int colSheet = workSheet.LastColumnUsed().ColumnNumber();

            RuloMigration ruloMigration = new RuloMigration();
            var migrationCategoryList = await factory.RuloMigrations.GetMigrationCategoryList();

            MigrationControl migrationControl = new MigrationControl();
            migrationControl.ExcelFilePath = pathFileName;
            migrationControl.FileName = Path.GetFileName(pathFileName);
            migrationControl.LastMigratedRowOfExcelFile = 0;
            migrationControl.FileRowsTotal = 0;
            migrationControl.RowsTotalMigrated = 0;
            migrationControl.BeginDate = DateTime.Now;

            await factory.RuloMigrations.AddMigrationControl(migrationControl, userId);

            var styles = await factory.RuloMigrations.GetStylesFromProductionLoteList();

            var definitionProcesses = await factory.DefinationProcesses.GetDefinationProcessList();

            List<List<string>> errorByRowListOfList = new List<List<string>>();
            List<string> errorByRowList = null;
            List<RuloMigration> ruloMigrationList = new List<RuloMigration>();

            try
            {
                for (row = rowIni; row <= rowSheet; row++)
                {
                    errorByRowList = new List<string>();

                    colIni = 1;
                    ruloMigration = new RuloMigration();

                    ruloMigration.ExcelFileRow = row;
                    ruloMigration.ExcelFileName = Path.GetFileName(pathFileName);

                    //Validate date
                    var date = workSheet.Cell(row, colIni).Value;

                    DateTime dateReg;
                    if (DateTime.TryParse(date.ToString(), out dateReg))
                        ruloMigration.Date = dateReg;
                    else
                        errorByRowList.Add($"Date no valid! Value \"{GetValue(date)}\", Row {row}, Col {colIni}");

                    //**********************
                    //Validate Test
                    var isTest = workSheet.Cell(row, ++colIni).Value;
                    if (!string.IsNullOrWhiteSpace(isTest.ToString()))
                        ruloMigration.IsTestStyle = isTest.ToString().Trim().ToUpper() == "YES" ? true : false;
                    else 
                        ruloMigration.IsTestStyle = false;
                    //**********************

                    //Validate style
                    var style = workSheet.Cell(row, ++colIni).Value;
                    var styleName = workSheet.Cell(row, ++colIni).Value;

                    if (style != null && !string.IsNullOrWhiteSpace(style.ToString()))
                    {
                        //TODO: Continuar aquí
                        var styleData = styles.Where(x => x.Style.Contains(style.ToString().Trim(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (styleData != null)
                        {
                            ruloMigration.Style = styleData.Style; //Convert.ToString(style);
                            if (ruloMigration.IsTestStyle)
                                ruloMigration.StyleName = (styleName != null && !string.IsNullOrWhiteSpace(styleName.ToString())) ? styleName.ToString() : string.Empty;
                            else
                                ruloMigration.StyleName = styleData.StyleName;
                        }
                        else errorByRowList.Add($"Style no valid! Value \"{GetValue(style)}\", Row {row}, Col {colIni}");
                    }
                    else errorByRowList.Add($"Style no valid! Value \"{GetValue(style)}\", Row {row}, Col {colIni}");

                    ////Validate style name
                    //var styleName = workSheet.Cell(row, ++colIni).Value;
                    //if (!string.IsNullOrWhiteSpace(styleName.ToString()))
                    //    ruloMigration.StyleName = Convert.ToString(styleName);
                    //else errorByRowList.Add($"Style Name no valid! Value \"{GetValue(styleName)}\", Row {row}, Col {colIni}");

                    //TODO: Como se deshabilitó hay que incrementar la columna para leer la siguiente
                    //++colIni;

                    //Validate machine
                    var nextMachine = workSheet.Cell(row, ++colIni).Value;
                    string sNextMachine = string.Empty;
                    if (!string.IsNullOrWhiteSpace(nextMachine.ToString()))
                    {
                        sNextMachine = nextMachine.ToString();
                        if (nextMachine.ToString().StartsWith("Bruckner", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var digits = from c in sNextMachine
                                         where Char.IsDigit(c)
                                         select c;

                            if (digits != null && digits.Count() > 0 && string.Join("", digits) == "1")
                                sNextMachine = sNextMachine.Substring(0, sNextMachine.IndexOf(string.Join("", digits)));
                        }

                        if (sNextMachine == "-")
                        {
                            ruloMigration.DefinitionProcessID = null;
                            ruloMigration.NextMachine = null;
                        }
                        else
                        {
                            var definitionProcess = definitionProcesses.Where(x => x.Name.Contains(sNextMachine, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                            if (definitionProcess != null)
                            {
                                ruloMigration.DefinitionProcessID = definitionProcess.DefinationProcessID;
                                ruloMigration.NextMachine = definitionProcess.Name; //Convert.ToString(nextMachine);
                            }
                            else errorByRowList.Add($"Machine no valid! Value \"{GetValue(nextMachine)}\", Row {row}, Col {colIni}");
                        }
                    }
                    else
                    {
                        if (!ruloMigration.IsTestStyle)
                            errorByRowList.Add($"Machine no valid! Value \"{GetValue(nextMachine)}\", Row {row}, Col {colIni}");
                    }


                    //Validate lote
                    var lote = workSheet.Cell(row, ++colIni).Value;
                    if (lote.IsNumeric())
                        ruloMigration.Lote = lote.ToString();
                    else
                    {
                        string sLote = null;
                        if (!string.IsNullOrWhiteSpace(lote.ToString())) 
                            sLote = lote.ToString(); 
                        //else errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                        if (!string.IsNullOrWhiteSpace(sLote))
                        {
                            if (sLote.Contains("-"))
                            {
                                string[] sLoteArray = null;
                                sLoteArray = sLote.Split("-");

                                if (sLoteArray.Length == 2)
                                {
                                    if (!string.IsNullOrEmpty(sLoteArray[0]) && sLoteArray[0].IsNumeric())
                                        ruloMigration.Lote = sLoteArray[0];
                                    else
                                        errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                    ruloMigration.BeamStop = Convert.ToString(sLoteArray[1]);
                                }
                                else if (sLoteArray.Length == 3)
                                {
                                    if (!string.IsNullOrEmpty(sLoteArray[0]) && sLoteArray[0].IsNumeric())
                                        ruloMigration.Lote = sLoteArray[0];
                                    else
                                        errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                    if (!string.IsNullOrEmpty(sLoteArray[1]) && sLoteArray[1].IsNumeric())
                                        ruloMigration.Partiality = Convert.ToInt32(sLoteArray[1]);
                                    else
                                        errorByRowList.Add($"Lote patiality no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                    ruloMigration.BeamStop = Convert.ToString(sLoteArray[2]);
                                }
                                else
                                    errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                            }
                            else
                            {
                                var digits = from c in sLote
                                             where Char.IsDigit(c)
                                             select c;

                                var alphas = from c in sLote
                                             where !Char.IsDigit(c)
                                             select c;

                                if (digits != null && digits.Count() > 0)
                                    ruloMigration.Lote = string.Join("", digits);
                                else
                                    errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                ruloMigration.BeamStop = string.Join("", alphas);
                            }
                        }
                        else
                            errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");
                    }

                    //Validate beam
                    var beam = workSheet.Cell(row, ++colIni).Value;
                    if (beam.IsNumeric())
                    {
                        ruloMigration.Beam = Convert.ToInt32(beam);
                    }
                    else
                    {
                        string sBeam = null;
                        if (!string.IsNullOrWhiteSpace(beam.ToString())) 
                            sBeam = beam.ToString(); 

                        if (!string.IsNullOrWhiteSpace(sBeam))
                        {
                            if (sBeam.Contains("-"))
                            {
                                string[] sBeamArray = null;
                                sBeamArray = sBeam.Split("-");

                                if (!string.IsNullOrEmpty(sBeamArray[0]) && sBeamArray[0].IsNumeric())
                                    ruloMigration.Beam = Convert.ToInt32(sBeamArray[0]);
                                else
                                    errorByRowList.Add($"Beam no valid! Value \"{GetValue(beam)}\", Row {row}, Col {colIni}");

                                ruloMigration.IsToyotaText = Convert.ToString(sBeamArray[1]);
                            }
                            else
                            {
                                var digits = from c in sBeam
                                             where Char.IsDigit(c)
                                             select c;

                                var alphas = from c in sBeam
                                             where !Char.IsDigit(c)
                                             select c;

                                if (digits != null && digits.Count() > 0)
                                    ruloMigration.Beam = Convert.ToInt32(string.Join("", digits));
                                else
                                    errorByRowList.Add($"Beam no valid! Value \"{GetValue(beam)}\", Row {row}, Col {colIni}");

                                ruloMigration.IsToyotaText = string.Join("", alphas);
                            }
                        }
                        else
                            errorByRowList.Add($"Beam no valid! Value \"{GetValue(beam)}\", Row {row}, Col {colIni}");

                    }

                    //Vaidate loom
                    var loom = workSheet.Cell(row, ++colIni).Value;
                    if (loom.IsNumeric())
                    {
                        //TODO: Salón 2: 101-148, Salón 1: 149-248, Salón 3: 301-324, Salón 4: 401-462
                        string pattern = @"^(10[1-9]|1[1-3][0-9]|14[0-8])|(14[9]|1[5-9][0-9]|2[0-3][0-9]|24[0-8])|(30[1-9]|31[0-9]|32[0-4])|(40[1-9]|4[1-5][0-9]|46[0-2])$";
                        if (System.Text.RegularExpressions.Regex.IsMatch(loom.ToString(), pattern, System.Text.RegularExpressions.RegexOptions.CultureInvariant))
                            ruloMigration.Loom = Convert.ToInt32(loom);
                        else
                            errorByRowList.Add($"Loom does not exist \"{GetValue(loom)}\", Row {row}, Col {colIni}");
                    }
                    else errorByRowList.Add($"Loom no valid! Value \"{GetValue(loom)}\", Row {row}, Col {colIni}");

                    //Vaidate piece no
                    var pieceNo = workSheet.Cell(row, ++colIni).Value;
                    if (pieceNo.IsNumeric())
                        ruloMigration.PieceNo = Convert.ToInt32(pieceNo);
                    else
                    {
                        string sPieceNo = null;
                        if (!string.IsNullOrWhiteSpace(pieceNo.ToString())) 
                            sPieceNo = pieceNo.ToString();

                        if (!string.IsNullOrWhiteSpace(sPieceNo))
                        {
                            if (sPieceNo.Contains("-"))
                            {
                                string[] sPieceNoArray = null;
                                sPieceNoArray = sPieceNo.Split("-");

                                if (!string.IsNullOrEmpty(sPieceNoArray[0]) && sPieceNoArray[0].IsNumeric())
                                    ruloMigration.PieceNo = Convert.ToInt32(sPieceNoArray[0]);
                                else
                                    errorByRowList.Add($"Piece Number no valid! Value \"{GetValue(pieceNo)}\", Row {row}, Col {colIni}");

                                ruloMigration.PieceBetilla = Convert.ToString(sPieceNoArray[1]);
                            }
                            else
                            {
                                var digits = from c in sPieceNo
                                             where Char.IsDigit(c)
                                             select c;

                                var alphas = from c in sPieceNo
                                             where !Char.IsDigit(c)
                                             select c;

                                if (digits != null && digits.Count() > 0)
                                    ruloMigration.PieceNo = Convert.ToInt32(string.Join("", digits));
                                else
                                    errorByRowList.Add($"Piece Number no valid! Value \"{GetValue(pieceNo)}\", Row {row}, Col {colIni}");

                                ruloMigration.PieceBetilla = string.Join("", alphas);
                            }
                        }
                        else
                            errorByRowList.Add($"Piece Number no valid! Value \"{GetValue(pieceNo)}\", Row {row}, Col {colIni}");
                    }

                    //Validate meters
                    var meters = workSheet.Cell(row, ++colIni).Value;
                    if (meters.IsNumeric())
                        ruloMigration.Meters = Convert.ToDecimal(meters);
                    else errorByRowList.Add($"Meters no valid! Value \"{GetValue(meters)}\", Row {row}, Col {colIni}");

                    //TODO: DEFINIR SI SE CARGARAN LOS METROS DE ENGOMADO O NO
                    //Validate gummed meters
                    var gummedMeters = workSheet.Cell(row, ++colIni).Value;
                    if (gummedMeters == null || string.IsNullOrEmpty(gummedMeters.ToString()))
                        ruloMigration.SizingMeters = 0;
                    else if (gummedMeters.IsNumeric())
                        ruloMigration.SizingMeters = Convert.ToDecimal(gummedMeters);
                    else errorByRowList.Add($"Gummed Meters no valid! Value \"{GetValue(meters)}\", Row {row}, Col {colIni}");

                    //Validate status
                    var status = workSheet.Cell(row, ++colIni).Value;
                    if (!string.IsNullOrWhiteSpace(status.ToString()))
                    {
                        string sStatus = status.ToString();

                        var migrationCategory = migrationCategoryList.Where(x => x.Name.Equals(sStatus, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                        if (migrationCategory != null)
                            ruloMigration.MigrationCategoryID = migrationCategory.MigrationCategoryID;
                        else
                            errorByRowList.Add($"Status no valid! Value \"{GetValue(status)}\", Row {row}, Col {colIni}");

                    }
                    else errorByRowList.Add($"Status no valid! Value \"{GetValue(status)}\", Row {row}, Col {colIni}");

                    //TODO: DEFINIR SI SE CARGARAN LAS OBSERVACIONES O NO
                    //Vaidate observation
                    var observacion = workSheet.Cell(row, ++colIni).Value;
                    ruloMigration.Observations = Convert.ToString(observacion);

                    //Validate shift
                    var shift = workSheet.Cell(row, ++colIni).Value;
                    if (shift.IsNumeric())
                        ruloMigration.WeavingShift = Convert.ToInt32(shift);
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(shift.ToString()))
                        {
                            var digits = from c in shift.ToString()
                                         where Char.IsDigit(c)
                                         select c;

                            if (digits != null && digits.Count() > 0)
                                ruloMigration.WeavingShift = Convert.ToInt32(string.Join("", digits));
                            else
                                ruloMigration.WeavingShift = 0;
                        }
                        else
                            errorByRowList.Add($"Shift no valid! Row {row}, Col {colIni}");

                    }

                    ruloMigration.OriginID = 1; //Default 1=PP0
                    ruloMigration.WarehouseCategoryID = 1; //Default 1:RAW

                    ruloMigrationList.Add(ruloMigration);

                    if (errorByRowList != null && errorByRowList.Count > 0)
                    {
                        errorByRowListOfList.Add(errorByRowList);
                    }

                }

                if (errorByRowListOfList != null && errorByRowListOfList.Count > 0)
                {
                    throw new Exception($"Errors were found.");
                }
                else
                {
                    await factory.RuloMigrations.AddRange(ruloMigrationList, userId);
                }

                migrationControl.LastMigratedRowOfExcelFile = row;
                migrationControl.FileRowsTotal = rowSheet - (rowIni - 1);
                migrationControl.RowsTotalMigrated = row - (rowIni - 1);
                migrationControl.EndDate = DateTime.Now;

                await factory.RuloMigrations.UpdateMigrationControls(migrationControl, userId);
                string message = "Migration completed successfully";
                return (true, message, errorByRowListOfList);

            }
            catch (Exception ex)
            {
                string errorMessage = string.Empty;
                if (migrationControl != null)
                {
                    errorMessage = ex.Message + " ";
                    //foreach (var item in errorByRowListOfList)
                    //{
                    //    errorMessage += string.Join(". ", item);
                    //}
                    //errorMessage += errorByRowListOfList?.Count > 0 ? string.Join(". ", errorByRowListOfList) : null;

                    migrationControl.LastMigratedRowOfExcelFile = rowIni;
                    migrationControl.FileRowsTotal = rowSheet - (rowIni - 1);
                    migrationControl.RowsTotalMigrated = row - (rowIni - 1);
                    migrationControl.ErrorMesage = errorMessage;
                    migrationControl.BeginDate = DateTime.Now;

                    await factory.RuloMigrations.UpdateMigrationControls(migrationControl, userId);
                }

                return (false, errorMessage, errorByRowListOfList);
            }
        }

        private string GetValue(object obj)
        {
            string val = (obj != null) ? obj.ToString() : string.Empty;
            return val;
        }


    }
}
