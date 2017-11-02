using System;
using System.Collections.Generic;
using PipelineFramework.Interfaces;
using PipelineFramework.Communication.Inbound;
using PipelineFramework.Communication.Outbound;

namespace PipelineFramework.Core.MiddlewareType
{
    public class AzureFunctionMiddleware : Middleware
    {   
        public AzureFunctionMiddleware() : base("AZM", "AzureFunctionMiddleware")
        {
            _allowedInboxTypes = new List<Type>()
            {
                typeof(WebServiceInbox),
                typeof(QueueInbox),
                typeof(TopicInbox),
            };

            _allowedOutboxTypes = new List<Type>()
            {
                typeof(WebServiceOutbox),
                typeof(EventOutbox),
                typeof(QueueOutbox)
            };
        }
    }
}