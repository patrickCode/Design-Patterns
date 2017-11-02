using System;
using PipelineFramework.Interfaces.Communication;
using PipelineFramework.Interfaces.Data;
using System.Threading.Tasks;
using PipelineFramework.Interfaces;
using PipelineFramework.Communication.Outbound;
using System.Reflection;

namespace PipelineFramework.CommunicationTrigger.InMemory
{
    public class InMemoryOutboxTrigger : OutboxTrigger
    {
        private InMemoryNativeOutbox _inMemoryNativeOutbox;
        public InMemoryOutboxTrigger(Middleware middleware) : base(middleware)
        {
            _inMemoryNativeOutbox = _outbox as InMemoryNativeOutbox;
        }

        public override async Task AbortAsync(MiddlewareException exception)
        {
            if (_inMemoryNativeOutbox == null)
                throw new Exception("The Outbox is malformed");

            var middlewareType = _middleware.GetType();
            MethodInfo executionMethod = middlewareType.GetMethod(_inMemoryNativeOutbox.ErrorMethod);

            var exceutionTask = executionMethod.Invoke(_middleware, new object[] { exception }) as Task;
            if (exceutionTask == null)
                throw new Exception("Abort method in middleware is not formed correctly. It should be returning a task");

            await exceutionTask;
        }

        public override async Task CompleteAsync(MiddlewareResponse response)
        {
            if (_inMemoryNativeOutbox == null)
                throw new Exception("The Outbox is malformed");

            var middlewareType = _middleware.GetType();
            MethodInfo executionMethod = middlewareType.GetMethod(_inMemoryNativeOutbox.SuccessMethod);

            var exceutionTask = executionMethod.Invoke(_middleware, new object[] { response }) as Task;
            if (exceutionTask == null)
                throw new Exception("Completion method in middleware is not formed correctly. It should be returning a task");

            await exceutionTask;
        }
    }
}
