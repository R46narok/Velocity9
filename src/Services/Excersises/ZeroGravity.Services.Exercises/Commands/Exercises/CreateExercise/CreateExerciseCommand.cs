using AutoMapper;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Exercises.CreateExercise;

public class CreateExerciseCommand : IRequest<ApiResponse>
{
    public string Name { get; set; }
    public string AuthorName { get; set; }
    public List<Muscle> TargetMuscles { get; set; }
    
    public CreateExerciseCommand(string name, string authorName, List<Muscle> targetMuscles)
    {
        Name = name;
        AuthorName = authorName;
        TargetMuscles = targetMuscles;
    }
}

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ApiResponse>
{
    private readonly IExerciseRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    
    public CreateExerciseCommandHandler(IExerciseRepository repository, IAuthorRepository authorRepository, IMapper mapper, IMessagePublisher publisher)
    {
        _repository = repository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<ApiResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(request);
        entity.Author = await _authorRepository.GetByUserNameAsync(request.AuthorName);
        await _repository.CreateAsync(entity);

        var @event = _mapper.Map<ExerciseCreatedEvent>(entity);
        await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
        
        return new(statusCode: "Ok");
    }
}