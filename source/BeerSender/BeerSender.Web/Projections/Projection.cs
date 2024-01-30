using BeerSender.Domain;

namespace BeerSender.Web.Projections;

public interface Projection
{
    int Batch_size { get; }
    TimeSpan Wait_time { get; }
    Type[] Event_types { get; }
    void Handle_batch(IEnumerable<Event_message> batch);
}