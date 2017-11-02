using System;

namespace PipelineFramework.Interfaces
{
    public abstract class BasePipeline
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid ExecutionId { get; set; }
    }
}