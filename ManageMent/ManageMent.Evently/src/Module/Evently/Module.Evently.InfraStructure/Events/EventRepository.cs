using Module.Evently.Domain.Events;
using Module.Evently.InfraStructure.Database;

namespace Module.Evently.InfraStructure.Events;
internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public void Insert(ModelEvent @event)
    {
        context.Events.Add(@event);
    }
}
