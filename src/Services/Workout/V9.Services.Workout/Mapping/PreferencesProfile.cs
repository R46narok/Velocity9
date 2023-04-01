using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Dto;

namespace V9.Services.Workout.Mapping;

public class PreferencesProfile : Profile
{
    public PreferencesProfile()
    {
        CreateMap<CreatePreferencesCommand, Preferences>();
        CreateMap<Preferences, PreferencesDto>();
    }
}