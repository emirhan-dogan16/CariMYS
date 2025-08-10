using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CQRS.Request;
using Core.CQRS.RequestHandler;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CQRS
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
    }

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _provider;

        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            var handler = _provider.GetService(handlerType);
            if (handler == null)
                throw new InvalidOperationException($"Handler bulunamadı: {handlerType.Name}");

            var method = handler.GetType().GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException($"Handle metodu bulunamadı: {handler.GetType().Name}");

            return await (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken });
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            var handlers = _provider.GetServices<INotificationHandler<TNotification>>();

            foreach (var handler in handlers)
            {
                await handler.Handle(notification, cancellationToken);
            }
        }
    }

}
