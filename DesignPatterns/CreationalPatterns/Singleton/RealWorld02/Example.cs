namespace DesignPatterns.CreationalPatterns.Singleton.RealWorld02
{
    class Example
    {
        public static void Run()
        {
            var db1 = Database.GetInstance();
            var db2 = Database.GetInstance();

            db1.Query("SELECT Title, Body, Date FROM Page");
            db1.Query("SELECT Name FROM Author");

            // A variável `db1` vai conter o mesmo objeto que a variável `db2`.
            if (db1 == db2)
            {
                Console.WriteLine("A variável 'db1' contém o mesmo objeto que a variável 'db2'.");
            }
            else
            {
                Console.WriteLine("A variável 'db1' não contém o mesmo objeto que a variável 'db2'.");
            }
        }
    }

    /// <summary>
    /// A classe Database define o método `getInstance` que permite clientes acessar a mesma instância de uma conexão a base de
    /// dados através do programa.
    /// </summary>
    class Database
    {
        // O campo para armazenar a instância singleton deve ser declarado como estático.
        private static Database? _instance;

        private static readonly object _lock = new();

        // O construtor do singleton devem sempre ser privado para prevenir chamadas diretas de construção com o operador
        // `new`.
        private Database()
        {
            // Algum código de inicialização, tal como uma conexão com um servidor de base de dados.
        }

        // O método estático que controla acesso à instância do singleton
        public static Database GetInstance()
        {
            lock (_lock)
            {
                // Certifique que a instância ainda não foi inicializada por outra thread enquanto está
                // estiver esperando pela liberação do `lock`.
                if (_instance == null)
                {
                    _instance = new();
                }

                return _instance;
            }
        }

        // Finalmente, qualquer singleton deve definir alguma lógica de negócio que deve ser executada em sua instância.
        public void Query(string sql)
        {
            // Por exemplo, todas as solicitações à base de dados de uma aplicação passam por esse método. Portanto, você
            // pode colocar a lógica de throttling ou cache aqui.
        }
    }
}
