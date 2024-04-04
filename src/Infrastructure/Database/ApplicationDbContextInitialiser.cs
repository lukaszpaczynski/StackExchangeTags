﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database
{
	public static class InitialiserExtensions
	{
		public static async Task InitialiseDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

			await initialiser.InitialiseAsync();
		}
	}

	public class ApplicationDbContextInitialiser
	{
		private readonly ILogger<ApplicationDbContextInitialiser> _logger;
		private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

		public async Task InitialiseAsync()
		{
			try
			{
				await _context.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while initialising the database.");
				throw;
			}
		}
	}
}
