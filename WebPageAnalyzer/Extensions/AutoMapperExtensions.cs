using AutoMapper;
using WebPageAnalyzer.OutputModels;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Extensions;

internal static class AutoMapperExtensions
{
    public static IMapperConfigurationExpression Setup(this IMapperConfigurationExpression expression)
    {
        expression.CreateMap<TaskInputModel, TaskDto>();
        expression.CreateMap<TaskDto, TaskOutputModel>();
        expression.CreateMap<ResultDto, ResultOutputModel>()
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => new DateTime(1970, 1, 1).AddSeconds(src.Id.Timestamp))
            );
        return expression;
    }
}