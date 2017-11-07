using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PipelineFramework.Core.Message
{
    public abstract class BaseMessage
    {
        [JsonProperty("messageId")]
        public Guid Id { get; set; }

        [JsonProperty("executionId")]
        public Guid ExecutionId { get; set; }

        [JsonProperty("pipelineId")]
        public string PipelineId { get; set; }

        [JsonProperty("middlewareId")]
        public string MiddlewareId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }
        
        [JsonProperty("executionConfiguration")]
        public IDictionary<string, object> ExecutionConfiguration { get; set; }

        public BaseMessage() {}
    }
}