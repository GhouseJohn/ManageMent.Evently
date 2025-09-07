
using MediatR;

namespace BuidingBlock.Application.Messaging;

public interface ICommand : ICommand<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
