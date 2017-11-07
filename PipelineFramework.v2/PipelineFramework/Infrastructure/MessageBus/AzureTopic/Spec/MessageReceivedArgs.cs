using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SB = Microsoft.Azure.ServiceBus;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic.Spec
{
    public class MessageReceivedArgs
    {
        public MessageBus.Spec.Message Message;

        public MessageReceivedArgs(SB.Message message)
        {
            Message.Id = message.MessageId;
            var messageBytes = message.Body;
            var messageStr = Encoding.UTF8.GetString(messageBytes);
            Message = new MessageBus.Spec.Message()
            {
                Payload = messageStr,
                IsControl = message.ContentType == "control",
                ContentType = message.ContentType,
                ReceivedAt = DateTime.UtcNow,
                SentAt = message.ScheduledEnqueueTimeUtc
            };
            if (message.UserProperties != null && message.UserProperties.Any())
                Message.Properties = new Dictionary<string, object>(message.UserProperties);
            else
                Message.Properties = new Dictionary<string, object>();
        }
    }
}