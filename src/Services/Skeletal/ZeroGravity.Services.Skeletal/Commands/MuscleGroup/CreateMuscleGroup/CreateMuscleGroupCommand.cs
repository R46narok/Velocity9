using AutoMapper;
using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands;

public record CreateMuscleGroupCommand(string Name, string Description) : IRequest<ApiResponse>;

public class CreateMuscleGroupCommandHandler : IRequestHandler<CreateMuscleGroupCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IMuscleGroupRepository _repository;

    public CreateMuscleGroupCommandHandler(IMapper mapper, IMuscleGroupRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ApiResponse> Handle(CreateMuscleGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MuscleGroup>(request);
        await _repository.CreateAsync(entity);

        return new(DetailsMessage.For(StatusCode.Created, nameof(MuscleGroup)));
    }
}