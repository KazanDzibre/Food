using System.Collections.Generic;
using Food.Dtos;
using Food.Model;

namespace Food.Core
{
    public interface IOrderService
    {
        Order Add(Order order);
        Order GetById(int id);
        IEnumerable<Order> GetAll();
        bool UpdateOrder(int id, OrderPatchDto patchingOrder);
        void Remove(Order order);
    }
}
