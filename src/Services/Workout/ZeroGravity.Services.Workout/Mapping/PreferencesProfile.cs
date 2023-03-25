using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Dto;

namespace ZeroGravity.Services.Workout.Mapping;

public class PreferencesProfile : Profile
{
    public PreferencesProfile()
    {
        CreateMap<CreatePreferencesCommand, Preferences>();
        CreateMap<Preferences, PreferencesDto>();
    }
}