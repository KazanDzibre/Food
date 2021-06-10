using System.Linq;
using Food.Core;
using Food.Model;

namespace Food.Repository
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(Context context) : base(context)
		{

		}

        public Order GetOrderById(int id)
        {
			return context.Orders.Where(x => x.Id == id).FirstOrDefault();
        }
    }

}
