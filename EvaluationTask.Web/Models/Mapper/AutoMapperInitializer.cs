using AutoMapper;
using EvaluationTask.Web.Models.Dtos;

namespace EvaluationTask.Web.Models.Mapper;

public class AutoMapperInitializer : Profile
{
    public AutoMapperInitializer()
    {
        CreateMap<CancelationRequest, CancelationRequestDto>().ReverseMap();
    }
}
