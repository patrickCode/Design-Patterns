using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineFramework.Infrastrucure.MessageBus.Spec
{
    public class Message
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Payload { get; set; }
    }
}
