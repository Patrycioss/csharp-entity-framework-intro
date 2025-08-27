| Signature                        | Purpose                          | Outputs                                                                                  |
|----------------------------------|----------------------------------|------------------------------------------------------------------------------------------|
| GetBook(int id)                  | get a book and its author        | returns a book and its author or notfound                                                |
| GetAllBooks()                    | get all books and their authors  | a list of books and their authors                                                        |
| UpdateBook(int id, BookPut book) | update a book and return it      | a book or not found when id is wrong                                                     |
| DeleteBook(int id)               | deletes a book                   | returns a bool whether the book could be deleted                                         |
| CreateBook(BookPut book)         | creates a book                   | returns NotFound when AuthorId is not valid and BadRequest when book object is not valid |
| GetAuthor(int id)                | get an author and their books    | returns the author and a list of their books or notfound                                 |
| GetAllAuthors()                  | gets all authors and their books | returns a list of authors and their books                                                |