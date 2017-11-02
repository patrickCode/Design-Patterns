using System.Threading.Tasks;
using PipelineFramework.Interfaces.Data;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Outbound
{
    public class InMemoryCallbackOutbox: Outbox
    {
        public delegate Task SuccessCallbackType(MiddlewareResponse response);
        public delegate Task ErrorCallbackType(MiddlewareException exception);

        public SuccessCallbackType SuccessCallback { get; set; }
        public ErrorCallbackType ErrorCallback { get; set; }

        public InMemoryCallbackOutbox(SuccessCallbackType successCallback, ErrorCallbackType errorCallback) : base("InMemoryCallbackOutbox", "IMCO")
        {
            SuccessCallback = successCallback;
            ErrorCallback = errorCallback;
        }
    }
}