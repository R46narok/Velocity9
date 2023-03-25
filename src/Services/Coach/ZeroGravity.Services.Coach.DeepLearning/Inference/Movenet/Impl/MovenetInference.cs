﻿using Microsoft.ML.OnnxRuntime;
using ZeroGravity.DeepLearning.Common;
using ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.IO;

namespace ZeroGravity.Services.Coach.DeepLearning.Inference.Movenet.Impl;

public class MovenetInference : 
    InferenceModelBase<MovenetInput, MovenetOutput>,
    IMovenetInference
{
    private const string ServingDefaultInput = "serving_default_input:0";
    public MovenetInference(string onnxPath) : base(onnxPath)
    {
    }

    // serving_default_input:0
    public override MovenetOutput Predict(MovenetInput input)
    {
        var namedInput = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor(ServingDefaultInput, input.Image)
        };

        var output = Inference
            .Run(namedInput)
            .ToArray();

        var tensor = output[0].AsTensor<float>();
        return new(tensor.ToDenseTensor());
    }
}