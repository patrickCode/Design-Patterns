using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Interfaces;
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

        public override void SetOutboundRule(Outbox outbox)
        {
            base.SetOutboundRule(outbox);
            if (outbox is InMemoryNativeOutbox)
            {
                (outbox as InMemoryNativeOutbox).SuccessMethod = "CompleteAsync";
                (outbox as InMemoryNativeOutbox).ErrorMethod = "AbortAsync";
            }

        }

        public abstract Task ExecuteAsync(MiddlewareRequest request);

        public abstract Task CompleteAsync(MiddlewareResponse result);

        public abstract Task AbortAsync(MiddlewareException exception);
    }
}