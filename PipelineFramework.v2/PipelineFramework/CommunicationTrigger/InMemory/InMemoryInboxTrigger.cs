using System;
using System.Reflection;
using System.Threading.Tasks;
using PipelineFramework.Interfaces;
using PipelineFramework.Interfaces.Data;
using PipelineFramework.Communication.Inbound;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.CommunicationTrigger.InMemory
{
    public class InMemoryInboxTrigger : InboxTrigger
    {
        private InMemoryNativeInbox _inMemoryInbox;
        public InMemoryInboxTrigger(Middleware middleware) : base(middleware)
        {
            _inMemoryInbox = _inbox as InMemoryNativeInbox;
        }

        protected override async Task TriggerAsync(MiddlewareRequest request)
        {
            if (_inMemoryInbox == null)
                throw new Exception("The inbox is malformed");

            var middlewareType = _middleware.GetType();
            MethodInfo executionMethod = middlewareType.GetMethod(_inMemoryInbox.ExecutionMethodName);

            var exceutionTask = executionMethod.Invoke(_middleware, new object[] { request }) as Task;
            if (exceutionTask == null)
                throw new Exception("Execution method in middleware is not formed correctly. It should be returning a task");

            await exceutionTask;
        }
    }
}