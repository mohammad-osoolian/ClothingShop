using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ClothingShop.Server.Controllers;
using ClothingShop.Shared.Models;
using System.IO;

namespace ClothingShop.Server.DB
{
    public class PantsProvider
    {
        private readonly PantsDbContext _context;
        private readonly ILogger _logger;
        public PantsProvider(PantsDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger("PantsProvider");
        }
        public List<Pants> GetAllPants()
        {
            return this._context.Pants.ToList();
        }
        public void AddNewPants(Pants pants)
        {
            var lastPants = this._context.Pants.OrderBy(pants=>pants.Id).LastOrDefault();
            pants.Id = lastPants==null?0:(lastPants.Id+1);
            if (pants.Image == string.Empty || pants.Image == null)
                pants.Image = "Images/NoImage.png";
            this._context.Add(pants);
            this._context.SaveChanges();
        }
        public Pants UpdatePants(Pants pants)
        {
            var updatingPants = this._context.Pants.Where(p=>p.Id == pants.Id).FirstOrDefault();
            if (updatingPants == null)
                throw new InvalidDataException("id was not found");
            updatingPants.SetValues(pants);
            this._context.SaveChanges();
            return updatingPants;
        }
        public Pants DeletePants(int id)
        {
            var pants = this._context.Pants.Where(p=>p.Id == id).FirstOrDefault();
            if (pants == null)
                return null;
            this._context.Remove(pants);
            this._context.SaveChanges();
            return pants;
        }
        public List<Pants> BuyPants(int[] CartPantsIds)
        {
            CartPantsIds.GroupBy(id=>id).Select(g=>g.Key).Select(id=>_context.Pants.Where(pants=>pants.Id==id).First())
                .ToList().ForEach(pants=>{pants.Number -= CartPantsIds.Count(id=>id==pants.Id);this.UpdatePants(pants);});
            this._context.SaveChanges();
            return this._context.Pants.ToList();
        }
        public List<Pants> ProducePants(int[] CartPantsIds)
        {
            CartPantsIds.GroupBy(id=>id).Select(g=>g.Key).Select(id=>_context.Pants.Where(pants=>pants.Id==id).First())
                .ToList().ForEach(pants=>{pants.Number += CartPantsIds.Count(id=>id==pants.Id);this.UpdatePants(pants);});
            this._context.SaveChanges();
            return this._context.Pants.ToList();
        }
    }
}