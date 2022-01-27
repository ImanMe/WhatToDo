using System.Reflection;

namespace WhatToDo.Api
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
