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

    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=comp;Username=postgres;Password=1234");
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
                Console.WriteLine("7. Выход");
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
                                Console.WriteLine($"{a.Id}: {a.Name} ({a.Role})");
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
                            return;

                        default:
                            Console.WriteLine("Неверный ввод.");
                            break;
                    }
                }

                // обработка ошибки
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
