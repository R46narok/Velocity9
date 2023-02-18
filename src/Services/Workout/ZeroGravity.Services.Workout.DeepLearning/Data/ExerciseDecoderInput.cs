using Microsoft.ML.OnnxRuntime.Tensors;

namespace ZeroGravity.Services.Workout.DeepLearning.Data;

public class ExerciseDecoderInput
{
    public Tensor<float>? Sequence { get; }
    public Tensor<float>? HiddenState { get; }
    public Tensor<float>? CellState { get; }

    public static int[] SequenceDim => new[] {1, 1};

    public ExerciseDecoderInput(float[] sequence, Tensor<float> hiddenState, Tensor<float> cellState)
    {
        Sequence = new DenseTensor<float>(sequence, SequenceDim);
        HiddenState = hiddenState;
        CellState = cellState;
    }
    
    public ExerciseDecoderInput()
    {
        
    }
}