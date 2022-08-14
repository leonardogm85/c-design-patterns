namespace DesignPatterns.CreationalPatterns.AbstractFactory.RealWorld03
{
    class Example
    {
        public static void Run()
        {
            var page = new Page("Sample page", "This is the body.");

            Console.WriteLine("Testing rendering with the Twig:");
            Console.WriteLine(page.Render(new TwigTemplateFactory()));

            Console.WriteLine();

            Console.WriteLine("Testing rendering with the PHP:");
            Console.WriteLine(page.Render(new PhpTemplateFactory()));
        }
    }

    class Page
    {
        public Page(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }

        public string Render(ITemplateFactory factory)
        {
            var pageTemplate = factory.CreatePageTemplate();

            var renderer = factory.GetRenderer();

            return renderer.Render(pageTemplate.GetTemplate(), new Dictionary<string, object> {
                { nameof(Title), Title },
                { nameof(Content), Content }
            });
        }
    }

    interface ITemplateFactory
    {
        ITitleTemplate CreateTitleTemplate();
        PageTemplate CreatePageTemplate();
        ITemplateRenderer GetRenderer();
    }

    class TwigTemplateFactory : ITemplateFactory
    {
        public ITitleTemplate CreateTitleTemplate() => new TwigTitleTemplate();

        public PageTemplate CreatePageTemplate() => new TwigPageTemplate(CreateTitleTemplate());

        public ITemplateRenderer GetRenderer() => new TwigRenderer();
    }

    class PhpTemplateFactory : ITemplateFactory
    {
        public ITitleTemplate CreateTitleTemplate() => new PhpTitleTemplate();

        public PageTemplate CreatePageTemplate() => new PhpPageTemplate(CreateTitleTemplate());

        public ITemplateRenderer GetRenderer() => new PhpRenderer();
    }

    interface ITitleTemplate
    {
        string GetTemplate();
    }

    class TwigTitleTemplate : ITitleTemplate
    {
        public string GetTemplate() => "<Twig.Title>@[Title]</Twig.Title>";
    }

    class PhpTitleTemplate : ITitleTemplate
    {
        public string GetTemplate() => "<PHP.Title>$(Title)</PHP.Title>";
    }

    abstract class PageTemplate
    {
        public PageTemplate(ITitleTemplate titleTemplate) => TitleTemplate = titleTemplate;

        public ITitleTemplate TitleTemplate { get; }

        public abstract string GetTemplate();
    }

    class TwigPageTemplate : PageTemplate
    {
        public TwigPageTemplate(ITitleTemplate titleTemplate) : base(titleTemplate) { }

        public override string GetTemplate()
        {
            return $"<Twig.Page>\n\t{TitleTemplate.GetTemplate()}\n\t<Twig.Content>@[Content]</Twig.Content>\n</Twig.Page>";
        }
    }

    class PhpPageTemplate : PageTemplate
    {
        public PhpPageTemplate(ITitleTemplate titleTemplate) : base(titleTemplate) { }

        public override string GetTemplate()
        {
            return $"<PHP.Page>\n\t{TitleTemplate.GetTemplate()}\n\t<PHP.Content>$(Content)</PHP.Content>\n</PHP.Page>";
        }
    }

    interface ITemplateRenderer
    {
        string Render(string template, IDictionary<string, object> arguments);
    }

    class TwigRenderer : ITemplateRenderer
    {
        public string Render(string template, IDictionary<string, object> arguments)
        {
            return arguments.Aggregate(template, (acc, arg) => acc.Replace($"@[{arg.Key}]", arg.Value.ToString()));
        }
    }

    class PhpRenderer : ITemplateRenderer
    {
        public string Render(string template, IDictionary<string, object> arguments)
        {
            return arguments.Aggregate(template, (acc, arg) => acc.Replace($"$({arg.Key})", arg.Value.ToString()));
        }
    }
}
