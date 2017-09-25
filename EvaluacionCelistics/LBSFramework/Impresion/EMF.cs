using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace LBSFramework.Impresion
{
    public class EMF
    {

        private static int m_currentPageIndex;
        private static IList<Stream> m_streams;

        // Crea un EMF (Enhanced Metafile) a partir de un reporte local recibido.
        public static void ImportarReporteLocal(LocalReport reporte, string tamanoPagina = "21;14.85", string margenes = "1;1;1;1")
        {
            string[] arrMargenes = margenes.Split(';');
            string[] arrTamanoPagina = tamanoPagina.Split(';');
            string deviceInfo =
              @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>" + arrTamanoPagina[0] + @"</PageWidth>
                    <PageHeight>" + arrTamanoPagina[1] + @"</PageHeight>
                    <MarginTop>" + arrMargenes[0] + @"</MarginTop>
                    <MarginLeft>" + arrMargenes[3] + @"</MarginLeft>
                    <MarginRight>" + arrMargenes[1] + @"</MarginRight>
                    <MarginBottom>" + arrMargenes[2] + @"</MarginBottom>
                </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            reporte.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Imprime el EMF en la impresora predeterminada
        public static void Imprimir()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("No hay nada para imprimir.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("No hay impresora predeterminada.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private static Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        // Handler for PrintPageEvents
        private static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
