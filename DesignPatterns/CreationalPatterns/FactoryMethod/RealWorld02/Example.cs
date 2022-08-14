namespace DesignPatterns.CreationalPatterns.FactoryMethod.RealWorld02
{
    /// <summary>
    /// O código cliente trabalha com uma instância de um criador concreto, ainda que com sua interface base. Desde que o
    /// cliente continue trabalhando com a criadora através da interface base, você pode passar qualquer subclasse da
    /// criadora.
    /// </summary>
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Renderizando componentes no estilo Windows.");
            var win = new Application(new WindowsDialog());
            win.Render();

            Console.WriteLine();

            Console.WriteLine("Renderizando componentes no estilo Web.");
            var web = new Application(new WebDialog());
            web.Render();
        }
    }

    /// <summary>
    /// A aplicação seleciona um tipo de criador dependendo da configuração atual ou definições de ambiente.
    /// </summary>
    class Application
    {
        public Application(Dialog dialog) => Dialog = dialog;

        public Dialog Dialog { get; }

        public void Render() => Dialog.Render();
    }

    /// <summary>
    /// A classe criadora declara o método fábrica que deve retornar um objeto de uma classe produto. As subclasses da criadora
    /// geralmente fornecem a implementação desse método.
    /// </summary>
    abstract class Dialog
    {
        // A criadora também pode fornecer alguma implementação padrão do Factory Method.
        public abstract IButton CreateButton();

        // Observe que, apesar do seu nome, a principal responsabilidade da criadora não é criar produtos. Ela
        // geralmente contém alguma lógica de negócio central que depende dos objetos produto retornados pelo método
        // fábrica. As subclasses pode mudar indiretamente essa lógica de negócio ao sobrescreverem o método fábrica e
        // retornarem um tipo diferente de produto dele.
        public void Render()
        {
            // Chame o método fábrica para criar um objeto produto.
            var okButton = CreateButton();

            // Agora use o produto.
            okButton.OnClick(Console.WriteLine);
            okButton.Render();
        }
    }

    /// <summary>
    /// Criadores concretos sobrescrevem o método fábrica para mudar o tipo de produto resultante.
    /// </summary>
    class WindowsDialog : Dialog
    {
        public override IButton CreateButton() => new WindowsButton();
    }

    /// <summary>
    /// Criadores concretos sobrescrevem o método fábrica para mudar o tipo de produto resultante.
    /// </summary>
    class WebDialog : Dialog
    {
        public override IButton CreateButton() => new WebButton();
    }

    /// <summary>
    /// A interface do produto declara as operações que todos os produtos concretos devem implementar.
    /// </summary>
    interface IButton
    {
        void OnClick(Action<string> action);
        void Render();
    }

    /// <summary>
    /// Produtos concretos fornecem várias implementações da interface do produto.
    /// </summary>
    class WindowsButton : IButton
    {
        public void OnClick(Action<string> action) => action("Vincula um evento de clique do SO nativo.");

        public void Render() => Console.WriteLine("Renderiza um botão no estilo Windows.");
    }

    /// <summary>
    /// Produtos concretos fornecem várias implementações da interface do produto.
    /// </summary>
    class WebButton : IButton
    {
        public void OnClick(Action<string> action) => action("Vincula um evento de clique no navegador web.");

        public void Render() => Console.WriteLine("Renderiza um botão no estilo HTML.");
    }
}
