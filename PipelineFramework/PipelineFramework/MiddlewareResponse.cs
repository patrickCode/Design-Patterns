using System;
using System.Collections.Generic;

namespace PipelineFramework
{
    [Serializable]
    public class MiddlewareResponse
    {
        public MiddlewareResponse()
        {
            ExecutionConfiguration = new Dictionary<string, object>();
        }

        public string Response { get; set; }
        public IDictionary<string, object> ExecutionConfiguration { get; set; }
        public DateTime CompletedAt { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public Guid TrackingGuid { get; set; }
    }
}