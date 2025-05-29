using ComputerClubApp.Models;
using ComputerClubApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClubApp.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;

        public ClientService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated(); 
        }

        public void CreateClient(string name, string email)
        {
            try
            {
                var client = new Client { Name = name, Email = email };
                _context.Clients.Add(client);
                _context.SaveChanges();
                Console.WriteLine("Клиент добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении клиента: {ex.Message}");
            }
        }

        public void ShowAllClients()
        {
            try
            {
                var clients = _context.Clients.ToList();
                foreach (var client in clients)
                    Console.WriteLine($"{client.Id}: {client.Name} | {client.Email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выводе клиентов: {ex.Message}");
            }
        }

        public void UpdateClient(int id, string name, string email)
        {
            try
            {
                var client = _context.Clients.Find(id);
                if (client == null)
                {
                    Console.WriteLine("Клиент не найден.");
                    return;
                }

                client.Name = name;
                client.Email = email;
                _context.SaveChanges();
                Console.WriteLine("Клиент обновлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении клиента: {ex.Message}");
            }
        }

        public void DeleteClient(int id)
        {
            try
            {
                var client = _context.Clients.Find(id);
                if (client == null)
                {
                    Console.WriteLine("Клиент не найден.");
                    return;
                }

                _context.Clients.Remove(client);
                _context.SaveChanges();
                Console.WriteLine("Клиент удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении клиента: {ex.Message}");
            }
        }
    }
}
