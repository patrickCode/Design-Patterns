using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PipelineFramework.Core.Message
{
    [Serializable]
    public class MiddlewareException: MiddlewareResponse
    {
        [JsonProperty("error")]
        public Exception Error { get; set; }

        [JsonProperty("errorMessages")]
        public List<string> ErrorMessages { get; set; }

        public MiddlewareException() { }

        public MiddlewareException(Exception error): base()
        {
            Error = error;
            ErrorMessages = new List<string>();
        }

        public MiddlewareException(List<string> errors) : base()
        {   
            ErrorMessages = errors;
        }
    }
}