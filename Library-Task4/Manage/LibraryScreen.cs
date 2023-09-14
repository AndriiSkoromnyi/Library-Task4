using Library_Task4.Class;
using LibraryManagementSystem;
using Newtonsoft.Json;

namespace Library_Task4.Manage
{

    public class LibraryScreen
    {
        private Library library;
        private ILibrary consoleUI;

        public LibraryScreen(Library library, ILibrary consoleUI)
        {
            this.library = library;
            this.consoleUI = consoleUI;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Save Library to JSON");
            Console.WriteLine("2. Load Library from JSON");
            Console.WriteLine("3. Go to Main Menu");
            Console.Write("Please enter your choice: ");
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
                        SaveLibraryToJson();
                        break;
                    case 2:
                        LoadLibraryFromJson();
                        break;
                    case 3:
                        Console.WriteLine("Going to Main Menu.");
                        break;
                    default:
                        ShowErrorMessage("Invalid choice. Please select again.");
                        break;
                }

                PauseExecution();

            } while (choice != 3);
        }

        public void SaveLibraryToJson()
        {
            try
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(directoryPath, "library.json");

                // Create a LibraryData object to store the library data
                var libraryData = new
                {
                    Books = library.GetBooks().ToList(),
                    Readers = library.GetReaders().ToList(),
                    BorrowedBooks = library.GetBorrowedBooks().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList())
                };
                string json = JsonConvert.SerializeObject(libraryData, Formatting.Indented);
                File.WriteAllText(filePath, json);
                ShowSuccessMessage($"Library data successfully saved to {filePath}.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error occurred while saving library data: {ex.Message}");
            }
        }

        public void LoadLibraryFromJson()
        {
            try
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(directoryPath, "library.json");

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var libraryData = JsonConvert.DeserializeObject<Library>(json);

                    // Update the current library data with the loaded data
                    if (libraryData != null)
                    {
                        library.Books = libraryData.Books ?? new List<Book>();
                        library.Readers = libraryData.Readers ?? new List<Reader>();
                        library.BorrowedBooks = libraryData.BorrowedBooks ?? new Dictionary<Reader, List<Book>>();

                        ShowSuccessMessage($"Library data successfully loaded from {filePath}.");
                    }
                }
                else
                {
                    ShowErrorMessage("library.json file not found.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error occurred while loading library data: {ex.Message}");
            }
        }

        public void PauseExecution()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
    }
}
