using System;
using PipelineFramework;
using System.Collections.Generic;

namespace InMemoryPipeline
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private List<BaseMiddleware> _middlewares;

        public PipelineBuilder()
        {
            _middlewares = new List<BaseMiddleware>();
        }

        public void Add(BaseMiddleware middleware)
        {
            _middlewares.Add(middleware);
        }

        public BasePipeline Build(string pipelineName, Guid Id)
        {   
            var pipeline = new InMemoryPipeline(pipelineName, Id, _middlewares);
            return pipeline;
        }
    }
}