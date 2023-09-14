using System.Text.Json;

namespace Library_Task4.Class
{
    public class Library
    {
        public List<Book> Books;
        public List<Reader> Readers;
        public Dictionary<Reader, List<Book>> BorrowedBooks;

        public Library()
        {
            Books = new List<Book>();
            Readers = new List<Reader>();
            BorrowedBooks = new Dictionary<Reader, List<Book>>();
        }

        public bool IsBookBorrowed(string isbn)
        {
            foreach (var kvp in BorrowedBooks)
            {
                foreach (var book in kvp.Value)
                {
                    if (book.ISBN == isbn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Get a list of all available books in the library
        public IEnumerable<Book> GetAvailableBooks()
        {
            return Books.FindAll(book => book.IsAvailable && !IsBookBorrowed(book.ISBN));
        }

        // Get a list of all borrowed books in the library
        public IReadOnlyDictionary<Reader, List<Book>> GetBorrowedBooks()
        {
            return BorrowedBooks;
        }

        // Get a list of borrowed books for a specific reader
        public List<Book> GetBorrowedBooksForReader(Reader reader)
        {
            if (BorrowedBooks.ContainsKey(reader))
            {
                return BorrowedBooks[reader];
            }
            return new List<Book>();
        }

        // Add book to the library
        public void AddBook(Book book)
        {
            book.SetAvailability(true);  // Set the availability to true when adding a book
            Books.Add(book);
        }

        // Remove book from the library
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        // Get a list of all books in the library
        public IReadOnlyList<Book> GetBooks()
        {
            return Books;
        }

        // Add reader to the library
        public void AddReader(Reader reader)
        {
            Readers.Add(reader);
        }

        // Remove reader from the library
        public void RemoveReader(Reader reader)
        {
            Readers.Remove(reader);
        }

        // Get a list of all readers in the library
        public IReadOnlyList<Reader> GetReaders()
        {
            return Readers;
        }

        public void BorrowBook(Reader reader, Book book)
        {
            if (!BorrowedBooks.ContainsKey(reader))
            {
                BorrowedBooks[reader] = new List<Book>();
            }

            BorrowedBooks[reader].Add(book);
            book.SetAvailability(false);
        }

        public void ReturnBook(Reader reader, Book book)
        {
            if (BorrowedBooks.ContainsKey(reader) && BorrowedBooks[reader].Contains(book))
            {
                BorrowedBooks[reader].Remove(book);
                book.SetAvailability(true);
            }
        }

        public void SaveToJson(string filePath)
        {
            var data = new LibraryData
            {
                Books = Books,
                Readers = Readers,
                BorrowedBooks = BorrowedBooks
            };

            string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }


        // Load library data from JSON file using System.Text.Json
        public static Library LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // Return a new empty Library if the file doesn't exist
                return new Library();
            }

            string jsonData = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<LibraryData>(jsonData);

            var library = new Library();

            library.Books = data.Books ?? new List<Book>();
            library.Readers = data.Readers ?? new List<Reader>();
            library.BorrowedBooks = data.BorrowedBooks ?? new Dictionary<Reader, List<Book>>();

            return library;
        }

        // Helper class for serializing and deserializing library data
        private class LibraryData
        {
            public List<Book> Books { get; set; }
            public List<Reader> Readers { get; set; }
            public Dictionary<Reader, List<Book>> BorrowedBooks { get; set; }
        }
    }

}
