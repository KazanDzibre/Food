using System;
using System.Collections.Generic;
using AutoMapper;
using Food.Configuration;
using Food.Dtos;
using Food.Model;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : DefaultController
	{
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper, ProjectConfiguration configuration) : base(configuration)
		{
			_mapper = mapper;
		}

		//Post
		[HttpPost]
		[Authorize("Dispatcher")]
		public ActionResult<Order> CreateOrder(OrderRegisterDto orderRegisterDto)
		{
			var orderModel = _mapper.Map<Order>(orderRegisterDto);

			Order order = _orderService.Add(orderModel);

			return Ok(order);	
		}

		[HttpGet("{id}")]
		public ActionResult<Order> GetOrderById(int id)
		{
			var orderModel = _orderService.GetById(id);
			if(orderModel != null)
			{
				return Ok(orderModel);
			}
			return NotFound();
		}

		[HttpGet]
		public ActionResult<IEnumerable<Order>> GetAll()
		{
			var orders = _orderService.GetAll();
			if(orders != null)
			{
				return Ok(orders);
			}
			return NotFound();
		}
		
		//PATCH /api/order/{id}
		[HttpPatch("{id}")]
		[Authorize("Dispatcher")]
		public bool AssignDriver(int id, OrderPatchDto patchingOrder)
		{
			return _orderService.UpdateOrder(id,patchingOrder);
		}

		[HttpDelete("{id}")]
		public void DeleteOrder(int id)
		{
			Order order = _orderService.GetById(id);
			_orderService.Remove(order);
		}
	}
}
