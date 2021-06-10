using Food.Model;

namespace Food.Core
{
	public interface IOrderRepository : IRepository<Order>
	{
		Order GetOrderById(int id);
	}
}
