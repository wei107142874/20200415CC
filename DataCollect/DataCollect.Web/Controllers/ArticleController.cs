using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataCollect.Web.Dto;
using DataCollect.Web.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataCollect.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        //public ArticleController(DapperHelper dapperHelper)
        //{
        //    this._dapperHelper = dapperHelper;
        //}

        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post(InsertArticle dto)
        {
            Article article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                Content_Type = dto.Content_Type,
            };
            DapperHelper.Insert(article);
        }

        // PUT api/<ArticleController>/5
        [HttpPut]
        public void Put(UpdateArticle dto)
        {
            Article article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                Content_Type = dto.Content_Type,
            };
            DapperHelper.Update(article);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
