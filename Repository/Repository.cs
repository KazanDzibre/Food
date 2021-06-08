using System.Collections.Generic;
using Food.Core;
using Food.Model;
using Microsoft.EntityFrameworkCore;

namespace Food.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext _context;

		public Context context
		{

			get { return _context as Context; }
		}

		public Repository(DbContext context)
		{
			_context = context;
		}
        public void Add(TEntity entity)
        {
			_context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
