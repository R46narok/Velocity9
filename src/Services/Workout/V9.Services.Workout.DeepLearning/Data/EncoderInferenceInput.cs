using Microsoft.ML.OnnxRuntime.Tensors;

namespace V9.Services.Workout.DeepLearning.Data;

public class EncoderInferenceInput
{
    public DenseTensor<float>? Exercises { get; }
    public DenseTensor<float>? Reps { get; }

    public const int MaxLength = 27; // maybe load it
    public static int[] ExercisesDim => new[] {1, MaxLength};
    public static int[] RepsDim => new[] {1, MaxLength, 1};

    public EncoderInferenceInput(float[] exercises, float[] reps)
    {
        Exercises = new DenseTensor<float>(exercises, ExercisesDim);
        Reps = new DenseTensor<float>(reps, RepsDim);
    }

    public EncoderInferenceInput()
    {
        
    }
}