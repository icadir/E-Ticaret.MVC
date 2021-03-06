﻿using System.Collections.Generic;
using System.Data.Entity;

namespace Abc.MvcWebUI.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
                new Category(){Name="Kamera",Description = "Kamera ürünleri"},
                new Category(){Name="Bilgisayar",Description = "Bilgisayar ürünleri"},
                new Category(){Name="NoteBook",Description = "Notebook ürünleri"},
                new Category(){Name="İçeçekler",Description = "İçecek ürünleri"},
                new Category(){Name="Telefon",Description = "Telefon ürünleri"},
            };
            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();

            var urunler = new List<Product>()
            {
            new Product(){Name = "Makine",Price = 124,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 10,IsApproved = true,CategoryId = 2,IsHome =true,Image = "1.jpg"},
            new Product(){Name = "Makine2",Price = 22,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 20,IsApproved = true,CategoryId = 1,IsHome =true,Image = "2.jpg"},
            new Product(){Name = "Makine3",Price = 15,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 100,IsApproved = true,CategoryId = 4,IsHome =true,Image = "3.jpg"},
            new Product(){Name = "Makine4",Price = 44,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 5,IsApproved = true,CategoryId = 3,IsHome =true,Image = "2.jpg"},
            new Product(){Name = "Makine5",Price = 33,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 8,IsApproved = false,CategoryId = 2,Image = "3.jpg"} ,
            new Product(){Name = "Makine6",Price = 33,Description = "SüperÖTekiasdşlmqwşeşqwemşqwe",Stock = 8,IsApproved = false,CategoryId = 2,Image = "3.jpg"}
            };
            foreach (var urun in urunler)
            {
                context.Products.Add(urun);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}