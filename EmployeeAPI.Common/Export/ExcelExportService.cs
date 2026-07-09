using ClosedXML.Excel;
using EmployeeAPI.Common.Export;

public class ExcelExportService : IExcelExportService
{
    public byte[] ExportToExcel<T>(
        IEnumerable<T> data,
        string worksheetName)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add(worksheetName);

        var properties = typeof(T).GetProperties();

        // Header
        for (int col = 0; col < properties.Length; col++)
        {
            var cell = worksheet.Cell(1, col + 1);

            cell.Value = properties[col].Name;

            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
        }

        // Data
        int row = 2;

        foreach (var item in data)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                worksheet.Cell(row, col + 1).Value =
                    properties[col].GetValue(item)?.ToString() ?? string.Empty;
            }

            row++;
        }

        // Auto Fit
        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}