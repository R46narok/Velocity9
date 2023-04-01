using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Services.Workout.Data.Repositories;
using V9.Services.Workout.Dto;

namespace V9.Services.Workout.Queires;

public record GetWorkoutQuery(string UserName, string WorkoutName) : IRequest<ErrorOr<WorkoutDto>>;

public class GetWorkoutQueryHandler : IRequestHandler<GetWorkoutQuery, ErrorOr<WorkoutDto>>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;

    public GetWorkoutQueryHandler(IWorkoutRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<WorkoutDto>> Handle(GetWorkoutQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.UserName, request.WorkoutName, false);
        var mapped = _mapper.Map<WorkoutDto>(entity);
        
        return mapped;
    }
}