using System.Reflection;

namespace WhatToDo.Application
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateItemDto>, CreateItemDtoValidator>();
            services.AddTransient<IValidator<UpdateItemDto>, UpdateItemDtoValidator>();
            return services;
        }
    }
}
