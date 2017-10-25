using System;

namespace PipelineFramework
{
    public interface IPipelineBuilder
    {
        void Add(BaseMiddleware middleware);
        BasePipeline Build(string pipelineName, Guid Id);
    }
}