using ProductRating.Bll.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IReviewService
    {
        Task AddScore(Guid userId, ScoreDto scoreDto);

        Task<TextReviewDto> AddReview(Guid userId, CreateEditTextReviewDto textReview);

        Task UpdateReview(Guid userId, Guid reviewId, CreateEditTextReviewDto textReview);

        Task DeleteReview(Guid userId, Guid reviewId, bool isAdmin);

        Task<List<TextReviewDto>> GetReviewsOfProduct(Guid? userId, Guid productId);

        Task<List<TextReviewWithProductInfoDto>> GetReviewsMadeByUser(Guid userId);

        Task UpvoteReview(Guid userId, Guid reviewId);

        Task DownVoteReview(Guid userId, Guid reviewId);

        Task<double?> GetScoreOfProduct(Guid productId);
    }
}
