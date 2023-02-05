using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    internal enum HotKey
    {
        Выбрать = ConsoleKey.Enter,
        Стереть = ConsoleKey.Backspace,
        Вверх = ConsoleKey.UpArrow,
        Вниз = ConsoleKey.DownArrow,
        Назад = ConsoleKey.Escape,
        Создать = ConsoleKey.F1,
        Удалить = ConsoleKey.Delete
    }
    internal enum Roly
    {
        Администратор,
        Кассир,
        Склад,
        Менеджер,
        Бухгалтер
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonConvertDS.SearchJsonFiles();
            while (true)
            {
                bool Error = true;
                AccountPost ap = new AccountPost();
                Console.CursorVisible = true;
                Console.WriteLine("Добро пожаловать!");
                Console.WriteLine("Логин: ");
                Console.WriteLine("Пароль: ");
                Console.SetCursorPosition(7, 1);
                string login = Console.ReadLine();
                string passwod = ReadPassword(9, 2);
                foreach (var element in JsonConvertDS.Deserialize<List<AccountPost>>("AccountPost.json"))
                {
                    if (element.Login == login && element.Password == passwod)
                    {
                        ap = element;
                        Error = false;
                        break;
                    }
                }
                if (Error)
                {
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Ошибка: неверный логин или пароль");
                    Console.WriteLine("Чтобы повторить попытку нажмите любую клавишу, или Escape, чтобы выйти");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Назад) break;
                    Console.Clear();
                }
                else
                {
                    if (ap.Post == (int)Roly.Администратор)
                    {
                        Admin ad = new Admin(JsonConvertDS.Deserialize<List<AccountPost>>("AccountPost.json"), ap);
                    }
                    else if (ap.Post == (int)Roly.Менеджер)
                    {
                        Manager mg = new Manager(JsonConvertDS.Deserialize<List<Staff>>("Staff.json"), ap);
                    }
                    else if (ap.Post == (int)Roly.Бухгалтер)
                    {
                        Buh buh = new Buh(JsonConvertDS.Deserialize<List<Recording>>("Recording.json"), ap);
                    }
                    else if (ap.Post == (int)Roly.Склад)
                    {
                        Sclad sclad = new Sclad(JsonConvertDS.Deserialize<List<Product>>("Product.json"), ap);
                    }
                    else if (ap.Post == (int)Roly.Кассир)
                    {
                        Kassir kassir = new Kassir(JsonConvertDS.Deserialize<List<Product>>("Product.json"), ap);
                    }
                }
            }
        }
        private static string ReadPassword(int x, int y)
        {
            List<char> pass = new List<Char>();
            string password = "";
            while (true)
            {
                Console.SetCursorPosition(x, y);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    break;
                }
                else if (key.Key == (ConsoleKey)HotKey.Стереть)
                {
                    if (pass.Count > 0)
                    {
                        pass.RemoveAt(x - 10);
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x + 1, y);
                        x--;
                    }
                }
                else
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("*");
                    pass.Add(key.KeyChar);
                    x++;
                }
            }
            foreach (var element in pass)
            {
                password += element;
            }
            return password;
        }
    }
}