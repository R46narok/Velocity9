using V9.DeepLearning.Common;
using V9.Services.Coach.DeepLearning.Inference.Movenet.IO;

namespace V9.Services.Coach.DeepLearning.Inference.Movenet;

public interface IMovenetInference : IInferenceModel<MovenetInput, MovenetOutput>
{
    
}