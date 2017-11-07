using System;
using Newtonsoft.Json;
using PipelineFramework.Core.Message;

namespace PipelineFramework.Core.Message
{
    [Serializable]
    public class MiddlewareResponse: BaseMessage
    {
        [JsonProperty("timeTaken")]
        public TimeSpan TimeTaken { get; set; }

        public MiddlewareResponse()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public MiddlewareResponse(Guid executionId, string result) : this()
        {
            ExecutionId = executionId;
            Message = result;
        }
    }
}