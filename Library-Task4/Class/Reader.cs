namespace Library_Task4.Class

{
    public class Reader
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Book> BorrowedBooks { get; }

        public Reader(string name, int age)
        {
            Name = name;
            Age = age;
            BorrowedBooks = new List<Book>();
        }


        public bool IsBookBorrowed(string isbn)
        {
            foreach (var book in BorrowedBooks)
            {
                if (book.ISBN == isbn)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
