﻿using System;
using System.Collections.Generic;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);

            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

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

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }

        }

        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            var order = new Order
            {
                OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString(),
                Total = cart.Total(),
                OrderDate = DateTime.Now,
                UserName = entity.UserName,
                AdresBasligi = entity.AdresBasligi,
                Adres = entity.Adres,
                Semt = entity.Semt,
                Sehir = entity.Sehir,
                Mahalle = entity.Mahalle,
                PostaKodu = entity.PostaKodu,
                Orderlines = new List<OrderLine>()
            };



            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine
                {
                    Quantity = pr.Quantity,
                    Price = pr.Quantity*pr.Product.Price,
                    ProductId = pr.Product.Id
                };

                order.Orderlines.Add(orderline);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}