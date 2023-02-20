using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Workout.Data.Repositories;
using ZeroGravity.Services.Workout.Dto;

namespace ZeroGravity.Services.Workout.Queires;

public record GetWorkoutQuery(string UserName, string WorkoutName) : IRequest<CqrsResult<WorkoutDto>>;

public class GetWorkoutQueryHandler : IRequestHandler<GetWorkoutQuery, CqrsResult<WorkoutDto>>
{
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;

    public GetWorkoutQueryHandler(IWorkoutRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CqrsResult<WorkoutDto>> Handle(GetWorkoutQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.UserName, request.WorkoutName, false);
        var mapped = _mapper.Map<WorkoutDto>(entity);
        
        return new (mapped);
    }
}