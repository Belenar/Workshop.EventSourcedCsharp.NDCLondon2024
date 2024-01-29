using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using FluentAssertions;

namespace BeerSender.Tests
{
    public class TestBase
    {
        private readonly Guid _aggregate_id = Guid.NewGuid();
        private List<Event_message> _event_stream = new();
        private List<Event_message> _new_events = new();

        protected void Given(params object[] past_events)
        {
            foreach (var @event in past_events)
            {
                _event_stream.Add(new Event_message(
                    _aggregate_id,
                    _event_stream.Count() + 1,
                    @event));
            }
        }

        protected void When(object command)
        {
            var router = new Command_router(_ => _event_stream,
                _new_events.Add);

            router.Handle(new Command_message(_aggregate_id, command));
        }

        protected void Expect(params object[] expected_events)
        {
            // Compare length of both lists
            // iterate
            // compare types
            // check equivalence
            _new_events.Select(msg => msg.Event)
                .Should()
                .BeEquivalentTo(expected_events);
        }
    }

    public class Box_test : TestBase
    {
        protected Get_box Create_box_with_desired_capacity(int desired_capacity)
        {
            return new Get_box(desired_capacity);
        }
    }

    public class Get_box_test : Box_test
    {
        [Fact]
        public void Test1()
        {
            // list of events
            Given();
            // Command
            When(
                Create_box_with_desired_capacity(17));
            // New events
            Expect(new Box_created(new Capacity(23)));
        }
    }
}