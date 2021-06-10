using System;
using System.Collections.Generic;
using Food.Configuration;
using Food.Model;
using Food.Repository;

namespace Food.Service
{
	public class OrderService
	{
		public OrderService(){}

		public OrderService(ProjectConfiguration configuration)
		{

		}

		public Order Add(Order order)
		{
			if(order == null)
			{
				return null;
			}
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					unitOfWork.Orders.Add(order);
					unitOfWork.Complete();
				}
			}
			catch (Exception e)
			{
				return null;
			}
			return order;
		}

		public Order GetById(int id)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					var order = unitOfWork.Orders.GetOrderById(id);
					if(order == null)
					{
						return null;
					}
					return order;
				}
			}
			catch(Exception e)
			{
				return null;
			}
		}

		public IEnumerable<Order> GetAll()
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					var orders = unitOfWork.Orders.GetAll();
					if(orders == null)
					{
						return null;
					}
					return orders;
				}
			}
			catch(Exception e)
			{
				return null;
			}
		}
	}
}
