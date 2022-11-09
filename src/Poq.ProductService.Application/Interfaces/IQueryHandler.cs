﻿using MediatR;

namespace Poq.ProductService.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
