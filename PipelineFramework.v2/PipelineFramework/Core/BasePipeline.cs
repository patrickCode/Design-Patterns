using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Core.Message;

namespace PipelineFramework.Core
{
    [Serializable]
    public abstract class BasePipeline : Middleware
    {
        public Guid CorrelationId { get; set; }

        public Dictionary<int, Middleware> Middlewares { get; set; }

        public BasePipeline() { }

        public BasePipeline(string id, string name): base(id, name)
        {
            Middlewares = new Dictionary<int, Middleware>();
        }

        public abstract Task ExecuteNextAsync(MiddlewareResponse response);
    }
}