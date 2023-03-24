using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.IO;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet;

public interface IMovenetInference : IInferenceModel<MovenetInput, MovenetOutput>
{
    
}