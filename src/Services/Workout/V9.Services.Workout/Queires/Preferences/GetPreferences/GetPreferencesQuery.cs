﻿using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Services.Workout.Data.Repositories;
using V9.Services.Workout.Dto;

namespace V9.Services.Workout.Queires;

public class GetPreferencesQuery : IRequest<ErrorOr<PreferencesDto>>
{
    public string UserName { get; set; }
}

public class GetPreferencesQueryHandler : IRequestHandler<GetPreferencesQuery, ErrorOr<PreferencesDto>>
{
    private readonly IPreferencesRepository _repository;
    private readonly IMapper _mapper;

    public GetPreferencesQueryHandler(IPreferencesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<PreferencesDto>> Handle(GetPreferencesQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByUserNameAsync(request.UserName, false);
        var dto = _mapper.Map<PreferencesDto>(entity);
        return dto;
    }
}