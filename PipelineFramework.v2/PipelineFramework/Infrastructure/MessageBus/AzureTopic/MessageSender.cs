using Microsoft.Azure.ServiceBus;
using PipelineFramework.Infrastrucure.MessageBus.Spec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public class MessageSender : ISender
    {
        private readonly IMessageBusBuilder _builder;
        private readonly string _topicName;
        private readonly TopicClient _client;

        public MessageSender()
        {
        }

        public Task SendAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
