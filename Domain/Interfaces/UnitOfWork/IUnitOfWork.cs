using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        public IRepository<ApplicationUser> UserRepository { get; }
        public IRepository<Product> ProductRepository { get; }

        void commit();
    }
}
