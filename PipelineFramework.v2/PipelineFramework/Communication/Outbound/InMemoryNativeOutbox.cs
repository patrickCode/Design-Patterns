using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Outbound
{
    public class InMemoryNativeOutbox: Outbox
    {
        public string SuccessMethod { get; set; }
        public string ErrorMethod { get; set; }

        public InMemoryNativeOutbox(string successMethod, string errorMethod) : base("InMemoryNativeOutbox", "IMNO")
        {
            SuccessMethod = successMethod;
            ErrorMethod = errorMethod;
        }
    }
}