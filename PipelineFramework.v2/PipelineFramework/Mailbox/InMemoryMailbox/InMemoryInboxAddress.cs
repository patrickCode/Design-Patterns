using PipelineFramework.Interfaces.Mailbox;

namespace PipelineFramework.InMemoryMailbox
{
    public class InMemoryInboxAddress: MailboxAddress
    {
        public string ExecutingMethodName { get; set; }
        public InMemoryInboxAddress(string executingMethodName)
        {
            ExecutingMethodName = executingMethodName;
        }
    }
}