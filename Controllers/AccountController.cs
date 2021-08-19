using BasicECommerce.Entity;
using BasicECommerce.Identity;
using BasicECommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicECommerce.Controllers
{
    public class AccountController : Controller
    {
        private DataContext _db = new DataContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        [Authorize]
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var orders = _db.Orders.Where(i => i.UserName == userName)
                                   .Select(i => new UserOrderModel()
                                   {
                                       Id = i.Id,
                                       OrderNumber = i.OrderNumber,
                                       OrderDate = i.OrderDate,
                                       OrderState = i.OrderState,
                                       Total = i.Total
                                   })
                                   .OrderByDescending(i => i.OrderDate)
                                   .ToList();
            return View(orders);
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var entity = _db.Orders.Where(i => i.Id == id)
                                   .Select(i => new OrderDetailsModel()
                                   {
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

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;

                IdentityResult result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if (roleManager.RoleExists("User"))
                    {
                        userManager.AddToRole(user.Id, "User");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "User could't be created!");
                }
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityClaims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = bool.Parse(model.RememberMe.ToString());

                    authManager.SignIn(authProperties, identityClaims);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "User could not be login!");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}