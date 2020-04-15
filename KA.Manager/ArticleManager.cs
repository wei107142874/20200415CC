using KA.IRepository;
using KA.Manager.Entity;
using KA.Manager.IDQ;
using System;
using System.Threading.Tasks;

namespace KA.Manager
{
    public class ArticleManager
    {
        IRepository<Article> _articleRepository;
        
        public ArticleManager(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }


        public void Test()
        {
            _articleRepository.Add(new Article()
            {
                Text = "1234123",
                Title = "123vmvj"
            });
        }

        public async Task Add(ArticleDtoAdd model)
        {
            await _articleRepository.Add(new Article()
            {
                Text = model.Text,
                Title = model.Title,
                CategoryId = model.CategoryId
            });
        }

        public async Task<Article> Get(long id)
        {
             return await _articleRepository.QueryById(id);
        }


    }
}
