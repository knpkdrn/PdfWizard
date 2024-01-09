using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace SplitPDF
{
    internal class Program
    {
        static PdfDocument pdfDoc;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Local link of pdf: ");
                string path = Console.ReadLine();
                LoadPdf(path);

                Console.Write("On which page should the new PDF begin? ");
                int startPage = int.Parse(Console.ReadLine());

                Console.Write("On which page should the new PDF begin? ");
                int endPage = int.Parse(Console.ReadLine());

                Console.Write("Do you want to save it as a PDF (1) or just store the text in a .txt (2) file? ");
                int saveFormat = int.Parse(Console.ReadLine());

                CutFromPdf(startPage, endPage, saveFormat, path);

                Console.WriteLine("\nDone.\n");

                Console.Write("Do you want to quit? Yes (0) or No (1) ");
                int quit = int.Parse(Console.ReadLine());

                if (quit == 0)
                    break;
                else if (quit == 1)
                    Console.Clear();
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error in quitting.");
                    Console.ReadKey();
                }
                    
            }

        }

        static void LoadPdf(string path)
        {
            pdfDoc = new PdfDocument(new PdfReader(path));
        }

        static void CutFromPdf(int start, int end, int saveFormat, string path)
        {
            // create the new file
            
            string newPath = path.Remove(path.Length - 4);
            

            if(saveFormat == 1)
            {
                string pathEnd = ".pdf";
                newPath += "_chopped" + pathEnd;

                using (var newFile = new PdfDocument(new PdfWriter(newPath)))
                    pdfDoc.CopyPagesTo(start, end, newFile);

            }
            else if(saveFormat == 2)
            {
                string pathEnd = ".txt";
                newPath += "_chopped" + pathEnd;

                List<string> allLines = new List<string>();
                for (int i = start; i <= end; ++i)
                {
                    string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
                    allLines.Add(text);
                }

                TxtFile newFile = new TxtFile(newPath, allLines.ToArray());
                if (!newFile.CreateFile())
                    Console.WriteLine("file is already existing.");
            }

        }
    }
}
