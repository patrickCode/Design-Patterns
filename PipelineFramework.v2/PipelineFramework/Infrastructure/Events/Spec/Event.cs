using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public class Event
    {
        public string Id { get; set; }
        public string PartitionKey { get; set; }
        public string Payload { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public EventData ToAzureEvent()
        {
            var messageBytes = Encoding.UTF8.GetBytes(Payload);
            var azureEvent = new EventData(messageBytes);
            if (Properties != null && Properties.Any())
            {
                foreach (var property in Properties)
                {
                    azureEvent.Properties.Add(property);
                }
            }
            return azureEvent;
        }
    }
}