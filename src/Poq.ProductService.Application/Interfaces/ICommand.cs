using MediatR;

namespace Poq.ProductService.Application.Interfaces;

public interface ICommand<out TResult> : IRequest<TResult>
{
}
