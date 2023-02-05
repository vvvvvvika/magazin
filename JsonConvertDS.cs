using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal class JsonConvertDS
    {
        private static string PathToDesctop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string PathToJsonFilesInDesctop  = PathToDesctop + "\\JsonFiles\\";
        private static string PathToJsonFilesInProgramm;
        public static void Serialize<T>(string FileName, IEnumerable<T> list)
        {
            File.WriteAllText(PathToJsonFilesInDesctop + FileName, JsonConvert.SerializeObject(list));
            File.WriteAllText(PathToJsonFilesInProgramm + "\\" + FileName, JsonConvert.SerializeObject(list));
        }
        public static T Deserialize<T>(string FileName)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(PathToJsonFilesInProgramm + "\\" + FileName));
        }
        public static void SearchJsonFiles()
        {
            string a = Directory.GetCurrentDirectory();
            for(int i = a.Length-1; i > 0; i--)
            {
                if (a[i].ToString() == @"\")
                {
                    a = a.Remove(i);
                    foreach(var element in Directory.GetDirectories(a))
                    {
                        if(element == a + "\\" + "JsonFiles")
                        {
                            PathToJsonFilesInProgramm = element;
                            CopyToDesctop();
                            i = 0;
                            break;
                        }
                    }
                }
                else a = a.Remove(i);
            }
        }
        private static void CopyToDesctop()
        {
            if(!Directory.Exists(PathToDesctop + "\\JsonFiles"))
            {
                Directory.CreateDirectory(PathToDesctop + "\\JsonFiles");
                FileSystem.CopyDirectory(PathToJsonFilesInProgramm, PathToDesctop + "\\JsonFiles");
            }
            else
            {
                Directory.Delete(PathToDesctop + "\\JsonFiles",true);
                Directory.CreateDirectory(PathToDesctop + "\\JsonFiles");
                FileSystem.CopyDirectory(PathToJsonFilesInProgramm, PathToDesctop + "\\JsonFiles");
            }
        }
    }
}
