using Microsoft.Extensions.DependencyInjection;
using WhatToDo.Core.Contracts;

namespace WhatToDo.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddIPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            return services;
        }
    }
}
