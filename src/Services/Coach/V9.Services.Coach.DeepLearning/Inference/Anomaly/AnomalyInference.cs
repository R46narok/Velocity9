﻿using Microsoft.ML.OnnxRuntime;
using V9.DeepLearning.Common;
using V9.Services.Coach.DeepLearning.Inference.Anomaly.IO;

namespace V9.Services.Coach.DeepLearning.Inference.Anomaly;

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