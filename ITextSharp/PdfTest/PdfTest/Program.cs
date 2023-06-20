using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public class HeaderEventHandler : PdfPageEventHelper
{
    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);

        PdfPTable headerTable = new PdfPTable(1);
        headerTable.TotalWidth = document.PageSize.Width;

        PdfPCell cell = new PdfPCell(new Phrase("Header Text"));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Border = Rectangle.NO_BORDER;
        headerTable.AddCell(cell);
        headerTable.WriteSelectedRows(0, -1, 0, document.Top, writer.DirectContent);
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

        document.Open();

        // Adding spacing
        document.Add(new Paragraph("\n\n"));



        // Create a table with one column
        PdfPTable table = new PdfPTable(1);

        // Create the box cell
        PdfPCell boxCell = new PdfPCell();
        boxCell.FixedHeight = 100f; // Adjust the box size as needed
        boxCell.Border = Rectangle.BOX;

        // Create the content for the box
        Paragraph content = new Paragraph("This is a square box with text.");
        content.Alignment = Element.ALIGN_CENTER;

        // Add the content to the cell
        boxCell.AddElement(content);

        // Add the box cell to the table
        table.AddCell(boxCell);

        // Add the table to the document
        document.Add(table);

        document.Add(new Paragraph("\n\n"));


        // Create another table with one column
        PdfPTable table2 = new PdfPTable(1);

        // Create the box cell
        PdfPCell cell2 = new PdfPCell();
        cell2.FixedHeight = 100f; // Adjust the box size as needed
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

        // table with multiple columns
        PdfPTable table3 = new PdfPTable(4);

        // Create the header cell
        PdfPCell headerCell = new PdfPCell(new Phrase("Header"));
        headerCell.BackgroundColor = new BaseColor(255, 0, 0); // Set the background color
        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
        headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;

        // Add the header cell to the table
        table3.AddCell(headerCell);

        // Add some content cells
        PdfPCell contentCell1 = new PdfPCell(new Phrase("Content 1"));
        // Create the content for the box
        Paragraph cell1Text = new Paragraph("This is a square box with text.");
        cell1Text.Alignment = Element.ALIGN_CENTER;
        contentCell1.AddElement(cell1Text);
        table3.AddCell(contentCell1);

        PdfPCell contentCell2 = new PdfPCell(new Phrase("Content 2"));
        table3.AddCell(contentCell2);

        PdfPCell contentCell3 = new PdfPCell(new Phrase("Content 3"));
        table3.AddCell(contentCell3);

        // Add the table to the document
        document.Add(table3);

        document.Close();
    }
}
