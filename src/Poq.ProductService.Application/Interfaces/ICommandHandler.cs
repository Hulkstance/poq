using MediatR;

namespace Poq.ProductService.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResult>
    : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}
