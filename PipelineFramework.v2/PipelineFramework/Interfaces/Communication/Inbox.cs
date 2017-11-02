namespace PipelineFramework.Interfaces.Communication
{
    public abstract class Inbox
    {
        public string InboxCode { get; set; }
        public string RuleName { get; set; }

        public Inbox() { }
        public Inbox(string ruleName, string inboxCode)
        {
            RuleName = ruleName;
            InboxCode = inboxCode;
        }
    }
}