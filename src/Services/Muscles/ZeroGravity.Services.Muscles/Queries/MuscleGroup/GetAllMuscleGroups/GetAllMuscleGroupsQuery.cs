using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Repositories;
using ZeroGravity.Services.Muscles.Dto;

namespace ZeroGravity.Services.Muscles.Queries.GetAllMuscleGroups;

public record GetAllMuscleGroupsQuery : IRequest<ApiResponse<List<MuscleGroupDto>>>;

public class GetAllMuscleGroupsQueryHandler : IRequestHandler<GetAllMuscleGroupsQuery, ApiResponse<List<MuscleGroupDto>>>
{
    private readonly IMuscleGroupRepository _repository;
    private readonly IMapper _mapper;

    public GetAllMuscleGroupsQueryHandler(IMuscleGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiResponse<List<MuscleGroupDto>>> Handle(GetAllMuscleGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = _repository
            .GetAll()
            .Select(x => _mapper.Map<MuscleGroupDto>(x))
            .ToList();
        return new(groups);
    }
}