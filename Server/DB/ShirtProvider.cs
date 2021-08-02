using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ClothingShop.Server.Controllers;
using ClothingShop.Shared.Models;
namespace ClothingShop.Server.DB
{
    public class ShirtProvider
    {
        private readonly ShirtDbContext _context;
        private readonly ILogger _logger;
        public ShirtProvider(ShirtDbContext context, ILoggerFactory logger)
        {
            this._context = context;
            this._logger = logger.CreateLogger("ShirtProvider");
        
        }
        public List<Shirt> GetAllShirts()
        {
            return this._context.Shirts.ToList();
        }
        public void AddNewShirt(Shirt shirt)
        {
            var lastshirt = _context.Shirts.OrderBy(shirt=>shirt.Id).LastOrDefault();
            shirt.Id = lastshirt==null?0:(lastshirt.Id+1);
            if (shirt.Image == string.Empty)
                shirt.Image = "Images/NoImage.png";
            this._context.Add(shirt);
            this._context.SaveChanges();
        }
        public Shirt UpdateShirt(Shirt shirt)
        {
            var updatingShirt = this._context.Shirts.Where(s=>s.Id == shirt.Id).FirstOrDefault();
            if (updatingShirt == null)
                throw new InvalidDataException("id was not found");
            updatingShirt.SetValues(shirt);
            this._context.SaveChanges();
            return updatingShirt;
        }
        public Shirt DeleteShirt(int id)
        {
            var shirt = this._context.Shirts.Where(s=>s.Id == id).FirstOrDefault();
            if (shirt == null)
                return null;
            this._context.Remove(shirt);
            this._context.SaveChanges();
            return shirt;
        }
        public List<Shirt> BuyShirts(int[] CartShirtIds)
        {
            CartShirtIds.GroupBy(id=>id).Select(g=>g.Key).Select(id=>_context.Shirts.Where(shirt=>shirt.Id==id).First())
                .ToList().ForEach(shirt=>{shirt.Number -= CartShirtIds.Count(id=>id==shirt.Id);this.UpdateShirt(shirt);});
            this._context.SaveChanges();
            return this._context.Shirts.ToList();
        }
        public List<Shirt> ProduceShirts(int[] CartShirtIds)
        {
            CartShirtIds.GroupBy(id=>id).Select(g=>g.Key).Select(id=>_context.Shirts.Where(shirt=>shirt.Id==id).First())
                .ToList().ForEach(shirt=>{shirt.Number += CartShirtIds.Count(id=>id==shirt.Id);this.UpdateShirt(shirt);});
            this._context.SaveChanges();
            return this._context.Shirts.ToList();
        }
    }
}