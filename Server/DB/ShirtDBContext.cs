using Microsoft.EntityFrameworkCore;
using ClothingShop.Server.Controllers;
using ClothingShop.Shared.Models;
namespace ClothingShop.Server.DB
{
    public class ShirtDbContext: DbContext
    {
        public ShirtDbContext(DbContextOptions<ShirtDbContext> options): base(options){}
        public  DbSet<Shirt> Shirts{get;set;}
    }
}