using System;

namespace Project1CB
{
    class Program
    {
        static User User;
        public static DBManager DBManager;

        static void Main(string[] args)
        {
            Console.WriteLine(@"__________                     ___________   ________                  _____            ");
            Console.WriteLine(@"___  ____/   _______ _________ ___(_)__  /   __  ___/______________   ____(_)__________ ");
            Console.WriteLine(@"__  __/________  __ `__ \  __ `/_  /__  /    _____ \_  _ \_  ___/_ | / /_  /_  ___/  _ \");
            Console.WriteLine(@"_  /___/_____/  / / / / / /_/ /_  / _  /     ____/ //  __/  /   __ |/ /_  / / /__ /  __/");
            Console.WriteLine(@"/_____/      /_/ /_/ /_/\__,_/ /_/  /_/      /____/ \___//_/    _____/ /_/  \___/ \___/ ");
            Console.WriteLine("");
            
            Console.WriteLine("Welcome to my e-mail service!");

            DBManager = new DBManager();
            
            Option option;
            do
            {
                Console.WriteLine("Please login to continue!");

                User = Manage.Login(DBManager);

                do
                {
                    option = User.ShowMenu();
                    Manage.ManageOption(option, DBManager, User);
                }
                while (option != Option.Logout && option != Option.Exit);
            }
            while (option != Option.Exit);
        }
    }
}
