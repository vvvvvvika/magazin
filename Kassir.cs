using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Kassir : AccountPost
    {
        private int Price = 0;
        public Kassir(List<Product> list, AccountPost account)
        {
            list = list == null ? new List<Product>() : list;
            DrewInterface(list, account);
        }
        protected override void HeaderMenu<T>(IList<T> list, AccountPost account)
        {
            base.HeaderMenu(list, account);
            foreach (var element in (List<Product>)list)
            {
                Console.WriteLine("  ID: " + element.ID + " Название: " + element.Title + " Цена за штуку: " + element.PriceForPiece + " Колличество: " + element.AlotOf + " Купить: " + element.AlotOfBuy);
            }
        }
        private void DrewInterface(List<Product> list, AccountPost account)
        {
            while (true)
            {
                Console.Clear();
                HeaderMenu(list, account);
                ConsoleKeyInfo key = ArrowMenu(1, list.Count());
                if(key.Key == (ConsoleKey)HotKey.Назад)
                {
                    break;
                }else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    list = BuyProduct(list, list[PositionCursor-1] ,account);
                }
            }
        }
        private List<Product> BuyProduct(List<Product> list,Product element, AccountPost account)
        {
            while (true)
            {
                Console.Clear();
                base.HeaderMenu(list, account);
                Console.WriteLine("->ID: " + element.ID + " Название: " + element.Title + " Цена за штуку: " + element.PriceForPiece + " Колличество: " + element.AlotOf + " Купить: " + element.AlotOfBuy);
                Console.WriteLine("Цена покупки: " + Price);
                ConsoleKeyInfo key = ArrowMenu(PositionCursor, PositionCursor);
                if(key.Key == (ConsoleKey)HotKey.Назад)
                {
                    Console.Clear();
                    break;
                }
                else if (key.Key == ConsoleKey.OemPlus)
                {
                    element.AlotOf--;
                    if(element.AlotOf < 0)
                    {
                        element.AlotOf = 0;
                    }
                    else
                    {
                        element.AlotOfBuy++;
                        Price += element.PriceForPiece;
                    }
                }
                else if (key.Key == ConsoleKey.OemMinus)
                {
                    element.AlotOfBuy--;
                    if(element.AlotOfBuy < 0)
                    {
                        element.AlotOfBuy = 0;
                    }
                    else
                    {
                        element.AlotOf++;
                        Price -= element.PriceForPiece;
                    }
                }
            }
            if(element.AlotOfBuy > 0)
            {
                List<Recording> list2 = JsonConvertDS.Deserialize<List<Recording>>("Recording.json");
                list2.Add(new Recording
                {
                    ID = list2.Count(),
                    Title = element.Title,
                    Price = Price,
                    Add = true
                });
                JsonConvertDS.Serialize("Recording.json", list2);
                if(element.AlotOf == 0)
                {
                    list.RemoveAt(PositionCursor-1);
                    JsonConvertDS.Serialize("Product.json", list);
                }
            }
            return list;
        }
    }
}
