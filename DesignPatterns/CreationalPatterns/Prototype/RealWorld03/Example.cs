namespace DesignPatterns.CreationalPatterns.Prototype.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            var author = new Author("Leonardo Martins");

            var originalPage = new Page("Title of first page", "Body of first page.", author);

            originalPage.AddComment("First comment");
            originalPage.AddComment("Second comment");
            originalPage.AddComment("Third comment");

            var copiedPage = (Page)originalPage.Clone();

            var app = new Application();

            Console.WriteLine("Original page:");
            app.Print(originalPage);

            Console.WriteLine();

            Console.WriteLine("Copied page:");
            app.Print(copiedPage);
        }
    }

    class Application
    {
        public void Print(Page page)
        {
            Console.WriteLine($"Title: {page.Title}");
            Console.WriteLine($"Body: {page.Body}");
            Console.WriteLine($"Author: {page.Author}");
            Console.WriteLine($"Date: {page.Date}");

            if (page.Comments.Any())
            {
                Console.WriteLine("Comments:");

                foreach (var comment in page.Comments)
                {
                    Console.WriteLine($"\t{comment}");
                }
            }
        }
    }

    class Page : ICloneable
    {
        public Page(string title, string body, Author author)
        {
            Title = title;
            Body = body;
            Author = author;

            Author.AddPage(this);
        }

        public string Title { get; }
        public string Body { get; }
        public Author Author { get; }
        public DateTime Date { get; } = DateTime.Now;
        public List<string> Comments { get; } = new();

        public void AddComment(string comment) => Comments.Add(comment);

        public object Clone() => new Page($"Copy of {Title}", Body, Author);
    }

    class Author : ICloneable
    {
        public Author(string name) => Name = name;

        public string Name { get; }
        public List<Page> Pages { get; } = new();

        public void AddPage(Page page) => Pages.Add(page);

        public override string ToString() => Name;

        public object Clone() => new Author(Name);
    }
}
