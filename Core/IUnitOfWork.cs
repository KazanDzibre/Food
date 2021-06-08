using System;

namespace Food.Core
{
	public interface IUnitOfWork : IDisposable
	{
		int Complete();
	}
}
