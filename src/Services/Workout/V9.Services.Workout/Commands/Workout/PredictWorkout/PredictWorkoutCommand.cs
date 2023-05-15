using AutoMapper;
using ErrorOr;
using MediatR;
using V9.DeepLearning.Common;
using V9.Services.Workout.Data.Repositories;
using V9.Services.Workout.DeepLearning.Data;
using V9.Services.Workout.DeepLearning.Models;

namespace V9.Services.Workout.Commands.PredictWorkout;

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

    public async Task<ErrorOr<WorkoutModelOutput>> Handle(PredictWorkoutCommand request,
        CancellationToken cancellationToken)
    {
        var last = await _repository.GetLastAsync(request.UserName);

        var exercises = new List<string>();
        var reps = new List<int>();

        if (last is null)
        {
            exercises = new List<string>(){ "HSPU", "HSPU" };
            reps = new List<int>(){ 8, 8};
        }
        else
        {
            exercises = last.Sets
                .Select(x => x.Exercise.Name)
                .ToList();
            reps = last.Sets
                .Select(x => x.TargetReps)
                .ToList();
        }


        var input = new WorkoutModelInput(exercises, reps);
        var output = _model.Predict(input);

        return output;
    }
}