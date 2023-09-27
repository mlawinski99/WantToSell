﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WantToSell.Application.Contracts.Logs;

namespace WantToSell.Infrastructure.Logs
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
