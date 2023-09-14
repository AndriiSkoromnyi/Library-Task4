using System.Text.Json;
using LibraryManagementSystem;
using Library_Task4.Class;
using Library_Task4.Manage;

public class MainScreen : ILibrary
{
    private Library library;
    private LibraryScreen libraryManager; 

    public MainScreen(Library library)
    {
        this.library = library;
        this.libraryManager = new LibraryScreen(library, this); 
    }

    public void Run()
    {
        int choice;
        do
        {
            ShowMainMenu();
            choice = GetUserChoice(4);

            switch (choice)
            {
                case 1:
                    BookScreen bookManager = new BookScreen(library);
                    bookManager.Run();
                    break;
                case 2:
                    ReaderScreen readerManager = new ReaderScreen(library);
                    readerManager.Run();
                    break;
                case 3:
                    libraryManager.Run(); 
                    break;
                case 4:
                    Console.WriteLine("Exiting the program.");
                    break;
                default:
                    ShowErrorMessage("Invalid choice. Please select again.");
                    PauseExecution();
                    break;
            }

        } while (choice != 4);
    }


    public void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Books");
            Console.WriteLine("2. Readers");
            Console.WriteLine("3. Library");
            Console.WriteLine("4. Exit");
            Console.Write("Please enter your choice: ");
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


        public void ShowReadersCatalog(IEnumerable<Reader> readers)
        {
            Console.WriteLine("Readers Catalog:");
            foreach (var reader in readers)
            {
                // Check for null values and display reader information
                if (reader != null)
                {
                    Console.WriteLine($"Reader: {reader.Name} | {reader.Age} y.o");
                    Console.WriteLine("Borrowed Books:");
                    foreach (var book in reader.BorrowedBooks)
                    {
                        // Check for null values and display borrowed book information
                        if (book != null)
                        {
                            Console.WriteLine($"{book.Title} - {book.Author} (ISBN: {book.ISBN})");
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

    public int GetUserChoice(int maxChoice)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxChoice)
        {
            ShowErrorMessage("Invalid choice. Please select again.");
        }
        return choice;
    }

    public void PauseExecution()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }   
}

