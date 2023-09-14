using Library_Task4.Class;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Library_Task4.Manage
{
    public class BookScreen
    {

        private Library library;

        public BookScreen(Library library)
        {
            this.library = library;
        }

        public void ShowBooksManagementMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Books Management Menu:");
            Console.WriteLine("1. Show Books Catalog");
            Console.WriteLine("2. Add Book");
            Console.WriteLine("3. Remove Book");
            Console.WriteLine("4. Show Available Books");
            Console.WriteLine("5. Show Borrowed Books");
            Console.WriteLine("6. Go Back to Main Menu");
            Console.Write("Please enter your choice: ");
        }

        public void Run()
        {
            int choice;
            do
            {
                ShowBooksManagementMenu();
                choice = GetUserChoice(6);

                switch (choice)
                {
                    case 1:
                        ShowBooksCatalog(library.GetBooks());
                        PauseExecution();
                        break;

                    case 2:
                        AddBook();
                        PauseExecution();
                        break;

                    case 3:
                        RemoveBook();
                        PauseExecution();
                        break;

                    case 4:
                        ShowAvailableBooks();
                        PauseExecution();

                        break;
                    case 5:
                        ShowBorrowedBooks();
                        PauseExecution();

                        break;
                    case 6:
                        Console.WriteLine("Going back to Main Menu.");
                        break;

                    default:
                        ShowErrorMessage("Invalid choice. Please select again.");
                        PauseExecution();
                        break;
                }

            } while (choice != 6);
        }

        public void AddBook()
        {
            Console.WriteLine("Enter book details:");
            Book book = GetBookFromUser();
            library.AddBook(book);
            ShowSuccessMessage("Book successfully added to the library.");
        }

        public void RemoveBook()
        {
            Console.WriteLine("Enter the ISBN of the book to remove:");
            string isbn = Console.ReadLine();

            Book bookToRemove = library.GetBooks().FirstOrDefault(book => book.ISBN == isbn);

            if (bookToRemove != null)
            {
                library.RemoveBook(bookToRemove);
                ShowSuccessMessage($"Book \"{bookToRemove.Title}\" successfully removed from the library.");
            }
            else
            {
                ShowErrorMessage($"No book with ISBN \"{isbn}\" found in the library.");
            }
        }


        public void ShowBooksCatalog(IEnumerable<Book> books)
        {
            Console.WriteLine("Books Catalog:");
            foreach (var book in books)
            {
                if (book != null)
                {
                    string isAvailable = book.IsAvailable ? "Yes" : "No";
                    Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN}), Available: {isAvailable}");
                }
            }
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

        public void ShowBorrowedBooks()
        {
            var borrowedBooks = library.GetBorrowedBooks();

            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No books are currently borrowed.");
            }
            else
            {
                Console.WriteLine("Borrowed Books List:");

                foreach (var kvp in borrowedBooks)
                {
                    var reader = kvp.Key;
                    var books = kvp.Value;

                    if (reader != null)
                    {
                        Console.WriteLine($"Reader: {reader.Name} | {reader.Age}");
                        Console.WriteLine("Borrowed Books:");

                        foreach (var book in books)
                        {
                            if (book != null)
                            {
                                Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN})");
                            }
                        }
                    }
                }
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

        public void SaveYourBook(IEnumerable<Book> userBooks)
        {
            // Implement the logic to save the user's book data to a JSON file
            string json = JsonSerializer.Serialize(userBooks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("userBooks.json", json);
            ShowSuccessMessage("Your book data successfully saved to userBooks.json.");
        }

        public void PauseExecution()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
