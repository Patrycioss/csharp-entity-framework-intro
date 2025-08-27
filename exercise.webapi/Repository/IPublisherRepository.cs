using exercise.webapi.Models;

namespace exercise.webapi.Repository;

public interface IPublisherRepository
{
    public Task<Publisher?> GetPublisher(int id);
    public Task<IEnumerable<Publisher>> GetAllPublishers();
}