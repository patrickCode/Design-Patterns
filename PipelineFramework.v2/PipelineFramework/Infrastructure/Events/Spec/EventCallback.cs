using System;

namespace PipelineFramework.Infrastructure.Events.Spec
{
    public class EventCallback
    {
        public event EventHandler<EventReceivedArgs> EventReceived;
        public EventCallback(EventHandler<EventReceivedArgs> eventReceived)
        {
            EventReceived = eventReceived;
        }

        public void Receive(EventReceivedArgs e)
        {
            EventReceived(this, e);
        }
    }
}