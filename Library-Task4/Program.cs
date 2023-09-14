using LibraryManagementSystem;
using Library_Task4.Class;

public class Program
{
    static void Main()
    {
        Library library = new Library();
        ILibrary consoleUI = new MainScreen(library);
        consoleUI.Run();


        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
