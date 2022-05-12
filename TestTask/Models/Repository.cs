using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public interface IRepository
    {
        public SalesPoint JoinSalesPoint(int salesPointId, int productQuantity, int productId);

        public Sale AddSale(int salesPointId, int productQuantity, int productId, string buyerName);

        public ProvidedProduct UpdateProductQuantity(int productId, int productQuantity);

        public SalesDatum AddSalesData(int productId, int productQuantity);

        public Buyer AddBuyer(string buyerName);

        public Buyer DeleteBuyer(int id);

        public void BuyerPost();

        public Buyer BuyerUpdate();

        public Buyer BuyerRead(int id);

        public Product DeleteProduct(int id);

        public void ProductPost();

        public Product ProductUpdate();

        public Product ProductRead(string name);

        public void AddTestDataBuyer();
        public void AddTestDataProducts();

        public void AddTestDataSalesPoint();

        public void AddTestDataProvidedProduct();

        public void AddTestDataSale();

        public void AddTestDataSalesData();

    }
    public class Repository : IRepository
    {
        ApplicationDbContext _context;
        public Repository()
        {
            _context = new ApplicationDbContext();
        }

        public void AddTestDataBuyer()
        {
            Buyer Valera = new Buyer { Name = "Valera2000", SalesIds = 1 };
            Buyer Lena = new Buyer { Name = "LenaGryshko", SalesIds = 4 };
            Buyer Nastya = new Buyer { Name = "NastyuhaShaeva", SalesIds = 15 };

            _context.Buyers.Add(Valera);
            _context.Buyers.Add(Lena);
            _context.Buyers.Add(Nastya);

            _context.SaveChanges();
        }
        public void AddTestDataProducts()
        {
            Product dress = new Product { Name = "Платье", Price = 3000 };
            Product shirt = new Product { Name = "Майка", Price = 1000 };
            Product jeans = new Product { Name = "Джинсы", Price = 4000 };

            _context.Products.Add(dress);
            _context.Products.Add(shirt);
            _context.Products.Add(jeans);

            _context.SaveChanges();
        }

        public void AddTestDataSalesPoint()
        {
            SalesPoint firstPoint = new SalesPoint { Name = "Магазин 'Три толстушки'", ProvidedProducts = 1 };
            SalesPoint secondPoint = new SalesPoint { Name = "Магазин 'Новая волна'", ProvidedProducts = 3 };
            SalesPoint thirdPoint = new SalesPoint { Name = "Магазин 'Старая ведуга'", ProvidedProducts = 2 };

            _context.SalesPoints.Add(firstPoint);
            _context.SalesPoints.Add(secondPoint);
            _context.SalesPoints.Add(thirdPoint);

            _context.SaveChanges();
        }

        public void AddTestDataProvidedProduct()
        {
            ProvidedProduct firstProvided = new ProvidedProduct { ProductId = 1, ProductQuantity = 100 };
            ProvidedProduct secondProvided = new ProvidedProduct { ProductId = 2, ProductQuantity = 50 };
            ProvidedProduct thirdProvided = new ProvidedProduct { ProductId = 3, ProductQuantity = 35 };

            _context.ProvidedProducts.Add(firstProvided);
            _context.ProvidedProducts.Add(secondProvided);
            _context.ProvidedProducts.Add(thirdProvided);

            _context.SaveChanges();
        }

        public void AddTestDataSale()
        {
            DateTime firstDate = new DateTime(2022, 04, 26);
            TimeSpan firstTime = new TimeSpan(15, 23, 12);

            DateTime secondDate = new DateTime(2022, 04, 28);
            TimeSpan secondTime = new TimeSpan(17, 12, 21);

            DateTime thirdDate = new DateTime(2022, 04, 29);
            TimeSpan thirdTime = new TimeSpan(18, 16, 15);

            Sale firstSale = new Sale { Date = firstDate, Time = firstTime, SalesPointId = 1, BuyerId = 3, SalesData = 1, TotalAmount = 12100 };
            Sale secondSale = new Sale { Date = secondDate, Time = secondTime, SalesPointId = 2, BuyerId = 2, SalesData = 2, TotalAmount = 6400 };
            Sale thirdSale = new Sale { Date = thirdDate, Time = thirdTime, SalesPointId = 3, BuyerId = 1, SalesData = 3, TotalAmount = 3000 };

            _context.Sales.Add(firstSale);
            _context.Sales.Add(secondSale);
            _context.Sales.Add(thirdSale);

            _context.SaveChanges();
        }

        public void AddTestDataSalesData()
        {
            SalesDatum firstData = new SalesDatum { ProductId = 1, ProductQuantity = 15, ProductIdAmount = 12100 };
            SalesDatum secondData = new SalesDatum { ProductId = 2, ProductQuantity = 4, ProductIdAmount = 6400 };
            SalesDatum thirdData = new SalesDatum { ProductId = 3, ProductQuantity = 1, ProductIdAmount = 3000 };

            _context.SalesData.Add(firstData);
            _context.SalesData.Add(secondData);
            _context.SalesData.Add(thirdData);

            _context.SaveChanges();
        }

        public SalesPoint JoinSalesPoint(int salesPointId, int productQuantity, int productId)
        {
            
            SalesPoint salesPoint = new SalesPoint();
            ProvidedProduct providedProduct = new ProvidedProduct();

            if ((salesPoint=(from e in _context.SalesPoints where e.Id==salesPointId select e).FirstOrDefault()) !=null  && (providedProduct = (from e in _context.ProvidedProducts where e.ProductId == productId && e.ProductQuantity >= productQuantity select e).FirstOrDefault()) !=null)
            {
                salesPoint = (from p in _context.SalesPoints join c in _context.ProvidedProducts on p.ProvidedProducts equals c.Id select p).FirstOrDefault();
                return salesPoint;
            }

            else return null;
               
        }
  

        public Sale AddSale (int salesPointId, int productQuantity, int productId, string buyerName)
        {
            Sale sale = new Sale();

            sale = (from e in _context.Sales
                    where e.SalesPointId == salesPointId
                    select e).FirstOrDefault();
            sale.Date = DateTime.UtcNow.Date;
            sale.Time = DateTime.Now.TimeOfDay;
            sale.SalesPointId = salesPointId;
            var buyer = (from e in _context.Buyers where e.Name == buyerName select e.Id).FirstOrDefault();
            sale.BuyerId = buyer;
            sale.SalesData = sale.SalesData + 1;
            var totalAmount = (from e in _context.Products where e.Id == productId select e.Price).FirstOrDefault();
            sale.TotalAmount +=Convert.ToInt32(totalAmount);

            _context.SaveChanges();

            return sale;
        }

        public SalesDatum AddSalesData (int productId, int productQuantity)
        {
            SalesDatum salesDatum = new SalesDatum();
            salesDatum = (from e in _context.SalesData where e.ProductId == productId select e).FirstOrDefault();

            salesDatum.ProductId = productId;
            salesDatum.ProductQuantity = productQuantity;
            var price = (from e in _context.Products where e.Id == productId select e.Price).FirstOrDefault();
            salesDatum.ProductIdAmount = Convert.ToInt32(salesDatum.ProductQuantity * price);

            _context.SaveChanges();

            return salesDatum;
        }

        public ProvidedProduct UpdateProductQuantity(int productId, int productQuantity)
        {
            ProvidedProduct providedProduct = new ProvidedProduct();

            providedProduct = (from e in _context.ProvidedProducts where e.Id == productId select e).FirstOrDefault();
            providedProduct.ProductQuantity -= productQuantity;

            _context.SaveChanges();

            return providedProduct;
        }

        public Buyer AddBuyer(string buyerName)
        {
            Buyer buyer = new Buyer();

            var buyerId = (from e in _context.Buyers where e.Name == buyerName select e.Id).FirstOrDefault();

            var sales = (from e in _context.Sales join p in _context.SalesData on e.SalesData equals p.Id where e.BuyerId == buyerId select p.ProductQuantity).FirstOrDefault();
            
            buyer.SalesIds += sales;

            return buyer;
        }

        //CRUD for Byuer

        public Buyer DeleteBuyer(int id)
        {
            Buyer buyer = new Buyer();

            buyer = (from e in _context.Buyers where e.Id == id select e).SingleOrDefault();

            _context.Remove(buyer);

            _context.SaveChanges();

            return buyer;
        }

        public void BuyerPost ()
        {
            Buyer Alice = new Buyer { Name = "AliceJonson", SalesIds = 6 };

            _context.AddRange(Alice);
        }

        public Buyer BuyerUpdate ()
        {
            Buyer lena = new Buyer();

            if ((_context.Buyers.FirstOrDefault()) != null)
            {
                lena.Name = "Kolya123";
                lena.SalesIds = 2;

                _context.Buyers.Update(lena);
            }

            _context.SaveChanges();

            return lena;
        }

        public Buyer BuyerRead (int id)
        {
            Buyer buyer = new Buyer();

            buyer = (from e in _context.Buyers where e.Id == id select e).FirstOrDefault();

            return buyer;
        }

        //CRUD for Product

        public Product DeleteProduct(int id)
        {
            Product product = new Product();

            product = (from e in _context.Products where e.Id == id select e).SingleOrDefault();

            _context.Remove(product);

            _context.SaveChanges();

            return product;
        }

        public void ProductPost()
        {
            Product socks = new Product { Name ="Носки", Price = 500 };

            _context.AddRange(socks);
        }

        public Product ProductUpdate()
        {
            Product jeans = new Product();

            if ((_context.Products.FirstOrDefault()) != null)
            {
                
                jeans.Price = 6000;

                _context.Products.Update(jeans);
            }

            _context.SaveChanges();

            return jeans;
        }

        public Product ProductRead(string name)
        {
           Product product = new Product();

            product = (from e in _context.Products where e.Name == name select e).FirstOrDefault();

            return product;
        }

    }
}
