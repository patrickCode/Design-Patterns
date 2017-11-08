using System;
using System.Threading.Tasks;
using PipelineFramework.Core;
using System.Collections.Generic;
using PipelineFramework.Core.Message;
using PipelineFramework.Infrastructure.Events;
using PipelineFramework.AzureEventMailbox.Spec;
using PipelineFramework.Infrastructure.Events.Spec;

namespace PipelineFramework.AzureEventMailbox
{
    public class AzureEventMailbox : Mailbox
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<BaseMessage> UnreadMessages { get; set; }
        public List<BaseMessage> ReadMessages { get; set; }

        public event EventHandler<BaseMessage> NewMessageReceived;

        public MailboxAddress Address { get => _address;
            set
            {
                if (value is AzureEventAddress)
                    _address = value as AzureEventAddress;
                else
                    throw new Exception("Bad Address: Only Event Hub Address is allowed");
            }
        }

        private AzureEventAddress _address;
        private readonly IEventSender _eventSender;
        private readonly IEventReceiver _eventReceiver;
        private readonly IEventHubBuilder _eventHubBuilder;
        private readonly IEventConverter _eventConverter;

        public AzureEventMailbox(string id, string name, AzureEventAddress address)
        {
            Id = id;
            Name = name;
            Address = address;

            UnreadMessages = new List<BaseMessage>();
            ReadMessages = new List<BaseMessage>();

            _eventHubBuilder = new EventHubBuilder(_address.Configuration);
            _eventSender = new EventSender(_eventHubBuilder, _address.EventHubName);
            _eventReceiver = new EventReceiver(_eventHubBuilder, _address.EventHubName, _address.ConsumerGroupName);
            _eventConverter = new EventConverter();
        }

        public async Task SendMessage(BaseMessage message)
        {
            var @event = _eventConverter.Convert(message);
            await _eventSender.SendAsync(@event);
        }

        public async Task StartListeningToIncomingMessages()
        {
            await _eventReceiver.StartAsync();
            _eventReceiver.Callback.EventReceived += MessageReceived;
        }

        private void MessageReceived(object sender, EventReceivedArgs e)
        {
            var @event = e.Event;
            var message = _eventConverter.Convert(@event);
            NewMessageReceived(this, message);
        }

        public Task StopListening()
        {
            throw new NotImplementedException();
        }
    }
}