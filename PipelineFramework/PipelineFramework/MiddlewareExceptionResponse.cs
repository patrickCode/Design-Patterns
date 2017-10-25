using System;

namespace PipelineFramework
{
    public class MiddlewareExceptionResponse: MiddlewareResponse
    {
        public Exception Exception { get; set; }

    }
}