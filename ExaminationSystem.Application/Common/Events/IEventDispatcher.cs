namespace ExaminationSystem.Application.Common.Events;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}
