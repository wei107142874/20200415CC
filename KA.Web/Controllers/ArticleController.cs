using KA.Manager;
using KA.Manager.IDQ;
using KA.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace KA.Web.Controllers
{
    [ApiController]
    [Route("article/[action]")]
    public class ArticleController:ControllerBase
    {
        private readonly ArticleManager _articleManager;

        public ArticleController(ArticleManager articleManager)
        {
            this._articleManager = articleManager;
        }

        [HttpPost]
        public async Task Add(ArticleDtoAdd model)
        {
            await _articleManager.Add(model);
        }

        public async Task Get(long id)
        {
            var data= await _articleManager.Get(id);
            //return File(new FileStream("", FileMode.Open), "application/octet-stream", "");
        }
    }
}
