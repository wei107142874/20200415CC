using Blog.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Blog.Web.Pages
{
    public abstract class BlogPageModel : AbpPageModel
    {
        protected BlogPageModel()
        {
            LocalizationResourceType = typeof(BlogResource);
        }
    }
}