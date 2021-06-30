using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Util
{
    public class GeraLog
    {
        //public static bool Debug(string Valor)
        //{

        //    string path = System.Web.HttpContext.Current.Server.MapPath("/Logs/Log_da_Cielo_" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + ".txt");

        //    try
        //    {

        //        if (!System.IO.File.Exists(path))
        //        {
        //            using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
        //            {
        //                sw.WriteLine(Valor);
        //            }
        //            return true;
        //        }
        //        else
        //        {
        //            using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
        //            {
        //                sw.WriteLine(Valor);
        //            }
        //            return true;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("The process failed: {0}", e.ToString());
        //        return false;
        //    }
        //}
    }
}
