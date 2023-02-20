using ZeroGravity.DeepLearning.Common;
using ZeroGravity.DeepLearning.Preprocessing;

namespace ZeroGravity.Services.Workout.DeepLearning.Models;

public class WorkoutModel : IInferenceModel<WorkoutModelInput, WorkoutModelOutput>
{
    private readonly EncoderInferenceModel _encoder;
    private readonly ExerciseInferenceDecoder _exerciseDecoder;
    private readonly Dictionary<string, int> _tokens;
    private readonly Dictionary<string, int> _targetTokens;
    private readonly Dictionary<int, string> _reverse;

    public WorkoutModel(
        string encoderOnnx,
        string exerciseDecoderOnnx,
        List<string> availableExercises)
    {
        _encoder = new EncoderInferenceModel(encoderOnnx);
        _exerciseDecoder = new ExerciseInferenceDecoder(exerciseDecoderOnnx);

        availableExercises.Sort();
        _tokens = Lookup.For(availableExercises.ToArray());

        var array = availableExercises.ToArray();
        Array.Sort(array);

        var list = array.ToList();
        list.Insert(7, "START_");
        list.Add("_END");

        array = list.ToArray();
        _targetTokens = Lookup.For(array);
        
        _reverse = Lookup.ReverseFor(array);
    }

    public WorkoutModelOutput Predict(WorkoutModelInput input)
    {
        var exercises = input.Exercises;
        var reps = input.Reps;
        const int maxLength = 27;

        var exerciseInput = new float[maxLength];
        var repsInput = new float[maxLength];
        for (int i = 0; i < exercises.Count; ++i)
        {
            exerciseInput[i] = _tokens[exercises[i]];
            repsInput[i] = reps[i] / 10.0f;
        }

        var encoded = _encoder.Predict(new(exerciseInput, repsInput));
        var hiddenState = encoded.HiddenState;
        var cellState = encoded.CellState;
        var data = new float[1];
        data[0] = _targetTokens["START_"];

        var predictedExercises = new List<string>();
        while (true)
        {
            var decoded = _exerciseDecoder.Predict(new(data, hiddenState!, cellState!));
            var tokenDistribution = decoded.Dense!.ToArray();
            var index = Array.IndexOf(tokenDistribution, tokenDistribution.Max());
            var sampled = _reverse[index];

            if (sampled == "_END" || predictedExercises.Count >= 27)
                break;

            predictedExercises.Add(sampled);

            hiddenState = decoded.HiddenState;
            cellState = decoded.CellState;
            data[0] = index;
        }

        return new(predictedExercises, null!);
    }
}