using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<Region,AddRegionRequestDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk,AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk,UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<ImageUploadRequestDTO,Image>()
                .ForMember(dest=>dest.FileExtension,opt=>opt.MapFrom(src=>Path.GetExtension(src.File.FileName)))
                .ForMember(dest=>dest.FileSizeInBytes,opt=>opt.MapFrom(src=>src.File.Length))
                .ReverseMap();
        }
    }
}
