﻿using System.Linq;
using Abc.MvcWebUI.Identity;
using Abc.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Controllers
{

    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;

        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        [Authorize]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders.Where(x => x.UserName == username)
                .Select(x => new UserOrderModel()
                {
                    Id = x.Id,
                    Total = x.Total,
                    OrderNumber = x.OrderNumber,
                    OrderState = x.OrderState,
                    OrderDate = x.OrderDate
                }).OrderByDescending(x => x.OrderDate)
                .ToList();
            return View(orders);
        }
        [Authorize]
        public ActionResult Details(int id)
        {

            var entity = db.Orders.Where(x => x.Id == id)
                .Select(x => new OrderDetailsModel()
                {
                    OrderId = x.Id,
                    OrderNumber = x.OrderNumber,
                    Total = x.Total,
                    OrderDate = x.OrderDate,
                    OrderState = x.OrderState,
                    AdresBasligi = x.AdresBasligi,
                    Adres = x.Adres,
                    Sehir = x.Sehir,
                    Semt = x.Semt,
                    Mahalle = x.Mahalle,
                    PostaKodu = x.PostaKodu,
                    Orderlines = x.Orderlines.Select(a => new OrderLineModel()
                    {
                        Quantity = a.Quantity,
                        Price = a.Price,
                        ProductId = a.ProductId,
                        ProductName = a.Product.Name.Length > 50 ? a.Product.Name.Substring(0, 40) + ".." : a.Product.Name,
                        Image = a.Product.Image
                    }).ToList()
                }).FirstOrDefault();

            return View();
        }

        // GET: Account
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
                // kayıt işlemleri

                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.UserName
                };

                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    // kullanıcı olustu ve bir rol e ata
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı Olusturma Hatası");
                }
            }
            return View(model);
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
                // Login İşlemleri
                var user = UserManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    // var olan kullanıcıyı sisteme dahil et
                    // applicationCookie oluşturup sisteme bırak.

                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    (new AuthenticationProperties()).IsPersistent = model.RememberMe;

                    authManager.SignIn(authProperties, identityclaims);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");
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