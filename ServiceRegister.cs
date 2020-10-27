using GreenDoorV1.Services;
using GreenDoorV1.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1
{
    public static class ServiceRegister
    {
        public static IServiceCollection GreenDoorV1(this IServiceCollection services)
        {
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IFeedPostService, FeedPostService>();
            services.AddTransient<IReviewService, ReviewService>();

            return services;
        }
    }
}
