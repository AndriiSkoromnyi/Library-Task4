using Library_Task4.Class;

namespace LibraryManagementSystem
{
    public interface ILibrary
    {
        // Method to start the library UI
        void Run();

        // Method to display the main menu
        void ShowMainMenu();

        // Method to display the list of available books
        void ShowBooksCatalog(IEnumerable<Book> books);

        // Method to display the list of readers and their borrowed books
        void ShowReadersCatalog(IEnumerable<Reader> readers);
        
        // Method to get the user's choice from the main menu
        int GetUserChoice(int maxChoice);

    }
}
