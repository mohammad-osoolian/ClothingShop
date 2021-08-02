using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClothingShop.Shared;
using ClothingShop.Server.DB;
using ClothingShop.Shared.Models;

namespace ClothingShop.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PantsController: ControllerBase
    {
        public readonly PantsProvider _provider;
        public PantsController(PantsProvider provider)
        {
            this._provider = provider;
        }
        [HttpGet]
        [Route("GetAllPants")]
        public List<Pants> GetAllPants()
        {
            return this._provider.GetAllPants();
        }
        [HttpPost]
        [Route("AddNewPants")]
        public Pants AddNewPants(Pants pants)
        {
            this._provider.AddNewPants(pants);
            return pants;
        }
        [HttpPut]
        [Route("UpdatePants")]
        public Pants UpdatePants(Pants pants)
        {
            return this._provider.UpdatePants(pants);
        }
        [HttpDelete]
        [Route("DeletePants")]
        public Pants DeletePants(int id)
        {
            return this._provider.DeletePants(id);
        }
        [HttpPut]
        [Route("BuyPants")]
        public List<Pants> BuyPants(int[] CartPantsIds)
        {
            return this._provider.BuyPants(CartPantsIds);
        }
        [HttpPut]
        [Route("ProducePants")]
        public List<Pants> ProducePants(int[] CartPantsIds)
        {
            return this._provider.ProducePants(CartPantsIds);
        }

    }
}