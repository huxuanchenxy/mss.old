using MSS.API.Core.V1.Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.API.Core.Infrastructure
{
    public static class EssentialServiceCollectionExtensions
    {
        public static IServiceCollection AddEssentialService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
