namespace PipelineFramework.Interfaces.Mailbox
{
    public abstract class Inbox : Mailbox
    {
        public Inbox(string id, string name) : base(id, name)
        {
        }
    }
}