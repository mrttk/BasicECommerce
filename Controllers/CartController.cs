using BasicECommerce.Entity;
using BasicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BasicECommerce.Controllers
{
    public class CartController : Controller
    {
        private DataContext _db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id)
        {
            var product = _db.Products.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int id)
        {
            var product = _db.Products.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        [Authorize]
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(ShippingDetails shippingDetails)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("CartError", "The Cart is Empty!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cart, shippingDetails);

                    cart.Clear();
                    return View("Completed");
                }
                else
                {
                    return View(shippingDetails);
                }
            }
            return View();
        }

        private void SaveOrder(Cart cart, ShippingDetails shippingDetails)
        {
            var order = new Order();
            order.OrderNumber = "ORD" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.AddressTitle = shippingDetails.AddressTitle;
            order.Address = shippingDetails.Address;
            order.City = shippingDetails.City;
            order.District = shippingDetails.District;
            order.Neighborhood = shippingDetails.Neighborhood;
            order.PostCode = shippingDetails.PostCode;
            order.OrderLines = new List<OrderLine>();
            foreach (var product in cart.CartLines)
            {
                var orderLine = new OrderLine();
                orderLine.Quantity = product.Quantity;
                orderLine.Price = product.Product.Price * product.Quantity;
                orderLine.ProductId = product.Product.Id;

                order.OrderLines.Add(orderLine);
            }

            _db.Orders.Add(order);
            _db.SaveChanges();
        }
    }
}