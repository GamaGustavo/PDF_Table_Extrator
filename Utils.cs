using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.IO;
using System.Text;

namespace PDF_Table_Extrator
{
    public class Utils
    {
        public static string ConvertPdfToText(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new System.ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new System.ArgumentException($"File '{filePath}' not found.", nameof(filePath));

            }

            var resultado = new StringBuilder();

            using PdfReader leitorDePdf = new PdfReader(filePath);

            using PdfDocument pdfDoc = new PdfDocument(leitorDePdf);
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                resultado.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy));
            }
            pdfDoc.Close();
            leitorDePdf.Close();

            return resultado.ToString();
        }

    }
}