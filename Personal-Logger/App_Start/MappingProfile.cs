using AutoMapper;
using PersonalLogger.DTO;
using PersonalLogger.Models;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Security.Cryptography;
using WebGrease.Css.Extensions;
using System.Linq;

namespace PersonalLogger.App_Start
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {
            var context = new ApplicationDbContext();

            Mapper.CreateMap<FieldType, FieldTypeDTO>();
            Mapper.CreateMap<FieldTypeDTO, FieldType>();

            Mapper.CreateMap<CategoryField, CategoryFieldDTO>()
                .ForMember(dest=>dest.FieldType,
                opts=>opts.MapFrom(src=>context.FieldTypes
                        .Where(ft => ft.Id == src.FieldTypeId)
                        .Select(Mapper.Map<FieldType,FieldTypeDTO>)
                        .FirstOrDefault()));


            Mapper.CreateMap<CategoryFieldDTO, CategoryField>()
                .ForMember(dest => dest.FieldTypeId,
                opts=>opts.MapFrom(src=>src.FieldType.Id))
                .ForMember(dest=>dest.FieldType,
                opts=>opts.Ignore());

            Mapper.CreateMap<LogCategory, LogCategoryDTO>();
            Mapper.CreateMap<LogCategoryDTO, LogCategory>();

            Mapper.CreateMap<Field, FieldDTO>()
                .ForMember(dest => dest.Value,
                opts => opts.MapFrom(src => src.GetValue()));
            Mapper.CreateMap<FieldDTO, Field>();

            Mapper.CreateMap<MyLog, MyLogDTO>();

        }
    }
}