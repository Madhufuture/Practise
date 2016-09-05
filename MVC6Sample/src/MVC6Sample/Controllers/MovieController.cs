using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC6Sample.Controllers
{
    public class MovieController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Welcome(string name, int ID = 1)
        {
            ViewData["name"] = name;
            ViewData["ID"] = ID;

            return View();

            //return HtmlEncoder.Default.Encode($"Hello {name}, ID:{ID}");
        }
    }
}
