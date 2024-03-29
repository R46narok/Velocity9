﻿using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Commands.Exercises.CreateExercise;

public record CreateExerciseCommandResponse(int Id);

public record CreateExerciseCommand
    (string Name, string Description, List<string> TargetNames, string AuthorName) : IRequest<
        ErrorOr<CreateExerciseCommandResponse>>;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ErrorOr<CreateExerciseCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMuscleRepository _muscleRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMessagePublisher _publisher;

    public CreateExerciseCommandHandler(
        IMapper mapper, 
        IExerciseRepository exerciseRepository,
        IMuscleRepository muscleRepository,
        IAuthorRepository authorRepository,
        IMessagePublisher publisher)
    {
        _mapper = mapper;
        _exerciseRepository = exerciseRepository;
        _muscleRepository = muscleRepository;
        _authorRepository = authorRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<CreateExerciseCommandResponse>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(request);
        var author = await _authorRepository.GetByNameAsync(request.AuthorName);
        var targets = request.TargetNames
            .Select(async x => await _muscleRepository.GetByNameAsync(x))!
            .Select<Task<Muscle>, Muscle>(y =>
            {
                y.Wait(cancellationToken);
                return y.Result;
            })
            .ToList();


        entity.Author = author!;
        entity.Targets = targets;

        var id = await _exerciseRepository.CreateAsync(entity);

        entity = await _exerciseRepository.GetByNameAsync(request.Name, false);
        
        var @event = _mapper.Map<ExerciseCreatedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
        
        return new CreateExerciseCommandResponse(id);
    }
}