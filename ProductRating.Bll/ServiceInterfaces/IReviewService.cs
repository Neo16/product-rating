using ProductRating.Bll.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IReviewService
    {
        Task Add(CreateEditTextReviewDto textReview);

        Task Update(Guid reviewId, CreateEditTextReviewDto textReview);

        Task Delete(Guid reviewId);

        Task<List<TextReviewDto>> GetReviewsOfProduct(Guid productId);

        Task<List<TextReviewWithProductInfoDto>> GetReviewsMadeByUser(Guid userId);
    }
}
