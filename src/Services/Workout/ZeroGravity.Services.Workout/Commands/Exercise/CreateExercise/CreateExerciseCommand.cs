using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public record CreateExerciseCommandResponse(int Id);
    
public class CreateExerciseCommand : IRequest<ErrorOr<CreateExerciseCommandResponse>>
{
    public string Name { get; set; }
    public string AuthorName { get; set; }
    public List<string> TargetNames { get; set; }
}

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ErrorOr<CreateExerciseCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMuscleRepository _muscleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMessagePublisher _publisher;

    public CreateExerciseCommandHandler(
        IMapper mapper, 
        IExerciseRepository exerciseRepository,
        IMuscleRepository muscleRepository,
        IUserRepository authorRepository,
        IMessagePublisher publisher)
    {
        _mapper = mapper;
        _exerciseRepository = exerciseRepository;
        _muscleRepository = muscleRepository;
        _userRepository = authorRepository;
        _publisher = publisher;
    }

    public async Task<ErrorOr<CreateExerciseCommandResponse>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(request);
        var author = await _userRepository.GetByNameAsync(request.AuthorName);
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
        
        return new CreateExerciseCommandResponse(id);
    }
}



