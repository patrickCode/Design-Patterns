using System;
using System.Collections.Generic;

namespace PipelineFramework
{
    [Serializable]
    public class PipelineRequest
    {
        public string RequestObject { get; set; }
        public string ExecutedBy { get; set; }
        public IDictionary<string, object> Configuration { get; set; }
    }
}