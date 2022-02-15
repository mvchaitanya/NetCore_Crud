namespace BookClub2.Models
{
    public class BookRank : IComparable<BookRank>
    {
        public Book Book { get; set; }
        public double AvgGrade => (double)GradeSum / (double)GradesNumber;

        public int GradesNumber { get; set; } = 0;

        public int GradeSum { get; set; } = 0;

        public BookRank(Book book)
        {
            this.Book = book;
        }

        public string DisplayAvgGrade()
        {
            if(GradesNumber == 0)
            {
                return "no grades";
            }
            return Math.Round(AvgGrade, 2).ToString();
        }

        public int CompareTo(BookRank? other)
        {
            if (this.GradesNumber == 0 && other.GradesNumber == 0)
                return 0;
            if(this.GradesNumber == 0)
                return 1;
            if(other.GradesNumber == 0)
                return -1;

            return other.AvgGrade.CompareTo(this.AvgGrade);
        }
    }
}
