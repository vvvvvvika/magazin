using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class AccountPost : CRUD
    {
        public int ID;
        public string Name;
        public string Login;
        public string Password;
        public int Post;
        public bool Connection;

        private bool DopMenuEnable;
        protected int PositionCursor;
        protected int StartPositionCursor;
        protected int EndPositionCursor;
        protected void DrewInterface<T>(IList<T> list, AccountPost account)
        {
            while (true)
            {
                Console.Clear();
                if (DopMenuEnable)
                {
                    this.Read(list, list[PositionCursor - 1]);
                    DopMenuEnable = false;
                }
                if(!DopMenuEnable)
                {
                    Console.Clear();
                    this.HeaderMenu(list, account);
                    StartPositionCursor = 1;
                    if(list != null && list.Count < 1)
                    {
                        EndPositionCursor = 1;
                    }
                    else
                    {
                        EndPositionCursor = list.Count;
                    }
                }
                ConsoleKeyInfo key = ArrowMenu(StartPositionCursor, EndPositionCursor);

                if(key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    if (!DopMenuEnable)
                    {
                        DopMenuEnable = true;
                    }
                }else if (key.Key == (ConsoleKey)HotKey.Назад)
                {
                    if (DopMenuEnable)
                    {
                        DopMenuEnable = false;
                    }
                    else
                    {
                        break;
                    }
                }else if(key.Key == (ConsoleKey)HotKey.Создать)
                {
                    if (!DopMenuEnable)
                    {
                        list = list == null ? new List<T>(): list;
                        list = this.Create(list);
                    }
                }
                else if(key.Key == (ConsoleKey)HotKey.Удалить)
                {
                    if (!DopMenuEnable)
                    {
                        list = this.Delete(list,PositionCursor-1).ToList();
                    }
                }
            }
            Console.Clear();
        }

        protected virtual void HeaderMenu<T>(IList<T> list, AccountPost account)
        {
            DrefInfoAboutAccount(account);
        }
        protected void DrefInfoAboutAccount(AccountPost account)
        {
            string Name = account.Name;
            if (Name == null || Name == "null")
            {
                Name = account.Login;
            }
            Console.WriteLine("Добро пожаловать: " + Name + " Ваша роль: " + DrewPostWorker(account.Post));
        }
        protected string DrewPostWorker(int x)
        {
            
            if (x == (int)Roly.Администратор)
            {
                return Convert.ToString(Roly.Администратор);
            }
            else if (x == (int)Roly.Кассир)
            {
                return Convert.ToString(Roly.Кассир);
            }
            else if (x == (int)Roly.Склад)
            {
                return Convert.ToString(Roly.Склад);
            }
            else if (x == (int)Roly.Менеджер)
            {
                return Convert.ToString(Roly.Менеджер);
            }
            else if (x == (int)Roly.Бухгалтер)
            {
                return Convert.ToString(Roly.Бухгалтер);
            }
            else
            {
                return Convert.ToString("null");
            }
        }

        public virtual IList<T> Read<T>(IList<T> list, T account)
        {
            throw new NotImplementedException();
        }
        public virtual IList<T> Create<T>(IList<T> list)
        {
            throw new NotImplementedException();
        }
        public virtual IList<T> Delete<T>(IList<T> list, int x)
        {
            throw new NotImplementedException();
        }
        public virtual IList<T> Update<T>(IList<T> list,T account)
        {
            throw new NotImplementedException();
        }

        protected ConsoleKeyInfo ArrowMenu(int startPositionCursot, int EndPositionCursor)
        {
            Console.CursorVisible = false;
            PositionCursor = startPositionCursot;
            int LastPositionCursor;
            ConsoleKeyInfo key;
            while (true)
            {
                Console.SetCursorPosition(0, PositionCursor);
                Console.WriteLine("->");
                key = Console.ReadKey();
                if (key.Key == (ConsoleKey)HotKey.Вверх)
                {
                    LastPositionCursor = PositionCursor--;
                    PositionCursor = PositionCursor < startPositionCursot ? EndPositionCursor : PositionCursor;
                    ClearLastPositionArrow(LastPositionCursor);
                }
                else if (key.Key == (ConsoleKey)HotKey.Вниз)
                {
                    LastPositionCursor = PositionCursor++;
                    PositionCursor = PositionCursor > EndPositionCursor ? startPositionCursot : PositionCursor;
                    ClearLastPositionArrow(LastPositionCursor);
                }
                else
                {
                    break;
                }
            }
            return key;
        }
        private void ClearLastPositionArrow(int LastPosition)
        {
            Console.SetCursorPosition(0, LastPosition);
            Console.WriteLine("  ");
        }

        protected string? RightInt(int x, int y , int LenString)
        {
            List<char> a = new List<Char>();
            string b = "";
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
                    if (a.Count > 0)
                    {
                        a.RemoveAt(x - LenString);
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x + 1, y);
                        x--;
                    }
                }
                else
                {
                    if (char.IsNumber(key.KeyChar))
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(key.KeyChar);
                        a.Add(key.KeyChar);
                        x++;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                    }
                }
            }
            foreach (var element in a)
            {
                b += element;
            }
            return string.IsNullOrEmpty(b)?null:b;
        }
    }
}
