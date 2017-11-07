using System;
using PipelineFramework.Interfaces.Mailbox;

namespace PipelineFramework.InMemoryMailbox
{
    public class InMemoryInbox : Mailbox
    {
        private InMemoryInboxAddress _inMemoryAddress { get; set; }
        public override MailboxAddress Address
        {
            get => _inMemoryAddress; 
            set
            {
                if (value is InMemoryInboxAddress)
                    _inMemoryAddress = value as InMemoryInboxAddress;
                else
                    throw new Exception("Bad Address: Only In Memory Address is allowed");
            }
        }

        public InMemoryInbox(string id, string name, string executingMethodName) : base(id, name)
        {
            _inMemoryAddress = new InMemoryInboxAddress(executingMethodName);
        }

        public InMemoryInbox(string id, string name, InMemoryInboxAddress address) : base(id, name)
        {
            _inMemoryAddress = address;
        }
    }
}