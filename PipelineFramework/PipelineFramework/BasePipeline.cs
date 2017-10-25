using System;
using System.Threading.Tasks;

namespace PipelineFramework
{
    public abstract class BasePipeline
    {
        public string Name { get; private set; }
        public Guid ID { get; private set; }
        public Guid ExecutionId { get; set; }

        public BasePipeline(string name, Guid id)
        {
            Name = name;
            ID = id;
        }

        public abstract Task Execute(PipelineRequest request);
        public abstract Task<PipelineResponse> Complete(MiddlewareResponse response);
    }
}