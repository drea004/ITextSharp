using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public class HeaderEventHandler : PdfPageEventHelper
{
    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);

        // Create the content for the title
        Paragraph titleParagraph = new Paragraph("International Transload Logistics\n" + "PO Box 5274\nNaperville, Illinois");
        Font boldFont = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD);
        titleParagraph.Alignment = Element.ALIGN_LEFT;
        titleParagraph.Font = boldFont; 

        // Add the table to the document
        document.Add(titleParagraph);


    }
}

//FOOTER
public class CustomFooter : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        // Create a footer table with one column
        PdfPTable footerTable = new PdfPTable(1);
        footerTable.TotalWidth = document.PageSize.Width;
        footerTable.DefaultCell.Border = Rectangle.NO_BORDER;

        // Add content to the footer
        PdfPCell footerCell = new PdfPCell(new Phrase("This is the footer", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL)));
        footerCell.HorizontalAlignment = Element.ALIGN_CENTER;
        footerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        footerTable.AddCell(footerCell);

        // Position the footer at the bottom of the page
        footerTable.WriteSelectedRows(0, -1, 0, document.BottomMargin, writer.DirectContent);

        base.OnEndPage(writer, document);
    }
}



public class Program
{
    static void Main()
    {
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("output.pdf", FileMode.Create));

        HeaderEventHandler eventHandler = new HeaderEventHandler();
        writer.PageEvent = eventHandler;

        // Attach the custom footer event handler
        CustomFooter footerEventHandler = new CustomFooter();
        writer.PageEvent = footerEventHandler;

        document.Open();

        // Adding spacing
        document.Add(new Paragraph("\n\n"));

        // Create a table with 3 columns
        PdfPTable table4 = new PdfPTable(1);
        table4.WidthPercentage = 20;
        table4.HorizontalAlignment = Element.ALIGN_LEFT; 

        // Add headers to the table
        table4.AddCell(GetHeaderCell("Bill To Info"));

        // Add custom rows to the table
        table4.AddCell(GetCustomCell("Invoice Number:          10001", 7, true));
        table4.AddCell(GetCustomCell("Invoice Date:          5/16/23", 7, true));

        // Add the table to the document
        document.Add(table4);

        // Adding spacing
        document.Add(new Paragraph("\n\n"));

        // Create a table with one column
        PdfPTable table = new PdfPTable(1);

        // Create the box cell
        PdfPCell boxCell = new PdfPCell();
        boxCell.FixedHeight = 200f; // Adjust the box size as needed
        boxCell.Border = Rectangle.BOX;

        // Create the content for the box using Paragraph
        Paragraph content = new Paragraph("ITL JOLIET\n");
        content.Alignment = Element.ALIGN_LEFT;

        Paragraph content2 = new Paragraph("Term:     Net 30\n");

        Paragraph content3 = new Paragraph("Order Number:     JOLIB100837\n");
        content3.Alignment = Element.ALIGN_LEFT;



        // Add the content to the cell
        boxCell.AddElement(content);
        boxCell.AddElement(content2);
        boxCell.AddElement(content3);


        // Add the box cell to the table
        table.AddCell(boxCell);

        // Add the table to the document
        document.Add(table);

        document.Add(new Paragraph("\n\n"));


        // Create another table with one column
        PdfPTable table2 = new PdfPTable(1);

        // Create the box cell
        PdfPCell cell2 = new PdfPCell();
        cell2.FixedHeight = 200f; // Adjust the box size as needed
        cell2.Border = Rectangle.BOX;

        // Create the content for the box
        Paragraph text = new Paragraph("This is a square box with text.");
        text.Alignment = Element.ALIGN_CENTER;

        // Add the content to the cell
        cell2.AddElement(text);

        // Add the box cell to the table
        table2.AddCell(cell2);

        // Add the table to the document
        document.Add(table2);

        document.Add(new Paragraph("\n\n"));

        // Create a table with three columns
        PdfPTable table3 = new PdfPTable(3);

        // Set the width percentage for the table
        table3.WidthPercentage = 100;

        // Set the table border properties
        table3.DefaultCell.Border = Rectangle.NO_BORDER;
        table3.DefaultCell.BorderColor = BaseColor.BLACK;
        table3.DefaultCell.BorderWidth = 0;

        // Set the cell padding
        float cellPadding = 5f;
        table3.DefaultCell.Padding = cellPadding;

        // Create and add cells to the table
        for (int i = 1; i <= 9; i++)
        {
            PdfPCell cell = new PdfPCell(new Phrase($"Cell {i}"));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER; 
            table3.AddCell(cell);
        }

        // Add the table to a wrapper cell with a border
        PdfPCell wrapperCell = new PdfPCell(table3);
        wrapperCell.Border = Rectangle.BOX;
        wrapperCell.BorderColor = BaseColor.BLACK;
        wrapperCell.BorderWidth = 1f;

        // Add the wrapper cell to a table with a single cell
        PdfPTable wrapperTable = new PdfPTable(1);
        wrapperTable.AddCell(wrapperCell);

        // Add the wrapper table to the document
        document.Add(wrapperTable);


        document.Add(new Paragraph("\n\n"));


        

        // Add more pages
        for (int i = 1; i <= 5; i++)
        {
            Paragraph paragraph = new Paragraph($"Page {i}");
            document.Add(paragraph);
            document.NewPage();
        }


        document.Close();
    }

    static PdfPCell GetHeaderCell(string text)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.BackgroundColor = BaseColor.BLACK;
        cell.BorderWidthTop = 1f;
        cell.BorderWidthBottom = 1f;
        cell.BorderWidthLeft = 1f;
        cell.BorderWidthRight = 1f;
        return cell;
    }

    static PdfPCell GetCustomCell(string text, int fontSize, bool isBold)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, fontSize, isBold ? Font.BOLD : Font.NORMAL)));
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.BackgroundColor = BaseColor.WHITE;
        cell.BorderWidth = 0;
        return cell;
    }
}
