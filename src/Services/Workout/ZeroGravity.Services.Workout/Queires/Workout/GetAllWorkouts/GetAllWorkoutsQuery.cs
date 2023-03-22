using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Services.Workout.Data.Repositories;
using ZeroGravity.Services.Workout.Dto;

namespace ZeroGravity.Services.Workout.Queires;

public record GetAllWorkoutsQuery(string UserName) : IRequest<ErrorOr<List<WorkoutDto>>>;

public class GetAllWorkoutQueryHandler : IRequestHandler<GetAllWorkoutsQuery, ErrorOr<List<WorkoutDto>>>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;

    public GetAllWorkoutQueryHandler(IWorkoutRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<WorkoutDto>>> Handle(GetAllWorkoutsQuery request,
        CancellationToken cancellationToken)
    {
        var list = await _repository.GetAll(request.UserName);
        var mapped = list
            .Select(x => _mapper.Map<WorkoutDto>(x))
            .ToList();

        return mapped;
    }
}