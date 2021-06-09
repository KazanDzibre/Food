using System.Collections.Generic;

namespace Food.Core
{
	public interface IRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);

		IEnumerable<TEntity> GetAll();
	}
}
