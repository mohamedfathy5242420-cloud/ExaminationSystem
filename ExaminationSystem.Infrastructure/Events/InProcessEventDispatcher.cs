using ExaminationSystem.Application.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace ExaminationSystem.Infrastructure.Events;

public class InProcessEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InProcessEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(domainEvent, cancellationToken);
        }
    }
}
