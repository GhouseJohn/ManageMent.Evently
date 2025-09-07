﻿using MediatR;

namespace BuidingBlock.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
