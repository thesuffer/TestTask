using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.Sale;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : Controller
    {
        private readonly IRepository _repository;
        private readonly SaleProducts _saleProducts;
        public ApplicationController(IRepository repository, SaleProducts saleProducts)
        {
            _repository = repository;
            _saleProducts = saleProducts;
        }

        [HttpPost]
        public void DbTestData()
        {
            _repository.AddTestDataBuyer();

            _repository.AddTestDataProducts();

            _repository.AddTestDataSalesPoint();

            _repository.AddTestDataProvidedProduct();

            _repository.AddTestDataSale();

            _repository.AddTestDataSalesData();

            _repository.BuyerPost();

            _repository.ProductPost();
        }

      
        [HttpGet("{salesPointId}, {productQuantity}, {productId}")]

        public IActionResult Get(int salesPointId, int productQuantity, int productId, string buyerName)
        {
            var exam = _repository.JoinSalesPoint(salesPointId, productQuantity, productId);

            if (exam == null)
            {
                return NotFound();
            }

            else
            {
                if (buyerName != null)
                {
                    _repository.AddBuyer(buyerName);
                }
                _saleProducts.SalesProducts(salesPointId, productQuantity, productId, buyerName);

                return Ok();
            }
        }

        [HttpGet("{Id}")]

        public IActionResult Get(int id)
        {
            _repository.BuyerRead(id);

            return Ok();
        }

        [HttpGet("{Name}")]

        public IActionResult Get(string name)
        {
            _repository.ProductRead(name);

            return Ok();
        }

        
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            _repository.DeleteBuyer(id);
            _repository.DeleteProduct(id);

            return Ok();
        }


        [HttpPut]

        public IActionResult Put()
        {
              _repository.BuyerUpdate();
              _repository.ProductUpdate();
              return Ok();
        }

        
    }
}

