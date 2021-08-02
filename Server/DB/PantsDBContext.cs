using Microsoft.EntityFrameworkCore;
using ClothingShop.Shared.Models;
namespace ClothingShop.Server.DB
{
    public class PantsDbContext: DbContext
    {
        public PantsDbContext(DbContextOptions options): base(options){}
        public DbSet<Pants> Pants{get;set;}
    }
}