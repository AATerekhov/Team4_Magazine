﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.DataAccess
{
    public static class EntityFrameworkInstaller
    {
        public static async Task MigrationDataBaseAsync(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;

            await using var db = services.GetRequiredService<EfDbContext>();
            try
            {
                await db.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                throw;
            }
        }
    }
}
