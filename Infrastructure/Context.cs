using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Product { get; set; }
        //public DbSet<Permission> Permissions { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups  { get; set; }
        public DbSet<Permission> Permissions  { get; set; }
        public DbSet<GroupPermission> GroupPermissions   { get; set; }
    }
}
