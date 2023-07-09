// NuGet:
// itext7
// itext7.bouncy-castle-adapter
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Reflection.PortableExecutable;

namespace PdfExtractor
{
    public class PDFSplitter
    {
        static public string targetPdfPath { get; set; }
        static void Main(string[] args)
        {
            if (args.Length < 2 || !File.Exists(args[0]))
            {
                System.Console.WriteLine("Usage: PDFExtractor PDF_file extracting_page");
                return;
            }
            targetPdfPath = args[0];

            int extractingPage = int.Parse(args[1]);
            SplitByPageRange(targetPdfPath, extractingPage);

            System.Console.WriteLine("Done.");
        }

        static private void SplitByPageRange(string targetPdfPath, int pageNum)
        {
            using (var pdfDocument = new PdfDocument(new PdfReader(targetPdfPath).SetUnethicalReading(true)))
            {
                using (var pdfNew = new PdfDocument(new PdfWriter($"{Path.GetFileNameWithoutExtension(targetPdfPath)}_{pageNum:000}.pdf")))
                {
                    var newPage = pdfDocument.GetPage(pageNum).CopyTo(pdfNew);
                    pdfNew.AddPage(newPage);
                }
            }
        }
    }
}