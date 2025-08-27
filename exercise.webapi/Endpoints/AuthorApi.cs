using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            app.MapGet("/authors/{id:int}", GetAuthor);
            app.MapGet("/authors", GetAuthors);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAuthor(IAuthorRepository authorRepository, IBookRepository bookRepository,
            int id)
        {
            var author = await authorRepository.GetAuthor(id);
            if (author == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(
                new AuthorPost
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Email = author.Email,
                    Books = _findCorrespondingBooks(author, bookRepository),
                }
            );
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAuthors(IAuthorRepository authorRepository,
            IBookRepository bookRepository)
        {
            var authors = (await authorRepository.GetAllAuthors()).ToArray();

            if (authors.Length == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(
                authors.Select(author => new AuthorPost
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Email = author.Email,
                    Books = _findCorrespondingBooks(author, bookRepository),
                })
            );
        }

        private static List<string> _findCorrespondingBooks(Author author, IBookRepository bookRepository)
        {
            var allBooks = bookRepository.GetAllBooks().Result;
            return allBooks.Where(book => book.AuthorId == author.Id)
                .Select(book => book.Title).ToList();
        }
    }
}