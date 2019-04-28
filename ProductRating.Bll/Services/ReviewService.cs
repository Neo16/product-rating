using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Review;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ReviewService : ServiceBase, IReviewService
    {

        public ReviewService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task AddScore(Guid userId, ScoreDto scoreDto)
        {
            var dbScrore = new Scorereview()
            {
                AuthorId = userId,
                CreatedAt = DateTime.Now,
                ProductId = scoreDto.ProductId,
                Value = scoreDto.Score
            };

            context.Scores.Add(dbScrore);
            await context.SaveChangesAsync();
        }

        public async Task AddReview(Guid userId, CreateEditTextReviewDto textReview)
        {
            var dbReview = new TextReview()
            {
                AuthorId = userId,
                CreatedAt = DateTime.Now,
                Mood = textReview.Mood,
                ProductId = textReview.ProductId,
                Text = textReview.Text,
                Points = 1
            };

            context.Reviews.Add(dbReview);
            await context.SaveChangesAsync();
        }
        public async Task UpdateReview(Guid userId, Guid reviewId, CreateEditTextReviewDto textReview)
        {
            var dbReview = await context.Reviews
                .Where(e => e.Id == reviewId)
                .Where(e => e.AuthorId == userId)
                .FirstOrDefaultAsync();

            if (dbReview != null)
            {
                dbReview.Text = textReview.Text;
                dbReview.Mood = textReview.Mood;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteReview(Guid userId, Guid reviewId)
        {
            var dbReview = await context.Reviews
              .Where(e => e.Id == reviewId)
              .Where(e => e.AuthorId == userId)
              .FirstOrDefaultAsync();

            context.Reviews.Remove(dbReview);
            await context.SaveChangesAsync();
        }

        public async Task<List<TextReviewWithProductInfoDto>> GetReviewsMadeByUser(Guid userId)
        {
            return await context.Reviews
              .Include(e => e.Product)
              .Include(e => e.Product.Brand)
              .Include(e => e.Product.Category)
              .Where(e => e.AuthorId == userId)
              .Select(e => new TextReviewWithProductInfoDto()
              {
                  Id = e.Id,
                  Text = e.Text,
                  ProductBrandName = e.Product.Brand.Name,
                  ProductCategoryName = e.Product.Category.Name,
                  ProductName = e.Product.Name
              })
              .ToListAsync();
        }

        public async Task<List<TextReviewDto>> GetReviewsOfProduct(Guid? userId, Guid productId)
        {
            return await context.Reviews
               .Include(e => e.Product)
               .Include(e => e.Votes)
               .Where(e => e.ProductId == productId)
               .Select(e => new TextReviewDto()
               {
                   Id = e.Id,
                   IsMine = userId != null ? (Guid?)e.AuthorId == userId : false,
                   Text = e.Text,
                   Mood = e.Mood,
                   WasDownvotedByMe = userId != null
                            ? e.Votes != null && e.Votes.Any(v => v.VoteType == VoteType.Down && (Guid?)v.UserId == userId)
                            : false,
                   WasUpvotedByMe = userId != null
                            ? e.Votes != null && e.Votes.Any(v => v.VoteType == VoteType.Up && (Guid?)v.UserId == userId)
                            : false
               })
               .ToListAsync();
        }

        public async Task UpvoteReview(Guid userId, Guid reviewId)
        {
            var dbReview = await context.Reviews
             .Where(e => e.Id == reviewId)
             .FirstOrDefaultAsync();

            if (dbReview == null)
                return;

            var vote = new ReviewVote()
            {
                UserId = userId,
                TextReviewId = dbReview.Id,
                VoteType = VoteType.Up
            };

            context.ReviewVotes.Add(vote);
            await context.SaveChangesAsync();
        }

        public async Task DownVoteReview(Guid userId, Guid reviewId)
        {
            var dbReview = await context.Reviews
                .Where(e => e.Id == reviewId)
                .FirstOrDefaultAsync();

            if (dbReview == null)
                return;

            var vote = new ReviewVote()
            {
                UserId = userId,
                TextReviewId = dbReview.Id,
                VoteType = VoteType.Down
            };

            context.ReviewVotes.Add(vote);
            await context.SaveChangesAsync();
        }
    }
}
