using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WhatToDo.Api.Dtos;
using WhatToDo.Api.Middleware;
using WhatToDo.Api.Validations;
using WhatToDo.Persistence;

namespace WhatToDo.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation();

        services.AddTransient<IValidator<CreateItemDto>, CreateItemDtoValidator>();

        services.AddDbContext<WhatToDoContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("WhatToDoConnection")));

        services.AddEndpointsApiExplorer();

        services.AddIPersistenceServices();

        services.AddApiServices();

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action=Index}/{id?}");
        });
    }
}