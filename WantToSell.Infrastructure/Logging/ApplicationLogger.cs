using Microsoft.Extensions.Logging;
using WantToSell.Application.Contracts.Logging;

namespace WantToSell.Infrastructure.Logging
{
	public class ApplicationLogger<T> : IApplicationLogger<T>
	{
		private readonly ILogger<T> _logger;

		public ApplicationLogger(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<T>();
		}
		public void LogInformation(string message, params object[] args)
		{
			_logger.LogInformation(message, args);
		}

		public void LogWarning(string message, params object[] args)
		{
			_logger.LogWarning(message, args);
		}

		public void LogError(string message, params object[] args)
		{
			_logger.LogError(message, args);
		}
	}
}
