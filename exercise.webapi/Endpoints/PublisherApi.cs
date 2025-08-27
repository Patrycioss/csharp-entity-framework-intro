using exercise.webapi.DTOs;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.webapi.Endpoints
{
    public static class PublisherApi
    {
        public static void ConfigurePublisherApi(this WebApplication app)
        {
            app.MapGet("/publishers/{id:int}", GetPublisher);
            app.MapGet("/publishers", GetPublishers);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPublisher(IPublisherRepository publisherRepository,
            IBookRepository bookRepository,
            int id)
        {
            var publisher = await publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return Results.NotFound();
            }


            return Results.Ok(
                new PublisherPost
                {
                    Name = publisher.Name,
                    Books = _findCorrespondingBooks(publisher, bookRepository),
                }
            );
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPublishers(IPublisherRepository publisherRepository,
            IBookRepository bookRepository)
        {
            var publishers = (await publisherRepository.GetAllPublishers()).ToArray();

            if (publishers.Length == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(
                publishers.Select(publisher => new PublisherPost
                {
                    Name = publisher.Name,
                    Books = _findCorrespondingBooks(publisher, bookRepository),
                })
            );
        }

        private static List<BookPost> _findCorrespondingBooks(Publisher publisher,
            IBookRepository bookRepository)
        {
            var allBooks = bookRepository.GetAllBooks().Result;
            return allBooks.Where(book => book.PublisherId == publisher.Id)
                .Select<Book, BookPost>(book => new BookPost
                {
                    Title = book.Title,
                    BookAuthor = new BookAuthorPost
                    {
                        FirstName = book.Author.FirstName,
                        LastName = book.Author.LastName,
                        Email = book.Author.Email,
                    },
                }).ToList();
        }
    }
}