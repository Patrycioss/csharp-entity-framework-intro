using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            app.MapGet("/books/{id:int}", GetBook);
            app.MapGet("/books", GetBooks);
            app.MapPut("/books/update/", UpdateBook);
            app.MapDelete("/books/{id:int}", DeleteBook);
            app.MapPost("/books/create/", CreateBook);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetBook(IBookRepository bookRepository, int id)
        {
            var book = await bookRepository.GetBook(id);
            if (book == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(
                BookToPost(book)
            );
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = (await bookRepository.GetAllBooks()).ToArray();

            if (books.Length == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(
                books.Select(BookToPost)
            );
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> UpdateBook(IBookRepository bookRepository, BookUpdatePut bookUpdatePut)
        {
            var foundBook = await bookRepository.GetBook(bookUpdatePut.Id);
            if (foundBook == null)
            {
                return Results.NotFound();
            }

            if (bookUpdatePut.Title != null) foundBook.Title = bookUpdatePut.Title;
            if (bookUpdatePut.AuthorId != null) foundBook.AuthorId = bookUpdatePut.AuthorId.Value;

            return Results.Created($"/books/update/",await bookRepository.UpdateBook(foundBook));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteBook(IBookRepository bookRepository, int id)
        {
            var deleted = await bookRepository.DeleteBook(id);
            return deleted ? Results.Ok() : Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateBook(IBookRepository bookRepository, IAuthorRepository authorRepository, BookCreatePut bookCreatePut)
        {
            var book = new Book
            {
                Title = bookCreatePut.Title,
                AuthorId = bookCreatePut.AuthorId
            };

            var author =  await authorRepository.GetAuthor(bookCreatePut.AuthorId);

            if (author == null)
            {
                return Results.NotFound("AuthorID is not valid!");
            }

            book.Author = author;
            await bookRepository.CreateBook(book);
            return Results.Ok(BookToPost(book));
        }
        
        private static BookPost BookToPost(Book book)
        {
            return new BookPost
            {
                Title = book.Title,
                BookAuthor = new BookAuthorPost
                {
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                    Email = book.Author.Email,
                },
            };
        }
    }
}