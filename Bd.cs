using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComputerClubApp
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Session
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
    }

    public class Computer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specs { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Client Client { get; set; }
        public Session Session { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=compclub;Username=postgres;Password=1234");
        }
    }

    class Program
    {
        static void Main()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            while (true)
            {
                Console.WriteLine("\nГлавное меню:");
                Console.WriteLine("1. Работа с Админами");
                Console.WriteLine("2. Работа с Клиентами");
                Console.WriteLine("3. Работа с Сеансами");
                Console.WriteLine("4. Работа с Играми");
                Console.WriteLine("5. Работа с Компьютерами");
                Console.WriteLine("6. Работа с Расписанием");
                Console.WriteLine("7. Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AdminMenu(db); break;
                        case "2": ClientMenu(db); break;
                        case "3": SessionMenu(db); break;
                        case "4": GameMenu(db); break;
                        case "5": ComputerMenu(db); break;
                        case "6": ScheduleMenu(db); break;
                        case "7": return;
                        default: Console.WriteLine("Неверный ввод."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static void AdminMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Админы ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.WriteLine("3. Обновить");
            Console.WriteLine("4. Удалить");
            Console.WriteLine("5. Удалить всех");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();
                    if (db.Admins.Any(a => a.
                        Name == name))
                    {
                        Console.WriteLine("Такой админ уже существует.");
                        return;
                    }
                    Console.Write("Роль: ");
                    string role = Console.ReadLine();
                    db.Admins.Add(new Admin { Name = name, Role = role });
                    db.SaveChanges();
                    Console.WriteLine("Админ добавлен.");
                    break;
                case "2":
                    db.Admins.ToList().ForEach(a => Console.WriteLine($"{a.Id}: {a.Name} ({a.Role})"));
                    break;
                case "3":
                    Console.Write("Id: ");
                    int id = int.Parse(Console.ReadLine());
                    var admin = db.Admins.Find(id);
                    if (admin != null)
                    {
                        Console.Write("Новое имя: ");
                        admin.Name = Console.ReadLine();
                        Console.Write("Новая роль: ");
                        admin.Role = Console.ReadLine();
                        db.SaveChanges();
                        Console.WriteLine("Обновлено.");
                    }
                    else Console.WriteLine("Не найден.");
                    break;
                case "4":
                    Console.Write("Id: ");
                    int delId = int.Parse(Console.ReadLine());
                    var del = db.Admins.Find(delId);
                    if (del != null) { db.Admins.Remove(del); db.SaveChanges(); Console.WriteLine("Удалено."); }
                    else Console.WriteLine("Не найден.");
                    break;
                case "5":
                    db.Admins.RemoveRange(db.Admins);
                    db.SaveChanges();
                    Console.WriteLine("Все удалены.");
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }

        static void ClientMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Клиенты ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.WriteLine("3. Удалить всех");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Имя клиента: ");
                    string name = Console.ReadLine();
                    if (db.Clients.Any(c => c.Name == name))
                    {
                        Console.WriteLine("Клиент уже существует.");
                        return;
                    }
                    db.Clients.Add(new Client { Name = name });
                    db.SaveChanges();
                    Console.WriteLine("Клиент добавлен.");
                    break;
                case "2":
                    db.Clients.ToList().ForEach(c => Console.WriteLine($"{c.Id}: {c.Name}"));
                    break;
                case "3":
                    db.Clients.RemoveRange(db.Clients);
                    db.SaveChanges();
                    Console.WriteLine("Все клиенты удалены.");
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }

        static void SessionMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Сеансы ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Имя клиента: ");
                    string cname = Console.ReadLine();
                    Console.Write("Начало: ");
                    DateTime start = DateTime.Parse(Console.ReadLine());
                    Console.Write("Длительность: ");
                    int dur = int.Parse(Console.ReadLine());
                    db.Sessions.
                        Add(new Session { ClientName = cname, StartTime = start, DurationMinutes = dur });
                    db.SaveChanges();
                    Console.WriteLine("Сеанс добавлен.");
                    break;
                case "2":
                    db.Sessions.ToList().ForEach(s =>
                        Console.WriteLine($"{s.Id}: {s.ClientName} | {s.StartTime} | {s.DurationMinutes} мин"));
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }

        static void GameMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Игры ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Название: ");
                    string title = Console.ReadLine();
                    Console.Write("Жанр: ");
                    string genre = Console.ReadLine();
                    Console.Write("Рейтинг: ");
                    decimal rating = decimal.Parse(Console.ReadLine());
                    db.Games.Add(new Game { Title = title, Genre = genre, Rating = rating });
                    db.SaveChanges();
                    Console.WriteLine("Игра добавлена.");
                    break;
                case "2":
                    db.Games.ToList().ForEach(g => Console.WriteLine($"{g.Id}: {g.Title} | {g.Genre} | {g.Rating}"));
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }

        static void ComputerMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Компьютеры ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Характеристики: ");
                    string specs = Console.ReadLine();
                    Console.Write("Доступен (true/false): ");
                    bool isAvailable = bool.Parse(Console.ReadLine());
                    db.Computers.Add(new Computer { Name = name, Specs = specs, IsAvailable = isAvailable });
                    db.SaveChanges();
                    Console.WriteLine("Добавлен.");
                    break;
                case "2":
                    db.Computers.ToList().ForEach(c =>
                        Console.WriteLine($"{c.Id}: {c.Name} | {c.Specs} | Доступен: {c.IsAvailable}"));
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }

        static void ScheduleMenu(AppDbContext db)
        {
            Console.WriteLine("\n--- Расписание ---");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Показать");
            Console.Write("Выбор: ");
            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Id клиента: ");
                    int clientId = int.Parse(Console.ReadLine());
                    Console.Write("Id сеанса: ");
                    int sessionId = int.Parse(Console.ReadLine());
                    Console.Write("Начало: ");
                    DateTime start = DateTime.Parse(Console.ReadLine());
                    Console.Write("Конец: ");
                    DateTime end = DateTime.Parse(Console.ReadLine());
                    db.Schedules.Add(new Schedule
                    {
                        ClientId = clientId,
                        SessionId = sessionId,
                        StartTime = start,
                        EndTime = end
                    });
                    db.SaveChanges();
                    Console.WriteLine("Добавлено.");
                    break;
                case "2":
                    db.Schedules.Include(s => s.Client).Include(s => s.Session).ToList().ForEach(s =>
                        Console.WriteLine($"{s.Id}: Клиент {s.ClientId}, Сеанс {s.SessionId}, {s.StartTime} - {s.EndTime}"));
                    break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }
    }
}
