using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class HomeController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();
        // GET: Home
        public ActionResult Contact(string id)
        {
            return View();
        }
    }
}