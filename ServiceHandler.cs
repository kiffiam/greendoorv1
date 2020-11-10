using GreenDoorV1.Services;
using GreenDoorV1.Services.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1
{
    public static class ServiceHandler
    {
        public static IServiceCollection GreenDoorV1(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IFeedPostService, FeedPostService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static void RegisterSwagger()
        {

        }
    }
}
