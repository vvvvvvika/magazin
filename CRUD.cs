using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    internal interface CRUD
    {
        IList<T> Create<T>(IList<T> list);
        IList<T> Read<T>(IList<T> list, T account);
        IList<T> Update<T>(IList<T> list,T account);
        IList<T> Delete<T>(IList<T> list, int x);
    }
}
