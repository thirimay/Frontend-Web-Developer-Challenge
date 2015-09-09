using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Holmusk_FoodApp.Models
{
    public class MyDBContext : DbContext
    {
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodLog> FoodLog { get; set; }
    }
}