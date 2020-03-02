using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOnlineStore.Shared.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class ProductItemRepository : Repository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(MyContext context) : base(context)
        {
        }

        public bool Dead(ProductItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetInventoryForStore(Guid storeId)
        {
            var result = _Context.ProductItems
                .Include(o => o.ProductOffer)
                .Where(pi => pi.MyStoreId == storeId).ToList();
            

            return result;
        }

        public ProductItem GetItemByNameFromInventory(Guid storeId, string productName)
        {
            var item = _Context.ProductItems
                .Where(p => p.Name == productName && p.MyStoreId == storeId)
                .FirstOrDefault();

            return item;
        }

        public IEnumerable<Offer> GetOfferOfStore(string storeId)
        {
            var items = _Context.Offers.Where(offer => offer.StoreId.ToString() == storeId).ToList();

            return items;
        }

        public IEnumerable<ProductItem> GetProductItemsWithQuantity(int quantity, string storeId)
        {
                return _Context.ProductItems.Where(e => e.MyStoreId.ToString() == storeId && e.Quantity <= quantity).ToList();         
        }

        public bool InsertProductOffer(Offer storeOffer)
        {
           if(_Context.Offers.Where(o=>o.Id==storeOffer.Id).ToList().Count == 0)
            {
                _Context.Offers.Add(storeOffer);
                return Save();
            }
           else
            {
                return false;
            }
        }

        public bool ProductHasOffer(string productId,string storeId)
        {          
                if(_Context.Offers.Where(o => o.MyProductId.ToString() == productId && o.StoreId.ToString() == storeId).FirstOrDefault() == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }

        public bool RemoveOffert(Offer offer)
        {
           
            if(_Context.Offers.Contains(offer))
            {
                _Context.Offers.Remove(offer);
                return Save();
            }
            else
            {
                return false;
            }

        }

        public bool UpdateProductPrice(string productId, double newprice)
        {
           if(_Context.ProductItems.Where(p=>p.Id.ToString()== productId).ToList().Count > 0)
            {
                var product= _Context.ProductItems.Where(p => p.Id.ToString() == productId).FirstOrDefault();

                _Context.ProductItems.RemoveRange(product);

                product.Price =(float)newprice;
              

                _Context.ProductItems.Add(product);

            }

            return Save();
        }
    }
}
