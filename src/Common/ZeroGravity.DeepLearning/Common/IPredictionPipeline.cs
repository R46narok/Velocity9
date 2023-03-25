namespace ZeroGravity.DeepLearning.Common;

public interface IPredictionPipeline
{
    public void Run(string fileName);
}

public class PredictionPipelineBase : IPredictionPipeline
{
    public virtual void Run(string fileName)
    {
        
    }
}