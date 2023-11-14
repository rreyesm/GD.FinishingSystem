﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GD.FinishingSystem.WEB.Classes
{
    public class ExportToExcel
    {
        private class Total
        {
            internal string Name { get; set; }
            internal decimal Value { get; set; }
            internal int Column { get; set; }
        }

        public async Task<Tuple<bool, FileStreamResult>> Export<T>(string nameFabric, string area, string title, string fileName, List<T> list)
        {
            bool isOk = true;

            using (var workbook = new XLWorkbook())
            {
                var workSheet = workbook.Worksheets.Add(title);

                //Title
                int rowIni = 1;
                int colIni = 1;
                int rowEnd = 1;

                Type elementType = typeof(T);

                var property = typeof(List<T>).GetProperty("Count");
                int rowCount = (int)property.GetValue(list, null);
                int colEndCount = elementType.GetProperties().Length;

                workSheet.Cell(rowIni, colIni).Value = nameFabric;
                workSheet.Range(rowIni, rowIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ++rowIni;
                ++rowEnd;
                workSheet.Cell(rowIni, colIni).Value = area;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ++rowIni;
                ++rowEnd;
                workSheet.Cell(rowIni, colIni).Value = title;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rowIni++;
                colIni = 0;

                rowIni++;
                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    workSheet.Cell(rowIni, ++colIni).Value = propInfo.Name;
                    workSheet.Cell(rowIni, colIni).Style.Font.Bold = true;
                }

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    ++rowIni;
                    colIni = 0;
                    foreach (var propInfo in elementType.GetProperties())
                    {
                        var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                        workSheet.Cell(rowIni, ++colIni).Value = value;

                        Type propertyType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                        TypeCode typeCode = Type.GetTypeCode(propertyType);
                        switch (typeCode)
                        {
                            case TypeCode.Empty:
                                break;
                            case TypeCode.Object:
                                break;
                            case TypeCode.DBNull:
                                break;
                            case TypeCode.Boolean:
                                break;
                            case TypeCode.Char:
                                break;
                            case TypeCode.SByte:
                                break;
                            case TypeCode.Byte:
                                break;
                            case TypeCode.Int16:
                            case TypeCode.UInt16:
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                                workSheet.Cell(rowIni, colIni).Style.NumberFormat.Format = "###0";
                                break;
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                workSheet.Cell(rowIni, colIni).Style.NumberFormat.Format = "#,##0.00";
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.String:
                                break;
                            default:
                                break;
                        }

                    }

                }

                for (int i = 1; i < colEndCount; i++)
                    workSheet.Column(i).AdjustToContents();

                try
                {
                    var memoryStream = new MemoryStream();
                    workbook.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return await Task.FromResult(new Tuple<bool, FileStreamResult>(isOk, new FileStreamResult(memoryStream, contentType) { FileDownloadName = fileName }));
                }
                catch (Exception)
                {
                    return await Task.FromResult(new Tuple<bool, FileStreamResult>(false, null));
                }
            }


        }

        public async Task<Tuple<bool, FileStreamResult>> ExportWithDisplayName<T>(string nameFabric, string area, string title, string fileName, List<T> list, List<string> excludeFieldList = null, List<Tuple<int, string>> withColorList = null, string idFieldName = "", string setDateRange = "", List<string> totaleList = null)
        {
            bool isOk = true;

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet workSheet;
                if (title.Length > 31)
                    workSheet = workbook.Worksheets.Add(title.Substring(0, 28) + "...");
                else
                    workSheet = workbook.Worksheets.Add(title);

                //Title
                int rowIni = 1;
                int colIni = 1;
                int rowEnd = 1;

                Type elementType = typeof(T);

                var property = typeof(List<T>).GetProperty("Count");
                int rowCount = (int)property.GetValue(list, null);
                int colEndCount = 0;
                if (excludeFieldList != null)
                    colEndCount = elementType.GetProperties().Length - excludeFieldList.Count;
                else
                    colEndCount = elementType.GetProperties().Length;

                workSheet.Cell(rowIni, colIni).Value = nameFabric;
                workSheet.Range(rowIni, rowIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ++rowIni;
                ++rowEnd;
                workSheet.Cell(rowIni, colIni).Value = area;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ++rowIni;
                ++rowEnd;
                workSheet.Cell(rowIni, colIni).Value = title;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Merge();
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (setDateRange != string.Empty)
                {
                    ++rowIni;
                    ++rowEnd;
                    workSheet.Cell(rowIni, colIni).Value = setDateRange;
                    workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Merge();
                    workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Font.Bold = true;
                    workSheet.Range(rowIni, colIni, rowEnd, colEndCount).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                rowIni++;
                colIni = 0;

                rowIni++;
                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    if (excludeFieldList?.Contains(propInfo.Name) ?? false)
                        continue;

                    var displayName = GetPropertyDisplayName(propInfo);
                    if (!string.IsNullOrWhiteSpace(displayName))
                        workSheet.Cell(rowIni, ++colIni).Value = displayName;
                    else
                        workSheet.Cell(rowIni, ++colIni).Value = propInfo.Name;
                    workSheet.Cell(rowIni, colIni).Style.Font.Bold = true;
                }

                List<Total> totals = null;
                //Validate if exists totals
                if (totaleList != null && totaleList.Count > 0)
                    totals = new List<Total>();

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    ++rowIni;
                    colIni = 0;

                    //Added to set color 2023-02-17
                    bool withColor = false;
                    string color = string.Empty;
                    if (withColorList != null)
                    {
                        var valueRuloID = elementType.GetProperties().Where(x => x.Name == idFieldName).FirstOrDefault().GetValue(item, null) ?? DBNull.Value;
                        if (valueRuloID != null && withColorList.Any(x => x.Item1 == (int)valueRuloID))
                        {
                            withColor = true;
                            color = withColorList.Where(x => x.Item1 == (int)valueRuloID).Select(x => x.Item2).FirstOrDefault();
                        }
                    }

                    foreach (var propInfo in elementType.GetProperties())
                    {
                        if (excludeFieldList?.Contains(propInfo.Name) ?? false)
                            continue;

                        var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                        int col = ++colIni;
                        workSheet.Cell(rowIni, col).Value = value;

                        //Added to set color 2023-02-17
                        if (withColor)
                            workSheet.Cell(rowIni, col).Style.Fill.BackgroundColor = XLColor.FromArgb(int.Parse(color, NumberStyles.HexNumber));

                        Type propertyType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                        TypeCode typeCode = Type.GetTypeCode(propertyType);
                        switch (typeCode)
                        {
                            case TypeCode.Empty:
                                break;
                            case TypeCode.Object:
                                break;
                            case TypeCode.DBNull:
                                break;
                            case TypeCode.Boolean:
                                break;
                            case TypeCode.Char:
                                break;
                            case TypeCode.SByte:
                                break;
                            case TypeCode.Byte:
                                break;
                            case TypeCode.Int16:
                            case TypeCode.UInt16:
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                                workSheet.Cell(rowIni, colIni).Style.NumberFormat.Format = "###0";
                                if (totaleList != null && totaleList.Any(x=> x.Contains(propInfo.Name, StringComparison.InvariantCultureIgnoreCase)) && !totals.Any(x => x.Name == propInfo.Name))
                                    totals.Add(new Total() { Name = propInfo.Name, Value = value != DBNull.Value ? (decimal)value : 0, Column = colIni });
                                else if (totaleList != null && totaleList.Any(x => x.Contains(propInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    var element = totals.Where(x => x.Name == propInfo.Name).FirstOrDefault();
                                    element.Value += value != DBNull.Value ? (decimal)value : 0;
                                }
                                break;
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                workSheet.Cell(rowIni, colIni).Style.NumberFormat.Format = "#,##0.00";
                                workSheet.Cell(rowIni, colIni).Style.NumberFormat.Format = "###0";
                                if (totaleList != null && totaleList.Any(x => x.Contains(propInfo.Name, StringComparison.InvariantCultureIgnoreCase)) && !totals.Any(x => x.Name == propInfo.Name))
                                    totals.Add(new Total() { Name = propInfo.Name, Value = value != DBNull.Value ? (decimal)value : 0, Column = colIni });
                                else if (totaleList != null && totaleList.Any(x => x.Contains(propInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    var element = totals.Where(x => x.Name == propInfo.Name).FirstOrDefault();
                                    element.Value += value != DBNull.Value ? (decimal)value : 0;
                                }
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.String:
                                break;
                            default:
                                break;
                        }

                    }

                }

                if (totals != null && totals.Count > 0)
                {
                    ++rowIni;
                    workSheet.Cell(rowIni, 1).Style.Font.Bold = true;
                    workSheet.Cell(rowIni, 1).Value = "Total";
                    foreach (var item in totals)
                    {
                        workSheet.Cell(rowIni, item.Column).Style.Font.Bold = true;
                        workSheet.Cell(rowIni, item.Column).Style.NumberFormat.Format = "#,##0.00";
                        workSheet.Cell(rowIni, item.Column).Value = item.Value;
                    }
                }

                for (int i = 1; i < colEndCount; i++)
                    workSheet.Column(i).AdjustToContents();

                try
                {
                    var memoryStream = new MemoryStream();
                    workbook.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return await Task.FromResult(new Tuple<bool, FileStreamResult>(isOk, new FileStreamResult(memoryStream, contentType) { FileDownloadName = fileName }));
                }
                catch (Exception)
                {
                    return await Task.FromResult(new Tuple<bool, FileStreamResult>(false, null));
                }
            }


        }

        private string GetPropertyDisplayName(PropertyInfo pi)
        {
            var displayName = pi.CustomAttributes.FirstOrDefault()?.NamedArguments.Where(x => x.MemberName == "Name").FirstOrDefault().TypedValue.Value?.ToString();

            return displayName;
        }


    }
}
