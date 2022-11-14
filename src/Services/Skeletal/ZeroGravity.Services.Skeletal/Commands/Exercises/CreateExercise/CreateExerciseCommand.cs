﻿using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;

public record CreateExerciseCommand(string Name, string Description, List<int> TargetIds, int AuthorId) : IRequest<ApiResponse>;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMuscleRepository _muscleRepository;
    private readonly IAuthorRepository _authorRepository;

    public CreateExerciseCommandHandler(
        IMapper mapper, 
        IExerciseRepository exerciseRepository,
        IMuscleRepository muscleRepository,
        IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _exerciseRepository = exerciseRepository;
        _muscleRepository = muscleRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ApiResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(request);
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);
        var targets = request.TargetIds
            .Select(async x => await _muscleRepository.GetByIdAsync(x))!
            .Select<Task<Muscle>, Muscle>(y =>
            {
                y.Wait(cancellationToken);
                return y.Result;
            })
            .ToList();


        entity.Author = author!;
        entity.Targets = targets;


        await _exerciseRepository.CreateAsync(entity);
        
        return new();
    }
}