﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Tester.Data;

public static class Configuration
{
    public static void Configure(IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<TesterContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
