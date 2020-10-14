using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AutoMapper;
using System;

namespace AmmoFinder.Retailers.AimSurplus
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Inventory > 0))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Name.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Description.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Name.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Name.GetGrain() ?? src.Description.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Name.GetRoundCount() ?? src.Description.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Tuple<Product, string>, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Item1.Inventory > 0))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item1.Inventory))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item1.Url))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Item1.Price))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.Name.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Item1.Description.GetCasing() ?? src.Item2.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Item1.Name.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Item1.Name.GetGrain() ?? src.Item1.Description.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Item1.Name.GetRoundCount() ?? src.Item1.Description.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Item1.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
