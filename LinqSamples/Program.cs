using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using LinqSamples.Data;
using Microsoft.EntityFrameworkCore;

namespace LinqSamples
{
    class CustomerModel
    {
        public CustomerModel()
        {
            this.Orders = new List<OrderModel>();
        }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public List<OrderModel> Orders { get; set; }
    }

    class OrderModel
    {
        public int OrderID { get; set; }
        public decimal Total { get; set; }
        public List<ProductModel> Products { get; set; }
    }

    class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
                //NewMethod(db);
                //KayıtEkleme(db);

                //Sorgulama(db);

                ////change tracking
                //var product = db.Products.FirstOrDefault(p => p.ProductId == 1);

                //if(product != null)
                //{
                //    product.UnitsInStock += 10;
                //    db.SaveChanges();
                //    Console.WriteLine("veri güncelllendi");
                //}

                //db.Products.Attach(product); // change tracking kendimiz başlattık

                //var product2 = db.Products.Find(1);
                // olmaz
                //Console.WriteLine(product2.Category.CategoryName);

                //if (product2 != null)
                //{
                //    product2.UnitPrice = 28;

                //    db.Update(product2);
                //    db.SaveChanges();
                //}

                //db.SaveChanges();


                // kayıt silme
                //var p = db.Products.Find(88);

                //if (p != null)
                //{
                //    db.Products.Remove(p);
                //    db.SaveChanges();
                //}

                //var p1 = new Product()
                //{
                //    ProductId = 87
                //};
                //var p2 = new Product()
                //{
                //    ProductId = 84
                //};

                //var products = new List<Product>() { p1, p2 };

                ////db.Entry(p).State = EntityState.Deleted;
                //db.Products.RemoveRange(products);

                //db.SaveChanges();


                //// birden fazla tablo ile çalışma

                //var products2 = db.Products.Where(p => p.Category.CategoryName == "Beverages").ToList();

                //foreach (var p in products2)
                //{
                //    Console.WriteLine(p.ProductName);
                //}


                //LinqKullanimi(db);

                // Müşterilerin verdiği sipariş toplamı ?
                
                

                //var customers = db.Customers.Where(cus => cus.Orders.Count > 0)
                  var customers = db.Customers.Where(cus => cus.CustomerId == "PERIC")
                    .Select(cus => new CustomerModel()
                    {
                        CustomerID = cus.CustomerId,
                        CustomerName = cus.ContactName,
                        OrderCount = cus.Orders.Count,
                        Orders = cus.Orders.Select(order => new OrderModel()
                        {
                            OrderID = order.OrderId,
                            Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),
                            Products = order.OrderDetails.Select(od => new ProductModel()
                            {
                                ProductID = od.Product.ProductId,
                                ProductName = od.Product.ProductName,
                                Price = od.UnitPrice, // kullanıcı o satın alırkenki ürün fiyatı
                                Quantity = od.Quantity
                            }).ToList()

                        }).ToList()
                        /*
                        cus.ContactName,
                        OrderCount=cus.Orders.Count
                        */
                    })
                    .OrderBy(i => i.OrderCount)
                    .ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.CustomerName + "     Order Count:" + customer.OrderCount);
                    Console.WriteLine("*************************************");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine("Sipariş: " + order.OrderID + " => "+ order.Total);
                        Console.WriteLine("-------------------------------");
                        foreach (var orderProduct in order.Products)
                        {
                            Console.WriteLine(orderProduct.ProductID + "=>" +orderProduct.ProductName + "=>" + orderProduct.Price + "=>" + orderProduct.Quantity) ;
                        }
                        Console.WriteLine("/////////////////////////////////////");
                    }
                }

                var query = "4";
                var sql_product = db.Products.FromSqlRaw($"Select * from products where categoryId={query}").ToList();


            }
            Console.ReadLine();

            void KayıtEkleme(NorthwindContext db)
            {
                var category = db.Categories.Where(c => c.CategoryName == "").FirstOrDefault();
                var p1 = new Product() { ProductName = "Yeni ürün 1", Category = new Category(){CategoryName = "Yeni kategori"}};
                var p2 = new Product() { ProductName = "Yeni ürün 2", CategoryId = 3};

                var products = new List<Product>()
                {
                    p1, p2
                };

                var result = db.Products.OrderByDescending(p => p.UnitPrice).ToList();

                db.Products.AddRange(products);
            }
        }

        private static void LinqKullanimi(NorthwindContext db)
        {
            var categories = db.Categories.Where(c => c.Products.Any()).ToList();

            foreach (var category in categories)
            {
                //Console.WriteLine(category.CategoryName);
            }

            var products = db.Products
                .Select(s => new
                {
                    CompanyName = s.Supplier.CompanyName,
                    ContactName = s.Supplier.ContactName,
                    s.ProductName

                })
                    
                .ToList();

            foreach (var product in products)
            {
                //Console.WriteLine("Product Name: "+ product.ProductName+ "Product Company Name:" + product.CompanyName + "Product Contact Name" +product.ContactName);
                //Console.WriteLine();
                //Console.WriteLine();
            }

            var productss = (from p in db.Products
                select p).ToList();

            // inner join: suppliari olmayan product gelemez illa suplier olmak zorunda
            var productsSupplier = (from p in db.Products
                join s in db.Suppliers on p.SupplierId equals s.SupplierId
                select new
                {
                    p.ProductName,
                    ContactName = s.ContactName,
                    CompanyName = s.CompanyName

                }).ToList();
        }

        private static void Sorgulama(NorthwindContext db)
        {
            var result = db.Products.Count();

            Console.WriteLine(result);

            var result2 = db.Products.Count(i => !i.Discontinued);

            var result3 = db.Products.Min(i => i.UnitPrice);

            var result4 = db.Products.Where(i => i.CategoryId == 1).Max(m => m.UnitPrice);

            //satışta olan ürünlerin ortlama fiyatları
            var result5Ort = db.Products.Where(p => !p.Discontinued).Average(p => p.UnitPrice);
                
            var resultTotal = db.Products.Where(p => !p.Discontinued).Sum(p => p.UnitPrice);
        }

        private static void NewMethod(NorthwindContext db)
        {
            //var products = db.Products.ToList();
            var products = db.Products.Select(p => new ProductModel() { ProductName = p.ProductName, Price = p.UnitPrice }).ToList();

            var Product_First = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).FirstOrDefault();
            // top 1

            var productsChai = db.Products.Where(i => i.ProductName.Contains("Chai")).ToList();

            foreach (var p in products)
            {
                Console.WriteLine(p.ProductName + " " + p.Price);
            }

            var productsSi = db.Products
                .Select(p => new { p.ProductName, p.UnitPrice })
                .Where(p => p.UnitPrice == 1 || 30 < p.UnitPrice).ToList();
        }
    }
}
