using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public record CreatePreferencesCommandResponse(int Id);

public class CreatePreferencesCommand : IRequest<ErrorOr<CreatePreferencesCommandResponse>>
{
    public double ExerciseRestTime { get; set; }
    public double SetRestTime { get; set; }
    
    public string? UserName { get; set; }
}

public class CreatePreferencesCommandHandler : IRequestHandler<CreatePreferencesCommand,ErrorOr<CreatePreferencesCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPreferencesRepository _repository;
    private readonly IUserRepository _userRepository;


    public CreatePreferencesCommandHandler(IMapper mapper, IPreferencesRepository repository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CreatePreferencesCommandResponse>> Handle(CreatePreferencesCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Preferences>(request);
        entity.User = (await _userRepository.GetByNameAsync(request.UserName!))!;
        
        var id = await _repository.CreateAsync(entity);
        return new CreatePreferencesCommandResponse(id);
    }
}