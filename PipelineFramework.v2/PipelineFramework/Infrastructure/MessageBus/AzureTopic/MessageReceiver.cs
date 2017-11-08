using System;
using PipelineFramework.Infrastrucure.MessageBus.Spec;
using System.Threading.Tasks;
using PipelineFramework.Infrastrucure.MessageBus.AzureTopic.Spec;
using SB = Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus;
using System.Threading;

namespace PipelineFramework.Infrastrucure.MessageBus.AzureTopic
{
    public class MessageReceiver : IReceiver
    {
        public event EventHandler<MessageReceivedArgs> MessageReceived;

        private readonly IMessageBusBuilder _builder;
        private SubscriptionClient _client;
        private readonly string _topicName;
        private readonly string _subscriptionName;
        private readonly string _correlationFilter;
        private readonly string _correlationRuleName;
        private readonly int _concurrentCalls;

        public MessageReceiver(IMessageBusBuilder builder, string topicName, string subscriptionName, string filterCorrelation, string correlationRuleName, int concurrentCalls)
        {
            _builder = builder;
            _topicName = topicName;
            _subscriptionName = subscriptionName;
            _correlationFilter = filterCorrelation;
            _correlationRuleName = correlationRuleName;
            _concurrentCalls = concurrentCalls;
        }

        public async Task StartAsync()
        {
            _client = await _builder.CreateSubscriptionClient(_topicName, _subscriptionName, _correlationFilter, _correlationRuleName);
            var messageHandlerOptions = new MessageHandlerOptions(exceptionReceivedEventArgs =>
            {
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = _concurrentCalls,
                AutoComplete = true
            };

            _client.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageAsync(SB.Message message, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var messageReceivedArgs = new MessageReceivedArgs(message);
                MessageReceived(this, messageReceivedArgs);
            });
        }
    }
}