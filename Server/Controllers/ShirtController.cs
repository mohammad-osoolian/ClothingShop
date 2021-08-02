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
    public class ShirtController: ControllerBase
    {
        public readonly ShirtProvider _provider;
        public ShirtController(ShirtProvider provider)
        {
            this._provider = provider;
        }
        [HttpGet]
        [Route("GetAllShirts")]
        public List<Shirt> GetAllShirts()
        {
            return this._provider.GetAllShirts();
        }
        [HttpPost]
        [Route("AddNewShirt")]
        public Shirt AddNewShirt(Shirt shirt)
        {
            this._provider.AddNewShirt(shirt);
            return shirt;
        }
        [HttpPut]
        [Route("UpdateShirt")]
        public Shirt UpdateShirt(Shirt shirt)
        {
            return this._provider.UpdateShirt(shirt);
        }
        [HttpDelete]
        [Route("DeleteShirt")]
        public Shirt DeleteShirt(int id)
        {
            return this._provider.DeleteShirt(id);
        }
        [HttpPut]
        [Route("BuyShirts")]
        public List<Shirt> BuyShirts(int[] CartShirtIds)
        {
            return this._provider.BuyShirts(CartShirtIds);
        }
        [HttpPut]
        [Route("ProduceShirts")]
        public List<Shirt> ProduceShirts(int[] CartShirtIds)
        {
            return this._provider.ProduceShirts(CartShirtIds);
        }
    }

}