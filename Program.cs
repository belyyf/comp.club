class Program
{
    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("== меню компьютерного клуба ==");
            Console.WriteLine("1. управление клиентами");
            Console.WriteLine("2. управление администраторами");
            Console.WriteLine("3. управление играми");
            Console.WriteLine("4. управление сеансами");
            Console.WriteLine("5. управление платежами");
            Console.WriteLine("6. управление расписанием");
            Console.WriteLine("0. выход");
            Console.Write("выберите действие:");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ManageClients();
                    break;
                case "2":
                    ManageAdmins();
                    break;
                case "3":
                    ManageGames();
                    break;
                case "4":
                    ManageSessions();
                    break;
                case "5":
                    ManagePayments();
                    break;
                case "6":
                    ManageShedule();
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("выход из программы");
                    break;
                default:
                    Console.WriteLine("неккоретный ввод");
                    Pause();
                    break;
            }
        }
    }

    static void ManageClients()
    {
        Console.Clear();
        Console.WriteLine("управление клентами");
        Pause();
    }

    static void ManageAdmins()
    {
        Console.Clear();
        Console.WriteLine("управление админами");
        Pause();
    }

    static void ManageGames()
    {
        Console.Clear();
        Console.WriteLine("управление играми");
        Pause();
    }

    static void ManageSessions()
    {
        Console.Clear();
        Console.WriteLine("управление сеансами");
        Pause();
    }

    static void ManagePayments()
    {
        Console.Clear();
        Console.WriteLine("управление платежами");
        Pause();
    }

    static void ManageShedule()
    {
        Console.Clear();
        Console.WriteLine("управление расписанием");
    }

    static void Pause()
    {
        Console.WriteLine("нажмите на любую кнопку для продолжение");
        Console.ReadLine();
    }
}
