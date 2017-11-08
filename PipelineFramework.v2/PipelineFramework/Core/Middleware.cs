using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Common.Enum;
using PipelineFramework.Core.Message;

namespace PipelineFramework.Core
{
    [Serializable]
    public abstract class Middleware: IDisposable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MiddlewareStatus Status { get; set; }

        public Mailbox Inbox { get; set; }
        public Mailbox Outbox { get; set; }

        public Guid ExecutionId { get; set; }
        public Guid RefernceId { get; set; }
        public TimeSpan Timeout { get; set; }
        public IDictionary<string, object> EnvironmentConfiguration { get; set; }

        protected List<Type> _allowedInboxTypes;
        protected List<Type> _allowedOutboxTypes;

        public Middleware() { }

        public Middleware(string id, string name)
        {
            Id = id;
            Name = name;
            EnvironmentConfiguration = new Dictionary<string, object>();
            _allowedInboxTypes = new List<Type>();
            _allowedOutboxTypes = new List<Type>();
            RefernceId = Guid.NewGuid();
            Status = MiddlewareStatus.Created;
        }

        public virtual void AddConfiguration(string key, object value, Func<string, object, object> updateCallback)
        {
            if (EnvironmentConfiguration.ContainsKey(key))
            {
                var updatedValue = updateCallback(key, EnvironmentConfiguration[key]);
                EnvironmentConfiguration[key] = updatedValue;
            }
            else
                EnvironmentConfiguration.Add(key, value);
        }

        public virtual void SetInbox(Mailbox inbox)
        {
            Inbox = inbox;
        }

        public virtual void SetOutbox(Mailbox outbox)
        {
            Outbox = outbox;
        }

        public virtual async Task RunAsync()
        {   
            await Validate();
            await Inbox.StartListeningToIncomingMessages();
            Inbox.NewMessageReceived += OnMessageReceived;
            Status = MiddlewareStatus.Running;
        }

        protected virtual void OnMessageReceived(object sender, BaseMessage e)
        {
            var request = e as MiddlewareRequest;
            if (request == null)
            {   
                var exception = new Exception("Bad Message: Message needs to be Middleware Request");
                var middlewareException = new MiddlewareException(exception)
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    ErrorMessages = new List<string>() { exception.Message },
                    MiddlewareId = this.Id,
                    PipelineId = EnvironmentConfiguration.ContainsKey("PipelineId") 
                        ? EnvironmentConfiguration["PipelineId"].ToString() : string.Empty,
                    TimeTaken = TimeSpan.Zero,
                    ExecutionId = this.ExecutionId,
                    Message = exception.ToString()
                };
                AbortAsync(middlewareException).Wait();
            }   

            ExecuteAsync(request);
        }

        protected abstract Task ExecuteAsync(MiddlewareRequest request);

        protected virtual async Task CompleteAsync(MiddlewareResponse response)
        {
            await Outbox.SendMessage(response);
        }

        public virtual async Task AbortAsync(MiddlewareException exception)
        {
            await Outbox.SendMessage(exception);
        }

        public abstract Task<string> CreateImage();

        protected virtual async Task<IEnumerable<string>> Validate()
        {
            var errors = new List<string>();
            await Task.Run(() =>
            {
                if (Inbox == null)
                    errors.Add("No inbox for the Middleware.");

                if (Outbox == null)
                    errors.Add("No outbox specified for the Middleware.");

                if (!ValidateInboxTypes())
                    errors.Add($"Inbox of type {Inbox.Name} is not supported by this Middleware.");

                if (!ValidateOutboxTypes())
                    errors.Add($"Outbox of type {Outbox.Name} is not supported by this Middleware.");

                if (Timeout == TimeSpan.Zero)
                    errors.Add("Middleware cannot have zero timespan");
            });
            return errors;
        }

        private bool ValidateInboxTypes()
        {
            var currentInboxType = Inbox.GetType();
            return _allowedInboxTypes == null || _allowedInboxTypes.Contains(currentInboxType);
        }

        private bool ValidateOutboxTypes()
        {
            var currentOutboxType = Outbox.GetType();
            return _allowedOutboxTypes == null || _allowedOutboxTypes.Contains(currentOutboxType);
        }

        public void Dispose()
        {
            //TODO
            //Inbox.Dispose();
            //Outbox.Dispose();
        }
    }
}