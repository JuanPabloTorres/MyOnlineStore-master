using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.DTOs.Orders;
using MyOnlineStore.Shared.Interfaces.Factories;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected IOrderRepository OrderRepo { get; private set; }
        protected IOrderFactory OrderFactory { get; private set; }
        public OrderController(IOrderRepository orderRepository, IOrderFactory orderFactory)
        {
            OrderRepo = orderRepository;
            OrderFactory = orderFactory;
        }
        // GET: api/Order
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateOrder([FromBody] NewOrder newOrder)
        {
            bool isOrderCreated;
            Order order;

            order = OrderFactory.CreateOrderFromNewOrder(newOrder);
            order.OrderStatus = null; // Don't insert new status
            isOrderCreated = await OrderRepo.AddAsync(order);

            if (isOrderCreated)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task UpdateOrder([FromBody] Order order)
        {
            order.OrderItems.ForEach(item
                    => item.ProductItem = null
                );
            order.OrderStatus = null;

            await OrderRepo.UpdateOrder(order);
        }

        [HttpGet("[action]/{orderId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task RemoveItemFromOrder([FromRoute] string orderId, [FromRoute] string productId)
        {
            Guid orderguid, productguid;

            if (Guid.TryParse(orderId,out orderguid) && Guid.TryParse(productId,out productguid))
            {
                await OrderRepo.RemoveItemFromOrder(orderguid, productguid);
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //[HttpGet("[action]/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IEnumerable<OrderDTO>> GetClientPendingOrders([FromRoute] string id)
        //{
        //    Guid guid;
        //    List<OrderDTO> resultDTO = new List<OrderDTO>();
        //    IEnumerable<Order> result = new List<Order>();

        //    if (Guid.TryParse(id, out guid))
        //    {
        //        result = new List<Order>(await OrderRepo.GetClientPendingOrdersAsync(guid));
        //    }

        //    foreach (var order in result)
        //    {
        //        resultDTO.Add(new OrderDTO(order));
        //    }

        //    return resultDTO;
        //}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<OrderDTO> GetClientPendingOrders([FromRoute] string id)
        {
            Guid guid;
            List<OrderDTO> resultDTO = new List<OrderDTO>();
            IEnumerable<Order> result = new List<Order>();

            if (Guid.TryParse(id, out guid))
            {
                result = new List<Order>(OrderRepo.GetClientPendingOrdersAsync(guid).Result);
            }

            foreach (var order in result)
            {
                resultDTO.Add(new OrderDTO(order));
            }

            return resultDTO;
        }
    }
}
