using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public record CreateFiberCommandResponse(int Id);

public record CreateFiberCommand(
    string Name, string Description, 
    MotorUnitType MotorUnitType, TwitchSpeed TwitchSpeed, TwitchForce TwitchForce,
    ResistanceToFatigue ResistanceToFatigue
    ) : IRequest<ErrorOr<CreateFiberCommandResponse>>;

public class CreateFiberCommandHandler : IRequestHandler<CreateFiberCommand, ErrorOr<CreateFiberCommandResponse>>
{
    private readonly IFiberRepository _repository;
    private readonly IMapper _mapper;

    public CreateFiberCommandHandler(IFiberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<CreateFiberCommandResponse>> Handle(CreateFiberCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Fiber>(request);
        var id = await _repository.CreateAsync(entity);

        return new CreateFiberCommandResponse(id);
    }
}