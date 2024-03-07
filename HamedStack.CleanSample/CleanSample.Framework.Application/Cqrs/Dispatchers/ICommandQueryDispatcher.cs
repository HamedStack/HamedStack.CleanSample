using MediatR;

namespace CleanSample.Framework.Application.Cqrs.Dispatchers;

public interface ICommandQueryDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest;
    Task<object?> Send(object request, CancellationToken cancellationToken = default);

    Task Publish(object notification, CancellationToken cancellationToken = default);
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}