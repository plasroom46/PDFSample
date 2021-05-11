using System;
using System.IO;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDFSample
{
    class Program
    {
        public const String DATA = "united_states.csv";

        public const String DOG = "dog.bmp";

        public const String DEST = "united_states.pdf";
        static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            CreatePdf(DEST);
        }

        static void CreatePdf(String dest)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf, PageSize.A4.Rotate());
            document.SetMargins(20, 20, 20, 20);


            // Compose Paragraph
            Image dog = new Image(ImageDataFactory.Create(DOG));
            Paragraph p = new Paragraph("").Add(dog);
            // Add Paragraph to document
            document.Add(p);


            // PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            PdfFont font = PdfFontFactory.CreateFont("kaiu.ttf", PdfEncodings.IDENTITY_H, true);

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Table table = new Table(9, false) //UnitValue.CreatePercentArray(new float[] { 4, 1, 3, 4, 3, 3, 3, 3, 1 })
                .UseAllAvailableWidth();
            using (StreamReader sr = File.OpenText(DATA))
            {
                String line = sr.ReadLine();
                Process(table, line, bold, true);
                while ((line = sr.ReadLine()) != null)
                {
                    Process(table, line, font, false);
                }
            }

            document.Add(table);

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            document.Add(table);

            //Close document
            document.Close();
        }

        static void Process(Table table, String line, PdfFont font, bool isHeader)
        {
            //StringTokenizer tokenizer = new StringTokenizer(line, ";");
            //while (tokenizer.HasMoreTokens())
            //{
            //    if (isHeader)
            //    {
            //        table.AddHeaderCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
            //    }
            //    else
            //    {
            //        table.AddCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
            //    }
            //}

            foreach (var cell in line.Split(';'))
            {
                if (isHeader)
                {
                    table.AddHeaderCell(new Cell().Add(new Paragraph(cell).SetFont(font)));
                }
                else
                {
                    table.AddCell(new Cell().Add(new Paragraph(cell).SetFont(font)));
                }
            }
        }
    }
}
