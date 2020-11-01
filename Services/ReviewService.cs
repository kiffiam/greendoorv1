using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenDoorV1.Services
{
    public class ReviewService : IReviewService
    {
        protected ApplicationDbContext Context { get; }

        public ReviewService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Review> AddReview(Review review)
        {
            review.Room = await Context.Rooms.FindAsync(review.Id);

            review.ApplicationUser = await Context.Users.SingleOrDefaultAsync(u => u.Id.Equals(review.UserId));

            await Context.Reviews.AddAsync(review);

            await Context.SaveChangesAsync();

            return review;
        }

        public Task<bool> DeleteReview(long? reviewId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var result = await Context.Reviews.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Review>> GetRoomReviews(long? roomId)
        {
            var roomReviews = await Context.Reviews.Where(r => r.RoomId.Equals(roomId)).ToListAsync();
            //var roomReviews = await Context.Reviews.Where(r => r.RoomId.Equals(roomId)).Include(x => x.Room).ToListAsync();

            if (roomReviews.Count == 0)
            {
                return null;
            }

            return roomReviews;
        }
    }
}
