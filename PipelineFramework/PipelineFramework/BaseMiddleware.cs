using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PipelineFramework
{   
    public abstract class BaseMiddleware
    {
        public BaseMiddleware(string name, string id)
        {
            Name = name;
            Id = id;
            EnvironmentConfiguration = new Dictionary<string, object>();
        }

        public string Name { get; private set; }
        public string Id { get; private set; }
        public Guid ExecutionId { get; private set; }
        public IDictionary<string, object> EnvironmentConfiguration { get; set; }

        public delegate Task Log(string message);
        public delegate Task CompleteCallback(MiddlewareResponse response);
        public delegate Task AbortCallback(MiddlewareExceptionResponse response);

        public virtual async Task Invoke(MiddlewareRequest request)
        {
            await LogMessage($"{Name} middleware has been executed");
            await Process(request);
        }

        public abstract Task Process(MiddlewareRequest request);

        public async Task LogMessage(string message)
        {
            var logger = EnvironmentConfiguration["PipelineLogger"] as Log;
            await logger(message);
        }

        public async Task Complete(MiddlewareResponse response)
        {
            var successCallback = EnvironmentConfiguration["PipelineSuccessCallback"] as CompleteCallback;
            await successCallback(response);
        }

        public async Task Abort(MiddlewareExceptionResponse response)
        {
            var abortCallback = EnvironmentConfiguration["PipelineAbortCallback"] as AbortCallback;
            await abortCallback(response);
        }

        public void AddPipelineConfiguration(IDictionary<string, object> pipelineConfiguration)
        {
            foreach(var config in pipelineConfiguration)
            {
                if (EnvironmentConfiguration.ContainsKey(config.Key))
                {
                    EnvironmentConfiguration[config.Key] = config.Value;
                }
                else
                {
                    EnvironmentConfiguration.Add(config);
                }
            }
        }

        public void AddConfiguration(string key, object value)
        {
            if (EnvironmentConfiguration.ContainsKey(key))
            {
                EnvironmentConfiguration[key] = key;
            }
            else
            {
                EnvironmentConfiguration.Add(key, value);
            }
        }
    }
}