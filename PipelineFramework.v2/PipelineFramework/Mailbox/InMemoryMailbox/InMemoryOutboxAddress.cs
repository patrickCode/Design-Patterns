using System.Threading.Tasks;
using PipelineFramework.Interfaces.Message;
using PipelineFramework.Interfaces.Mailbox;

namespace PipelineFramework.InMemoryMailbox
{
    public delegate Task CompletedCallbackTypeAsync(MiddlewareResponse response);
    public delegate Task AbortCallbackTypeAsync(MiddlewareException exception);

    public class InMemoryOutboxAddress: MailboxAddress
    {
        public CompletedCallbackTypeAsync CompletedCallback;
        public AbortCallbackTypeAsync AbortCallback;
        
        public InMemoryOutboxAddress(CompletedCallbackTypeAsync completedCallback, AbortCallbackTypeAsync abortCallback)
        {
            CompletedCallback = completedCallback;
            AbortCallback = abortCallback;
        }
    }
}