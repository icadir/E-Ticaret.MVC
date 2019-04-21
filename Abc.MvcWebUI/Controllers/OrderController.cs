using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(x => new AdminOrderModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderState = x.OrderState,
                Total = x.Total,
                Count = x.Orderlines.Count

            }).OrderByDescending(x => x.OrderDate)
              .ToList();
            return View(orders);
        }

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

            return View(entity);
        }

        public ActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState)
        {

            var order = db.Orders.FirstOrDefault(x => x.Id == OrderId);
            if (order != null)
            {
                order.OrderState = OrderState;
                db.SaveChanges();
                TempData["message"] = "Bilgileriniz Kayıt Edildi";

                return RedirectToAction("Details", new { id = OrderId });
            }


            return RedirectToAction("Index");
        }

    }
}