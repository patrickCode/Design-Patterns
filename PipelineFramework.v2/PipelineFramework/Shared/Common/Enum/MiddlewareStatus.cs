namespace PipelineFramework.Common.Enum
{
    public class MiddlewareStatus: Enumeration
    {
        private readonly string _description;
        private readonly bool _isExecutionCompleted;

        public string Description { get => _description; }
        public bool IsExecutionCompleted { get => _isExecutionCompleted; }

        public MiddlewareStatus() : base() { }

        public MiddlewareStatus(int code, string name, string description, bool isExecutionCompleted): base(code, name)
        {
            _description = description;
            _isExecutionCompleted = isExecutionCompleted;
        }

        public static MiddlewareStatus Created = new CreatedStatusEnum();
        public static MiddlewareStatus Running = new RunningStatusEnum();
        public static MiddlewareStatus Executing = new ExecutingStatusEnum();
        public static MiddlewareStatus Completed = new CompletedStatusEnum();
        public static MiddlewareStatus Faulted = new FaultedStatusEnum();

        private class CreatedStatusEnum: MiddlewareStatus
        {
            public CreatedStatusEnum(): base(1, "Created", "The middleware has been created.", false) { }
        }

        private class RunningStatusEnum : MiddlewareStatus
        {
            public RunningStatusEnum() : base(2, "Running", "The middleware is running and is ready to accept incoming mails.", false) { }
        }

        private class ExecutingStatusEnum : MiddlewareStatus
        {
            public ExecutingStatusEnum() : base(3, "Executing", "The middleware has receiveid a message and is executing the message.", false) { }
        }

        private class CompletedStatusEnum : MiddlewareStatus
        {
            public CompletedStatusEnum() : base(4, "Completed", "The middleware has succesfully completed executing the message.", true) { }
        }

        private class FaultedStatusEnum : MiddlewareStatus
        {
            public FaultedStatusEnum() : base(5, "Fauled", "The middleware has encountered an error while excuting the message.", true) { }
        }
    }
}