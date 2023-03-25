using AutoMapper;
using ZeroGravity.UI.Portal.Blazor.Pages.Exercise;
using ZeroGravity.UI.Portal.Services.Skeletal.Requests;

namespace ZeroGravity.UI.Portal.Blazor.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<
                ZeroGravity.UI.Portal.Blazor.Pages.Exercise.ExerciseCreate.FormData,CreateExerciseRequest>()
            .ForMember(x => x.AuthorName, opt => opt.Ignore());
    }
}