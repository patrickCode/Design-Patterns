using System;
using System.Reflection;
using System.Threading.Tasks;
using PipelineFramework.Interfaces;
using PipelineFramework.Interfaces.Message;
using PipelineFramework.Interfaces.Mailbox;

namespace PipelineFramework.InMemoryMailbox
{
    public class InMemoryMailboxMessenger : MailboxMessenger<InMemoryInbox>
    {
        public InMemoryMailboxMessenger(InMemoryInbox mailbox, Middleware middleware) : base(mailbox, middleware)
        {
        }

        public override async Task SendMessage(MiddlewareRequest message)
        {
            var middlewareType = _middleware.GetType();
            var address = _mailbox.Address as InMemoryInboxAddress;
            MethodInfo executionMethod = middlewareType.GetMethod(address.ExecutingMethodName);

            var exceutionTask = executionMethod.Invoke(_middleware, new object[] { message }) as Task;
            if (exceutionTask == null)
                throw new Exception("Execution method in middleware is not formed correctly. It should be returning a task");

            await exceutionTask;
        }
    }
}