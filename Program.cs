using System;
using System.Collections.Generic;
using System.Linq;

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ComputerName { get; set; } = null;
}

class Program
{
    static List<Client> clients = new List<Client>();
    static int nextId = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Добавить клиента");
            Console.WriteLine("2. Просмотреть клиентов");
            Console.WriteLine("3. Обновить имя клиента");
            Console.WriteLine("4. Удалить клиента");
            Console.WriteLine("5. Назначить клиента на сеанс к компьютеру");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите пункт: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddClient();
                    break;
                case "2":
                    ViewClients();
                    break;
                case "3":
                    UpdateClient();
                    break;
                case "4":
                    DeleteClient();
                    break;
                case "5":
                    AssignComputer();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    static void AddClient()
    {
        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine().Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Имя не может быть пустым.");
            return;
        }

        if (clients.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Клиент с таким именем уже существует.");
            return;
        }

        clients.Add(new Client { Id = nextId++, Name = name });
        Console.WriteLine("Клиент добавлен.");
    }

    static void ViewClients()
    {
        if (!clients.Any())
        {
            Console.WriteLine("Список клиентов пуст.");
            return;
        }

        foreach (var c in clients)
        {
            string comp = c.ComputerName != null ? $" | Назначен к: {c.ComputerName}" : " | Не назначен к компьютеру";
            Console.WriteLine($"{c.Id}: {c.Name}{comp}");
        }
    }

    static void UpdateClient()
    {
        Console.Write("Введите ID клиента для изменения имени: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                Console.Write("Введите новое имя: ");
                string newName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("Имя не может быть пустым.");
                    return;
                }

                if (clients.Any(c => c.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && c.Id != id))
                {
                    Console.WriteLine("Имя уже занято другим клиентом.");
                    return;
                }

                client.Name = newName;
                Console.WriteLine("Имя клиента обновлено.");
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID.");
        }
    }

    static void DeleteClient()
    {
        Console.Write("Введите имя клиента для удаления: ");
        string nameToDelete = Console.ReadLine().Trim();

        var client = clients.FirstOrDefault(c => c.Name.Equals(nameToDelete, StringComparison.OrdinalIgnoreCase));
        if (client != null)
        {
            clients.Remove(client);
            Console.WriteLine("Клиент удалён.");
        }
        else
        {
            Console.WriteLine("Клиент с таким именем не найден.");
        }
    }

    static void AssignComputer()
    {
        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine().Trim();

        var client = clients.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (client != null)
        {
            Console.Write("Введите имя компьютера: ");
            string compName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(compName))
            {
                Console.WriteLine("Имя компьютера не может быть пустым.");
                return;
            }

            client.ComputerName = compName;
            Console.WriteLine($"Клиент {client.Name} назначен на компьютер {compName}.");
        }
        else
        {
            Console.WriteLine("Клиент не найден.");
        }
    }
}
