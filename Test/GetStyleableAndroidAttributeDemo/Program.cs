using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetStyleableAndroidAttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            string file = "d:\\android\\platforms\\android-24\\data\\res\\values\\attrs.xml";

            var g = CoreXmlElement.LoadFile(file);


            Dictionary<string, CoreXmlElement> ge = new Dictionary<string, CoreXmlElement>();
            int ii = 0;
            g.Select("*").eachAll((i) => {
                if (i.Parent == g)
                if (i.TagName == "attr") {
                    if (i["format"]?.Value != null)
                    {
                        ge.Add(i["name"], i);
                        Console.WriteLine( (ii++)+" : Inf " + i["name"]);
                    }
                }

            });


            //CoreXmlElement theme = null;
            //foreach(var i in g.getElementsByTagName("declare-styleable")){

            //    //Console.WriteLine("styleable : " + i["name"]);
            //    if (i["name"] == "Theme") {
            //        //Console.WriteLine("Setting Theme ");
            //        theme = i as CoreXmlElement;
            //    }
            //}
            ////g.Select(">attr").eachAll((o) => {
            ////    Console.WriteLine("item : " + o["name"]);
            ////});
            //g.Add("div").SetAttribute("class", "inf fith").SetAttribute ("id", "info").Content = "OK";
            //List<CoreXmlElement> v_glist = new List<CoreXmlElement>();
            //g.Select(".inf, #info").eachAll((o) => {
            //    Console.WriteLine("item : " + o.TagName + " : name = " + o["name"] + " : " + o["format"]);
            //    if (o.TagName == "attr") {
            //        //Console.WriteLine("item : " + o.TagName + " : name = " + o["name"] + " : " + o["format"]);
            //        v_glist.Add(o);
            //    }
            //});


            //Console.WriteLine();

            //if (theme != null) {

            //    foreach (var i in theme.getElementsByTagName("attr")) {

            //        Console.WriteLine("Theme Attribute : " + i["name"] + " : " + i["format"]);
            //        v_glist.Add(i as CoreXmlElement );
            //    }
            //}

            Console.ReadLine();
        }
    }
}
