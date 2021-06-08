using Food.Core;
using Food.Model;

namespace Food.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

		private readonly Context _context;

		public UnitOfWork(Context _context)
		{
			this._context = _context;
			Users = new UserRepository(this._context);	
				
		}

		public IUserRepository Users { get; private set; }
		
		public Context context
		{
			get { return _context; }
		}
        
		public int Complete()
        {
			return _context.SaveChanges();
        }

        public void Dispose()
        {
			context.Dispose();
        }

    }
}
