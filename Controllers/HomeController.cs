using BasicECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicECommerce.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var result = _context.Products
                .Where(i => i.IsHomePage && i.IsApproved)
                .ToList();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = _context.Products.Where(i => i.Id == id).FirstOrDefault();
            return View(result);
        }
        public ActionResult List()
        {
            var result = _context.Products
               .Where(i => i.IsApproved)
               .ToList();
            return View(result);
        }
    }
}