using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KA.Web.Extensions
{
    public class FileDownload : FileStreamResult
    {
        public FileDownload(Stream fileStream, string contentType,string fileDownloadName) : base(fileStream, contentType)
        {
            base.FileDownloadName = fileDownloadName;
        }
    }
}
