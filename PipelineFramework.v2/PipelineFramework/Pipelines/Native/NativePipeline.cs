using System;
using System.Threading.Tasks;
using PipelineFramework.Core;
using System.Collections.Generic;
using PipelineFramework.Core.Message;
using PipelineFramework.Common.Enum;

namespace PipelineFramework.Pipelines.Native
{
    public abstract class NativePipeline : BasePipeline
    {
        protected int _currentMiddlewarePosition;
        public NativePipeline(string id, string name): base(id, name) { }

        public override async Task RunAsync()
        {   
            foreach (var middleware in Middlewares)
            {
                middleware.Value.SetOutbox(this.Outbox);
                await middleware.Value.RunAsync();
            }
            _currentMiddlewarePosition = 0;

            await base.RunAsync();
        }

        protected override void OnMessageReceived(object sender, BaseMessage e)
        {
            if (e is MiddlewareRequest)
                ExecuteAsync(e as MiddlewareRequest).ConfigureAwait(false);

            else if (e is MiddlewareResponse)
                ExecuteNextAsync(e as MiddlewareResponse).ConfigureAwait(false);

            else
            {
                var exception = new Exception("Bad Message: Message needs to be Middleware Request");
                var middlewareException = new MiddlewareException(exception)
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    ErrorMessages = new List<string>() { exception.Message },
                    PipelineId = this.Id,
                    TimeTaken = TimeSpan.Zero,
                    ExecutionId = this.CorrelationId,
                    Message = exception.ToString()
                };
                AbortAsync(middlewareException).Wait();
            }

        }

        protected override async Task ExecuteAsync(MiddlewareRequest request)
        {
            if (Status != MiddlewareStatus.Running)
                throw new Exception("Pipeline not ready");

            var middleware = Middlewares[_currentMiddlewarePosition];
            await ExecuteMiddleware(middleware, request);
        }

        public override async Task ExecuteNextAsync(MiddlewareResponse response)
        {
            _currentMiddlewarePosition++;
            if (_currentMiddlewarePosition == Middlewares.Count)
            {
                await CompleteAsync(response);
            }

            var middleware = Middlewares[_currentMiddlewarePosition];
            var request = new MiddlewareRequest(ExecutionId, response.Message)
            {
                Id = Guid.NewGuid(),
                ExecutionConfiguration = response.ExecutionConfiguration //TODO - Logic to be defined later
            };

            await ExecuteMiddleware(middleware, request);
        }

        private async Task ExecuteMiddleware(Middleware middleware, MiddlewareRequest request)
        {
            request.MiddlewareId = middleware.Id;
            request.PipelineId = Id;
            request.ReferenceId = middleware.ReferenceId.ToString();
            
            await middleware.Inbox.SendMessage(request);
        }
    }
}