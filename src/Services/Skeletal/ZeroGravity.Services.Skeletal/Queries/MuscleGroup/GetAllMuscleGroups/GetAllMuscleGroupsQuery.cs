using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllMuscleGroups;

public record GetAllMuscleGroupsQuery : IRequest<ErrorOr<List<MuscleGroupDto>>>;

public class GetAllMuscleGroupsQueryHandler : IRequestHandler<GetAllMuscleGroupsQuery, ErrorOr<List<MuscleGroupDto>>>
{
    private readonly IMuscleGroupRepository _repository;
    private readonly IMapper _mapper;

    public GetAllMuscleGroupsQueryHandler(IMuscleGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<List<MuscleGroupDto>>> Handle(GetAllMuscleGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = _repository
            .GetAll()
            .Select(x => _mapper.Map<MuscleGroupDto>(x))
            .ToList();
        return groups;
    }
}