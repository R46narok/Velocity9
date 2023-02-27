using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllMuscles;

public record GetAllMusclesQuery : IRequest<ErrorOr<List<MuscleDto>>>;

public class GetAllMusclesQueryHandler : IRequestHandler<GetAllMusclesQuery, ErrorOr<List<MuscleDto>>>
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
    
    public async Task<ErrorOr<List<MuscleDto>>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        var muscles = _repository
            .GetAll()
            .Select(m => _mapper.Map<MuscleDto>(m))
            .ToList();
        
        return muscles;
    }
}