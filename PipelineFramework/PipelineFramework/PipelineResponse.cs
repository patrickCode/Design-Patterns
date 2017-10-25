using System;

namespace PipelineFramework
{
    public class PipelineResponse
    {
        public string ResponseObject { get; set; }
        public bool IsSuccessfull { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public Exception Exception { get; set; }
    }
}