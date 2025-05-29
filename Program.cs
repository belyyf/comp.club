using System;
using ComputerClubApp.Services;

namespace ComputerClubApp
{
    class Program
    {
        static void Main()
        {
            var clientService = new ClientService();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить клиента");
                Console.WriteLine("2. Показать всех клиентов");
                Console.WriteLine("3. Обновить клиента");
                Console.WriteLine("4. Удалить клиента");
                Console.WriteLine("5. Выход");
                Console.Write("Выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Имя клиента: ");
                        string name = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        clientService.CreateClient(name, email);
                        break;

                    case "2":
                        clientService.ShowAllClients();
                        break;

                    case "3":
                        Console.Write("Id клиента: ");
                        int idUpd = int.Parse(Console.ReadLine());
                        Console.Write("Новое имя: ");
                        string nameUpd = Console.ReadLine();
                        Console.Write("Новый email: ");
                        string emailUpd = Console.ReadLine();
                        clientService.UpdateClient(idUpd, nameUpd, emailUpd);
                        break;

                    case "4":
                        Console.Write("Id клиента: ");
                        int idDel = int.Parse(Console.ReadLine());
                        clientService.DeleteClient(idDel);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }
    }
}
