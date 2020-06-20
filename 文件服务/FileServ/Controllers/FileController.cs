using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileServ.Controllers
{
    public class FileUploadDto
    {
        /// <summary>
        /// 1=二进制上传 2= 网络路径解析
        /// </summary>
        public int Type { get; set; }

        public IFormFile File { get; set; }


        public string Url { get; set; }
    }


    public class FileModel
    {
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>文件路径</returns>
        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadDto dto)
        {
            ResposeResult result = new ResposeResult();
            if (dto.Type==0)
            {
                return Ok(result);
            }

            FileModel fileModel = new FileModel();
            string filePath = string.Empty;
            string webRootPath = _webHostEnvironment.WebRootPath; //获取wwwroot文件夹
            string fileDir = $"{webRootPath}/upload/{DateTime.Now.ToShortDateString()}";//文件目录 E:/upload/2020-06-20/
          
             
            try
            {
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                if (dto.Type == 1)
                {
                    IFormFile file = dto.File;
                    string fileExt = Path.GetExtension(file.FileName); //文件扩展名，不含“.”
                    if (string.IsNullOrEmpty(fileExt))
                    {
                        result.Msg = "找不到文件扩展名";
                        return Ok(result);
                    }

                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名

                    filePath = $"{fileDir}/{newFileName}";//文件路径

                    //通过文件流的方式保存文件
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else if (dto.Type == 2)
                {
                    Stream fromFile =await HttpClientHelp.Download(dto.Url, HttpMethod.Get);

                    string fileExt = Path.GetExtension(dto.Url); //文件扩展名
                    if (string.IsNullOrEmpty(fileExt))
                    {
                        fileExt = "jpg";
                        //result.Msg = "找不到文件扩展名";
                        //return Ok(result);
                    }
                    string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名

                    //已追加的方式 写入文件流
                    filePath = $"{fileDir}/{newFileName}";//文件路径
                    FileHelp.CopyFile(fromFile, filePath, 1024 * 1024);                   
                }
            }
            catch (Exception e)
            {
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                result.Msg = e.Message;
                return Ok(result);
            }
            result.IsSuccess = true;
            result.Code = HttpStatusCode.Created;
            fileModel.AbsolutePath = filePath;
            fileModel.VirtualPath =  filePath.Replace(webRootPath, "");
            result.Data = fileModel;
            return Ok(result);
        }
    }
}
