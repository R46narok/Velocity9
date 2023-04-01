using Microsoft.ML.OnnxRuntime.Tensors;

namespace V9.Services.Workout.DeepLearning.Data;

public class EncoderInferenceOutput
{
    public Tensor<float>? HiddenState { get; }
    public Tensor<float>? CellState { get; }
    
    public EncoderInferenceOutput(Tensor<float> hiddenState, Tensor<float> cellState)
    {
        HiddenState = hiddenState;
        CellState = cellState;
    }

    public EncoderInferenceOutput()
    {
    }
}