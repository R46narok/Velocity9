using Microsoft.ML.OnnxRuntime;
using ZeroGravity.DeepLearning.Preprocessing;
using ZeroGravity.Services.Workout.DeepLearning.Data;

namespace ZeroGravity.Services.Workout.DeepLearning.Models;

public class WorkoutModel
{
    private readonly EncoderInferenceModel _encoder;
    private readonly ExerciseInferenceDecoder _exerciseDecoder;
    private readonly Dictionary<string, int> _tokens;
    private readonly Dictionary<string, int> _targetTokens;
    private readonly Dictionary<int, string> _reverse;

    public WorkoutModel(string encoderOnnx, string exerciseDecoderOnnx)
    {
        _encoder = new EncoderInferenceModel(encoderOnnx);
        _exerciseDecoder = new ExerciseInferenceDecoder(exerciseDecoderOnnx);
        
        
        const string tokensAsString =
            "Bench_Press Incline_Bench_Press Triceps_Iso Shoulder_Raise Paused_Dips Chest_Flies Weighted_Dips Rings Shoulder_Press HSPU Decline_Bench_Press";
        var tokens = tokensAsString.Split(' ').ToArray();
        
        const string tokensAsString1 =
            "START_ _END Bench_Press Incline_Bench_Press Triceps_Iso Shoulder_Raise Paused_Dips Chest_Flies Weighted_Dips Rings Shoulder_Press HSPU Decline_Bench_Press";
        var tokens1 = tokensAsString1.Split(' ').ToArray();
        _tokens = Lookup.For(tokens);
        _targetTokens = Lookup.For(tokens1);
        _reverse = Lookup.ReverseFor(tokens1);
    }

    public void Predict(List<string> exercises, List<int> reps)
    {
        const int maxLength = 25;

        var exerciseInput = new float[maxLength];
        var repsInput = new float[maxLength];
        for (int i = 0; i < exercises.Count; ++i)
        {
            exerciseInput[i] = _tokens[exercises[i]];
            repsInput[i] = reps[i] / 10.0f;
        }

        var encoded = _encoder.Predict(new (exerciseInput, repsInput));
        var hiddenState = encoded.HiddenState;
        var cellState = encoded.CellState;
        var data = new float[1];
        data[0] = _targetTokens["START_"];

        var predictedExercises = new List<string>();
        while (true)
        {
            var decoded = _exerciseDecoder.Predict(new(data, hiddenState, cellState));
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

    }
}