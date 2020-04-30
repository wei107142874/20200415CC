using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 智能
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            await client.GetAsync("http://192.168.2.107/gpio/" + 0);
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            await client.GetAsync("http://192.168.2.107/gpio/" + 1);
        }
    }
}