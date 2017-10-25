using System;
using Newtonsoft.Json;
using PipelineFramework;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ItemBuilder.Middleware
{
    public class SkuAssignerMiddleware : BaseMiddleware
    {   
        public SkuAssignerMiddleware():base("SKU Assigner Middleware", "SKMW")
        {   
        }

        public override Task Process(MiddlewareRequest request)
        {
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                var items = JsonConvert.DeserializeObject<List<Item>>(request.Request);
                var prefix = EnvironmentConfiguration["SKU.Prefix"] as string;

                foreach(var item in items)
                {
                    item.SKU =  prefix + Guid.NewGuid().ToString().Substring(0, 6);
                }

                timer.Stop();
                var response = new MiddlewareResponse()
                {
                    Response = JsonConvert.SerializeObject(items),
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