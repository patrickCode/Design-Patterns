using System.Threading.Tasks;
using PipelineFramework.Interfaces.Data;

namespace PipelineFramework.Interfaces.Communication
{
    public abstract class OutboxTrigger
    {
        protected readonly Outbox _outbox;
        protected readonly Middleware _middleware;

        public OutboxTrigger(Middleware middleware)
        {
            _middleware = middleware;
        }

        public abstract Task CompleteAsync(MiddlewareResponse response);
        public abstract Task AbortAsync(MiddlewareException exception);
    }
}