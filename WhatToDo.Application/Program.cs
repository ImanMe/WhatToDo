using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using WhatToDo.Application;
using WhatToDo.Application.Dtos;
using WhatToDo.Application.Middleware;
using WhatToDo.Application.Validations;
using WhatToDo.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();

builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

builder.Services.AddTransient<IValidator<CreateItemDto>, CreateItemDtoValidator>();

builder.Services.AddDbContext<WhatToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WhatToDoConnection")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIPersistenceServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

var env = builder.Environment;

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

if (!env.IsDevelopment()) app.UseSpaStaticFiles();

app.MapControllers();

app.MapFallbackToFile("index.html"); ;

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "ClientApp";

//    if (env.IsDevelopment()) spa.UseAngularCliServer("start");
//});

await app.RunAsync();