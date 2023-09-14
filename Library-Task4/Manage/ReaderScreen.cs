using Library_Task4.Class;
using System;

namespace Library_Task4.Manage
{
    public class ReaderScreen
    {
        private Library library;

        public ReaderScreen(Library library)
        {
            this.library = library;
        }

        public void ShowReadersManagementMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Readers Management Menu:");
            Console.WriteLine("1. Show Readers Catalog");
            Console.WriteLine("2. Add Reader");
            Console.WriteLine("3. Remove Reader");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Show Available Books");
            Console.WriteLine("7. Go Back to Main Menu");
            Console.Write("Please enter your choice: ");
        }

        public void Run()
        {
            int choice;
            do
            {
                ShowReadersManagementMenu();
                choice = GetUserChoice(7);

                switch (choice)
                {
                    case 1:
                        ShowReadersCatalog(library.GetReaders());
                        break;
                    case 2:
                        AddReader();
                        break;
                    case 3:
                        RemoveReader();
                        break;
                    case 4:
                        BorrowBook();
                        break;
                    case 5:
                        ReturnBook();
                        break;
                    case 6:
                        ShowAvailableBooks();
                        break;
                    case 7:
                        Console.WriteLine("Going back to Main Menu.");
                        break;
                    default:
                        ShowErrorMessage("Invalid choice. Please select again.");
                        break;
                }

                PauseExecution();

            } while (choice != 7);
        }

        public void ShowReadersCatalog(IEnumerable<Reader> readers)
        {
            Console.WriteLine("Readers Catalog:");
            foreach (var reader in readers)
            {
                if (reader != null)
                {
                    Console.WriteLine($"Reader: {reader.Name} | {reader.Age} y.o");
                    Console.WriteLine("Borrowed Books:");
                    foreach (var book in reader.BorrowedBooks)
                    {
                        if (book != null)
                        {
                            string availability = book.IsAvailable ? "Available" : "Borrowed";
                            Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN}), {availability}");
                        }
                    }
                }
            }
        }

        public void AddReader()
        {
            Console.WriteLine("Enter reader details:");
            Reader reader = GetReaderFromUser();
            library.AddReader(reader);
            ShowSuccessMessage("Reader successfully added to the library.");
        }

        public void RemoveReader()
        {
            Console.WriteLine("Enter the name of the reader to remove:");
            string readerName = Console.ReadLine();

            Reader readerToRemove = library.GetReaders().FirstOrDefault(reader => reader.Name == readerName);

            if (readerToRemove != null)
            {
                library.RemoveReader(readerToRemove);
                ShowSuccessMessage($"Reader \"{readerToRemove.Name}\" successfully removed from the library.");
            }
            else
            {
                ShowErrorMessage($"No reader with name \"{readerName}\" found in the library.");
            }
        }

        public void BorrowBook()
        {
            Console.WriteLine("Select a book to borrow from the available books:");
            ShowAvailableBooks();
            Book bookToBorrow = GetBookFromUser();

            if (bookToBorrow != null && bookToBorrow.IsAvailable)
            {
                Reader borrower = GetReaderFromUser();
                library.BorrowBook(borrower, bookToBorrow);
                ShowSuccessMessage($"Book \"{bookToBorrow.Title}\" successfully borrowed by {borrower.Name}.");
            }
            else
            {
                ShowErrorMessage("Invalid book selection or the book is not available.");
            }
        }


        public void ReturnBook()
        {
            Console.WriteLine("Select a reader from the list:");
            ShowReadersCatalog(library.GetReaders());
            Reader reader = GetReaderFromUser();

            Console.WriteLine("Select a book from the list:");
            ShowBooksCatalog(library.GetBorrowedBooksForReader(reader));
            Book book = GetBookFromUser();

            if (library.IsBookBorrowed(book.ISBN))
            {
                library.ReturnBook(reader, book);
                ShowSuccessMessage($"Book \"{book.Title}\" successfully returned by {reader.Name}.");
            }
            else
            {
                ShowErrorMessage($"Book with ISBN {book.ISBN} is not borrowed by {reader.Name}.");
            }
        }


        public void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public int GetUserChoice(int maxChoice)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxChoice)
            {
                ShowErrorMessage("Invalid choice. Please select again.");
            }
            return choice;
        }

        public Reader GetReaderFromUser()
        {
            Console.Write("Enter reader name: ");
            string name = Console.ReadLine();

            Console.Write("Enter reader age: ");
            int age = int.Parse(Console.ReadLine());

            return new Reader(name, age);
        }


        public Book GetBookFromUser()
        {
            Console.WriteLine("Enter book title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter book author:");
            string author = Console.ReadLine();
            Console.WriteLine("Enter book ISBN:");
            string isbn = Console.ReadLine();

            // By default, a new book is assumed to be available for borrowing
            return new Book(title, author, isbn, true);
        }



        public void PauseExecution()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void ShowAvailableBooks()
        {
            Console.WriteLine("Available Books:");
            foreach (var book in library.GetAvailableBooks())
            {
                // Check for null values and display book information
                if (book != null)
                {
                    Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN})");
                }
            }
        }


        public void ShowBooksCatalog(IEnumerable<Book> books)
        {
            Console.WriteLine("Books Catalog:");
            foreach (var book in books)
            {
                // Check for null values and display book information
                if (book != null)
                {
                    Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN})");
                }
            }
        }

    }
}
