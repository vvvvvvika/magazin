using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Admin : AccountPost
    {
        public Admin(List<AccountPost> list , AccountPost ad)
        {
            DrewInterface(list,ad);
        } 
        protected override void HeaderMenu<T>(IList<T> list, AccountPost account) 
        {
            base.HeaderMenu(list, account);
            foreach(var element in (List<AccountPost>)list)
            {
                Console.WriteLine("  ID: " + element.ID + " Логин: " + element.Login + " Пароль: " + element.Password + " Роль: " + DrewPostWorker(element.Post));
            }
        }
        public override IList<T> Delete<T>(IList<T> list, int x)
        {
            list.RemoveAt(x);
            JsonConvertDS.Serialize("AccountPost.json", list.ToList());
            return list;
        }
        public override IList<T> Read<T>(IList<T> list,T account)
        {
            Console.WriteLine("  ID: " + (account as AccountPost).ID);
            Console.WriteLine("  Name: " + ((account as AccountPost).Name = (account as AccountPost).Name ?? "null"));
            Console.WriteLine("  Login:  " + (account as AccountPost).Login);
            Console.WriteLine("  Password: " + (account as AccountPost).Password);
            Console.WriteLine("  Post: " + (account as AccountPost).Post);
            Console.WriteLine("  Connection: " + (account as AccountPost).Connection);
            return Update(list,account);
        }
        public override IList<T> Update<T>(IList<T> list, T account)
        {
            while (true)
            {
                ConsoleKeyInfo key = ArrowMenu(0, 5);
                if (key.Key == (ConsoleKey)HotKey.Назад)
                {
                    Console.CursorVisible = false;
                    break;
                }
                else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            (account as AccountPost).ID = Convert.ToInt32(RightInt(6, 0, 7));
                            foreach (var element in (List<AccountPost>)list)
                            {
                                if (element != (account as AccountPost) && element.ID == (account as AccountPost).ID)
                                {
                                    error = true;
                                    break;
                                }
                            }
                        } while (error);
                    }
                    else if (Console.CursorTop == 3)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(10, 2);
                            (account as AccountPost).Login = Console.ReadLine();
                            if (!string.IsNullOrEmpty((account as AccountPost).Login))
                            {
                                foreach (var element in (List<AccountPost>)list)
                                {
                                    if (element != (account as AccountPost) && element.Login == (account as AccountPost).Login)
                                    {
                                        error = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                error = true;
                            }
                        } while (error);
                    }
                    else if (Console.CursorTop == 4)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(12, 3);
                            (account as AccountPost).Password = Console.ReadLine();
                            if (string.IsNullOrEmpty((account as AccountPost).Password))
                            {
                                error = true;
                            }
                        } while (error);
                        Console.SetCursorPosition(12, 2);
                    }
                    else if (Console.CursorTop == 5)
                    {
                        while (true)
                        {
                            (account as AccountPost).Post = Convert.ToInt32(RightInt(8, 4, 9));
                            if (DrewPostWorker((account as AccountPost).Post) != "null")
                            {
                                break;
                            }
                        }
                    }
                }
                Console.Clear();
                Console.WriteLine("  ID: " + (account as AccountPost).ID);
                Console.WriteLine("  Name: " + ((account as AccountPost).Name = (account as AccountPost).Name == null ? "null" : (account as AccountPost).Name));
                Console.WriteLine("  Login:  " + (account as AccountPost).Login);
                Console.WriteLine("  Password: " + (account as AccountPost).Password);
                Console.WriteLine("  Post: " + (account as AccountPost).Post);
                Console.WriteLine("  Connection: " + (account as AccountPost).Connection);
            }
            JsonConvertDS.Serialize("AccountPost.json", list);
            return list;
        }
        public override IList<T> Create<T>(IList<T> list)
        {
            AccountPost ap = new AccountPost
            {
                ID = list.Count()
            };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("  ID: " + ap.ID);
                Console.WriteLine("  Login: " + ap.Login);
                Console.WriteLine("  Password: " + ap.Password);
                Console.WriteLine("  Post: " + ap.Post);
                ConsoleKeyInfo key = ArrowMenu(0, 3);
                if (key.Key == (ConsoleKey)HotKey.Назад)
                {
                    Console.CursorVisible = false;
                    break;
                }
                else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            ap.ID = Convert.ToInt32(RightInt(6, 0, 7));
                            foreach (var element in (List<AccountPost>)list)
                            {
                                if (element.ID == ap.ID)
                                {
                                    error = true;
                                    break;
                                }
                            }
                        } while (error);
                    }
                    else if (Console.CursorTop == 2)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(10, 1);
                            ap.Login = Console.ReadLine();
                            if (!string.IsNullOrEmpty(ap.Login))
                            {
                                foreach (var element in (List<AccountPost>)list)
                                {
                                    if (element.Login == ap.Login)
                                    {
                                        error = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                error = true;
                            }
                        } while (error);
                    }
                    else if (Console.CursorTop == 3)
                    {
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(12, 2);
                            ap.Password = Console.ReadLine();
                            if (string.IsNullOrEmpty(ap.Password))
                            {
                                error = true;
                            }
                        } while (error);
                        Console.SetCursorPosition(12, 2);
                    }
                    else
                    {
                        while (true)
                        {
                            ap.Post = Convert.ToInt32(RightInt(8, 3, 9));
                            if (DrewPostWorker(ap.Post) != "null")
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (ap.Login != null && ap.Password != null)
            {
                list.Add((T)Convert.ChangeType(ap, typeof(T)));
                JsonConvertDS.Serialize("AccountPost.json", list);
            }
            return list;
        }
    }
}
