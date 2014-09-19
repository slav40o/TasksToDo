namespace TodoTasks.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using TodoTasks.Data;
    using TodoTasks.FileExporter;
    using Microsoft.AspNet.Identity;

    //[Authorize]
    public class GoogleDriveController : ApiController
    {
        private readonly ITodoTasksData data;
        public GoogleDriveController()
            :this(new TodoTasksData())
        {

        }

        public GoogleDriveController(ITodoTasksData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ExcelFileCreator.ExportReportToXlsxFile(this.data, userId);
            GoogleDriveFileExporter.UploadFile();
            return Ok();
        }
    }
}