using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.IO;

namespace NPOI系列
{
    class Program
    {
        static void Main(string[] args)
        {
            var hssfworkbook = new HSSFWorkbook();

            HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(0);

            ICell cell = row.CreateCell(0);

            cell.SetCellValue("Sales Report");

            ICellStyle style = hssfworkbook.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;

            IFont font = hssfworkbook.CreateFont();

            font.FontHeight = 20 * 20;

            style.SetFont(font);

            cell.CellStyle = style;

            sheet.AddMergedRegion(new CellRangeAddress(0,0,0,5));

            FileStream file = new FileStream(@"test.xls", FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
            Console.WriteLine("完成");
            Console.ReadKey();
        }
    }
}
