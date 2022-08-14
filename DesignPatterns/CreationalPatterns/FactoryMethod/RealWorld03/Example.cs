namespace DesignPatterns.CreationalPatterns.FactoryMethod.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Testing Facebook:");
            var facebook = new Application(new FacebookPoster("leonardo.martins", "Test@123"));
            facebook.Post();

            Console.WriteLine();

            Console.WriteLine("Testing LinkedIn:");
            var linkedIn = new Application(new LinkedInPoster("leonardo.martins@example.com", "Test@123"));
            linkedIn.Post();
        }
    }

    class Application
    {
        public SocialNetworkPoster Creator { get; }

        public Application(SocialNetworkPoster creator) => Creator = creator;

        public void Post()
        {
            Creator.Post("Hello world!");
            Creator.Post("I had a large hamburger this morning!");
        }
    }

    abstract class SocialNetworkPoster
    {
        public abstract ISocialNetworkConnector GetSocialNetwork();

        public void Post(string content)
        {
            var network = GetSocialNetwork();
            network.LogIn();
            network.CreatePost(content);
            network.LogOut();
        }
    }

    class FacebookPoster : SocialNetworkPoster
    {
        public FacebookPoster(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }

        public override ISocialNetworkConnector GetSocialNetwork() => new FacebookConnector(Login, Password);
    }

    class LinkedInPoster : SocialNetworkPoster
    {
        public LinkedInPoster(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }

        public override ISocialNetworkConnector GetSocialNetwork() => new LinkedInConnector(Email, Password);
    }

    interface ISocialNetworkConnector
    {
        public void CreatePost(string content);
        public void LogIn();
        public void LogOut();
    }

    class FacebookConnector : ISocialNetworkConnector
    {
        public FacebookConnector(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }

        public void LogIn() => Console.WriteLine($"Send HTTP API request to log in user {Login} with password {Password}.");

        public void LogOut() => Console.WriteLine($"Send HTTP API request to log out user {Login}.");

        public void CreatePost(string content) => Console.WriteLine("Send HTTP API requests to create a post in Facebook timeline.");
    }

    class LinkedInConnector : ISocialNetworkConnector
    {
        public LinkedInConnector(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }

        public void LogIn() => Console.WriteLine($"Send HTTP API request to log in user {Email} with password {Password}.");

        public void LogOut() => Console.WriteLine($"Send HTTP API request to log out user {Email}.");

        public void CreatePost(string content) => Console.WriteLine("Send HTTP API requests to create a post in LinkedIn timeline.");
    }
}
