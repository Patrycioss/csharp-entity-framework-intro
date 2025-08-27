using exercise.webapi.Models;

namespace exercise.webapi.Data
{
    public class Seeder
    {
        private List<string> _firstNames =
        [
            "Audrey",
            "Donald",
            "Elvis",
            "Barack",
            "Oprah",
            "Jimi",
            "Mick",
            "Kate",
            "Charles",
            "Kate"
        ];

        private readonly List<string> _lastNames =
        [
            "Hepburn",
            "Trump",
            "Presley",
            "Obama",
            "Winfrey",
            "Hendrix",
            "Jagger",
            "Winslet",
            "Windsor",
            "Middleton"
        ];

        private readonly List<string> _domain =
        [
            "bbc.co.uk",
            "google.com",
            "theworld.ca",
            "something.com",
            "tesla.com",
            "nasa.org.us",
            "gov.us",
            "gov.gr",
            "gov.nl",
            "gov.ru"
        ];

        private readonly List<string> _firstWord =
        [
            "The",
            "Two",
            "Several",
            "Fifteen",
            "A bunch of",
            "An army of",
            "A herd of"
        ];

        private readonly List<string> _secondWord =
        [
            "Orange",
            "Purple",
            "Large",
            "Microscopic",
            "Green",
            "Transparent",
            "Rose Smelling",
            "Bitter"
        ];

        private readonly List<string> _thirdWord =
        [
            "Buildings",
            "Cars",
            "Planets",
            "Houses",
            "Flowers",
            "Leopards"
        ];

        private readonly List<Publisher> _publishers = [];
        private readonly List<Author> _authors = [];
        private readonly List<Book> _books = [];

        public Seeder()
        {
            var authorRandom = new Random();
            var bookRandom = new Random();
            var publisherRandom = new Random();

            _publishers.AddRange([
                new Publisher { Id = 1, Name = "Big Ben Publishing" },
                new Publisher { Id = 2, Name = "Peters Publishing Party" },
                new Publisher { Id = 3, Name = "Publish Proudly" },
            ]);


            for (int x = 1; x < 250; x++)
            {
                var author = new Author();
                author.Id = x;
                author.FirstName = _firstNames[authorRandom.Next(_firstNames.Count)];
                author.LastName = _lastNames[authorRandom.Next(_lastNames.Count)];
                author.Email = $"{author.FirstName}.{author.LastName}@{_domain[authorRandom.Next(_domain.Count)]}"
                    .ToLower();
                _authors.Add(author);
            }


            for (int y = 1; y < 250; y++)
            {
                Book book = new Book();
                book.Id = y;
                book.Title =
                    $"{_firstWord[bookRandom.Next(_firstWord.Count)]} {_secondWord[bookRandom.Next(_secondWord.Count)]} {_thirdWord[bookRandom.Next(_thirdWord.Count)]}";
                book.AuthorId = _authors[authorRandom.Next(_authors.Count)].Id;
                book.PublisherId = _publishers[publisherRandom.Next(_publishers.Count)].Id;
                //book.Author = authors[book.AuthorId-1];
                _books.Add(book);
            }
        }

        public List<Author> Authors
        {
            get { return _authors; }
        }

        public List<Book> Books
        {
            get { return _books; }
        }

        public List<Publisher> Publishers
        {
            get { return _publishers; }
        }
    }
}