namespace DesignPatterns.StructuralPatterns.Bridge.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            Console.WriteLine("Client: Pushing Photo View Controller...");
            app.Push(new PhotoViewController());

            Console.WriteLine();

            Console.WriteLine("Client: Pushing Feed View Controller...");
            app.Push(new FeedViewController());
        }
    }

    class Application
    {
        public void Push(ISharingSupportable container)
        {
            var facebook = new FacebookSharingService();
            var instagram = new InstagramSharingService();

            var model = new FoodDomainModel(
                "This food is so various and delicious!",
                new() {
                    "food_1.png",
                    "food_2.png"
                },
                47);

            container.Accept(facebook);
            container.Update(model);

            container.Accept(instagram);
            container.Update(model);
        }
    }

    interface ISharingSupportable
    {
        void Accept(ISharingService service);
        void Update(Content content);
    }

    abstract class BaseViewController : ISharingSupportable
    {
        private ISharingService? _service;

        public void Accept(ISharingService service) => _service = service;

        public void Update(Content content)
        {
            Console.WriteLine($"{this}: User selected a {content} to share.");
            _service?.Share(content);
        }
    }

    class PhotoViewController : BaseViewController
    {
        public override string ToString() => "PhotoViewController";
    }

    class FeedViewController : BaseViewController
    {
        public override string ToString() => "FeedViewController";
    }

    interface ISharingService
    {
        void Share(Content content);
    }

    class FacebookSharingService : ISharingService
    {
        public void Share(Content content) => Console.WriteLine($"Service: {content} was posted to the Facebook.");
    }

    class InstagramSharingService : ISharingService
    {
        public void Share(Content content) => Console.WriteLine($"Service: {content} was posted to the Instagram.");
    }

    abstract class Content
    {
        protected Content(string title, List<string> images)
        {
            Title = title;
            Images = images;
        }

        public string Title { get; }
        public List<string> Images { get; }
    }

    class FoodDomainModel : Content
    {
        public FoodDomainModel(string title, List<string> images, int calories) : base(title, images) => Calories = calories;

        public int Calories { get; }

        public override string ToString() => "Food Model";
    }
}
