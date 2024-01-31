using BeerSender.Domain;
using Microsoft.AspNetCore.SignalR;

namespace BeerSender.Web.Hubs;

public class Event_publish_hub : Hub
{
    public async Task publish_event(Guid aggregate_id, Event @event)
    {
        await Clients.Group(aggregate_id.ToString())
            .SendAsync("publish_event", aggregate_id, @event);
    }

    public async Task subscribe_to_aggregate(Guid aggregate_id)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, aggregate_id.ToString());
    }
}

