using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public record CreateMuscleGroupCommandResponse(int Id);
public record CreateMuscleGroupCommand(string Name, string Description) : IRequest<ErrorOr<CreateMuscleGroupCommandResponse>>;

public class CreateMuscleGroupCommandHandler : IRequestHandler<CreateMuscleGroupCommand, ErrorOr<CreateMuscleGroupCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleGroupRepository _repository;

    public CreateMuscleGroupCommandHandler(IMapper mapper, IMuscleGroupRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<CreateMuscleGroupCommandResponse>> Handle(CreateMuscleGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MuscleGroup>(request);
        var id = await _repository.CreateAsync(entity);

        return new CreateMuscleGroupCommandResponse(id);
    }
}