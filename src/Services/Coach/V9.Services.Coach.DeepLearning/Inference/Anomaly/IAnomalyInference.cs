using V9.DeepLearning.Common;
using V9.Services.Coach.DeepLearning.Inference.Anomaly.IO;

namespace V9.Services.Coach.DeepLearning.Inference.Anomaly;

public interface IAnomalyInference : IInferenceModel<AnomalyInput, AnomalyOutput>
{
    
}