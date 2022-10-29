using MediatR;

namespace Poq.ProductService.Application.Interfaces;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
