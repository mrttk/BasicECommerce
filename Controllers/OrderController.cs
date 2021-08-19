using BasicECommerce.Entity;
using BasicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private DataContext _db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = _db.Orders
                .Select(i => new AdminOrderModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    Total = i.Total,
                    OrderState = i.OrderState,
                    Count = i.OrderLines.Count
                })
                .OrderBy(i => i.OrderDate)
                .ToList();

            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var entity = _db.Orders.Where(i => i.Id == id)
                                   .Select(i => new OrderDetailsModel()
                                   {
                                       UserName = i.UserName,
                                       OrderId = i.Id,
                                       OrderNumber = i.OrderNumber,
                                       Total = i.Total,
                                       OrderDate = i.OrderDate,
                                       OrderState = i.OrderState,
                                       AddressTitle = i.AddressTitle,
                                       Address = i.Address,
                                       City = i.City,
                                       District = i.District,
                                       PostCode = i.PostCode,
                                       OrderLines = i.OrderLines
                                                                .Select(x => new OrderLineModel()
                                                                {
                                                                    ProductId = x.ProductId,
                                                                    ProductName = x.Product.Name,
                                                                    Image = x.Product.Image,
                                                                    Quantity = x.Quantity,
                                                                    Price = x.Price
                                                                }).ToList()
                                   })
                                   .FirstOrDefault();

            return View(entity);
        }

        public ActionResult UpdateOrderState(int orderId, EnumOrderState orderState)
        {
            var order = _db.Orders.FirstOrDefault(i => i.Id == orderId);

            if (order != null)
            {
                order.OrderState = orderState;
                _db.SaveChanges();

                TempData["message"] = "Order State Updated.";

                return RedirectToAction("Details", new { id = orderId });
            }


            return RedirectToAction("Index");
        }
    }
}