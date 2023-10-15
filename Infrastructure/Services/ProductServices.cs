using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductServices : IProductServicce
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork) 
        {
         _unitOfWork = unitOfWork;
        }
        public async  Task<ResultDTO> AddProduct(ProductDTO productDTO)
        {
            if (productDTO.name != null && productDTO.price != null)
            {
                Product product = new Product();
                product.name = productDTO.name;
                product.price = productDTO.price;
                _unitOfWork.ProductRepository.Create(product);
                _unitOfWork.commit();
                return new ResultDTO()
                {
                    StatusCode = 200,
                    Data = "Product Is Added successfully"
                };
            }
            else
            {
                return new ResultDTO() { StatusCode = 400, Data = "Invalid operation" };
            }
        }
    }
}
