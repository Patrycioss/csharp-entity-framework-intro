using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _db;

    public AuthorRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<Author?> GetAuthor(int id)
    {
        return await _db.Authors
            .FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _db.Authors.ToListAsync();
    }
}