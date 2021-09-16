using System;

namespace EventBus.Messages.Events
{
    public class IntegrationEvents
    {
        public IntegrationEvents(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }

        public IntegrationEvents()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime CreatedDate { get; }
    }
}
