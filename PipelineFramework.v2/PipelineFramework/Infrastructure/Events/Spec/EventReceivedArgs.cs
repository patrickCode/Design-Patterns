using System;
using System.Linq;
using System.Text;
using Microsoft.Azure.EventHubs;
using System.Collections.Generic;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public class EventReceivedArgs
    {
        public Event Event { get; set; }

        public EventReceivedArgs(EventData azureEvent)
        {
            var messageBytes = azureEvent.Body;
            var payload = Encoding.UTF8.GetString(messageBytes.Array, messageBytes.Offset, messageBytes.Count);
            Event = new Event()
            {
                Payload = payload,
                ReceivedOn = DateTime.UtcNow
            };
            if (azureEvent.Properties != null && azureEvent.Properties.Any())
                Event.Properties = new Dictionary<string, object>(azureEvent.Properties);
            else
                Event.Properties = new Dictionary<string, object>();

            Event.ContentType = Event.Properties.ContainsKey("ContentType") ? Event.Properties["ContentType"].ToString() : "control";
        }

        public EventReceivedArgs(EventData azureEvent, string partitionKey) : this(azureEvent)
        {
            Event.PartitionKey = partitionKey;
        }
    }
}