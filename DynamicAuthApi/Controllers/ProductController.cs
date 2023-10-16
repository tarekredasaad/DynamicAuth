using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServicce product;
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IProductServicce product
            ,IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.product = product;
        }

        [Authorize(Policy = "CanAddProduct")]
        [HttpPost]
        public  ActionResult<ResultDTO> AddProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };
            return Ok(product.AddProduct(productDTO));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = new Product();
           product =   unitOfWork.ProductRepository.GetById(id);
            return Ok(product);
        }


        [Authorize(Policy = "CanEditProduct")]
        [HttpPut]
        public ActionResult update(ProductDTO productDTO , int id) 
        {
            Product product = unitOfWork.ProductRepository.GetById(id);
            product.price = productDTO.price;
            product.name = productDTO.name;
            unitOfWork.ProductRepository.Update(product);
            unitOfWork.commit();
            return Ok(product);
        }
        
        [Authorize(Policy = "CanDeleteProduct")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            unitOfWork.ProductRepository.Delete(id);
            unitOfWork.commit();
            return Ok("product has been deleted");
        }

       
    }
}
