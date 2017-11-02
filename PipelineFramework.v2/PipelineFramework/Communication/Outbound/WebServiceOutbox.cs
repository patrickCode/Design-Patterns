using PipelineFramework.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineFramework.Communication.Outbound
{
    public class WebServiceOutbox: Outbox
    {
        public Uri SuccessCallback { get; set; }
        public Uri ErrorCallback { get; set; }

        public WebServiceOutbox(Uri successCallback, Uri errorCallback): base("WebServiceOutbox", "WBSO")
        {
            SuccessCallback = successCallback;
            ErrorCallback = errorCallback;
        }
    }
}
