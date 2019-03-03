using ProductRating.Bll.Dtos.Review;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ReviewService : ServiceBase, IReviewService
    {
        public ReviewService(ApplicationDbContext context) : base(context)
        {

        }

        public Task Add(CreateEditTextReviewDto textReview)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TextReviewWithProductInfoDto>> GetReviewsMadeByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TextReviewDto>> GetReviewsOfProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid reviewId, CreateEditTextReviewDto textReview)
        {
            throw new NotImplementedException();
        }
    }
}
