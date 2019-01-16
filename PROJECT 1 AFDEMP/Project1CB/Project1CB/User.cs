using System;
using System.Collections.Generic;

namespace Project1CB
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public List<Option> Options { get; set; }

        public User(string username, UserType type)
        {
            Username = username;
            Type = type;
            Options = LoadOptions(type);
        }

        public List<Option> LoadOptions(UserType type)
        {
            switch(type)
            {
                case UserType.Admin:
                    return new List<Option> { Option.CreateUser, Option.ViewUsers, Option.UpdateUser, Option.DeleteUser, Option.Logout, Option.Exit };
                case UserType.V:
                    return new List<Option> { Option.Send, Option.ViewData, Option.Logout, Option.Exit };
                case UserType.VE:
                    return new List<Option> { Option.Send, Option.ViewData, Option.UpdateData, Option.Logout, Option.Exit };
                case UserType.VED:
                    return new List<Option> { Option.Send, Option.ViewData, Option.UpdateData, Option.DeleteData, Option.Logout, Option.Exit };
                default:
                    return null;
            }
        }

        public Option ShowMenu()
        {
            Console.WriteLine("~ ~ ~ ~ Menu ~ ~ ~ ~");
            var count = 0;
            foreach (var option in Options)
                Console.WriteLine(++count + ") " + option);

            var opt = Console.ReadLine();
            int.TryParse(opt, out var index);

            return Options[index - 1];
        }
    }

    enum UserType
    {
        Admin,
        V,
        VE,
        VED
    }
}