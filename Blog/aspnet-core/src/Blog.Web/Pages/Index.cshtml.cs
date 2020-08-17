using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Blog.Web.Pages
{
    public class IndexModel : BlogPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}