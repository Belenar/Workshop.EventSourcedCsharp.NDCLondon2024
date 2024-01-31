using BeerSender.Domain;
using BeerSender.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BeerSender.Web.Event_stream;

public class Event_service(
    Event_context event_context, 
    IHubContext<Event_publish_hub> hub_context)
{
    private List<Event_message> _messages = new ();

    public IEnumerable<Event_message> Get_events(Guid aggregate_id)
    {
        return event_context.Events
            .Where(e => e.Aggregate_id == aggregate_id)
            .OrderBy(e => e.Sequence_nr)
            .Select(e => new Event_message(e.Aggregate_id, e.Sequence_nr, e.Event));
    }

    public void AddEvent(Event_message @event)
    {
        event_context.Events.Add(
            new Event_model
            {
                Aggregate_id = @event.Aggregate_id,
                Sequence_nr = @event.Sequence,
                Event = @event.Event,
                Timestamp = DateTime.UtcNow
            });
        _messages.Add(@event);
    }

    public void Commit()
    {
        event_context.SaveChanges();

        foreach (var event_message in _messages)
        {

            hub_context.Clients.Groups(event_message.Aggregate_id.ToString())
                .SendAsync("publish_event", event_message.Aggregate_id, event_message.Event);
        }
    }
}