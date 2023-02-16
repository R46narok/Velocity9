﻿using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllMuscles;

public record GetAllMusclesQuery : IRequest<PipelineResult<List<MuscleDto>>>;

public class GetAllMusclesQueryHandler : IRequestHandler<GetAllMusclesQuery, PipelineResult<List<MuscleDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleRepository _repository;

    public GetAllMusclesQueryHandler(
        IMapper mapper,
        IMuscleRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<PipelineResult<List<MuscleDto>>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        var muscles = _repository
            .GetAll()
            .Select(m => _mapper.Map<MuscleDto>(m))
            .ToList();
        
        return new(muscles);
    }
}