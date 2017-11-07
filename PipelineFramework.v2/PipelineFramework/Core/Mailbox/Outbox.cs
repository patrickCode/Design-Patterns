namespace PipelineFramework.Interfaces.Mailbox
{
    public abstract class Outbox : Mailbox
    {
        public Outbox(string id, string name) : base(id, name)
        {
        }
    }
}