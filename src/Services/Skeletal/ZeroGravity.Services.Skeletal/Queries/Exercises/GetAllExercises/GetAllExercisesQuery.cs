﻿using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllExercises;

public record GetAllExercisesQuery : IRequest<ErrorOr<List<ExerciseDto>>>;

public class GetAllExercisesQueryHandler : IRequestHandler<GetAllExercisesQuery, ErrorOr<List<ExerciseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public GetAllExercisesQueryHandler(IMapper mapper, IExerciseRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<List<ExerciseDto>>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        var exercises = _repository
            .GetAll()
            .Select(x => _mapper.Map<ExerciseDto>(x))
            .ToList();

        return exercises;
    }
}