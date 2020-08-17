using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basic.Common;
using Basic.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDemo.Basic.Web.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserService _userService;

        public ValuesController(UserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
           long ID=  _userService.AddUser();
            //throw new BusinessException("我说的不行");
            //this.User.Claims[]
            return Ok(ID);
        }

        private Task<string> T1()
        {
            return Task.Factory.StartNew(() => {
                string a = DateTime.Now.ToString();
                throw new Exception(a);
                Console.WriteLine(a);
                return a;
            });
        }

        /// <summary>
        /// 测试2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lid"></param>
        /// <returns></returns>
        [HttpGet("{id}/list/{lid}")]
        public List<int> Get(int id,int lid)
        {
            return new List<int> {id,lid };
        }

        /// <summary>
        ///  测试3
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        ///  测试4
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">值</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// 测试5
        /// </summary>
        /// <param name="id">编号</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
