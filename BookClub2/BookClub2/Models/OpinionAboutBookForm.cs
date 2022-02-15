namespace BookClub2.Models
{
    public class OpinionAboutBookForm
    {
        public List<Book> books { get; set; }
        public List<Opinion> opinions { get; set; }

        public OpinionAboutBookForm(List<Book> books)
        {
            this.books = books;
        }
    }
}
