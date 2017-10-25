using System;
using PipelineFramework;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using static PipelineFramework.BaseMiddleware;

namespace InMemoryPipeline
{
    public class InMemoryPipeline : BasePipeline
    {
        private List<BaseMiddleware> _middlewares;
        private int _currentExecutingMidlewareIndex;
        private IDictionary<string, object> _pipelineConfiguration;
        private Stopwatch _timer;

        public InMemoryPipeline(string name, Guid id, List<BaseMiddleware> middleware) : base(name, id)
        {
            _middlewares = middleware;
            _currentExecutingMidlewareIndex = 0;
            _pipelineConfiguration = new Dictionary<string, object>();
            _timer = new Stopwatch();
            SetupEnvironmentConfiguration();
        }

        private void SetupEnvironmentConfiguration()
        {
            Log log = Log;
            _pipelineConfiguration["PipelineLogger"] = log;

            CompleteCallback completeCallback = MiddlewareExecutionCompleted;
            _pipelineConfiguration["PipelineSuccessCallback"] = completeCallback;

            AbortCallback abortCallback = MiddlewareExecutionAborted;
            _pipelineConfiguration["PipelineAbortCallback"] = abortCallback;

            _middlewares.ForEach(middleware => middleware.AddPipelineConfiguration(_pipelineConfiguration));
        }

        //public delegate Task Logger(string message);
        //public delegate Task CompleteCallback(MiddlewareResponse response);
        //public delegate Task AbortCallback(MiddlewareExceptionResponse response);

        public async Task Log(string message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(message);
            });
        }

        public async Task MiddlewareExecutionCompleted(MiddlewareResponse response)
        {
            var currentMiddleware = _middlewares[_currentExecutingMidlewareIndex];
            Console.WriteLine($"Middleware {currentMiddleware.Name} executed in {response.TimeTaken.TotalMilliseconds} ms");
            await ExecuteNext(response);
        }

        public async Task MiddlewareExecutionAborted(MiddlewareExceptionResponse exception)
        {
            await Task.Run(() =>
            {
                var currentMiddleware = _middlewares[_currentExecutingMidlewareIndex];
                Console.WriteLine($"Pipeline Aborted at {currentMiddleware.Name}. Exception - {exception.Exception.Message}");
            });
        }

        private Task ExecuteNext(MiddlewareResponse lastResponse)
        {
            var nextRequest = new MiddlewareRequest()
            {
                ExecutionConfiguration = lastResponse.ExecutionConfiguration,
                Request = lastResponse.Response,
                InvokedAt = DateTime.UtcNow,
                ExecutionId = ExecutionId
            };

            _currentExecutingMidlewareIndex++;
            if (_currentExecutingMidlewareIndex >= _middlewares.Count)
            {
                return Complete(lastResponse);
            }
            else
            {
                var currentMiddleware = _middlewares[_currentExecutingMidlewareIndex];
                return currentMiddleware.Invoke(nextRequest);
            }   
        }
        
        public override Task Execute(PipelineRequest request)
        {
            _currentExecutingMidlewareIndex = 0;
            var middleware = _middlewares[_currentExecutingMidlewareIndex];
            var executionId = Guid.NewGuid();

            var middlewareRequest = new MiddlewareRequest()
            {
                Request = request.RequestObject,
                InvokedAt = DateTime.UtcNow,
                ExecutionConfiguration = request.Configuration,
                ExecutionId = executionId
            };
            return middleware.Invoke(middlewareRequest);
        }

        public override async Task<PipelineResponse> Complete(MiddlewareResponse response)
        {
            return await Task.Run(() =>
            {
                Console.WriteLine($"Pipeline {Name} execution {ExecutionId} completed.");
                _timer.Stop();
                return new PipelineResponse()
                {
                    IsSuccessfull = true,
                    Exception = null,
                    ResponseObject = response.Response,
                    TimeTaken = _timer.Elapsed
                };
            });
        }
    }
}