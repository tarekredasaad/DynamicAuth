using Domain;
using Domain.Interfaces.Repository;
using Infrastructure.Repository;
using Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly Context Context;
        public IRepository<ApplicationUser> UserRepository { get;private set; }

        public IRepository<Product> ProductRepository { get; private set; }


        //public IRepository<User> Users => throw new NotImplementedException();

        public UnitOfWork(Context context) 
        {
            Context = context;
            UserRepository = new Repository<ApplicationUser>(Context);
            ProductRepository = new Repository<Product>(Context);
        }
        public void commit() 
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
