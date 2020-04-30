using Microsoft.AspNetCore.Mvc;
using System;

namespace MsgService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            System.Console.WriteLine($"{DateTime.Now}:健康检查中...");
            return Ok("ok");
        }
    }
}
