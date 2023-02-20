using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllMuscles;

public record GetAllMusclesQuery : IRequest<CqrsResult<List<MuscleDto>>>;

public class GetAllMusclesQueryHandler : IRequestHandler<GetAllMusclesQuery, CqrsResult<List<MuscleDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMuscleRepository _repository;

    public GetAllMusclesQueryHandler(
        IMapper mapper,
        IMuscleRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<CqrsResult<List<MuscleDto>>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        var muscles = _repository
            .GetAll()
            .Select(m => _mapper.Map<MuscleDto>(m))
            .ToList();
        
        return new(muscles);
    }
}