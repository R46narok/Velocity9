using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Repositories;

namespace ZeroGravity.Services.Workout.Commands;

public class CreateUserCommand : IRequest<PipelineResult>
{
    public string UserName { get; set; }
    public string ExternalId { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateUserCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public CreateAuthorCommandHandler(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<PipelineResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(request);
        await _repository.CreateAsync(entity);
        return new();
    }
}
