using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Sale
{
    
    public class SaleProducts
    {
        private readonly IRepository _repository;
        public SaleProducts(IRepository repository)
        {
            _repository = repository;
        }

        public void SalesProductsForBuyer(int salesPointId, int productQuantity, int productId, string buyerName)
        {
            var result = _repository.JoinSalesPoint(salesPointId, productQuantity, productId);

            if (result != null)
            {
                _repository.AddSale(salesPointId, productQuantity, productId, buyerName);
                _repository.UpdateProductQuantity(productId, productQuantity);
                _repository.AddSalesData(productId, productQuantity);
            }
        }
        public void SalesProducts(int salesPointId,int productQuantity, int productId, string buyerName)
        {
           var result = _repository.JoinSalesPoint(salesPointId, productQuantity, productId);

            if(result != null) 
            {
                _repository.AddSale(salesPointId, productQuantity, productId, buyerName);
                _repository.UpdateProductQuantity(productId, productQuantity);
                _repository.AddSalesData(productId, productQuantity);
            }
        }
    }
}
