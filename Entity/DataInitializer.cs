using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicECommerce.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category(){Name="Kamera",Description="Kamera Ürünleri"},
                new Category(){Name="Bilgisayar",Description="Bilgisayar Ürünleri"},
                new Category(){Name="Elektronik",Description="Elektronik Ürünler"},
                new Category(){Name="Telefon",Description="Telefonlar"},
                new Category(){Name="Beyaz Eşya",Description="Beyaz Eşya Ürünleri"},
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            List<Product> products = new List<Product>()
            {
                new Product() {Name="Canon EOS 1200D",Description="18-55mm DC Camera",Image="camera.jpg",Price=350,IsHomePage=true,Stock=55,IsApproved=true,CategoryId=1},
                new Product() {Name="Nikon 120D",Description="18-55mm DC Camera",Image="camera1.jpg",Price=450,Stock=23,IsApproved=true,CategoryId=1},
                new Product() {Name="Canon EOS 700D",Description="18-55mm DC Camera",Image="camera2.jpg",Price=467,IsHomePage=true,Stock=34,IsApproved=false,CategoryId=1},
                new Product() {Name="Canon EOS 100D",Description="18-55mm DC Camera",Image="camera.jpg",Price=598,Stock=12,IsApproved=true,CategoryId=1},
                new Product() {Name="Nikon EOS 300",Description="18-55mm DC Camera",Image="camera1.jpg",Price=605,Stock=3,IsApproved=true,CategoryId=1},
                new Product() {Name="Dell Inspriron 5567",Description="Intel Core I7, 16 GB RAM, 1 TB SSD",Image="laptop.jpg",IsHomePage=true,Price=750,Stock=24,IsApproved=true,CategoryId=2},
                new Product() {Name="Lenovo Ideapad 500",Description="Intel Core I5, 8 GB RAM, 1 TB SSD",Image="laptop1.jpg",Price=600,Stock=10,IsApproved=true,CategoryId=2},
                new Product() {Name="Casper Nirvana",Description="Intel Core I7, 16 GB RAM, 1 TB SSD",Image="laptop2.jpg",IsHomePage=true,Price=800,Stock=25,IsApproved=true,CategoryId=2},
                new Product() {Name="Asus Notebook",Description="Intel Core I7, 16 GB RAM, 1 TB SSD",Image="laptop.jpg",Price=1200,Stock=63,IsApproved=true,CategoryId=2},
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}