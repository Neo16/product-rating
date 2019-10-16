using AutoMapper;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.Models;
using ProductRating.Dal.Model.Entities;
using ProductRating.Dal.Model.Entities.Products;
using ProductRating.Dal.Model.Entities.Products.Attributes;
using System;
using System.Linq;

namespace ProductRating.Bll.Mappings
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            this.CreateMap<Product, CreateEditProductDto>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
               .ForMember(e => e.BrandId, e => e.MapFrom(f => f.BrandId))
               .ForMember(e => e.CategoryId, e => e.MapFrom(f => f.CategoryId))
               .ForMember(e => e.StartOfProduction, e => e.MapFrom(f => f.StartOfProduction))
               .ForMember(e => e.EndOfProduction, e => e.MapFrom(f => f.EndOfProduction))
               .ForMember(e => e.Pictures, e => e.MapFrom(f => f.Pictures.Select(g => g.Picture)))
               .ForMember(e => e.ThumbnailPicture, e => e.MapFrom(f => f.ThumbnailPicture))           
               .ForMember(e => e.IntAttributes,
                    e => e.MapFrom(f => f.PropertyValueConnections
                        .Select(g => g.ProductAttributeValue)
                        .OfType<ProductAttributeIntValue>()))
                .ForMember(e => e.StringAttributes,
                    e => e.MapFrom(f => f.PropertyValueConnections
                        .Select(g => g.ProductAttributeValue)
                        .OfType<ProductAttributeStringValue>()))
               .ReverseMap()
               .ForMember(e => e.ThumbnailPicture, e => e.Ignore())
               .ForMember(e => e.ThumbnailPictureId, e => e.MapFrom(f => f.ThumbnailPicture.Id))
               .ForMember(e => e.PropertyValueConnections, e => e.Ignore())
               .ForMember(e => e.ScoreValue, e => e.Ignore())
               .ForMember(e => e.Reviews, e => e.Ignore())
               .ForMember(e => e.Scores, e => e.Ignore());

            this.CreateMap<Picture, PictureDto>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Data, e => e.MapFrom(f => Convert.ToBase64String(f.Data)));

            this.CreateMap<DateTime, SimpleDateData>()
                .ForMember(e => e.Day, e => e.MapFrom(f => f.Day))
                .ForMember(e => e.Month, e => e.MapFrom(f => f.Month))
                .ForMember(e => e.Year, e => e.MapFrom(f => f.Year));


            this.CreateMap<SimpleDateData, DateTime>()
                .ConstructUsing(e => new DateTime(e.Year, e.Month, e.Day));

            this.CreateMap<Product, ProductDetailsDto>()
                .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.PropertyValueConnections.Select(g => g.ProductAttributeValue)))
                .ForMember(e => e.BrandName, e => e.MapFrom(f => f.Brand.Name))
                .ForMember(e => e.CategoryName, e => e.MapFrom(f => f.Category.Name))
                .ForMember(e => e.ThumbnailImage, e => e.MapFrom(f => Convert.ToBase64String(f.ThumbnailPicture.Data)))
                .ForMember(e => e.ScoreValue, e => e.MapFrom(f => f.ScoreValue));

             this.CreateMap<ProductAttributeValue, AttributeBase>()
                 .ForMember(e => e.AttributeName, e => e.MapFrom(f => f.Attribute.Name))
                 .ForMember(e => e.AttributeId, e => e.MapFrom(f => f.Attribute.Id))
                 .ForMember(e => e.ValueId, e => e.MapFrom(f => f.Id));

            this.CreateMap<ProductAttributeIntValue, IntAttribute>()
               .IncludeBase<ProductAttributeValue, AttributeBase>()
               .ForMember(e => e.Value, e => e.MapFrom(f => f.IntValue));

            this.CreateMap<ProductAttributeStringValue, StringAttribute>()
                .IncludeBase<ProductAttributeValue, AttributeBase>()
                .ForMember(e => e.Value, e => e.MapFrom(f => f.StringValue));

            this.CreateMap<Product, ProductHeaderDto>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
               .ForMember(e => e.BrandName, e => e.MapFrom(f => f.Brand.Name))
               .ForMember(e => e.CategoryName, e => e.MapFrom(f => f.Category.Name))
               .ForMember(e => e.ThumbnailImage, e => e.MapFrom(f => Convert.ToBase64String(f.ThumbnailPicture.Data)))
               .ForMember(e => e.Score, e => e.MapFrom(f => f.ScoreValue))
               .ForMember(e => e.Price, e => e.MapFrom(f => f.SmallestPrice));

            this.CreateMap<Product, ProductManageQueryModel>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
               .ForMember(e => e.BrandName, e => e.MapFrom(f => f.Brand.Name))
               .ForMember(e => e.CategoryName, e => e.MapFrom(f => f.Category.Name))
               .ForMember(e => e.CreatorId, e => e.MapFrom(f => f.CreatorId))
               .ForMember(e => e.SellerIds, e => e.MapFrom(f => f.Offers.Select(g => g.SellerId)))
               .ForMember(e => e.CreatedAt, e => e.MapFrom(f => f.CreatedAt.ToString("yyyy.MM.dd HH:mm")));            

            this.CreateMap<Brand, BrandHeaderDto>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Name, e => e.MapFrom(f => f.Name));              
        }
    }
}
