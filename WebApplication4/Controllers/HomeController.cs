using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
     public class HomeController : Controller {
        public ViewResult Index() {
            return View();
        }
    }
}

