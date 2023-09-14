namespace Library_Task4.Class
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }
        public bool IsAvailable { get; private set; }

        public Book(string title, string author, string isbn, bool isAvailable = true)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = isAvailable;
        }

        public void SetAvailability(bool available)
        {
            IsAvailable = available;
        }
    }


}

