using System.Text.RegularExpressions;

namespace DesignPatterns.StructuralPatterns.Adapter.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            var app = new Application();

            Console.WriteLine("Client code is designed correctly and works with email notifications:");
            var emailNotification = new EmailNotification("leonardo.martins@example.com");
            app.SendNotification(emailNotification);

            Console.WriteLine();

            Console.WriteLine("The same client code can work with other classes via adapter:");
            var slackApi = new SlackApi("leonardo.martins", "xxxxxxxxxx");
            var slackNotification = new SlackNotification(slackApi, "Example.com Developers");
            app.SendNotification(slackNotification);
        }
    }

    class Application
    {
        public void SendNotification(INotification notification)
        {
            notification.Send(
                "Website is down!",
                "<strong style='color:red;font-size:50px;'>Alert!</strong> Our website is not responding. Call admins and bring it up!");
        }
    }

    interface INotification
    {
        void Send(string title, string message);
    }

    class EmailNotification : INotification
    {
        private readonly string _adminEmail;

        public EmailNotification(string email) => _adminEmail = email;

        public void Send(string title, string message)
        {
            Console.WriteLine($"Sent email with title '{title}' to '{_adminEmail}' that says '{message}'.");
        }
    }

    class SlackApi
    {
        private readonly string _login;
        private readonly string _apiKey;

        public SlackApi(string login, string apiKey)
        {
            _login = login;
            _apiKey = apiKey;
        }

        public void LogIn() => Console.WriteLine($"Logged in to a slack account '{_login}'.");

        public void SendMessage(string chatId, string message)
        {
            Console.WriteLine($"Posted following message into the '{chatId}' chat: '{message}'.");
        }
    }

    class SlackNotification : INotification
    {
        private readonly SlackApi _slackApi;
        private readonly string _chatId;

        public SlackNotification(SlackApi slackApi, string chatId)
        {
            _slackApi = slackApi;
            _chatId = chatId;
        }

        public void Send(string title, string message)
        {
            var slackMessage = $"#{title}# {message.StripTags()}";

            _slackApi.LogIn();

            _slackApi.SendMessage(_chatId, slackMessage);
        }
    }

    static class StringExtension
    {
        public static string StripTags(this string source) => Regex.Replace(source, "<.*?>", string.Empty);
    }
}
