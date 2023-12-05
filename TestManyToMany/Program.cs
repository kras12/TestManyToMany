using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace TestManyToMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new DatabaseContext();
            context.Database.EnsureCreated();

            try
            {
                AddData();

                try
                {
                    // Property Person.Books can't be accessed here if lazy loading is used because the context is disposed.
                    var firstPersonTest = GetFirstPerson();
                    var failure = firstPersonTest.Books.Count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine();
                }

                Console.WriteLine("====================================================================");
                Console.WriteLine("Person:");
                Console.WriteLine("====================================================================");
                var person = context.Persons.Single();
                Console.WriteLine($"Person: {person.Name} - Book Count: {person.Books.Count}");
                Console.WriteLine();
                Console.WriteLine("====================================================================");
                Console.WriteLine("All Books:");
                Console.WriteLine("====================================================================");

                int iteration = 0;

                foreach (var book in context.Books.ToList())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Book {++iteration}: {book.Title}");
                    Console.WriteLine("Categories:");

                    foreach (var category in book.Categories)
                    {
                        Console.WriteLine(category.CategoryName);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("====================================================================");
                Console.WriteLine("All Categories:");
                Console.WriteLine("====================================================================");

                iteration = 0;

                foreach (var category in context.Categories.ToList())
                {
                    Console.WriteLine();
                    Console.WriteLine($"Category {++iteration}: {category.CategoryName}");
                    Console.WriteLine("Books:");

                    foreach (var book in category.Books)
                    {
                        Console.WriteLine(book.Title);
                    }
                }

                List<BookEntity> test2;
                using (var context2 = new DatabaseContext())
                {
                    test2 = context2.Books.ToList();
                }

                Console.ReadKey();
            }
            finally
            {
                RemoveData();
            }
        }

        static private void AddData()
        {
            // Create categories
            var newCategory1 = new CategoryEntity() { CategoryName = "Human" };
            var newCategory2 = new CategoryEntity() { CategoryName = "Nature" };
            var newCategory3 = new CategoryEntity() { CategoryName = "Killers" };

            // Create books
            var newBook1 = new BookEntity() { Title = "Freaky Fanatics"};
            newBook1.Categories.Add(newCategory1);
            newBook1.Categories.Add(newCategory3);

            var newBook2 = new BookEntity() { Title = "Survival of the fittest" };
            newBook2.Categories.Add(newCategory2);
            newBook2.Categories.Add(newCategory3);

            // Create persons
            var newPerson = new PersonEntity() { Name = "Bo Ek" };
            newPerson.Books.Add(newBook1);
            newPerson.Books.Add(newBook2);

            // Save data
            using var context = new DatabaseContext();
            context.Persons.Add(newPerson);
            context.Categories.Add(newCategory1);
            context.Categories.Add(newCategory2);
            context.Categories.Add(newCategory3);
            context.Books.Add(newBook1);
            context.Books.Add(newBook2);
            context.SaveChanges();
        }

        static private void RemoveData()
        {
            using var context = new DatabaseContext();
            context.Books.Where(x => true).ExecuteDelete();
            context.Categories.Where(x => true).ExecuteDelete();
            context.Persons.Where(x => true).ExecuteDelete();
        }

        static private PersonEntity GetFirstPerson()
        {
            using var context = new DatabaseContext();
            return context.Persons.First();
        }
    }
}
