using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PipelineFramework.Interfaces.Data
{
    [Serializable]
    public class MiddlewareResponse
    {
        [JsonProperty("executionId")]
        public Guid ExecutionId { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("completedAt")]
        public DateTime CompletedAt { get; set; }

        [JsonProperty("timeTaken")]
        public TimeSpan TimeTaken { get; set; }

        [JsonProperty("executionConfiguration")]
        public IDictionary<string, object> ExecutionConfiguration { get; set; }

        public MiddlewareResponse()
        {
            CompletedAt = DateTime.UtcNow;
        }

        public MiddlewareResponse(Guid executionId, string result) : this()
        {
            ExecutionId = executionId;
            Response = result;
        }
    }
}