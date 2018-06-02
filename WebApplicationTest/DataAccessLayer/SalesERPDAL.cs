using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplicationTest.DataAccessLayer;

using WebApplicationTest.Models;

namespace WebApplicationTest.DataAccessLayer
{
    
    public class SalesERPDAL:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AreaModel> AreaInfo { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
            
        //    base.OnModelCreating(modelBuilder);
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("newsletter_log");
            modelBuilder.Entity<AreaModel>().ToTable("area");
            base.OnModelCreating(modelBuilder);
        }
    }
}