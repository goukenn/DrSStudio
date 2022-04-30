/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/**
 * 
 * source permettant de traduire le fichier glew.h en glew.cs
 * en vue de la génération d'une classe permettant la création d'un librairie OpenGL basée sur cette traduction.
 * L'implémentation glew.h tester est une implémentation qui supporte la version 1.1 à la version 2.1
 * 
 **/ 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace NetGLFileGenerator
{
    class delegateInfo
    {
        internal string rType;
        internal string name;
        internal string param;

        public delegateInfo( string rType , string name , string param)
        {
            this.rType = rType;
            this.name = name;
            this.param = param;
        }
    }
    class addMethod
    {
        internal string delName;
        internal string name;

        public addMethod(string delName, string name)
        {
            this.delName = delName;
            this.name = name;
        }
    }
    class glewTraductor
    {
        //compteur de methode additionel
        private static int methCount;
        private static List<string> constants = new List<string>();
        private static List<delegateInfo> delegates = new List<delegateInfo> ();
        private static List<addMethod> addmethods= new List<addMethod> ();
        static void Main(string[] args)
        {
            //prise du fichier 
            string infilename = "glew.h";
            string outfilename = "glew.cs";
            
            //création des flux de fichier
            StreamReader streamR = new StreamReader(infilename);
            StreamWriter streamW = new StreamWriter(outfilename);

            string str = string.Empty;
            //ecriture de l'en tête de fichier
            str = "using System;\nusing System.Runtime.InteropServices;\n";
            str += "namespace GK.Graphics.OpenGL\n{";
            str += "public partial class GLApi {\n";
          
            streamW.WriteLine (str);

            //lecture des lignes
            while ((str = streamR.ReadLine()) != null)
            { 
                //ecriture de la ligne de commentaire
                if (str.StartsWith("/*"))
                {
                    streamW.WriteLine(str);
                    continue;
                }
                //ecriture de la chaine traiter
                str = ThreatText(str);
                if (!string.IsNullOrEmpty(str))
                    streamW.WriteLine(str);

            }
            //ecrirute des method public
            streamW.WriteLine ("/************************************Exported Method********/");
            foreach(addMethod m in addmethods)
            {
                str = string.Empty ;

                foreach(delegateInfo d in delegates)
                {
                    if (d.name == m.delName )
                    {
                        str = "public static "+d.rType +" "+m.name +d.param.Replace(";","") +
                            "{ if (_"+m.name +"!=null) "+
                            ((d.rType=="void")?"":"return ")+

                            "_"+m.name +""+RemoveType(d.param)+" else throw new GLException (GLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,\""+m.name+ "\"));}";
                        streamW.WriteLine (str);
                        break;
                    }
                }
            }
            streamW.WriteLine("}\n}");
            streamW.Flush ();
            //fermeture du flux;
            streamR.Close();
            streamW.Close();

            Console.WriteLine ("Methode count = "+ methCount);
            Console.Read ();
        }

        private static string RemoveType(string p)
        {
            p = p.Replace ("uint ","");
            p = p.Replace ("float ","");
            p = p.Replace ("int ","");
            p = p.Replace ("ushort ","");
            p = p.Replace ("short ","");
            p = p.Replace ("float ","");
            p = p.Replace ("double ","");
            p = p.Replace ("IntPtr ","");
            p = p.Replace ("sbyte ","");
            p = p.Replace ("byte ","");
            p = p.Replace ("bool ","");
            p = p.Replace ("[]","");
            //throw new Exception("The method or operation is not implemented.");
            return p ;
        }

        private static string ThreatText(string str)
        {
            
            //détection de constante
            string name , value, type, param;
            if (DetectConstante(str, out name, out value))
            {
                str = "public const uint " + name + " = " + value + ";";
                if (constants.Contains(name))
                {
                    str = "//" + str;
                }
                else
                    constants.Add(name);
                return str;
            }
            else { 
                //Replace TypeDef
                str = ReplaceType(str);
                if (DectectApiMethod(str, out type, out name, out param))
                {
                    str = "[DllImport(\"opengl32.dll\")] static extern public " + type + " " + name + " " + param;
                    return str;
                }
                else if (DectectDelegateMethod(str, out type, out name, out param))
                { 
                    //detecte Delegate
                    str = "public delegate " + type + " " + name + " " + param;
                    delegates.Add (new delegateInfo (type,name,param));
                    return str;
                }
                else if (DetectAdditionaleMethod(str, out type, out name))
                {//detection des methode addionel
                    if (name == "define")
                        return null;
                    methCount++;
                    str = "internal static "+type+" _"+name+";// =("+type+") GetProcedure(\""+name+"\", typeof("+type+"));";
                    
                    addmethods.Add (new addMethod (type,name));
                    return str;
                }
                else if (DetectAdditionalValue(str, out type, out name))
                {
                    str = "public static readonly "+type+" "+name+";";
                    return str;
                }

            }   
            return null;
        }

          private static bool DetectAdditionalValue(string str, out string type, out string name)
        {
            string[] strTab = null;
            strTab = str.Split(' ');           
                if (strTab.Length > 2)
                {
                    if (strTab[2].StartsWith("GLEW_GET_VAR"))
                    {
                        type = "bool";
                        name = "is"+strTab[1].Replace ("GLEW_","GL_");
                        //name = "is" + strTab[1].Replace("GLEW_", "GL_");
                        return true;

                    }
                }
            
            type = null;
            name = null;            
            return false;
        }

        private static bool DetectAdditionaleMethod(string str, out string type, out string name)
        {
            string[] strTab = null;
            strTab = str.Split(' ');           
                if (strTab.Length > 2)
                {
                    if (strTab[2].StartsWith("GLEW_GET_FUN"))
                    {
                        type = "PFN" + strTab[1].ToUpper () + "PROC";
                        name = strTab[1];
                        return true;

                    }
                }
            
            type = null;
            name = null;            
            return false;
        }

        private static bool DectectDelegateMethod(string str, out string type, out string name, out string param)
        {
            string[] strTab = null;
            strTab = str.Split(' ');
            if (strTab.Length > 3)
            {
                if (strTab[2] == "(GLAPIENTRY")
                {
                    type = strTab[1];
                    name = strTab[4].Replace (")","");                  
                    param = str.Remove(0, str.IndexOf(name) + name.Length+1);

                
                    return true;
                }
            }
            type = null;
            name = null;
            param = null;
            return false;
        }


        private static bool DectectApiMethod(string str, out string type, out string name, out string param)
        {
            string[] strTab = null;
            strTab = str.Split(' ');
            
            if (strTab.Length > 3)
            {
                if (strTab[2] == "GLAPIENTRY")
                {
                    type = strTab[1];
                    name = strTab[3];
                    param = str.Remove(0, str.IndexOf(name) + name.Length);
                   
                    return true;
                }
            }
            type = null;
            name = null;
            param = null;
            return false;
        }

     
        private static bool DetectConstante(string str, out string constant, out string value)
        {
            bool isHex = false;
            string [] strTab  = null;
               if (str.StartsWith("#define"))
            {
                strTab = str.Split(' ');
                if (strTab.Length == 3)
                {
                    int i = 0;
                    if (strTab[2].StartsWith ("0x"))
                    {
                       isHex = strTab[2].Contains("0x");
                       strTab[2] = strTab[2].Replace ("0x","");
                    }
                    if (Int32.TryParse(strTab[2], out i))
                    {
                        constant = strTab[1];
                        value = "0x" + i.ToString();
                        return true;
                    }
                    if (isHex)
                    {
                        constant = strTab[1];
                        value = "0x" + strTab[2];
                        return true;
                    }
                    if (strTab[2].StartsWith("GL_"))
                    {
                        constant = strTab[1];
                        value =  strTab[2];
                        return true;
                    }
                }
              
            }
            constant = null;
            value = null;
            return false;
        }

        static int j = 0;
        private static string ReplaceType(string str)
        {
            j = 0;
            if (str.Contains("PFNGLMAPOBJECTBUFFERATIPROC"))
            { 
            }
            str = str.Replace("(void)", "()");
            
            str = str.Replace("const GLvoid * const *", "const GLvoid** ");

            //str = str.Replace("void * ", "void* ");
            //str = str.Replace("void**", "GLvoid**");
            //str = str.Replace("void*", "GLvoid*");
            str = str.Replace("const void**", "const GLvoid**");
            str = str.Replace("GLvoid * ", "GLvoid* ");
            str = str.Replace("GLintptrARB", "GLsizei**");
            str = str.Replace("GLsizeiptrARB", "GLsizei**");
            str = str.Replace("GLsizeiptr", "GLsizei*");
            str = str.Replace("GLintptr", "GLsizei*");

            str = ReplaceType(str, "GLvoid", "void");
            str = ReplaceType(str, "GLboolean", "bool");
            str = ReplaceType(str, "GLfloat", "float");
            str = ReplaceType(str, "GLdouble", "double");
            str = ReplaceType(str, "GLclampf", "float");
            str = ReplaceType(str, "GLclampd", "double");
            str = ReplaceType(str, "GLenum", "uint");
            str = ReplaceType(str, "GLsizei", "int");
            str = ReplaceType(str, "GLbyte", "sbyte");
            str = ReplaceType(str, "GLubyte", "byte");
            str = ReplaceType(str, "GLint", "int");
            str = ReplaceType(str, "GLuint", "uint");
            str = ReplaceType(str, "GLshort", "short");
            str = ReplaceType(str, "GLushort", "ushort");
            str = ReplaceType(str, "GLbitfield", "uint");
            
            str = ReplaceType(str, "GLhalf", "ushort");
            str = ReplaceType(str, "GLint64EXT", "Int64");
            str = ReplaceType(str, "GLuint64EXT", "Int64");
            str = ReplaceType(str, "GLcharARB", "char");
            str = ReplaceType(str, "GLhandleARB", "uint");
            str = ReplaceType(str, "GLchar", "char");
            if (str.Contains("PFNGLMAPOBJECTBUFFERATIPROC"))
            { 
            }
            str = ReplaceType(str, "GLIntPtr", "IntPtr");

            str = str.Replace(" params", " _params");
            str = str.Replace(" ref", " _ref");
            str = str.Replace(" base", " _base");
            str = str.Replace(" string", " _string");
            str = str.Replace(" in ", " _in ");
            str = str.Replace(" in,", " _in,");
            str = str.Replace(" object", " _object");
            str = str.Replace("m[16]", "m /*[16]*/");
            str = str.Replace("const void*", "IntPtr ");
            str = str.Replace("v[]", "v/*[]*/");
            str = str.Replace("void** ", "IntPtr ");
            str = str.Replace("void*", "IntPtr ");
            str = str.Replace("const GLIntPtr", "IntPtr");
            //remplacer le double espace par un seul
            str = str.Replace("  ", " ");
            return str;
        }
        private static string ReplaceType(string str , string glType, string netType)
        {
            //if (str.Contains("PFNGLGETVERTEXATTRIBPOINTERVPROC"))
            //{ 
            //}
            
            int count = 0;
            string[] tab = new string []{glType + ",", glType + "*,",
                glType + ")", glType + "*)"
            };
            string[] tab1 = new string []{
                ",",",",")",")"
            }; 
            for (int i = 0 ; i < tab.Length ; i++)
            {
                count = 0;
                while ((count = str.IndexOf(tab[i], count)) >= 0)
                {
                    str = str.Remove(count, tab[i].Length);
                    str = str.Insert (count ,glType+ (((i==1)|| (i==3))? "*":"")+ " v" +(j++)+tab1[i]);
                    count += tab1[i].Length;
                }
            }
            

            //replace empty parameter           
          
              // while (str != str.Replace("" + glType + ",", "" + glType + " v" + (i++) + ","))
              //  str = str.Replace("" + glType + ",", "" + glType + " v" + (i++) + ",");
            
              //while (  str != str.Replace("" + glType + ")", "" + glType + " v"+(i++)+")"))
              //      str = str.Replace("" + glType + ")", "" + glType + " v"+(i++)+")");
              //while ( str != str.Replace("" + glType + "*)", "" + glType + "* v" + (i++) + ")"))
              //    str = str.Replace("" + glType + "*)", "" + glType + "* v" + (i++) + ")");
              //while (str != str.Replace("" + glType + "*,", "" + glType + "* v" + (i++) + ")"))
              //    str = str.Replace("" + glType + "*,", "" + glType + "* v" + (i++) + ")");

            str = str.Replace("const " + glType + "* *", "IntPtr ");
            str = str.Replace("const "+glType+"** ", "IntPtr ");
            str = str.Replace("const " + glType + "**", "IntPtr ");
            str = str.Replace("const " + glType + " **", "IntPtr ");
            
            str = str.Replace("const " + glType + "* ", "IntPtr ");
            str = str.Replace("const " + glType + " * ", "IntPtr ");
            str = str.Replace("const " + glType + " *", "IntPtr ");
            str = str.Replace("const " + glType + "*", "IntPtr ");
            str = str.Replace("const " + glType + " ", "IntPtr ");

            str = str.Replace("" + glType + " **", "IntPtr ");
            str = str.Replace("" + glType + "* *", "IntPtr ");
            str = str.Replace("" + glType + "** ", "IntPtr ");
            str = str.Replace("" + glType + "**", "IntPtr ");
            str = str.Replace("" + glType + "* ", "IntPtr ");
            str = str.Replace("" + glType + " *", "IntPtr ");
            str = str.Replace("" + glType + "*", "IntPtr ");
            str = str.Replace("" + glType + " ", ""+netType+" ");          

            return str;
        }
    }
}
