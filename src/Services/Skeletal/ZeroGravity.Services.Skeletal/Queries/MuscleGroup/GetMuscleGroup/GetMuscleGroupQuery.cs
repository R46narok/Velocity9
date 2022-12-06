using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries;

public record GetMuscleGroupQuery(string Name) : IRequest<PipelineResult<MuscleGroupDto>>;

public class GetMuscleGroupQueryHandler : IRequestHandler<GetMuscleGroupQuery, PipelineResult<MuscleGroupDto>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleGroupRepository _repository;

    public GetMuscleGroupQueryHandler(IMapper mapper, IMuscleGroupRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<PipelineResult<MuscleGroupDto>> Handle(GetMuscleGroupQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.Name);
        var dto = _mapper.Map<MuscleGroupDto>(entity);
        
        return new (dto);
    }
}