namespace DesignPatterns.CreationalPatterns.FactoryMethod.RealWorld
{
    // The Factory Method design pattern defines an interface for creating an
    // object, but let subclasses decide which class to instantiate. This pattern lets a
    // class defer instantiation to subclasses.

    class Example
    {
        public static void Run()
        {
            List<Document> documents = new()
            {
                new Resume(),
                new Report()
            };

            foreach (Document document in documents)
            {
                Console.WriteLine($"Document: {document}");

                foreach (Page page in document.Pages)
                {
                    Console.WriteLine($"  Page: {page}");
                }

                Console.WriteLine();
            }
        }
    }

    // Product (Page)

    abstract class Page
    {
        public override string ToString() => GetType().Name;
    }

    // ConcreteProduct (SkillsPage, EducationPage, ExperiencePage, IntroductionPage, ResultsPage, ConclusionPage, SummaryPage, BibliographyPage)

    class SkillsPage : Page
    {
    }

    class EducationPage : Page
    {
    }

    class ExperiencePage : Page
    {
    }

    class IntroductionPage : Page
    {
    }

    class ResultsPage : Page
    {
    }

    class ConclusionPage : Page
    {
    }

    class SummaryPage : Page
    {
    }

    class BibliographyPage : Page
    {
    }

    // Creator (Document)

    abstract class Document
    {
        public List<Page> Pages { get; private set; } = new();

        public Document() => CreatePages();

        public abstract void CreatePages();

        public override string ToString() => GetType().Name;
    }

    // ConcreteCreator (Report, Resume)

    class Resume : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new SkillsPage());
            Pages.Add(new EducationPage());
            Pages.Add(new ExperiencePage());
        }
    }

    class Report : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new IntroductionPage());
            Pages.Add(new ResultsPage());
            Pages.Add(new ConclusionPage());
            Pages.Add(new SummaryPage());
            Pages.Add(new BibliographyPage());
        }
    }
}
