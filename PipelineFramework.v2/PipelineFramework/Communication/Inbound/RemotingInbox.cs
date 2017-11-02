using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Inbound
{
    public class RemotingInbox: Inbox
    {
        public string ServiceUri { get; set; }
        public string ListenerName { get; set; }

        public RemotingInbox(string serviceUri): base("RemotingInboundRule", "RMT")
        {   
            ServiceUri = serviceUri;
        }

        public RemotingInbox(string serviceUri, string listenerName) : this (serviceUri)
        {
            ListenerName = listenerName;
        }
    }
}