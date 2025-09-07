using BuidingBlock.Application.Messaging;
using BuidingBlock.Domain;
using Module.Evently.Domain.Events;

namespace Module.Evently.Application.Event;

public sealed record CreateEventRequest(string Title, string Description, string Location,
               DateTime StartsAtUtc, DateTime EndsAtUtc) : ICommand<Result<CreateEventResult>>;
public sealed record CreateEventResult(Guid Id);

internal sealed class CreateEventCommand(IEventRepository eventRepository, IUnitOfWork unitOfWork)
                : ICommandHandler<CreateEventRequest, Result<CreateEventResult>>
{
    public async Task<Result<CreateEventResult>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var @event = ModelEvent.Create(
                              request.Title,
                              request.Description,
                              request.Location,
                              request.StartsAtUtc,
                              request.EndsAtUtc);
        eventRepository.Insert(@event);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new CreateEventResult(@event.Id));
    }
}
