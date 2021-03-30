using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class ExportToExcel
    {
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

        public async Task<Tuple<bool, FileStreamResult>> ExportWithDisplayName<T>(string nameFabric, string area, string title, string fileName, List<T> list, List<string> excludeFieldList = null)
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

                rowIni++;
                colIni = 0;

                rowIni++;
                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    if (excludeFieldList != null && !excludeFieldList.Contains(propInfo.Name))
                    {
                        var displayName = GetPropertyDisplayName(propInfo);
                        if (!string.IsNullOrWhiteSpace(displayName))
                            workSheet.Cell(rowIni, ++colIni).Value = displayName;
                        else
                            workSheet.Cell(rowIni, ++colIni).Value = propInfo.Name;
                        workSheet.Cell(rowIni, colIni).Style.Font.Bold = true;
                    }
                }

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    ++rowIni;
                    colIni = 0;
                    foreach (var propInfo in elementType.GetProperties())
                    {
                        if (excludeFieldList != null && !excludeFieldList.Contains(propInfo.Name))
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
