using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipelineFramework.Interfaces.Communication;

namespace PipelineFramework.Interfaces
{
    public abstract class Middleware
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Inbox Inbox { get; set; }
        public Outbox Outbox { get; set; }
        public Guid ExecutionId { get; set; }
        public TimeSpan Timeout { get; set; }
        public IDictionary<string, object> EnvironmentConfiguration { get; set; }

        protected List<Type> _allowedInboxTypes;
        protected List<Type> _allowedOutboxTypes;

        protected ITriggerResolver _triggerResolver;

        public Middleware(string id, string name)
        {
            Id = id;
            Name = name;
            EnvironmentConfiguration = new Dictionary<string, object>();
            _allowedInboxTypes = new List<Type>();
            _allowedOutboxTypes = new List<Type>();
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

        public virtual void SetInboundRule(Inbox inbox)
        {
            Inbox = inbox;
        }

        public virtual void SetOutboundRule(Outbox outbox)
        {
            Outbox = outbox;
        }

        public virtual async Task<IEnumerable<string>> Validate()
        {
            var errors = new List<string>();
            await Task.Run(() =>
            {
                if (Inbox == null)
                    errors.Add("No inbound rule specified for the Middleware.");

                if (Outbox == null)
                    errors.Add("No outbound rule specified for the Middleware.");

                if (!ValidateInboxTypes())
                    errors.Add($"Inbox of type {Inbox.RuleName} is not supported by this Middleware.");

                if (!ValidateOutboxTypes())
                    errors.Add($"Outbox of type {Outbox.RuleName} is not supported by this Middleware.");

                if (Timeout == TimeSpan.Zero)
                    errors.Add("Middleware cannot have zero timespan");
            });
            return errors;
        }

        private bool ValidateInboxTypes()
        {
            var currentInboxType = Inbox.GetType();
            return _allowedInboxTypes.Contains(currentInboxType);
        }

        private bool ValidateOutboxTypes()
        {
            var currentOutboxType = Outbox.GetType();
            return _allowedOutboxTypes.Contains(currentOutboxType);
        }
    }
}