using Microsoft.ML.OnnxRuntime;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Workout.DeepLearning.Data;

namespace ZeroGravity.Services.Workout.DeepLearning.Models;

public class EncoderInferenceModel : InferenceModelBase<EncoderInferenceInput, EncoderInferenceOutput>
{
    public EncoderInferenceModel(string onnxPath) : base(onnxPath)
    {
    }

    public override EncoderInferenceOutput Predict(EncoderInferenceInput input)
    {
        var namedInput = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("exercises_encoder_input", input.Exercises),
            NamedOnnxValue.CreateFromTensor("reps_encoder_input", input.Reps)
        };

        var output = Inference
            .Run(namedInput)
            .ToArray();

        return new(output[0].AsTensor<float>(), output[1].AsTensor<float>());
    }

}