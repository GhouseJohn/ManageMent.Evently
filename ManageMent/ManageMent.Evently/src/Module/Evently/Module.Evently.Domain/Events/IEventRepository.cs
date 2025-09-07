namespace Module.Evently.Domain.Events;
public interface IEventRepository
{
    void Insert(ModelEvent @event);
}
