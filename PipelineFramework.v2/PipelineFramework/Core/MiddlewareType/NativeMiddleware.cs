using PipelineFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PipelineFramework.Interfaces.Data;
using PipelineFramework.Communication.Inbound;
using PipelineFramework.Communication.Outbound;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Core.MiddlewareType
{
    public abstract class NativeMiddleware : Middleware
    {
        public NativeMiddleware() : base("NTVM", "NativeMiddleware")
        {
            _allowedInboxTypes = new List<Type>
            {
                typeof(InMemoryNativeInbox)
            };

            _allowedOutboxTypes = new List<Type>
            {
                typeof(InMemoryCallbackOutbox)
            };
        }

        public override void SetInboundRule(Inbox inbox)
        {
            base.SetInboundRule(inbox);
            (inbox as InMemoryNativeInbox).ExecutionMethodName = "ExecuteAsync";
        }

        public abstract Task ExecuteAsync(MiddlewareRequest request);
    }
}
