using System;
using Newtonsoft.Json;
using PipelineFramework;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ItemBuilder.Middleware
{
    public class PrintMiddleware : BaseMiddleware
    {
        public PrintMiddleware() : base("Print Middleware", "PMW") { }

        public override Task Process(MiddlewareRequest request)
        {
            var timer = new Stopwatch();
            timer.Start();
            try
            {
                var items = JsonConvert.DeserializeObject<List<Item>>(request.Request);

                foreach(var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}");
                    Console.WriteLine($"Name: {item.Id}");
                    Console.WriteLine($"Description: {item.Id}");
                    Console.WriteLine($"SKU: {item.SKU}");
                    Console.WriteLine($"Cost: {item.Cost}");
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