namespace DesignPatterns.StructuralPatterns.Adapter.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            Console.WriteLine("Starting an authorization via Facebook");
            app.StartAuthorization(new FacebookAuthSdk());

            Console.WriteLine();

            Console.WriteLine("Starting an authorization via Twitter.");
            app.StartAuthorization(new TwitterAuthSdk());
        }
    }

    class Application
    {
        public void StartAuthorization(IAuthService service) => service.PresentAuthFlow();
    }

    interface IAuthService
    {
        void PresentAuthFlow();
    }

    partial class FacebookAuthSdk
    {
        public void PresentAuthFlow() => Console.WriteLine("Facebook WebView has been shown.");
    }

    partial class TwitterAuthSdk
    {
        public void StartAuthorization() => Console.WriteLine("Twitter WebView has been shown. Users will be happy :)");
    }

    partial class TwitterAuthSdk : IAuthService
    {
        public void PresentAuthFlow()
        {
            Console.WriteLine("The Adapter is called! Redirecting to the original method...");
            StartAuthorization();
        }
    }

    partial class FacebookAuthSdk : IAuthService { }
}
