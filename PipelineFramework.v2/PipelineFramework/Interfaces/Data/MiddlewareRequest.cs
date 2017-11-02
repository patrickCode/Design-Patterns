using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PipelineFramework.Interfaces.Data
{
    [Serializable]
    public class MiddlewareRequest
    {
        [JsonProperty("executionId")]
        public Guid ExecutionId { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("invokedAt")]
        public DateTime InvokedAt { get; set; }

        [JsonProperty("executionConfiguration")]
        public IDictionary<string, object> ExecutionConfiguration { get; set; }

        public MiddlewareRequest()
        {
            InvokedAt = DateTime.UtcNow;
            ExecutionConfiguration = new Dictionary<string, object>();
        }

        public MiddlewareRequest(Guid executionId, string request) : this()
        {
            ExecutionId = executionId;
            Request = request;
        }
        
        public MiddlewareResponse CreateResponse(string result)
        {
            var response = new MiddlewareResponse(ExecutionId, result)
            {
                ExecutionConfiguration = this.ExecutionConfiguration
            };
            response.TimeTaken = response.CompletedAt.Subtract(InvokedAt);
            return response;
        }
    }
}