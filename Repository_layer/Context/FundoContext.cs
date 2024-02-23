using Microsoft.EntityFrameworkCore;
using Repository_layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_layer.Context
{
    public class FundoContext:DbContext
    {
        public FundoContext(DbContextOptions options):base(options)
        { }
        DbSet<DemoEntity> DemoEntities { get; set; }
        public DbSet<User> users { get; set; }
    }
}
