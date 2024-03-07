using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Dispatchers;

public class CommandQueryDispatcher : ICommandQueryDispatcher
{
    private readonly IMediator _mediator;

    public CommandQueryDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(request, cancellationToken);
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        return _mediator.Send(request, cancellationToken);
    }

    public Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(request, cancellationToken);
    }

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        return _mediator.Publish(notification, cancellationToken);

    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        return _mediator.Publish(notification, cancellationToken);

    }
}