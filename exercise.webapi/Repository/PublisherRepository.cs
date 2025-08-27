using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly DataContext _db;

    public PublisherRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<Publisher?> GetPublisher(int id)
    {
        return await _db.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == id);
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishers()
    {
        return await _db.Publishers.ToListAsync();
    }
}