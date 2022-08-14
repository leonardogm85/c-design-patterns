namespace DesignPatterns.CreationalPatterns.AbstractFactory.RealWorld04
{
    class Example
    {
        public static void Run()
        {
            Console.WriteLine("Testing Student Factory:");

            var student = new Present(new StudentAuthViewFactory());

            student.Login();
            student.SignUp();

            Console.WriteLine();

            Console.WriteLine("Testing Teacher Factory:");

            var teacher = new Present(new TeacherAuthViewFactory());

            teacher.Login();
            teacher.SignUp();
        }
    }

    class Present
    {
        public Present(IAuthViewFactory factory) => Factory = factory;

        public IAuthViewFactory Factory { get; }

        public void Login()
        {
            Console.WriteLine("Login screen has been presented.");

            Factory.AuthController(AuthType.Login);
        }

        public void SignUp()
        {
            Console.WriteLine("Sign up screen has been presented.");

            Factory.AuthController(AuthType.SignUp);
        }
    }

    enum AuthType
    {
        Login,
        SignUp
    }

    interface IAuthViewFactory
    {
        IAuthView AuthView(AuthType type);
        AuthViewController AuthController(AuthType type);
    }

    class StudentAuthViewFactory : IAuthViewFactory
    {
        public IAuthView AuthView(AuthType type)
        {
            Console.WriteLine("Student View has been created.");

            switch (type)
            {
                case AuthType.SignUp:
                    return new StudentSignUpView();
                default:
                    return new StudentLoginView();
            }
        }

        public AuthViewController AuthController(AuthType type)
        {
            var controller = new StudentAuthViewController(AuthView(type));

            Console.WriteLine("Student View Controller has been created.");

            return controller;
        }
    }

    class TeacherAuthViewFactory : IAuthViewFactory
    {
        public IAuthView AuthView(AuthType type)
        {
            Console.WriteLine("Teacher View has been created.");

            switch (type)
            {
                case AuthType.SignUp:
                    return new TeacherSignUpView();
                default:
                    return new TeacherLoginView();
            }
        }

        public AuthViewController AuthController(AuthType type)
        {
            var controller = new TeacherAuthViewController(AuthView(type));

            Console.WriteLine("Teacher View Controller has been created.");

            return controller;
        }
    }

    interface IAuthView { }

    class StudentSignUpView : IAuthView { }

    class StudentLoginView : IAuthView { }

    class TeacherSignUpView : IAuthView { }

    class TeacherLoginView : IAuthView { }

    abstract class AuthViewController
    {
        public AuthViewController(IAuthView contentView) => ContentView = contentView;

        public IAuthView ContentView { get; }
    }

    class TeacherAuthViewController : AuthViewController
    {
        public TeacherAuthViewController(IAuthView contentView) : base(contentView) { }
    }

    class StudentAuthViewController : AuthViewController
    {
        public StudentAuthViewController(IAuthView contentView) : base(contentView) { }
    }
}
