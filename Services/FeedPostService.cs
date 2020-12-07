using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GreenDoorV1.Services
{
  public class FeedPostService : IFeedPostService
  {
    protected ApplicationDbContext Context { get; }

    public FeedPostService(ApplicationDbContext context)
    {
      Context = context;
    }

    public async Task<ActionResult<FeedPost>> AddFeedPost(FeedPost feedPost)
    {
      feedPost.PostingDate = DateTime.Now;
      var result = await Context.FeedPosts.AddAsync(feedPost);
      await Context.SaveChangesAsync();

      return feedPost;
    }

    public async Task<bool> DeleteFeedPost(long? id)
    {
      var deletable = await Context.FeedPosts.SingleOrDefaultAsync(f => f.Id.Equals(id));
      if (deletable != null)
      {
        Context.FeedPosts.Remove(deletable);
        await Context.SaveChangesAsync();
        return true;
      }
      return false;
    }

    public async Task<IEnumerable<FeedPost>> GetAllFeedPost()
    {
      var result = await Context.FeedPosts.OrderByDescending(x => x.PostingDate).ToListAsync();
      return result;
    }

    public async Task<ActionResult<FeedPost>> UpdateFeedPost(long? id, FeedPost feedPost)
    {
      var set = Context.FeedPosts;

      var original = await set.SingleOrDefaultAsync(f => f.Id.Equals(id));

      if (original == null)
      {
        return null;
      }

      original.PostText = feedPost.PostText;

      set.Update(original);

      await Context.SaveChangesAsync();

      return feedPost;
    }
  }
}
