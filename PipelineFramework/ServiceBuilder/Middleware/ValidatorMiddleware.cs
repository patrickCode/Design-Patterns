using System;
using Newtonsoft.Json;
using PipelineFramework;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ItemBuilder.Middleware
{
    public class ValidatorMiddleware : BaseMiddleware
    {
        public ValidatorMiddleware(): base("Validator Middleware", "VMW")
        {   
        }

        public override Task Process(MiddlewareRequest request)
        {
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                var items = JsonConvert.DeserializeObject<List<Item>>(request.Request);
                if (items == null)
                {
                    timer.Stop();
                    var exception = new MiddlewareExceptionResponse()
                    {
                        Exception = new Exception("JSON not formed properly"),
                        CompletedAt = DateTime.UtcNow,
                        TimeTaken = timer.Elapsed,
                        ExecutionConfiguration = request.ExecutionConfiguration
                    };
                    return Abort(exception);
                }

                timer.Stop();
                var response = new MiddlewareResponse()
                {
                    Response = request.Request,
                    CompletedAt = DateTime.UtcNow,
                    TimeTaken = timer.Elapsed,
                    ExecutionConfiguration = request.ExecutionConfiguration
                };
                return Complete(response);
            }
            catch (Exception error)
            {
                timer.Stop();
                var response = new MiddlewareExceptionResponse()
                {
                    Exception = error,
                    CompletedAt = DateTime.UtcNow,
                    TimeTaken = timer.Elapsed,
                    ExecutionConfiguration = request.ExecutionConfiguration
                };
                return Abort(response);
            }
        }
    }
}