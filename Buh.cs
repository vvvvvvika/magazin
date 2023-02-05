using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Buh : AccountPost
    {
        public Buh(List<Recording> list, AccountPost account)
        {
            list = list == null ? new List<Recording>() : list;
            DrewInterface(list, account);
        }
        protected override void HeaderMenu<T>(IList<T> list, AccountPost account)
        {
            base.HeaderMenu(list, account);
            double price = 0;
            foreach (var element in (List<Recording>)list)
            {
                Console.WriteLine(" ID: " + element.ID + " Название: " + element.Title + " Цена " + element.Price + " Время записи: " + element.Date + " Прибаква?: " + element.Add);
                if (element.Add)
                {
                    price += element.Price;
                }
                else
                {
                    price -= element.Price;
                }
            }
            Console.WriteLine("Итог: " + price);
        }
        public override IList<T> Delete<T>(IList<T> list, int x)
        {
            list.RemoveAt(x);
            JsonConvertDS.Serialize("Recording.json", list);
            return list;
        }
        public override IList<T> Read<T>(IList<T> list, T account)
        {
            Console.Clear();
            Console.WriteLine("  ID: " + (account as Recording).ID);
            Console.WriteLine("  Название: " + (account as Recording).Title);
            Console.WriteLine("  Цена: " + (account as Recording).Price);
            Console.WriteLine("  Дата: " + (account as Recording).Date);
            Console.WriteLine("  Прибавка?: " + (account as Recording).Add);
            return Update(list,account);
        }
        public override IList<T> Update<T>(IList<T> list, T account)
        {
            while (true)
            {
                Console.CursorVisible = false;
                ConsoleKeyInfo key = ArrowMenu(0, 4);
                if (key.Key == (ConsoleKey)HotKey.Назад)
                {
                    break;
                }
                else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        int ID;
                        bool error = true;
                        do
                        {
                            error = false;
                            ID = Convert.ToInt32(RightInt(5, 0,6));
                            foreach (var element in (List<Recording>)list)
                            {
                                if(element.ID == ID)
                                {
                                    error = true;
                                    break;
                                }
                            }
                        } while (error);
                        (account as Recording).ID = ID;
                    }
                    else if (Console.CursorTop == 2)
                    {
                        Console.SetCursorPosition(12, 1);
                        (account as Recording).Title = Console.ReadLine();
                    }
                    else if (Console.CursorTop == 3)
                    {
                        (account as Recording).Price = Convert.ToInt32(RightInt(7, 2, 8));
                    }else if (Console.CursorTop == 5)
                    {
                        (account as Recording).Add = (account as Recording).Add == false ? true : false;
                    }
                }
                Console.Clear();
                Console.WriteLine("  ID: " + (account as Recording).ID);
                Console.WriteLine("  Название: " + (account as Recording).Title);
                Console.WriteLine("  Цена: " + (account as Recording).Price);
                Console.WriteLine("  Дата: " + (account as Recording).Date);
                Console.WriteLine("  Прибавка?: " + (account as Recording).Add);
            }
            JsonConvertDS.Serialize("Recording.json", list);
            return list;
        }
        public override IList<T> Create<T>(IList<T> list)
        {
            Recording rec = new Recording
            {
                ID = list.Count()
            };
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.WriteLine("  ID: " + rec.ID);
                Console.WriteLine("  Название: " + rec.Title);
                Console.WriteLine("  Цена: " + rec.Price);
                Console.WriteLine("  Прибавка?: " + rec.Add);
                ConsoleKeyInfo key = ArrowMenu(0,3);
                if(key.Key == (ConsoleKey)HotKey.Назад)
                {
                    break;
                }
                else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    Console.CursorVisible = true;
                    if(Console.CursorTop == 1)
                    {
                        bool error;
                        do
                        {
                            rec.ID = Convert.ToInt32(RightInt(6, 0, 7));
                            error = false;
                            foreach (var element in (List<Recording>)list)
                            {
                                if (element.ID == rec.ID)
                                {
                                    error = true;
                                    break;
                                }
                            }

                        } while (error);
                    }
                    else if(Console.CursorTop == 2)
                    {
                        Console.SetCursorPosition(11, 1);
                        rec.Title = Console.ReadLine();

                    }
                    else if (Console.CursorTop == 3)
                    {
                        rec.Price = Convert.ToInt32(RightInt(7, 2, 8));
                    }
                    else if (Console.CursorTop == 4)
                    {
                        rec.Add = rec.Add == false ? true : false;
                    }
                }
            }
            if(rec.Title != null)
            {
                list.Add((T)Convert.ChangeType(rec,typeof(T)));
                JsonConvertDS.Serialize("Recording.json",list);
            }
            return list;
        }
    }
}
