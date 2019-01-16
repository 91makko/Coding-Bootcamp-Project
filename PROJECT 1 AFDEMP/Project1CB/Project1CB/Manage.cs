using System;
using System.IO;

namespace Project1CB
{
    class Manage
    {
        internal static User Login(DBManager dBManager)
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();

            while (!dBManager.CheckUser(username))
            {
                Console.WriteLine("User does not exist!");
                Console.Write("Username: ");
                username = Console.ReadLine();
            }

            Console.Write("Password: ");
            var password = Console.ReadLine();
            User user = dBManager.CheckUser(username, password);
            while (user == null)
            {
                Console.WriteLine("Wrong password!");
                Console.Write("Password: ");
                password = Console.ReadLine();
                user = dBManager.CheckUser(username, password);
            }

            return user;
        }

        internal static void ManageOption(Option option, DBManager dBManager, User user)
        {
            switch (option)
            {
                case Option.Send:
                    Console.Write("Receiver: ");
                    var receiver = Console.ReadLine();

                    while (!dBManager.CheckUser(receiver) || receiver == "admin")
                    {
                        Console.WriteLine("User does not exists!");
                        Console.Write("Receiver: ");
                        receiver = Console.ReadLine();
                    }

                    Console.Write("Message: ");
                    var message = Console.ReadLine();

                    while (message.Length > 250)
                    {
                        Console.WriteLine("Message too long!");
                        Console.Write("Message: ");
                        receiver = Console.ReadLine();
                    }

                    dBManager.InsertMessage(user.Username, receiver, message);

                    using (StreamWriter writetext = File.AppendText("myfile.txt"))
                    {
                        writetext.WriteLine(user.Username + " " + receiver + " " + message + " " + DateTime.Now.ToString("dd/MM/yyyy"));
                    }

                    break;

                case Option.CreateUser:
                    Console.Write("Username: ");
                    var username = Console.ReadLine();

                    while (dBManager.CheckUser(username))
                    {
                        Console.WriteLine("User already exists!");
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                    }

                    Console.Write("Password: ");
                    var password = Console.ReadLine();

                    Console.Write("Name: ");
                    var name = Console.ReadLine();

                    Console.Write("Surname: ");
                    var surname = Console.ReadLine();

                    Console.WriteLine("Admin              - 1");
                    Console.WriteLine("View               - 2");
                    Console.WriteLine("View, Edit         - 3");
                    Console.WriteLine("View, Edit, Delete - 4");
                    Console.WriteLine("Privileges: ");

                    var input = Console.ReadLine();

                    var valid = false;
                    var privileges = 0;

                    while (!valid)
                    {
                        if (!int.TryParse(input, out privileges))
                        {
                            Console.WriteLine("Wrong input!");
                            Console.WriteLine("Privileges: ");
                            Console.WriteLine("Admin              - 1");
                            Console.WriteLine("View               - 2");
                            Console.WriteLine("View, Edit         - 3");
                            Console.WriteLine("View, Edit, Delete - 4");
                            Console.WriteLine("Privileges: ");

                            input = Console.ReadLine();
                        }
                        else if (privileges > 0 && privileges < 5)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input!");
                            Console.WriteLine("Admin              - 1");
                            Console.WriteLine("View               - 2");
                            Console.WriteLine("View, Edit         - 3");
                            Console.WriteLine("View, Edit, Delete - 4");
                            Console.WriteLine("Privileges: ");
                            input = Console.ReadLine();
                        }
                    }

                    dBManager.InsertUser(username, name, surname, password, privileges - 1);
                    while (!dBManager.CheckUser(username))
                    {
                        Console.WriteLine("User already exists!");
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                    }
                    break;

                case Option.ViewUsers:
                    dBManager.ViewUsers();
                    break;

                case Option.ViewData:
                    Console.Write("User1: ");
                    var user1 = Console.ReadLine();
                    while (!dBManager.CheckUser(user1))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User1: ");
                        user1 = Console.ReadLine();
                    }

                    Console.Write("User2: ");
                    var user2 = Console.ReadLine();
                    while (!dBManager.CheckUser(user2))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User2: ");
                        user2 = Console.ReadLine();
                    }

                    dBManager.ViewData(user1, user2);

                    break;

                case Option.UpdateUser:
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    while (!dBManager.CheckUser(username) || username == "admin")
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                    }

                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    Console.WriteLine("Admin              - 1");
                    Console.WriteLine("View               - 2");
                    Console.WriteLine("View, Edit         - 3");
                    Console.WriteLine("View, Edit, Delete - 4");
                    Console.WriteLine("Privileges: ");

                    input = Console.ReadLine();

                    valid = false;
                    privileges = 0;

                    while (!valid)
                    {
                        if (!int.TryParse(input, out privileges))
                        {
                            Console.WriteLine("Wrong input!");
                            Console.WriteLine("Privileges: ");
                            Console.WriteLine("Admin              - 1");
                            Console.WriteLine("View               - 2");
                            Console.WriteLine("View, Edit         - 3");
                            Console.WriteLine("View, Edit, Delete - 4");
                            Console.WriteLine("Privileges: ");

                            input = Console.ReadLine();
                        }
                        else if (privileges > 0 && privileges < 5)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input!");
                            Console.WriteLine("Admin              - 1");
                            Console.WriteLine("View               - 2");
                            Console.WriteLine("View, Edit         - 3");
                            Console.WriteLine("View, Edit, Delete - 4");
                            Console.WriteLine("Privileges: ");
                            input = Console.ReadLine();
                        }
                    }

                    dBManager.UpdateUser(username, password, privileges - 1);

                    break;

                case Option.UpdateData:
                    Console.Write("User1: ");
                    user1 = Console.ReadLine();
                    while (!dBManager.CheckUser(user1))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User1: ");
                        user1 = Console.ReadLine();
                    }

                    Console.Write("User2: ");
                    user2 = Console.ReadLine();
                    while (!dBManager.CheckUser(user2))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User2: ");
                        user2 = Console.ReadLine();
                    }

                    dBManager.ViewData(user1, user2);
                    Console.Write("Choose message by id: ");
                    var id = Console.ReadLine();

                    Console.Write("New message: ");
                    message = Console.ReadLine();

                    dBManager.UpdateMessage(id, message);

                    break;

                case Option.DeleteUser:
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    while (!dBManager.CheckUser(username) || username == "admin")
                    {
                        Console.WriteLine("Username does not exist!");
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                    }

                    dBManager.DeleteUser(username);
                    
                    break;

                case Option.DeleteData:
                    Console.Write("User1: ");
                    user1 = Console.ReadLine();
                    while (!dBManager.CheckUser(user1))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User1: ");
                        user1 = Console.ReadLine();
                    }

                    Console.Write("User2: ");
                    user2 = Console.ReadLine();
                    while (!dBManager.CheckUser(user2))
                    {
                        Console.WriteLine("User does not exist!");
                        Console.Write("User2: ");
                        user2 = Console.ReadLine();
                    }

                    dBManager.ViewData(user1, user2);
                    Console.Write("Choose message by id: ");
                    id = Console.ReadLine();

                    dBManager.DeleteMessage(id);
                    break;

                default:
                    break;
            }

        }
    }
}
