namespace DesignPatterns.CreationalPatterns.AbstractFactory.RealWorld02
{
    /// <summary>
    /// A aplicação seleciona o tipo de fábrica dependendo da atual configuração do ambiente e cria o widget no tempo de execução
    /// (geralmente no estágio de inicialização).
    /// </summary>
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Renderizando componentes no estilo Windows.");
            var win = new Application(new WinFactory());
            win.Paint();

            Console.WriteLine();

            Console.WriteLine("Renderizando componentes no estilo MacOS.");
            var mac = new Application(new MacFactory());
            mac.Paint();
        }
    }

    /// <summary>
    /// O código cliente trabalha com fábricas e produtos apenas através de tipos abstratos: GUIFactory, Button e Checkbox.
    /// Isso permite que você passe qualquer subclasse fábrica ou de produto para o código cliente sem quebrá-lo.
    /// </summary>
    class Application
    {
        public Application(IGuiFactory factory)
        {
            Button = factory.CreateButton();
            Checkbox = factory.CreateCheckbox();
        }

        public IButton Button { get; }
        public ICheckbox Checkbox { get; }

        public void Paint()
        {
            Button.Paint();
            Checkbox.Paint();
        }
    }

    /// <summary>
    /// A interface fábrica abstrata declara um conjunto de métodos que retorna diferentes produtos abstratos. Estes produtos são
    /// chamados uma família e estão relacionados por um tema ou conceito de alto nível. Produtos de uma família são
    /// geralmente capazes de colaborar entre si. Uma família de produtos pode ter várias variantes, mas os produtos de uma
    /// variante são incompatíveis com os produtos de outro variante.
    /// </summary>
    interface IGuiFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    /// <summary>
    /// As fábricas concretas produzem uma família de produtos que pertencem a uma única variante. A fábrica garante que os
    /// produtos resultantes sejam compatíveis. Assinaturas dos métodos fabrica retornam um produto abstrato, enquanto que
    /// dentro do método um produto concreto é instanciado.
    /// </summary>
    class WinFactory : IGuiFactory
    {
        public IButton CreateButton() => new WinButton();
        public ICheckbox CreateCheckbox() => new WinCheckbox();
    }

    /// <summary>
    /// Cada fábrica concreta tem uma variante de produto correspondente.
    /// </summary>
    class MacFactory : IGuiFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();
    }

    /// <summary>
    /// Cada produto distinto de uma família de produtos deve ter uma interface base. Todas as variantes do produto devem
    /// implementar essa interface.
    /// </summary>
    interface IButton
    {
        void Paint();
    }

    /// <summary>
    /// Produtos concretos são criados por fábricas concretas correspondentes.
    /// </summary>
    class WinButton : IButton
    {
        public void Paint() => Console.WriteLine("Renderiza um botão no estilo Windows.");
    }

    /// <summary>
    /// Produtos concretos são criados por fábricas concretas correspondentes.
    /// </summary>
    class MacButton : IButton
    {
        public void Paint() => Console.WriteLine("Renderiza um botão no estilo MacOS.");
    }

    /// <summary>
    /// Aqui está a interface base de outro produto. Todos os produtos podem interagir entre si, mas a interação apropriada
    /// só é possível entre produtos da mesma variante concreta.
    /// </summary>
    interface ICheckbox
    {
        void Paint();
    }

    /// <summary>
    /// Produtos concretos são criados por fábricas concretas correspondentes.
    /// </summary>
    class WinCheckbox : ICheckbox
    {
        public void Paint() => Console.WriteLine("Renderiza uma caixa de seleção no estilo Windows.");
    }

    /// <summary>
    /// Produtos concretos são criados por fábricas concretas correspondentes.
    /// </summary>
    class MacCheckbox : ICheckbox
    {
        public void Paint() => Console.WriteLine("Renderiza uma caixa de seleção no estilo MacOS.");
    }
}
