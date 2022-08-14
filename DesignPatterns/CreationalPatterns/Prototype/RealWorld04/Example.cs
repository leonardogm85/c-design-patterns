namespace DesignPatterns.CreationalPatterns.Prototype.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            var author = new Author("Leonardo Martins");

            var originalPage = new Page("Title of first page", "Body of first page.", author);

            originalPage.AddComment(new("First comment"));
            originalPage.AddComment(new("Second comment"));
            originalPage.AddComment(new("Third comment"));

            var copiedPage = (Page)originalPage.Clone();

            Console.WriteLine($"Original title: {originalPage.Title}.");
            Console.WriteLine($"Number of comments: {originalPage.NumberOfComments()}.");

            Console.WriteLine();

            Console.WriteLine($"Copied title: {copiedPage.Title}.");
            Console.WriteLine($"Number of comments: {copiedPage.NumberOfComments()}.");

            Console.WriteLine();

            Console.WriteLine($"Author: {author.Name}.");
            Console.WriteLine($"Number of pages: {author.NumberOfPages()}.");
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
        public List<Comment> Comments { get; } = new();

        public void AddComment(Comment comment) => Comments.Add(comment);

        public int NumberOfComments() => Comments.Count;

        public object Clone() => new Page($"Copy of {Title}", Body, Author);
    }

    class Author : ICloneable
    {
        public Author(string name) => Name = name;

        public string Name { get; }
        public List<Page> Pages { get; } = new();

        public void AddPage(Page page) => Pages.Add(page);

        public int NumberOfPages() => Pages.Count;

        public override string ToString() => Name;

        public object Clone() => new Author(Name);
    }

    class Comment : ICloneable
    {
        public Comment(string message) => Message = message;

        public DateTime Date { get; } = DateTime.Now;
        public string Message { get; }

        public object Clone() => new Comment(Message);
    }
}
