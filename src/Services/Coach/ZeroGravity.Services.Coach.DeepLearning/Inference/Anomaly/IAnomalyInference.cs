using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly.IO;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Anomaly;

public interface IAnomalyInference : IInferenceModel<AnomalyInput, AnomalyOutput>
{
    
}