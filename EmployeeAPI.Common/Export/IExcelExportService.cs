using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Common.Export
{
    public interface IExcelExportService
    {
        /// <summary> Exports the given data to an Excel file. </summary> ///
        byte[] ExportToExcel<T>( IEnumerable<T> data, string worksheetName);
    }
}
