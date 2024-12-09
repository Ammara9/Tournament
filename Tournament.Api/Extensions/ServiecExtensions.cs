﻿namespace Tournament.Api.Extensions
{
    public static class ServiecExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(builder =>
            {
                builder.AddPolicy(
                    "AllowAll",
                    p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                );
            });
        }
    }
}