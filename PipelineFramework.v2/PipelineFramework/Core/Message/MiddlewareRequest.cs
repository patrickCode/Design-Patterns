using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using PipelineFramework.Core.Message;

namespace PipelineFramework.Core.Message
{
    [Serializable]
    public class MiddlewareRequest: BaseMessage
    {
        public MiddlewareRequest()
        {
            CreatedOn = DateTime.UtcNow;
            ExecutionConfiguration = new Dictionary<string, object>();
        }

        public MiddlewareRequest(Guid executionId, string request) : this()
        {
            ExecutionId = executionId;
            Message = request;
        }
        
        public MiddlewareResponse CreateResponse(string result)
        {
            var response = new MiddlewareResponse(ExecutionId, result)
            {
                ExecutionConfiguration = this.ExecutionConfiguration,
                PipelineId = this.PipelineId
            };
            response.TimeTaken = response.CreatedOn.Subtract(this.CreatedOn);
            return response;
        }
    }
}