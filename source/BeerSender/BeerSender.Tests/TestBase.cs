using BeerSender.Domain;
using FluentAssertions;

namespace BeerSender.Tests;

public class TestBase
{
    private readonly Guid _aggregate_id = Guid.NewGuid();
    private readonly List<Event_message> _event_stream = new();
    private readonly List<Event_message> _new_events = new();

    protected void Given(params Event[] past_events)
    {
        foreach (var @event in past_events)
        {
            _event_stream.Add(new Event_message(
                _aggregate_id,
                _event_stream.Count + 1,
                @event));
        }
    }

    protected void When(Command command)
    {
        var router = new Command_router(_ => _event_stream,
            _new_events.Add);

        router.Handle(new Command_message(_aggregate_id, command));
    }

    protected void Expect(params object[] expected_events)
    {
        _new_events.Count
            .Should().Be(expected_events.Length);

        for (var i = 0; i < _new_events.Count; i++)
        {
            var new_event = _new_events[i].Event;
            var expected_event = expected_events[i];

            new_event.GetType()
                .Should().Be(expected_event.GetType());

            try
            {
                new_event
                    .Should().BeEquivalentTo(expected_event);
            }
            catch (InvalidOperationException ex)
            {
                if (!ex.Message.StartsWith("No members were found for comparison."))
                    throw;
            }
        }
    }
}