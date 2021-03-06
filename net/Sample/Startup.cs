﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sample.Models;

namespace Sample {

    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();

            services
                .AddLogging()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<NorthwindContext>(options => options
                    .UseSqlServer("Server=.\\SQLEXPRESS; Database=Northwind; Trusted_Connection=True")
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                );

            services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });
        }

        public void Configure(IApplicationBuilder app) {
            app.UseMvc();
            app.UseStaticFiles();
            app.UseCors(options => options.AllowAnyOrigin());
        }
    }

}
