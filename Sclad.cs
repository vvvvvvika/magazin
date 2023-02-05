using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Sclad : AccountPost
    {
        public Sclad(List<Product> list, AccountPost account)
        {
            list = list == null ? new List<Product>() : list;
            DrewInterface(list, account);
        }
        protected override void HeaderMenu<T>(IList<T> list, AccountPost account)
        {
            base.HeaderMenu(list, account);
            foreach (var element in (List<Product>)list)
            {
                Console.WriteLine("  ID: " + element.ID + " Название: " + element.Title + " Цена за штуку: " + element.PriceForPiece + " Колличество: " + element.AlotOf) ;
            }
        }
        public override IList<T> Delete<T>(IList<T> list, int x)
        {
            list.RemoveAt(x);
            JsonConvertDS.Serialize("Product.json", list);
            return list;
        }
        public override IList<T> Read<T>(IList<T> list, T account)
        {
            Console.Clear();
            Console.WriteLine("  ID: " + (account as Product).ID);
            Console.WriteLine("  Название: " + (account as Product).Title);
            Console.WriteLine("  Цена за штуку: " + (account as Product).PriceForPiece);
            Console.WriteLine("  Колличество товара: " + (account as Product).AlotOf);
            return Update(list,account);
        }
        public override IList<T> Update<T>(IList<T> list, T account)
        {
            while (true)
            {
                ConsoleKeyInfo key = ArrowMenu(0, 3);
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        int ID;
                        bool error;
                        do
                        {
                            error = false;
                            ID = Convert.ToInt32(RightInt(6, 0, 7));
                            foreach (var element in (List<Product>)list)
                            {
                                if (element.ID == ID)
                                {
                                    error = true;
                                    break;
                                }
                            }
                        } while (error);
                        (account as Product).ID = ID;
                    }
                    else if (Console.CursorTop == 2)
                    {
                        string Title;
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(12, 1);
                            Title = Console.ReadLine();
                            foreach (var element in (List<Product>)list)
                            {
                                if (element.Title == Title)
                                {
                                    error = true;
                                    break;
                                }
                            }

                        } while (error);
                        (account as Product).Title = Title;
                    }
                    else if (Console.CursorTop == 3)
                    {
                        (account as Product).PriceForPiece = Convert.ToInt32(RightInt(17, 2, 18));
                    }
                    else
                    {
                        int a = 0;
                        do
                        {
                            a = Convert.ToInt32(RightInt(22, 3, 23));
                        } while (a == 0);
                        (account as Product).AlotOf = a;
                    }
                }
                Console.CursorVisible = false;
                Console.Clear();
                Console.WriteLine("  ID: " + (account as Product).ID);
                Console.WriteLine("  Название: " + (account as Product).Title);
                Console.WriteLine("  Цена за штуку: " + (account as Product).PriceForPiece);
                Console.WriteLine("  Колличество товара: " + (account as Product).AlotOf);
            }
            JsonConvertDS.Serialize("Product.json", list);
            return list;
        }
        public override IList<T> Create<T>(IList<T> list)
        {
            Product prod = new Product
            {
                ID = list.Count
            };
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.WriteLine("  ID: " + prod.ID);
                Console.WriteLine("  Название: "+ prod.Title);
                Console.WriteLine("  Цена за чтуку: " + prod.PriceForPiece);
                Console.WriteLine("  Колличество товара: " + prod.AlotOf);

                ConsoleKeyInfo key = ArrowMenu(0, 3);
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        int ID;
                        bool error;
                        do
                        {
                            error = false;
                            ID = Convert.ToInt32(RightInt(6, 0, 7));
                            foreach(var element in (List<Product>)list)
                            {
                                if(element.ID == ID)
                                {
                                    error = true;
                                    break;
                                }
                            }
                        } while (error);
                        prod.ID = ID;
                    }
                    else if (Console.CursorTop == 2)
                    {
                        string Title;
                        bool error;
                        do
                        {
                            error = false;
                            Console.SetCursorPosition(12, 1);
                            Title = Console.ReadLine();
                            foreach(var element in (List<Product>)list)
                            {
                                if(element.Title == Title)
                                {
                                    error = true;
                                    break;
                                }
                            }

                        } while (error);
                        prod.Title = Title;
                    }
                    else if (Console.CursorTop == 3)
                    {
                        prod.PriceForPiece = Convert.ToInt32(RightInt(17, 2, 18));
                    }
                    else
                    {
                        prod.AlotOf = Convert.ToInt32(RightInt(22, 3, 23));
                    }
                }
            }
            if (prod.Title != null && prod.Title != "null" && prod.AlotOf != 0)
            {
                list.Add((T)Convert.ChangeType(prod,typeof(T)));
                JsonConvertDS.Serialize("Product.json",list);
            }
            return list;
        }
    }
}
