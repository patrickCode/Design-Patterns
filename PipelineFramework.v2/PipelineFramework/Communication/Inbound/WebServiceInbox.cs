using System;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Communication.Inbound
{
    public class WebServiceInbox: Inbox
    {
        public Uri Endpoint { get; set; }
        
        public WebServiceInbox(Uri endpoint) : base("WebServiceInbox", "WBSI")
        {
            Endpoint = endpoint;
        }
    }
}