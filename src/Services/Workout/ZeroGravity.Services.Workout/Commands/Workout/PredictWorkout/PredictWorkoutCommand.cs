using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Workout.Data.Repositories;
using ZeroGravity.Services.Workout.DeepLearning.Models;

namespace ZeroGravity.Services.Workout.Commands.PredictWorkout;

#nullable disable

public record PredictWorkoutCommand(string UserName) : IRequest<ErrorOr<WorkoutModelOutput>>;

public class PredictWorkoutCommandHandler : IRequestHandler<PredictWorkoutCommand, ErrorOr<WorkoutModelOutput>>
{
    private readonly IInferenceModel<WorkoutModelInput, WorkoutModelOutput> _model;
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;

    public PredictWorkoutCommandHandler(
        IInferenceModel<WorkoutModelInput, WorkoutModelOutput> model,
        IWorkoutRepository repository,
        IMapper mapper)
    {
        _model = model;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ErrorOr<WorkoutModelOutput>> Handle(PredictWorkoutCommand request, CancellationToken cancellationToken)
    {
        var last = await _repository.GetLastAsync(request.UserName);
        var exercises = last.Sets
            .Select(x => x.Exercise.Name)
            .ToList();
        var reps = last.Sets
            .Select(x => x.TargetReps)
            .ToList();

        var input = new WorkoutModelInput(exercises, reps);
        var output = _model.Predict(input);
        
        return output;
    }
}
