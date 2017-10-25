using System;
using System.Collections.Generic;

namespace PipelineFramework
{
    [Serializable]
    public class MiddlewareRequest
    {
        public MiddlewareRequest()
        {
            ExecutionConfiguration = new Dictionary<string, object>();
        }

        public string Request { get; set; }
        public IDictionary<string, object> ExecutionConfiguration { get; set; }
        public DateTime InvokedAt { get; set; }
        public Guid ExecutionId { get; set; }
    }
}