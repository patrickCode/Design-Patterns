using System;
using System.Threading.Tasks;
using PipelineFramework.Interfaces.Message;
using PipelineFramework.Interfaces.Mailbox;

namespace PipelineFramework.InMemoryMailbox
{
    public class InMemoryOutbox : Outbox
    {
        private InMemoryOutboxAddress _outboxAddress;
        public override MailboxAddress Address {
            get => _outboxAddress;
            set
            {
                if (value is InMemoryOutboxAddress)
                    _outboxAddress = value as InMemoryOutboxAddress;
                else
                    throw new Exception("Bad Address: In Memory Outbox address allowed.");
            }
        }
        
        public InMemoryOutbox(string id, string name, CompletedCallbackTypeAsync completionCallback, AbortCallbackTypeAsync abortCallback) : base(id, name)
        {
            _outboxAddress = new InMemoryOutboxAddress(completionCallback, abortCallback);
        }

        public InMemoryOutbox(string id, string name, InMemoryOutboxAddress address) : base(id, name)
        {
            _outboxAddress = address;
        }

        public async Task SendCompletionMessage(MiddlewareResponse response)
        {
            await _outboxAddress.CompletedCallback(response);
        }

        public async Task SendAbortMessage(MiddlewareException exception)
        {
            await _outboxAddress.AbortCallback(exception);
        }
    }
}