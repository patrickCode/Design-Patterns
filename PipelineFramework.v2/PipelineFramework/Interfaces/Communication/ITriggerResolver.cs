namespace PipelineFramework.Interfaces.Communication
{
    public interface ITriggerResolver
    {
        InboxTrigger ResolveInbox<T>() where T : Inbox;
        OutboxTrigger ResolveOutbox<T>() where T : Outbox;
    }
}