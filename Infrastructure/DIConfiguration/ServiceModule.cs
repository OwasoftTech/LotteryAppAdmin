using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DIConfiguration
{
    public class ServiceModule
    {
        public static void Regsiter(IServiceCollection services)
        {
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<ISetupRepository, SetupRepository>();
            services.AddTransient<ILotteryRepository, LotteryRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
        }
    }
}
