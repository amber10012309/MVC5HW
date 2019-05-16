using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Cust
{
    public class ExcelExport
    {
        public void Export<T>(List<T> data, Stream Path)
        {
            if (data != null && data.Count > 0)
            {
                //建立Excel
                XLWorkbook workbook = new XLWorkbook();
                var sheet = workbook.Worksheets.Add("Index");
                Type type = data.First().GetType();
                int colIdx = 1;
                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (!property.GetMethod.IsVirtual)
                        sheet.Cell(1, colIdx++).Value = property.Name;
                }

                int rowIdx = 2;
                foreach (T item in data)
                {
                    colIdx = 1;

                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        if (!property.GetMethod.IsVirtual)
                        {
                            PropertyInfo propertyInfo = type.GetProperty(property.Name);
                            sheet.Cell(rowIdx, colIdx++).Value = propertyInfo.GetValue(item);
                        }
                    }

                    rowIdx++;
                }

                //寫入檔案
                workbook.SaveAs(Path);
            }
        }
    }
}