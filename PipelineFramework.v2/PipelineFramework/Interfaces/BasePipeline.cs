using System;

namespace PipelineFramework.Core
{
    public abstract class BasePipeline
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid ExecutionId { get; set; }
    }
}