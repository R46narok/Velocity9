using Microsoft.ML.OnnxRuntime;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly.IO;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly;

public class AnomalyInference : 
    InferenceModelBase<AnomalyInput, AnomalyOutput>,
    IAnomalyInference
{
    private const string InputTensor = "bidirectional_input";
    
    public AnomalyInference(string onnxPath) : base(onnxPath)
    {
    }

    public override AnomalyOutput Predict(AnomalyInput input)
    {
        var namedInput = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor(InputTensor, input.KeyPoints)
        };

        var output = Inference
            .Run(namedInput)
            .ToArray();
        
        return new(output[0].AsTensor<float>());
    }
}