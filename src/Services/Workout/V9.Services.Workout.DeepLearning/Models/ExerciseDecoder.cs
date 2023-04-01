using Microsoft.ML.OnnxRuntime;
using V9.DeepLearning.Common;
using V9.Services.Workout.DeepLearning.Data;

namespace V9.Services.Workout.DeepLearning.Models;
public class ExerciseInferenceDecoder : InferenceModelBase<ExerciseDecoderInput, ExerciseDecoderOutput>
{
    public ExerciseInferenceDecoder(string onnx) : base(onnx)
    {
    }

    public override ExerciseDecoderOutput Predict(ExerciseDecoderInput input)
    {
        var namedInput = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("exercises_decoder_input", input.Sequence),
            NamedOnnxValue.CreateFromTensor("exercises_hidden_input", input.HiddenState),
            NamedOnnxValue.CreateFromTensor("exercises_cell_input", input.CellState),
        };

        var prediction = Inference
            .Run(namedInput)
            .ToArray();

        return new(
            prediction[0].AsTensor<float>(),
            prediction[1].AsTensor<float>(),
            prediction[2].AsTensor<float>());
    }
}