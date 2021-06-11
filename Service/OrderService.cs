using System;
using System.Collections.Generic;
using Food.Configuration;
using Food.Dtos;
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

		public bool UpdateOrder(int id, OrderPatchDto patchingOrder)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					Console.WriteLine("To accept/decline order press: Y/N");
					string answer = Console.ReadLine();
					if(answer == "Y")
					{
						Order orderToAsing = unitOfWork.Orders.GetOrderById(id);
						if(orderToAsing == null)
						{
							return false;
						}
						unitOfWork.Orders.Update(orderToAsing);

						orderToAsing.DriverId = patchingOrder.DriverId;

						unitOfWork.Complete();

						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch(Exception e)
			{
				return false;
			}
		}

		public void Remove(Order order)
		{
			try
			{
				using(var unitOfWork = new UnitOfWork(new Context()))
				{
					unitOfWork.Orders.Remove(order);
					unitOfWork.Complete();
				}
			}
			catch(Exception e)
			{
				//nothing
			}
		}
	}
}
