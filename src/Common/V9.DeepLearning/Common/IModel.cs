using Microsoft.ML.OnnxRuntime;

namespace V9.DeepLearning.Common;

public interface IInferenceModel<TInput, TOutput>
    where TInput : new()
    where TOutput : new()
{
    public TOutput Predict(TInput input);
}

public class InferenceModelBase<TInput, TOutput> : IInferenceModel<TInput, TOutput> where TInput : new() where TOutput : new()
{
    protected readonly InferenceSession Inference;

    protected InferenceModelBase(string onnxPath)
    {
        Inference = new InferenceSession(onnxPath);
    }

    public virtual TOutput Predict(TInput input)
    {
        return new TOutput();
    }
}