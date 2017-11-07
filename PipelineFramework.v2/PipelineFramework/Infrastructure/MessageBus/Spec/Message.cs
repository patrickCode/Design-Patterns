using System;
using System.Collections.Generic;

namespace PipelineFramework.Infrastrucure.MessageBus.Spec
{
    public class Message
    {
        public string Id { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime ReceivedAt { get; set; }
        public object Payload { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string ContentType { get; set; }
        public bool IsControl { get; set; }
        public string FilterCorrelation { get; set; }
    }
}