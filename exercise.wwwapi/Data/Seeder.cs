
using exercise.wwwapi.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace exercise.wwwapi.Data
{
    public static class Seeder
    {
        private static List<string> Firstnames = new List<string>()
        {
            "Audrey",
            "Donald",
            "Elvis",
            "Barack",
            "Oprah",
            "Jimi",
            "Mick",
            "Kate",
            "Charles",
            "Kate",
            "Sam",
            "Nigel",
            "Elaine",
            "Olivia",
            "Harry",
            "Lewis",
            "Florence",
            "Amy",
            "Adele",
            "Ed"
        };
        private static List<string> Lastnames = new List<string>()
        {
            "Hepburn",
            "Trump",
            "Presley",
            "Obama",
            "Winfrey",
            "Hendrix",
            "Jagger",
            "Winslet",
            "Windsor",
            "Middleton",
            "Adkins",
            "Sheeran"

        };
        private static List<string> Domain = new List<string>()
        {
            "bbc.co.uk",
            "google.com",
            "theworld.ca",
            "something.com",
            "tesla.com",
            "nasa.org.us",
            "gov.us",
            "gov.gr",
            "gov.nl",
            "gov.ru"
        };
        private static List<string> FirstWord = new List<string>()
        {
            "The",
            "Two",
            "Several",
            "Fifteen",
            "A bunch of",
            "An army of",
            "A herd of"


        };
        private static List<string> SecondWord = new List<string>()
        {
            "Orange",
            "Purple",
            "Large",
            "Microscopic",
            "Green",
            "Transparent",
            "Rose Smelling",
            "Bitter"
        };
        private static List<string> ThirdWord = new List<string>()
        {
            "Buildings",
            "Cars",
            "Planets",
            "Houses",
            "Flowers",
            "Leopards"
        };
        private static string GeneratePublisherName()
        {
            string[] words = { "Blue", "Readable", "Flying", "Expensive", "Reflective", "Feathery", "Shiny", "Clean", "Brown Bagel", "Unlimited", "Unlikely","Dove", "Green", "Wooden", "Concrete",  "Empty", "Petersfield", "Durrington", "Meon", "Swanmore", "Herbeton", "Corhampton", "Harewood", "Stourvale"};


            Random random = new Random();
            return $"The {words[random.Next(words.Length)]} Publishing Company";

        }
       
        public static void Seed(this WebApplication app)
        {


            using (var db = new DatabaseContext())
            {
                Random authorRandom = new Random();
                Random bookRandom = new Random();
                Random publisherRandom = new Random();
                var authors = new List<Author>();
                var books = new List<Book>();
                var publishers = new List<Publisher>();


                if (!db.Authors.Any())
                {
                    for (int x = 1; x < 100; x++)
                    {
                        while(true)
                        {
                            Tuple<string, string> name = new Tuple<string, string>(Firstnames[authorRandom.Next(Firstnames.Count)], Lastnames[authorRandom.Next(Lastnames.Count)]);
                            if(!authors.Where(a => a.Firstname == name.Item1 && a.Lastname==name.Item2).Any())
                            {
                                Author author = new Author();
                                author.Id = x;
                                author.Firstname = name.Item1;
                                author.Lastname = name.Item2;
                                author.Email = $"{author.Firstname}.{author.Lastname}@{Domain[authorRandom.Next(Domain.Count)]}".ToLower();
                                authors.Add(author);
                                break;
                            }

                        }




                    }
                    db.Authors.AddRange(authors);
                }
                else
                {
                    authors.AddRange(db.Authors);
                }
                if(!db.Publishers.Any())
                {
                    for(int x = 1; x < 15; x++)
                    {
                        while(true)
                        {
                            string suggestion = GeneratePublisherName();
                            if (!publishers.Where(p => p.Name == suggestion).Any())
                            {
                                Publisher publisher = new Publisher();
                                publisher.Id = x;
                                publisher.Name = GeneratePublisherName();
                                publishers.Add(publisher);
                                break;
                            }
                        }
                    }
                    db.Publishers.AddRange(publishers);
                }
                else
                {
                    publishers.AddRange(db.Publishers);
                }

                if (!db.Books.Any())
                {

                    for (int x = 1; x < 100; x++)
                    {
                        while(true)
                        {
                            string titleSuggestion = $"{FirstWord[bookRandom.Next(FirstWord.Count)]} {SecondWord[bookRandom.Next(SecondWord.Count)]} { ThirdWord[bookRandom.Next(ThirdWord.Count)]}";
                            if (!books.Where(b => b.Title == titleSuggestion).Any())
                            {
                                Book book = new Book();
                                book.Id = x;
                                book.Title = $"{titleSuggestion}";
                                book.AuthorId = x;
                                book.PublisherId = publishers[publisherRandom.Next(publishers.Count)].Id;
                                books.Add(book);
                                break;
                            }
                          
                        }
                    }
                    db.Books.AddRange(books);
                }

                
                db.SaveChanges();
            }

        }
    }
}