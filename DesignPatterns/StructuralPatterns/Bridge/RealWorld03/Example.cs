namespace DesignPatterns.StructuralPatterns.Bridge.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            var htmlRenderer = new HtmlRenderer();
            var jsonRenderer = new JsonRenderer();

            var simplePage = new SimplePage(htmlRenderer, "Home", "Welcome to our website!");
            Console.WriteLine("HTML view of a simple content page:");
            Console.WriteLine(simplePage.View());
            Console.WriteLine();

            simplePage.ChangeRenderer(jsonRenderer);
            Console.WriteLine("JSON view of a simple content page, rendered with the same client code:");
            Console.WriteLine(simplePage.View());
            Console.WriteLine();

            var product = new Product(
                "123",
                "Star Wars, episode1",
                "A long time ago in a galaxy far, far away...",
                "/images/star-wars.jpeg",
                39.95f);

            var productPage = new ProductPage(htmlRenderer, product);
            Console.WriteLine("HTML view of a product page, same client code:");
            Console.WriteLine(productPage.View());
            Console.WriteLine();

            productPage.ChangeRenderer(jsonRenderer);
            Console.WriteLine("JSON view of a product page, with the same client code:");
            Console.WriteLine(productPage.View());
        }
    }

    abstract class Page
    {
        protected IRenderer _renderer;

        protected Page(IRenderer renderer) => _renderer = renderer;

        public void ChangeRenderer(IRenderer renderer) => _renderer = renderer;

        public abstract string View();
    }

    class SimplePage : Page
    {
        private readonly string _title;
        private readonly string _content;

        public SimplePage(IRenderer renderer, string title, string content) : base(renderer)
        {
            _title = title;
            _content = content;
        }

        public override string View()
        {
            return _renderer.RenderParts(new()
            {
                _renderer.RenderHeader(),
                _renderer.RenderTitle(_title),
                _renderer.RenderTextBlock(_content),
                _renderer.RenderFooter()
            });
        }
    }

    class ProductPage : Page
    {
        private readonly Product _product;

        public ProductPage(IRenderer renderer, Product product) : base(renderer) => _product = product;

        public override string View()
        {
            return _renderer.RenderParts(new()
            {
                _renderer.RenderHeader(),
                _renderer.RenderTitle(_product.GetTitle()),
                _renderer.RenderTextBlock(_product.GetDescription()),
                _renderer.RenderImage(_product.GetImage()),
                _renderer.RenderLink($"/cart/add/{_product.GetId()}", "Add to cart"),
                _renderer.RenderFooter()
            });
        }
    }

    class Product
    {
        private readonly string _id;
        private readonly string _title;
        private readonly string _description;
        private readonly string _image;
        private readonly float _price;

        public Product(string id, string title, string description, string image, float price)
        {
            _id = id;
            _title = title;
            _description = description;
            _image = image;
            _price = price;
        }

        public string GetId() => _id;
        public string GetTitle() => _title;
        public string GetDescription() => _description;
        public string GetImage() => _image;
        public float GetPrice() => _price;
    }

    interface IRenderer
    {
        string RenderTitle(string title);
        string RenderTextBlock(string text);
        string RenderImage(string url);
        string RenderLink(string url, string title);
        string RenderHeader();
        string RenderFooter();
        string RenderParts(List<string> parts);
    }

    class HtmlRenderer : IRenderer
    {
        public string RenderTitle(string title) => $"<h1>{title}</h1>";

        public string RenderTextBlock(string text) => $"<div class='text'>{text}</div>";

        public string RenderImage(string url) => $"<img src='{url}' />";

        public string RenderLink(string url, string title) => $"<a href='{url}'>{title}</a>";

        public string RenderHeader() => $"<html><body>";

        public string RenderFooter() => $"</body></html>";

        public string RenderParts(List<string> parts) => string.Join("\n", parts);
    }

    class JsonRenderer : IRenderer
    {
        public string RenderTitle(string title) => $"\"title\": \"{title}\"";

        public string RenderTextBlock(string text) => $"\"text\": \"{text}\"";

        public string RenderImage(string url) => $"\"img\": \"{url}\"";

        public string RenderLink(string url, string title) => $"\"link\": {{ \"href\": \"{url}\", \"title\": \"{title}\" }}";

        public string RenderHeader() => string.Empty;

        public string RenderFooter() => string.Empty;

        public string RenderParts(List<string> parts) => $"{{\n{string.Join(",\n", parts.Where(part => !string.IsNullOrEmpty(part)))}\n}}";
    }
}
