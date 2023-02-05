using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Manager : AccountPost
    {
        public Manager(List<Staff> list, AccountPost account)
        {
            list = list == null ? new List<Staff>() : list;
            DrewInterface(list, account);
        }
        protected override void HeaderMenu<T>(IList<T> list, AccountPost account)
        {
            base.HeaderMenu(list, account);
            foreach(var element in (List<Staff>)list)
            {
                Console.WriteLine("  ID: " + (element.ID == null?"null":element.ID) + " Фамилия: " + element.SurName + " Имя: " + element.Name + " Отчество: " + element.Otchestvo + " Должность: " + (element.Post == null ? "null" : DrewPostWorker(Convert.ToInt32(element.Post))));
            }
        }
        public override IList<T> Delete<T>(IList<T> list, int x)
        {
            list.RemoveAt(x);
            JsonConvertDS.Serialize("Staff.json", list);
            return list;
        }
        public override IList<T> Read<T>(IList<T> list, T staff)
        {
            Console.WriteLine("  ID: " + (staff as Staff).ID);
            Console.WriteLine("  Имя: " + (staff as Staff).Name);
            Console.WriteLine("  Фамилия: " + (staff as Staff).SurName);
            Console.WriteLine("  Отчество: " + (staff as Staff).Otchestvo);
            Console.WriteLine("  Дата рождения: " + (staff as Staff).BirthDay);
            Console.WriteLine("  Серия и номер паспорта: " + (staff as Staff).SeriaNomerPassporta);
            Console.WriteLine("  Должность: " + (staff as Staff).Post);
            Console.WriteLine("  Зараплата: " + (staff as Staff).Zp);
            return Update(list, staff);
        }
        public override IList<T> Update<T>(IList<T> list, T staff)
        {
            while (true)
            {
                ConsoleKeyInfo key = ArrowMenu(0, 7);
                if (key.Key == (ConsoleKey)HotKey.Назад)
                {
                    break;
                } else if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    Console.CursorVisible = true;
                    if (Console.CursorTop == 1)
                    {
                        bool error;
                        do
                        {
                            var listAccount = JsonConvertDS.Deserialize<List<AccountPost>>("AccountPost.json");
                            error = false;
                            Console.SetCursorPosition(6, 0);
                            string ID = RightInt(6, 0, 7);
                            foreach (var element in listAccount)
                            {
                                if (element.ID.ToString() == (staff as Staff).ID)
                                {
                                    (staff as Staff).Post = null;
                                    (staff as Staff).ID = null;
                                    element.Connection = false;
                                    element.Name = null;
                                    break;
                                }
                            }
                            if (ID != null)
                            {
                                error = true;
                                foreach (var element in listAccount)
                                {
                                    if (element.ID.ToString() == ID)
                                    {
                                        (staff as Staff).ID = ID;
                                        (staff as Staff).Post = element.Post.ToString();
                                        element.Connection = true;
                                        element.Name = (staff as Staff).Name;
                                        error = false;
                                        break;
                                    }
                                }
                            }
                            JsonConvertDS.Serialize("AccountPost.json", listAccount);
                        }
                        while (error);
                    }
                    else if (Console.CursorTop == 2)
                    {
                        bool error = false;
                        do
                        {
                            Console.SetCursorPosition(7, 1);
                            (staff as Staff).Name = Console.ReadLine();
                            if (string.IsNullOrEmpty((staff as Staff).Name))
                            {
                                error = true;
                            }
                        }
                        while (error);

                    }
                    else if (Console.CursorTop == 3)
                    {
                        bool error = false;
                        do
                        {
                            Console.SetCursorPosition(10, 2);
                            (staff as Staff).SurName = Console.ReadLine();
                            if (string.IsNullOrEmpty((staff as Staff).Name))
                            {
                                error = true;
                            }
                        }
                        while (error);
                    }
                    else if (Console.CursorTop == 4)
                    {
                        bool error = false;
                        do
                        {
                            Console.SetCursorPosition(11, 3);
                            (staff as Staff).Otchestvo = Console.ReadLine();
                            if (string.IsNullOrEmpty((staff as Staff).Name))
                            {
                                error = true;
                            }
                        }
                        while (error);
                    }
                    else if (Console.CursorTop == 5)
                    {
                        DateTime dateTime;
                        do
                        {
                            Console.SetCursorPosition(17, 4);
                            (staff as Staff).BirthDay = Console.ReadLine();
                        }
                        while (!DateTime.TryParseExact((staff as Staff).BirthDay, "dd.MM.yyyy", null, DateTimeStyles.None, out dateTime));
                    }
                    else if (Console.CursorTop == 6)
                    {
                        bool error = false;
                        do
                        {
                            (staff as Staff).SeriaNomerPassporta = Convert.ToInt32(RightInt(26, 5, 27));
                        }
                        while (error);
                    }
                    else if (Console.CursorTop == 8)
                    {
                        bool error = false;
                        do
                        {
                            (staff as Staff).Zp = Convert.ToInt32(RightInt(12, 7, 13));
                        }
                        while (error);
                    }
                }
                Console.Clear();
                Console.WriteLine("  ID: " + (staff as Staff).ID);
                Console.WriteLine("  Имя: " + (staff as Staff).Name);
                Console.WriteLine("  Фамилия: " + (staff as Staff).SurName);
                Console.WriteLine("  Отчество: " + (staff as Staff).Otchestvo);
                Console.WriteLine("  Дата рождения: " + (staff as Staff).BirthDay);
                Console.WriteLine("  Серия и номер паспорта: " + (staff as Staff).SeriaNomerPassporta);
                Console.WriteLine("  Должность: " + (staff as Staff).Post);
                Console.WriteLine("  Зараплата: " + (staff as Staff).Zp);
            }
            JsonConvertDS.Serialize("Staff.json",list);
            return list;
        }
        public override IList<T> Create<T>(IList<T> list)
        {
            Staff st = new Staff
            {
                ID = null,
                Post = null,
            };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("  Имя: " + st.Name);
                Console.WriteLine("  Фамилия: " + st.SurName);
                Console.WriteLine("  Отчество: " + st.Otchestvo);
                Console.WriteLine("  Дата рождения: " + st.BirthDay);
                Console.WriteLine("  Серия и номер паспорта: " + st.SeriaNomerPassporta);
                Console.WriteLine("  Зараплата: " + st.Zp);
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
                        do
                        {
                            Console.SetCursorPosition(7, 0);
                            st.Name = Console.ReadLine();
                        }
                        while (string.IsNullOrEmpty(st.Name));

                    }
                    else if (Console.CursorTop == 2)
                    {
                        do
                        {
                            Console.SetCursorPosition(10, 1);
                            st.SurName = Console.ReadLine();
                        }
                        while (string.IsNullOrEmpty(st.Name));
                    }
                    else if (Console.CursorTop == 3)
                    {
                        do
                        {
                            Console.SetCursorPosition(11, 2);
                            st.Otchestvo = Console.ReadLine();
                        }
                        while (string.IsNullOrEmpty(st.Name));
                    }
                    else if (Console.CursorTop == 4)
                    {
                        DateTime dateTime;
                        do
                        {
                            Console.SetCursorPosition(17, 3);
                            st.BirthDay = Console.ReadLine();
                        }
                        while (!DateTime.TryParseExact(st.BirthDay, "dd.MM.yyyy", null, DateTimeStyles.None, out dateTime));
                    }
                    else if (Console.CursorTop == 5)
                    {
                        st.SeriaNomerPassporta = Convert.ToInt32(RightInt(26, 4, 27));
                    }
                    else if (Console.CursorTop == 6)
                    {
                        st.Zp = Convert.ToInt32(RightInt(12, 5, 13));
                    }
                }
            }
            if(st.Name != null && st.SurName != null && st.Otchestvo != null && st.BirthDay != null)
            {
                list.Add((T)Convert.ChangeType(st, typeof(T)));
                JsonConvertDS.Serialize("Staff.json", list);
            }
            return list;
        }
    }
}
