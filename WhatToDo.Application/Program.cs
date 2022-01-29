var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

builder.Services.AddDbContext<WhatToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WhatToDoConnection")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIPersistenceServices();

builder.Services.AddApiServices();

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

app.MapFallbackToFile("index.html");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var context = services.GetRequiredService<WhatToDoContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();


