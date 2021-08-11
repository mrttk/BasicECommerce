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
            var produts = _context.Products
                .Where(i => i.IsHomePage && i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + " ..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    CategoryId = i.CategoryId
                })
                .ToList();
            return View(produts);
        }

        public ActionResult Details(int id)
        {
            var produts = _context.Products.Where(i => i.Id == id).FirstOrDefault();
            return View(produts);
        }
        public ActionResult List(int? id)
        {
            var products = _context.Products
               .Where(i => i.IsApproved)
               .Select(i => new ProductModel()
               {
                   Id = i.Id,
                   Name = i.Name,
                   Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + " ..." : i.Description,
                   Price = i.Price,
                   Stock = i.Stock,
                   Image = i.Image,
                   CategoryId = i.CategoryId
               })
               .AsQueryable();

            if (id != null)
            {
                products = products.Where(i => i.CategoryId == id);
            }
            return View(products.ToList());
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }
    }
}