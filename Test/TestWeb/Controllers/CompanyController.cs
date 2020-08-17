using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWeb.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        static List<Company> companies=null;

        public CompanyController()
        {
            if(companies==null)
            {
                companies = new List<Company>()
                {
                    new Company{ Id =Guid.NewGuid(), Name ="重庆商号"},
                    new Company{ Id =Guid.NewGuid(), Name ="成都建材"},
                    new Company{ Id =Guid.NewGuid(), Name ="厦门银行"},
                    new Company{ Id =Guid.NewGuid(), Name ="上海老刀牌香烟"}
                };
            }
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompanies(Guid companyId)
        {
            var company = companies.Find(x => x.Id == companyId);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
    }

    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
