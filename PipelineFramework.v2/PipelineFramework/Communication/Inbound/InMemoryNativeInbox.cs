using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Inbound
{
    public class InMemoryNativeInbox: Inbox
    {
        public string ExecutionMethodName { get; set; }

        public InMemoryNativeInbox(): base("InMemoryNativeInbox", "IMNI") { }

        public InMemoryNativeInbox(string methodName) : this()
        {
            ExecutionMethodName = methodName;
        }
    }
}