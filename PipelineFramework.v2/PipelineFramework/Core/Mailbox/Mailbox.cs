using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Core.Message;


namespace PipelineFramework.Core
{
    public interface Mailbox
    {   
        string Id { get; set; }
        string Name { get; set; }
        MailboxAddress Address { get; set; }
        List<BaseMessage> UnreadMessages { get; set; }
        List<BaseMessage> ReadMessages { get; set; }

        event EventHandler<BaseMessage> NewMessageReceived;
        Task SendMessage(BaseMessage message);
        Task StartListeningToIncomingMessages();
        Task StopListening();
    }
}