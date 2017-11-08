using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Core.Message;
using PipelineFramework.Core;

namespace PipelineFramework.InMemoryMailbox
{
    public class InMemoryMailbox : Mailbox
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MailboxAddress Address { get; set; }
        public List<BaseMessage> UnreadMessages { get; set; }
        public List<BaseMessage> ReadMessages { get; set; }

        public event EventHandler<BaseMessage> NewMessageReceived;
        private bool _isActive;

        public InMemoryMailbox(string id, string name)
        {
            Id = id;
            Name = name;
            UnreadMessages = new List<BaseMessage>();
            ReadMessages = new List<BaseMessage>();

            _isActive = false;
        }

        public Task SendMessage(BaseMessage message)
        {
            if (_isActive)
            {
                UnreadMessages.Add(message);
                NewMessageReceived(this, message);
                UnreadMessages.Remove(message);
                ReadMessages.Add(message);
            }
            return Task.CompletedTask; ;
        }

        public Task StartListeningToIncomingMessages()
        {
            _isActive = true;
            return Task.CompletedTask;
        }

        public Task StopListening()
        {
            throw new NotImplementedException();
        }
    }
}