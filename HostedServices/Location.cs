using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Food.Model;
using Food.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Food.HostedServices
{
    public class Location : IHostedService, IDisposable
    {

		private int executionCount = 0;
		private readonly ILogger<Location> _logger;
		private Timer _timer;


		public Location(ILogger<Location> logger)
		{
			_logger = logger;
		}

        public Task StartAsync(CancellationToken cancellationToken)
        {
			_logger.LogInformation("Timed Hosted Service running.");

			_timer = new Timer(SendLocation, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
        }

		public void SendLocation(object state)
		{
			var count = Interlocked.Increment(ref executionCount);

			_logger.LogInformation("Timed Hosted Service is working. COunt: {Count}", count);

			var unitOfWork = new UnitOfWork(new Context());
			IEnumerable<User> Drivers = unitOfWork.Users.GetUsersByType("Driver");

			foreach( var driver in Drivers )
			{
				Console.WriteLine("Driver: {0} Coordinations: Longitude {1}, Latitude {2}", driver.UserName, driver.longitude, driver.latitude);
			}

		}
        public Task StopAsync(CancellationToken cancellationToken)
        {
			_logger.LogInformation("Timed Hosted Service is stopping.");

			_timer?.Change(Timeout.Infinite,0);

			return Task.CompletedTask;
        }
        public void Dispose()
        {
			_timer?.Dispose();
        }
    }
}
