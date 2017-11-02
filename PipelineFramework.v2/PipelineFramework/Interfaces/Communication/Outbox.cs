namespace PipelineFramework.Interfaces.Communication
{
    public abstract class Outbox
    {
        public string RuleName { get; set; }
        public string OutboxCode { get; set; }

        public Outbox() { }
        public Outbox(string ruleName, string outboxCode)
        {
            RuleName = ruleName;
            OutboxCode = outboxCode;
        }
    }
}