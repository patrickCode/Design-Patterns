using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Common.Converter;
using PipelineFramework.Core.Message;
using PipelineFramework.Core;
using PipelineFramework.AzureTopicMailbox.Spec;
using PipelineFramework.Infrastrucure.MessageBus.Spec;
using PipelineFramework.Infrastrucure.MessageBus.AzureTopic;
using PipelineFramework.Infrastrucure.MessageBus.AzureTopic.Spec;
using PipelieneFramework.Instrastructure.Security;

namespace PipelineFramework.AzureTopicMailbox
{
    public class AzureTopicMailbox : Mailbox
    {   
        public string Id { get; set; }
        public string Name { get; set; }
        public List<BaseMessage> UnreadMessages { get; set; }
        public List<BaseMessage> ReadMessages { get; set; }

        private AzureTopicAddress _address;
        public MailboxAddress Address
        {
            get => _address;
            set
            {
                if (value is AzureTopicAddress)
                    _address = value as AzureTopicAddress;
                else
                    throw new Exception("Bad Address: Azure Topic Inbox expected.");
            }
        }

        private ISender _messageSender;
        private IReceiver _messageReceiver;
        private readonly IMessageBusBuilder _messageBusBuilder;
        private readonly IMessageConverter _messageConverter;
        private bool disposed = false;

        public event EventHandler<BaseMessage> NewMessageReceived;

        public AzureTopicMailbox(string id, string name, AzureTopicAddress address)
        {
            Id = id;
            Name = name;
            UnreadMessages = new List<BaseMessage>();
            ReadMessages = new List<BaseMessage>();

            Address = address;
            var authContext = new AuthContext(new TokenCache(), address.AuthenticationConfiguration);
            _messageBusBuilder = new MessageBusBuilder(address.ServiceBusConfiguration, authContext);
            _messageSender = new MessageSender(_messageBusBuilder, new JsonConverter(), address.TopicName);
            _messageReceiver = new MessageReceiver(_messageBusBuilder, _address.TopicName, _address.SubscriptionName, _address.CorrelationFilter, _address.CorrelationFilter, 1);
            _messageConverter = new MessageConverter();
        }

        public async Task SendMessage(BaseMessage message)
        {
            var topicMessage = _messageConverter.Convert(message);
            await _messageSender.SendAsync(topicMessage);
        }

        public async Task StartListeningToIncomingMessages()
        {   
            await _messageReceiver.StartAsync();
            _messageReceiver.MessageReceived += MessageReceived; 
        }

        private void MessageReceived(object sender, MessageReceivedArgs e)
        {   
            var message = _messageConverter.Convert(e.Message);
            UnreadMessages.Add(message);

            NewMessageReceived(this, message);

            UnreadMessages.Remove(message);
            ReadMessages.Add(message);
        }

        public Task StopListening()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _messageBusBuilder.DeleteSubscriptionAsync(_address.TopicName, _address.SubscriptionName);
            }
            disposed = true;
        }
    }
}