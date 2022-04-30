using System;
using System.IO;
using System.Text;

namespace IGK.ICore.IO
{
    internal class WOFFLog
    {
        internal static void Log<T>(T vss) where T:struct
        {
            Console.WriteLine(vss.GetType().FullName);
        //    var m = typeof(WOFFFile).GetMethod("URead", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            foreach (var i in vss.GetType().GetFields( System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)) {

                var m = typeof(WOFFFile).GetMethod("URead", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                    null,
                     new Type[]{
                         i.FieldType 
                     },null);
                if (m!=null)
                Console.WriteLine(i.Name + " : " + m.Invoke(null, new object[] { i.GetValue(vss) }));
                else
                    Console.WriteLine(i.Name + " : " +  i.GetValue(vss) );
            }
            Console.WriteLine();
        }
        internal static string GetData(object vss) {
            StringBuilder sb = new StringBuilder();
            foreach (var i in vss.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {

                var m = typeof(WOFFFile).GetMethod("URead",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                    null,
                     new Type[]{
                         i.FieldType
                     }, null);
                if (m != null)
                    sb.AppendLine(i.Name + " : " + m.Invoke(null, new object[] { i.GetValue(vss) }));
                else
                    Console.WriteLine(i.Name + " : " + i.GetValue(vss));
            }
            return sb.ToString();
        }
        internal static void Log(string filenamev, string data)
        {
            using (var s = File.AppendText(filenamev))
            {
                s.WriteLine(data);
                s.Flush();
                s.Close();
            }
        }
        internal static void Log(string filenamev, WOFFFile.ttf_tabledirectory g)
        {
            using (var s = File.AppendText(filenamev))
            {
                s.WriteLine("HTAG : " + g.TagName);
                s.WriteLine(GetData(g));
                s.Flush();
                s.Close();
            }
        }
    }
}