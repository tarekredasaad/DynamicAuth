using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProductServicce
    {
        Task<ResultDTO> AddProduct(ProductDTO productDTO);
    }
}
