namespace TodoTasks.FileExporter
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using TodoTasks.Data;

    public static class ExcelFileCreator
    {
        public static void ExportReportToXlsxFile(ITodoTasksData data, string userId)
        {
            var tasks = data.Tasks.All().Where(t => t.Category.UserId == userId);

            //var newFile = new FileInfo(@".\" + userId + ".xlsx");
            var newFile = new FileInfo("../../15.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            ExcelPackage xlPackage = new ExcelPackage(newFile);

            using (xlPackage)
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("To do tasks");

                worksheet.Cells[1, 1].Value = "No:";
                worksheet.Cells[1, 2].Value = "Task";
                worksheet.Cells[1, 3].Value = "Created on";
                worksheet.Cells[1, 4].Value = "Deadline";
                worksheet.Cells[1, 5].Value = "Status";

                var columnsCount = worksheet.Dimension.End.Column;

                var row = 2;
                var no = 1;
                foreach (var task in tasks)
                {
                    worksheet.Cells[row, 1].Value = no;
                    worksheet.Cells[row, 2].Value = task.Content;
                    worksheet.Cells[row, 3].Value = task.CreationDate.ToString();
                    worksheet.Cells[row, 4].Value = task.Deadline == null ? "No deadline" : task.Deadline.ToString();
                    worksheet.Cells[row, 5].Value = task.Status == Models.StatusType.Completed ? "Completed" : "Not completed";

                    row++;
                }

                for (int i = 1; i <= columnsCount; i++)
                {
                    worksheet.Cells[1, i].Style.Font.Size = 12;
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Column(i).Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    worksheet.Column(i).AutoFit();
                }

                xlPackage.Save();
            }
        }
    }
}
