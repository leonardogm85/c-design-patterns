namespace DesignPatterns.CreationalPatterns.Singleton.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            var listVc = new MessagesListVc();
            var chatVc = new ChatVc();

            listVc.StartReceiveMessages();

            Console.WriteLine();

            chatVc.StartReceiveMessages();
        }
    }

    class Message
    {
        public Message(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; }
        public string Text { get; }
    }

    abstract class MessageSubscriber
    {
        public abstract void New(List<Message> message);
        public abstract void Removed(List<Message> message);
    }

    abstract class BaseVc : MessageSubscriber
    {
        public virtual void StartReceiveMessages() => FriendsChatService.GetInstance().Add(this);
    }

    class MessagesListVc : BaseVc
    {
        public override void New(List<Message> message) => Console.WriteLine("MessagesListVC accepted 'new messages'.");

        public override void Removed(List<Message> message) => Console.WriteLine("MessagesListVC accepted 'removed messages'.");

        public override void StartReceiveMessages()
        {
            Console.WriteLine("MessagesListVC starts receive messages.");
            base.StartReceiveMessages();
        }
    }

    class ChatVc : BaseVc
    {
        public override void New(List<Message> message) => Console.WriteLine("ChatVC accepted 'new messages'.");

        public override void Removed(List<Message> message) => Console.WriteLine("ChatVC accepted 'removed messages'.");

        public override void StartReceiveMessages()
        {
            Console.WriteLine("ChatVC starts receive messages.");
            base.StartReceiveMessages();
        }
    }

    interface IMessageService
    {
        void Add(MessageSubscriber subscriber);
    }

    class FriendsChatService : IMessageService
    {
        private static readonly Lazy<FriendsChatService> _instance = new(() => new());

        private List<MessageSubscriber> _subscribers = new();

        private FriendsChatService() { }

        public static FriendsChatService GetInstance() => _instance.Value;

        public void Add(MessageSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            StartFetching();
        }

        private void StartFetching()
        {
            var newMessages = new List<Message>
            {
                new(0, "Text0"),
                new(5, "Text5"),
                new(9, "Text9")
            };

            var removedMessages = new List<Message>
            {
                new(1, "Text1")
            };

            ReceivedNew(newMessages);
            ReceivedRemoved(removedMessages);
        }

        public void ReceivedNew(List<Message> messages) => _subscribers.ForEach(item => item.New(messages));

        public void ReceivedRemoved(List<Message> messages) => _subscribers.ForEach(item => item.Removed(messages));
    }
}
