using System.Threading.Tasks;
using PipelineFramework.Interfaces.Data;

namespace PipelineFramework.Interfaces.Communication
{
    public abstract class InboxTrigger
    {
        protected readonly Inbox _inbox;
        protected readonly Middleware _middleware;

        public InboxTrigger(Middleware middleware)
        {
            _inbox = middleware.Inbox;
            _middleware = middleware;
        }

        public virtual async Task InvokeAsync(MiddlewareRequest request)
        {
            await _middleware.Validate();
            await TriggerAsync(request);
        }

        protected abstract Task TriggerAsync(MiddlewareRequest request);
    }
}