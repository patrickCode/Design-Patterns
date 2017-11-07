using System.Threading.Tasks;
using PipelineFramework.Interfaces.Message;

namespace PipelineFramework.Interfaces.Mailbox
{
    public abstract class MailboxMessenger<M> where M: Mailbox
    {
        protected M _mailbox;
        protected Middleware _middleware;
        public MailboxMessenger(M mailbox, Middleware middleware)
        {
            _mailbox = mailbox;
            _middleware = middleware;
        }

        public abstract Task SendMessage(MiddlewareRequest message);
    }
}