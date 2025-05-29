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

    public class Session
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
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

        // Навигационные свойства (опционально)
        public Client Client { get; set; }
        public Session Session { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Client> Clients { get; set; }
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
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить админа");
                Console.WriteLine("2. Показать всех админов");
                Console.WriteLine("3. Обновить админа");
                Console.WriteLine("4. Удалить админа");
                Console.WriteLine("5. Добавить сеанс");
                Console.WriteLine("6. Показать все сеансы");
                Console.WriteLine("7. Добавить клиента");
                Console.WriteLine("8. Показать всех клиентов");
                Console.WriteLine("9. Добавить игру");
                Console.WriteLine("10. Показать все игры");
                Console.WriteLine("11. Добавить компьютер");
                Console.WriteLine("12. Показать все компьютеры");
                Console.WriteLine("13. Добавить расписание");
                Console.WriteLine("14. Показать все расписания");
                Console.WriteLine("15. Выход");
                Console.Write("Выбор: ");
                string input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.Write("Имя админа: ");
                            string name = Console.ReadLine();
                            Console.Write("Роль: ");
                            string role = Console.ReadLine();
                            db.Admins.Add(new Admin { Name = name, Role = role });
                            db.SaveChanges();
                            Console.WriteLine("Админ добавлен.");
                            break;

                        case "2":
                            var admins = db.Admins.ToList();
                            foreach (var a in admins)
                                Console.
                                    WriteLine($"{a.Id}: {a.Name} ({a.Role})");
                            break;

                        case "3":
                            Console.Write("Id админа для обновления: ");
                            int updateId = int.Parse(Console.ReadLine());
                            var admin = db.Admins.Find(updateId);
                            if (admin != null)
                            {
                                Console.Write("Новое имя: ");
                                admin.Name = Console.ReadLine();
                                Console.Write("Новая роль: ");
                                admin.Role = Console.ReadLine();
                                db.SaveChanges();
                                Console.WriteLine("Админ обновлен.");
                            }
                            else
                                Console.WriteLine("Админ не найден.");
                            break;

                        case "4":
                            Console.Write("Id админа для удаления: ");
                            int deleteId = int.Parse(Console.ReadLine());
                            var delAdmin = db.Admins.Find(deleteId);
                            if (delAdmin != null)
                            {
                                db.Admins.Remove(delAdmin);
                                db.SaveChanges();
                                Console.WriteLine("Админ удален.");
                            }
                            else
                                Console.WriteLine("Админ не найден.");
                            break;

                        case "5":
                            Console.Write("Имя клиента: ");
                            string client = Console.ReadLine();
                            Console.Write("Время начала (yyyy-mm-dd hh:mm): ");
                            DateTime start = DateTime.Parse(Console.ReadLine());
                            Console.Write("Длительность (мин): ");
                            int minutes = int.Parse(Console.ReadLine());
                            db.Sessions.Add(new Session { ClientName = client, StartTime = start, DurationMinutes = minutes });
                            db.SaveChanges();
                            Console.WriteLine("Сеанс добавлен.");
                            break;

                        case "6":
                            var sessions = db.Sessions.ToList();
                            foreach (var s in sessions)
                                Console.WriteLine($"{s.Id}: {s.ClientName} | {s.StartTime} | {s.DurationMinutes} мин");
                            break;

                        case "7":
                            Console.Write("ФИО клиента: ");
                            string fullName = Console.ReadLine();
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Телефон: ");
                            string phone = Console.ReadLine();
                            db.Clients.Add(new Client { FullName = fullName, Email = email, Phone = phone });
                            db.SaveChanges();
                            Console.WriteLine("Клиент добавлен.");
                            break;

                        case "8":
                            var clients = db.Clients.ToList();
                            foreach (var c in clients)
                                Console.WriteLine($"{c.Id}: {c.FullName}, Email: {c.Email}, Телефон: {c.Phone}");
                            break;

                        case "9":
                            Console.Write("Название игры: ");
                            string title = Console.ReadLine();
                            Console.Write("Жанр: ");
                            string genre = Console.ReadLine();
                            Console.Write("Рейтинг: ");
                            decimal rating = decimal.Parse(Console.ReadLine());
                            db.Games.
                                Add(new Game { Title = title, Genre = genre, Rating = rating });
                            db.SaveChanges();
                            Console.WriteLine("Игра добавлена.");
                            break;

                        case "10":
                            var games = db.Games.ToList();
                            foreach (var g in games)
                                Console.WriteLine($"{g.Id}: {g.Title} | {g.Genre} | Рейтинг: {g.Rating}");
                            break;

                        case "11":
                            Console.Write("Имя компьютера: ");
                            string compName = Console.ReadLine();
                            Console.Write("Характеристики: ");
                            string specs = Console.ReadLine();
                            Console.Write("Доступен (true/false): ");
                            bool isAvailable = bool.Parse(Console.ReadLine());
                            db.Computers.Add(new Computer { Name = compName, Specs = specs, IsAvailable = isAvailable });
                            db.SaveChanges();
                            Console.WriteLine("Компьютер добавлен.");
                            break;

                        case "12":
                            var computers = db.Computers.ToList();
                            foreach (var comp in computers)
                                Console.WriteLine($"{comp.Id}: {comp.Name} | {comp.Specs} | Доступен: {comp.IsAvailable}");
                            break;

                        case "13":
                            Console.Write("Id клиента: ");
                            int clientId = int.Parse(Console.ReadLine());
                            Console.Write("Id сеанса: ");
                            int sessionId = int.Parse(Console.ReadLine());
                            Console.Write("Время начала (yyyy-mm-dd hh:mm): ");
                            DateTime schStart = DateTime.Parse(Console.ReadLine());
                            Console.Write("Время окончания (yyyy-mm-dd hh:mm): ");
                            DateTime schEnd = DateTime.Parse(Console.ReadLine());

                            db.Schedules.Add(new Schedule
                            {
                                ClientId = clientId,
                                SessionId = sessionId,
                                StartTime = schStart,
                                EndTime = schEnd
                            });
                            db.SaveChanges();
                            Console.WriteLine("Расписание добавлено.");
                            break;

                        case "14":
                            var schedules = db.Schedules
                                .Include(s => s.Client)
                                .Include(s => s.Session)
                                .ToList();
                            foreach (var sch in schedules)
                            {
                                Console.WriteLine($"{sch.Id}: Клиент {sch.ClientId}, Сеанс {sch.SessionId}, Начало: {sch.StartTime}, Конец: {sch.EndTime}");
                            }
                            break;

                        case "15":
                            return;

                        default:
                            Console.WriteLine("Неверный ввод.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
