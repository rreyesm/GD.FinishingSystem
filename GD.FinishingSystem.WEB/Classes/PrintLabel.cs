using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using GD.FinishingSystem.Entities;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public class PrintLabel : IDisposable
    {
        private bool isDisposed = false;
        private Report Report { get; set; }
        private string reportPDFFileName = "PDF_Report.pdf";
        PDFSimpleExport export = null;

        public PrintLabel()
        {
        }

        public async Task<bool> ReportDBData(Rulo rulo, string reportPathName, string printerNameIP)
        {
            try
            {


                string filePath = string.Empty;

                export = new PDFSimpleExport();

                Report = new Report();
                Report.Load(reportPathName);

                var ruloListTemp = new List<Rulo>() { rulo };
                Report.RegisterData(ruloListTemp, "Rulo");
                Report.GetDataSource("Rulo").Enabled = true;

                Report.Prepare();

                export.Export(Report, reportPDFFileName);

                filePath = Config.ApplicationFolder + reportPDFFileName;
                await PrintPDF(filePath, printerNameIP);

                Report.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        /// <summary>
        /// Prints a PDF using its RAW data directly to the printer.It requires the nuGET package RawPrint
        /// </summary>
        private async Task PrintPDF(string filePath, string printerNameIP)
        {
            // Absolute path to your PDF to print (with filename)
            //string filepath = Config.ApplicationFolder + reportPDFFileName;
            // The name of the PDF that will be printed (just to be shown in the print queue)
            // The name of the printer that you want to use
            // Note: Check step 1 from the B alternative to see how to list
            // the names of all the available printers with C#

            string filename = System.IO.Path.GetFileName(filePath);

            MemoryStream ms = null;
            PdfReader pdfReader = new PdfReader(filePath);
            int numPages = pdfReader.NumberOfPages;
            for (int i = 1; i <= numPages; i++)
            {
                PdfDictionary page = pdfReader.GetPageN(i);
                PdfNumber pdfNumber = page.GetAsNumber(PdfName.Rotate);
                int rotation = pdfNumber == null ? 90 : (pdfNumber.IntValue + 90) % 360;
                page.Put(PdfName.Rotate, new PdfNumber(rotation));
            }

            ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(pdfReader, ms);
            stamper.Close();
            pdfReader.Close();

            string zpl = PDFtoZPL.Conversion.ConvertPdfPage(ms);
            ms.Flush();

            await RawPrinterHelper.PrintToZPLByIP(printerNameIP, zpl);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                //Dispose managed state (managed objects).
                Report?.Dispose();
                export?.Dispose();
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            //Dispose of unmanaged resources.
            Dispose(true);
            //Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
