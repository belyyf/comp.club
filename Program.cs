using System;
using System.Collections.Generic;
using System.Linq;

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ComputerName { get; set; } = null;
    public decimal? Payment { get; set; }
}

class Computer
{
    public string Name { get; set; }
    public decimal PricePerHour { get; set; }
}

class Program
{
    static List<Client> clients = new List<Client>();
    static List<Computer> computers = new List<Computer>
    {
        new Computer { Name = "ПК-1", PricePerHour = 150 },
        new Computer { Name = "ПК-2", PricePerHour = 200 },
        new Computer { Name = "ПК-3", PricePerHour = 180 }
    };

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
            Console.WriteLine("5. Назначить клиента к компьютеру и рассчитать платёж");
            Console.WriteLine("6. Просмотреть компьютеры");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите пункт: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": AddClient(); break;
                case "2": ViewClients(); break;
                case "3": UpdateClient(); break;
                case "4": DeleteClient(); break;
                case "5": AssignComputer(); break;
                case "6": ViewComputers(); break;
                case "7": return;
                default: Console.WriteLine("Неверный ввод."); break;
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
            string comp = c.ComputerName != null ? $" | Компьютер: {c.ComputerName}" : " | Без компьютера";
            string pay = c.Payment != null ? $" | Платёж: {c.Payment}₽" : "";
            Console.WriteLine($"{c.Id}: {c.Name}{comp}{pay}");
        }
    }

    static void UpdateClient()
    {
        Console.Write("Введите ID клиента: ");
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
        if (client == null)
        {
            Console.WriteLine("Клиент не найден.");
            return;
        }

        Console.WriteLine("Доступные компьютеры:");
        for (int i = 0; i < computers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {computers[i].Name} — {computers[i].PricePerHour}₽/час");
        }

        Console.Write("Выберите номер компьютера: ");
        if (!int.TryParse(Console.ReadLine(), out int compIndex) || compIndex < 1 || compIndex > computers.Count)
        {
            Console.WriteLine("Неверный выбор.");
            return;
        }

        var selectedComputer = computers[compIndex - 1];

        Console.Write("Сколько часов будет использоваться компьютер? ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal hours) || hours <= 0)
        {
            Console.WriteLine("Некорректное количество часов.");
            return;
        }

        client.ComputerName = selectedComputer.Name;
        client.Payment = selectedComputer.PricePerHour * hours;

        Console.WriteLine($"Клиент {client.Name} назначен к компьютеру {selectedComputer.Name}. Платёж: {client.Payment}₽");
    }

    static void ViewComputers()
    {
        Console.WriteLine("Список компьютеров:");
        foreach (var c in computers)
        {
            Console.WriteLine($"• {c.Name} — {c.PricePerHour}₽/час");
        }
    }
}
