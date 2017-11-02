using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PipelineFramework.Interfaces.Data
{
    [Serializable]
    public class MiddlewareException: MiddlewareResponse
    {
        [JsonProperty("error")]
        public Exception Error { get; set; }

        [JsonProperty("errorMessages")]
        public List<string> ErrorMessages { get; set; }

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