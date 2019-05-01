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

        public async Task<TextReviewDto> AddReview(Guid userId, CreateEditTextReviewDto textReview)
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

            return new TextReviewDto()
            {
                Id = dbReview.Id,
                IsMine = true,
                Text = dbReview.Text,
                Mood = dbReview.Mood,
                Points = 0,
                CreatedAt = dbReview.CreatedAt.ToString("yyyy.MM.dd HH:mm"),
                AuthorName = dbReview.Author.NickName,
                WasDownvotedByMe = false,
                WasUpvotedByMe = false
            };
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
               .Include(e => e.Author)
               .Where(e => e.ProductId == productId)
               .Select(e => new TextReviewDto()
               {
                   Id = e.Id,
                   IsMine = userId != null ? (Guid?)e.AuthorId == userId : false,
                   Text = e.Text,
                   Mood = e.Mood,
                   Points = e.Points,
                   CreatedAt = e.CreatedAt.ToString("yyyy.MM.dd HH:mm"),
                   AuthorName = e.Author.NickName,
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

            var downVoteByMe = await context.ReviewVotes
              .Where(e => e.TextReviewId == reviewId)
              .Where(e => e.UserId == userId)
              .Where(e => e.VoteType == VoteType.Down)
              .FirstOrDefaultAsync();
            var upVoteByMe = await context.ReviewVotes
               .Where(e => e.TextReviewId == reviewId)
               .Where(e => e.UserId == userId)
               .Where(e => e.VoteType == VoteType.Up)
               .FirstOrDefaultAsync();

            if (downVoteByMe != null)
            {
                context.Remove(downVoteByMe);
            }

            if (downVoteByMe == null && upVoteByMe == null)
            {
                var vote = new ReviewVote()
                {
                    UserId = userId,
                    TextReviewId = dbReview.Id,
                    VoteType = VoteType.Up
                };                
                context.ReviewVotes.Add(vote);
            }

            dbReview.Points++;
            await context.SaveChangesAsync();
        }

        public async Task DownVoteReview(Guid userId, Guid reviewId)
        {
            var dbReview = await context.Reviews
                .Where(e => e.Id == reviewId)
                .FirstOrDefaultAsync();

            if (dbReview == null)
                return;

            var upVoteByMe = await context.ReviewVotes
                .Where(e => e.TextReviewId == reviewId)
                .Where(e => e.UserId == userId)
                .Where(e => e.VoteType == VoteType.Up)
                .FirstOrDefaultAsync();
            var downVoteByMe = await context.ReviewVotes
                .Where(e => e.TextReviewId == reviewId)
                .Where(e => e.UserId == userId)
                .Where(e => e.VoteType == VoteType.Down)
                .FirstOrDefaultAsync();

            if (upVoteByMe != null)
            {
                context.Remove(upVoteByMe);
            }
            
            if (downVoteByMe == null && upVoteByMe == null)
            {
                var vote = new ReviewVote()
                {
                    UserId = userId,
                    TextReviewId = dbReview.Id,
                    VoteType = VoteType.Down
                };               
                context.ReviewVotes.Add(vote);
            }
            dbReview.Points--;
            await context.SaveChangesAsync();
        }
    }
}
