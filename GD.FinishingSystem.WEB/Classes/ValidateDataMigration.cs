using ClosedXML.Excel;
using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
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
                workbook = new XLWorkbook(stream);
                workSheet = workbook.Worksheet(1);
            }
            catch (Exception)
            {
                return (false, "Error reading Excel file", null);
            }

            int rowIni = 2;
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

                    //Validate style
                    var style = workSheet.Cell(row, ++colIni).Value;

                    if (!string.IsNullOrWhiteSpace(style.ToString()))
                        ruloMigration.Style = Convert.ToString(style);
                    else errorByRowList.Add($"Style no valid! Value \"{GetValue(style)}\", Row {row}, Col {colIni}");

                    //Validate style name
                    var styleName = workSheet.Cell(row, ++colIni).Value;
                    if (!string.IsNullOrWhiteSpace(styleName.ToString()))
                        ruloMigration.StyleName = Convert.ToString(styleName);
                    else errorByRowList.Add($"Style Name no valid! Value \"{GetValue(styleName)}\", Row {row}, Col {colIni}");

                    //Validate machine
                    var nextMachine = workSheet.Cell(row, ++colIni).Value;
                    if (!string.IsNullOrWhiteSpace(nextMachine.ToString()))
                        ruloMigration.NextMachine = Convert.ToString(nextMachine);
                    else errorByRowList.Add($"Machine no valid! Value \"{GetValue(nextMachine)}\", Row {row}, Col {colIni}");

                    //Validate lote
                    var lote = workSheet.Cell(row, ++colIni).Value;
                    if (lote.IsNumeric())
                        ruloMigration.Lote = Convert.ToInt32(lote);
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

                                if (!string.IsNullOrEmpty(sLoteArray[0]) && sLoteArray[0].IsNumeric())
                                    ruloMigration.Lote = Convert.ToInt32(sLoteArray[0]);
                                else
                                    errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                ruloMigration.LoteLetter = Convert.ToString(sLoteArray[1]);
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
                                    ruloMigration.Lote = Convert.ToInt32(string.Join("", digits));
                                else
                                    errorByRowList.Add($"Lote no valid! Value \"{GetValue(lote)}\", Row {row}, Col {colIni}");

                                ruloMigration.LoteLetter = string.Join("", alphas);
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

                                ruloMigration.BeamStop = Convert.ToString(sBeamArray[1]);
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

                                ruloMigration.BeamStop = string.Join("", alphas);
                            }
                        }
                        else
                            errorByRowList.Add($"Beam no valid! Value \"{GetValue(beam)}\", Row {row}, Col {colIni}");

                    }

                    //Vaidate loom
                    var loom = workSheet.Cell(row, ++colIni).Value;
                    if (loom.IsNumeric())
                        ruloMigration.Loom = Convert.ToInt32(loom);
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

                    //Vañidate gummed meters
                    var gummedMeters = workSheet.Cell(row, ++colIni).Value;
                    if (gummedMeters.IsNumeric())
                        ruloMigration.GummedMeters = Convert.ToDecimal(gummedMeters);

                    //Vaidate status
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

                    //Vaidate observation
                    var observacion = workSheet.Cell(row, ++colIni).Value;
                    ruloMigration.Observations = Convert.ToString(observacion);

                    //Validate shift
                    var shift = workSheet.Cell(row, ++colIni).Value;
                    if (shift.IsNumeric())
                        ruloMigration.Shift = Convert.ToInt32(shift);
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(shift.ToString()))
                        {
                            var digits = from c in shift.ToString()
                                         where Char.IsDigit(c)
                                         select c;

                            if (digits != null && digits.Count() > 0)
                                ruloMigration.Shift = Convert.ToInt32(string.Join("", digits));
                            else
                                ruloMigration.Shift = 0;
                        }
                        else
                            errorByRowList.Add($"Shift no valid! Row {row}, Col {colIni}");

                    }

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
                    errorMessage += errorByRowList?.Count > 0 ? string.Join(". ", errorByRowList) : null;

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
