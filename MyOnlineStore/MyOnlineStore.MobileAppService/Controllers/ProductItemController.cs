using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        protected readonly IProductItemRepository _ProductItemRepo;
        public ProductItemController(IProductItemRepository productItemRepository)
        {
            _ProductItemRepo = productItemRepository;
        }
        // GET: api/ProductItem
        [HttpGet]
        public IEnumerable<ProductItem> GetAll()
        {
            var resultList = _ProductItemRepo.GetAll();
            return resultList;
        }

        // GET: api/ProductItem/5
        [HttpGet("{id}")]
        public ProductItem Get(string id)
        {
            Guid guid;
            ProductItem item = null;
            if (Guid.TryParse(id,out guid))
            {
                item = _ProductItemRepo.Get(guid);
            }
           
            return item;
        }

        // POST: api/ProductItem
        [HttpPost]
        public void Create([FromBody] ProductItem item)
        {
           bool result = _ProductItemRepo.Add(item);
        }

        // PUT: api/ProductItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Delete(string id)
        {
            Guid guid;
            ProductItem item = null;
            if (Guid.TryParse(id, out guid))
            {
                item = _ProductItemRepo.Get(guid);
                _ProductItemRepo.Remove(item);
            }
            
        }

       

        [HttpGet("[action]/{storeId}/{productName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ProductItem GetItemByNameFromInventory([FromRoute]string storeId,[FromRoute]string productName)
        {
            Guid guid = Guid.Parse(storeId);
            var result = _ProductItemRepo.GetItemByNameFromInventory(guid, productName);
            return result;
        }

        [HttpGet("[action]/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<ProductItem> GetInventoryForStore([FromRoute]Guid storeId)
        {
            var result = _ProductItemRepo.GetInventoryForStore(storeId);
            return result;
        }

      

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool InsertOffer([FromBody]Offer item)
        {
            var result = _ProductItemRepo.InsertProductOffer(item);
            if (result)
            {
               var prodtoUpdate = _ProductItemRepo.Get(item.MyProductId);

                var temp = prodtoUpdate.Price;
                prodtoUpdate.RealPrice = temp;
                prodtoUpdate.Price = (float)item.BuyOne;
              

                _ProductItemRepo.Update(prodtoUpdate);
            }

            return result;


        }

        //Obtener los feature items de un store en especifico
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Offer> GetOfferOfStore(Guid id)
        {
            return _ProductItemRepo.GetOfferOfStore(id.ToString());
        }

        [HttpDelete(template: "[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool RemoveOffert([FromBody]Offer offer)
        {

            bool updated = _ProductItemRepo.RemoveOffert(offer);

            if (updated)
            {
                var prodtoUpdate = _ProductItemRepo.Get(offer.MyProductId);

                prodtoUpdate.Price = (float)prodtoUpdate.RealPrice;
                prodtoUpdate.RealPrice = 0;

                _ProductItemRepo.Update(prodtoUpdate);
            }
            return updated;
        }

        [HttpGet("[action]/{quantity}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<ProductItem> GetProductItemsWithQuantity(int quantity,string storeId)
        {
            var result = _ProductItemRepo.GetProductItemsWithQuantity(quantity, storeId);
            return result;
        }

        [HttpGet("[action]/{productId}/{storeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool ProductHasOffer(string productId,string storeId)
        {
            var result = _ProductItemRepo.ProductHasOffer(productId,storeId);
         

            return result;


        }
    }
}
