using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Muscles.Data.Repositories;
using ZeroGravity.Services.Muscles.Dto;

namespace ZeroGravity.Services.Muscles.Queries;

public record GetMuscleGroupQuery(string Name) : IRequest<ApiResponse<MuscleGroupDto>>;

public class GetMuscleGroupQueryHandler : IRequestHandler<GetMuscleGroupQuery, ApiResponse<MuscleGroupDto>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleGroupRepository _repository;

    public GetMuscleGroupQueryHandler(IMapper mapper, IMuscleGroupRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ApiResponse<MuscleGroupDto>> Handle(GetMuscleGroupQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.Name);
        var dto = _mapper.Map<MuscleGroupDto>(entity);
        
        return new (dto);
    }
}