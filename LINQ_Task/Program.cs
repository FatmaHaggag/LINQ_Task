using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Display book title and its ISBN

            var query1 = SampleData.Books.Select(b => new { b.Title, b.Isbn });

            foreach (var book in query1)
            {
                Console.WriteLine($"Title: {book.Title}, ISBN: {book.Isbn}");
            }

            //2. Display the first 3 books with price more than 25

            var query2 = SampleData.Books.Where(b => b.Price > 25).Take(3);

            foreach (var book in query2)
            {
                Console.WriteLine($"Title: {book.Title}, Price: {book.Price}");
            }

            ////3. Display Book title along with its publisher name(Method 1)

            var query31 = SampleData.Books.Select(b => new { b.Title, PublisherName = b.Publisher.Name });

            foreach (var book in query31)
            {
                Console.WriteLine($"Title: {book.Title}, Publisher: {book.PublisherName}");
            }

            ////(Method 2)

            var query32 = from b in SampleData.Books
                          select new { b.Title, PublisherName = b.Publisher.Name };

            foreach (var book in query32)
            {
                Console.WriteLine($"Title: {book.Title}, Publisher: {book.PublisherName}");
            }

            ////4. Find the number of books which cost more than 20

            int query4 = SampleData.Books.Count(b => b.Price > 20);
            Console.WriteLine($"Number of books with price > 20: {query4}");

            ////5. Display book title, price, and subject name sorted by its
            ////subject name ascending and by its price descending

            var query5 = SampleData.Books
                .OrderBy(b => b.Subject.Name)
                .ThenByDescending(b => b.Price)
                .Select(b => new { b.Title, b.Price, SubjectName = b.Subject.Name });

            foreach (var book in query5)
            {
                Console.WriteLine($"Title: {book.Title}, Price: {book.Price}, Subject: {book.SubjectName}");
            }

            ////6. Display All subjects with books related to this subject

            var query6 = from s in SampleData.Subjects
                         select new
                         {
                             SubjectName = s.Name,
                             Books = SampleData.Books.Where(b => b.Subject == s)
                         };

            foreach (var subject in query6)
            {
                Console.WriteLine($"Subject: {subject.SubjectName}");
                foreach (var book in subject.Books)
                {
                    Console.WriteLine($"\tBook Title: {book.Title}");
                }
            }

            ////7. Try to display book title & price (from book objects) returned from GetBooks Function

            var getBooks = SampleData.GetBooks().Cast<Book>();
            var query7 = getBooks.Select(b => new { b.Title, b.Price });

            foreach (var book in query7)
            {
                Console.WriteLine($"Title: {book.Title}, Price: {book.Price}");
            }

            ////8. Display books grouped by publisher & subject

            var query8 = SampleData.Books
                .GroupBy(b => new { b.Publisher, b.Subject })
                .Select(g => new
                {
                    PublisherName = g.Key.Publisher.Name,
                    SubjectName = g.Key.Subject.Name,
                    Books = g
                });

            foreach (var books in query8)
            {
                Console.WriteLine($"Publisher: {books.PublisherName}, Subject: {books.SubjectName}");
                foreach (var book in books.Books)
                {
                    Console.WriteLine($"\tBook Title: {book.Title}");
                }
            }

            Console.ReadKey();
        }

    }
}
