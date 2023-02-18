using Microsoft.ML.OnnxRuntime.Tensors;

namespace ZeroGravity.Services.Workout.DeepLearning.Data;

public class ExerciseDecoderOutput
{
    public Tensor<float>? Dense { get; }
    public Tensor<float>? HiddenState { get; }
    public Tensor<float>? CellState { get; }

    public ExerciseDecoderOutput(Tensor<float> dense, Tensor<float> hiddenState, Tensor<float> cellState)
    {
        Dense = dense;
        HiddenState = hiddenState;
        CellState = cellState;
    }

    public ExerciseDecoderOutput()
    {

    }
}