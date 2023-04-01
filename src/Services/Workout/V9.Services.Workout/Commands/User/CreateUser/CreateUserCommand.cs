using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Repositories;

namespace V9.Services.Workout.Commands;

public record CreateUserCommandResponse(int Id);
public class CreateUserCommand : IRequest<ErrorOr<CreateUserCommandResponse>>
{
    public string UserName { get; set; }
    public string ExternalId { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<CreateUserCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public CreateAuthorCommandHandler(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(request);
        var id = await _repository.CreateAsync(entity);
        return new CreateUserCommandResponse(id);
    }
}
