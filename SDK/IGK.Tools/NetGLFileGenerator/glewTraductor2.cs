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
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions ;
using System.Runtime.InteropServices;
using System.IO;
namespace NetGLFileGenerator
{
    class glewTraductor2
    {
        public static void Main()
        {
            string infilename = "glew.h";
            string outfilename = "glew.4.0.cs";

            StringBuilder sb = new StringBuilder ();

            string[] v_instring = File.ReadAllText(infilename).Split('\n');
            string singleComment = "^/\\*(.)+\\*/";
            string v_constant = "^(#define)(\\s)+(?<name>(\\w+))(\\s)+(?<value>(\\w+))";
            string v_field = "^(#define)(\\s)+(?<name>(\\w+))([^\\(]*)GLEW_GET_FUN(\\s)*\\((?<value>(__\\w+))";
          //  string v_typedef = "^(typedef)(\\s)+(?<type>([*\\w]+))(\\s)*\\((?<name>([^\\)])*)\\)\\s*\\((?<args>([^\\)]*))";
            string v_typedef = "^(typedef)(\\s)+(?<type>([^\\(]+))(\\s)*\\((?<name>([^\\)])*)\\)\\s*\\((?<args>([^\\)]*))";
            string v_staticdef = "^GLAPI(\\s)+(?<type>(.+))(\\s)+GLAPIENTRY\\s+(?<name>([^\\(])*)\\s*\\((?<args>([^\\)]*))";
            string v_str = string.Empty ;
            Regex v_rgx_singleComment = new Regex(singleComment);
            Regex v_rgx_constant = new Regex(v_constant);
            Regex v_rgx_typedef = new Regex(v_typedef );
            Regex v_rgx_field = new Regex(v_field);
            Regex v_rgx_apimeth = new Regex(v_staticdef);


            sb.AppendLine("using System;\nusing System.Runtime.InteropServices;");
            sb.AppendLine("namespace IGK.GLLib\n{");
            sb.AppendLine("public partial class GL {");
            sb.AppendLine("#pragma warning disable ");


            bool v_multicomment = false;


            Dictionary<string, GLDelegate> v_delegates = new Dictionary<string, GLDelegate>();
            List<string> m_method = new List<string>();
            Dictionary<string, GLConstant> v_constlist = new Dictionary<string, GLConstant>();
            foreach(string item in v_instring )
            {
                if (item.Contains("const GLchar**"))
                { 
                }
                v_str = item.Trim();
                if (v_rgx_singleComment.IsMatch(v_str))
                {
                    sb.AppendLine(item);
                    v_multicomment = false;
                    continue;
                }
                if (!v_multicomment)
                {
                    if (v_str.StartsWith("/*"))
                    {
                        v_multicomment = true;
                        sb.Append(v_str);
                        continue;
                    }
                }
                else { 
                    //write end wi
                    sb.Append(item);
                    if (v_str.EndsWith("*/"))
                    {
                        v_multicomment = false;
                    }
                    continue;
                }
                //-----------------------------------------------
                //the main traitement
                //-----------------------------------------------

                if (v_rgx_constant.IsMatch(v_str))
                {
                    GroupCollection v_group = v_rgx_constant.Match(v_str).Groups ;
                    string n = v_group["name"].Value;
                    string v = v_group["value"].Value;

                    if (n.StartsWith("GL_"))
                    {
                        GLConstant v_const = new GLConstant(n, v);
                        if (v_constlist.ContainsKey(v_const.Name))
                        {
                            sb.AppendLine("//"+v_const.ToString());
                        }
                        else
                        {
                            sb.AppendLine(v_const.ToString());
                            v_constlist.Add(v_const.Name, v_const);
                        }
                        continue;
                    }
                    else 
                    { 
                    }                    
                }
                //type definition
                if (v_rgx_typedef.IsMatch(v_str))
                {
                    GroupCollection v_group = v_rgx_typedef.Match(v_str).Groups;
                    string t = v_group["type"].Value;
                    string n = v_group["name"].Value;
                    string v = v_group["args"].Value;
                    n = n.Replace("GLAPIENTRY *", "").Trim ();
                    n = n.Replace("APIENTRY *", "").Trim();
                    MethodArguments[] args = GetArguments( v.Split(','));
                    GLDelegate d = new GLDelegate(n, GLUtils.TreatReturnType (t));
                    for (int i = 0; i < args.Length; i++)
                    {
                        d.AddArgs(args[i]);
                    }
                    sb.AppendLine(d.ToString());
                    v_delegates.Add(d.Name, d);
                    continue ;
                }

                if (v_rgx_field.IsMatch(v_str))
                {
                    GroupCollection v_group = v_rgx_field.Match(v_str).Groups;
                    string n = v_group["name"].Value;
                    string v = v_group["value"].Value;
                    m_method.Add(n);
                    continue;
                }

                if (v_rgx_apimeth.IsMatch(v_str))
                {
                    GroupCollection v_group = v_rgx_apimeth.Match(v_str).Groups;                 
                    string n = v_group["name"].Value.Trim ();
                    string t = v_group["type"].Value;
                    string a = v_group["args"].Value;
                    MethodArguments[] args = GetArguments(a.Split(','));
                    GLStaticMethod d = new GLStaticMethod(n, GLUtils.TreatReturnType(t));
                    for (int i = 0; i < args.Length; i++)
                    {
                        d.AddArgs(args[i]);
                    }
                    sb.Append(d.ToString());
                    continue;
                }

            }

            List<GLFieldMethod> v_fieldm = new List<GLFieldMethod>();
            foreach (string  pstr in m_method)
            {
                v_str = ("PFN" + pstr + "PROC").ToUpper();
                if (v_delegates.ContainsKey(v_str))
                {
                    v_fieldm.Add(new GLFieldMethod(pstr, v_str));
                    GLExtendedMethod ext = new GLExtendedMethod(pstr,
                        v_delegates[v_str].Type);
                    MethodArguments[] m = v_delegates[v_str].MethodArgs();
                    for (int i = 0; i < m.Length; i++)
                    {
                        ext.AddArgs(m[i]);
                    }                    
                    sb.Append(ext.ToString());
                }
                else {
                    Console.WriteLine("No key found : " + v_str);
                }
            }

            foreach (GLFieldMethod  m in v_fieldm)
            {
                sb.AppendLine(m.ToString());
            }


            //end stuff
            sb.AppendLine("}");
            sb.AppendLine("}");
            File.WriteAllText(outfilename, sb.ToString());

            Console.WriteLine("End");
        }

        private static MethodArguments[] GetArguments(string[] p)
        {
            List<MethodArguments> m_args = new List<MethodArguments>();
            MethodArguments v_g = null;
            string[] v_tab = null ;
            string v_declaration = null;
            string v_n = null;
            string v_ex = null;
            bool v_ispointer = false;

            for (int i = 0; i < p.Length; i++)
            {
                v_ispointer = false;
                v_ex = p[i].Trim();
                if (v_ex.Contains("*"))
                {
                    v_ex = v_ex.Replace("*", "* ");
                    v_ex = v_ex.Replace("* *", "** ");
                    v_ex = v_ex.Replace(" *", "*");

                    v_ispointer = true;
                }
                if (v_ispointer)
                {
                }
                v_tab = v_ex.Split(' ');
                switch (v_tab.Length)
                { 
                    case 1:
                       v_g = new MethodArguments ("arg_"+i, TreatArgsType(v_tab[0]));
                       m_args.Add(v_g);
                        break;
                    case 2:
                        v_n = v_tab[1];
                        if (v_n.EndsWith("*"))
                        {
                            v_tab[0] = String.Join(" ", v_tab);
                            v_n = "arg_" + i;
                        }
                        else if (v_n.Contains("*"))
                        {
                            v_n = "arg_" + i;
                            string[] st = v_n.Split('*');
                            v_tab[0] = String.Join(" ", v_tab);
                            v_tab[0] += String.Join(" ", st, 0, st.Length -1);
                        }
                        if (string.IsNullOrEmpty (v_n ))
                            v_n = "arg_" + i;

                        v_g = new MethodArguments(TreatArgName(v_n ), TreatArgsType(v_tab[0]));
                        m_args.Add(v_g);
                        break;
                    default :
                        if (v_tab[v_tab.Length - 1].Contains("*"))
                        {
                            v_tab[v_tab.Length - 1] = v_tab[v_tab.Length - 1].Replace("*", "");
                            v_tab[v_tab.Length - 2] += "*";
                        }
                        v_declaration = string.Empty;
                       v_declaration =  String.Join(" ", v_tab, 0, v_tab.Length - 1);
                       v_n = v_tab[v_tab.Length - 1];
                       if (string.IsNullOrEmpty(v_n))
                           v_n = "arg_" + i;
                        v_g = new MethodArguments(TreatArgName(v_n), TreatArgsType(v_declaration ));
                        m_args.Add(v_g);
                        break;
                }
            }
            return m_args.ToArray();
        }

        private static string TreatArgName(string p)
        {
            switch (p)
            {
                case "params": return "_params";
                case "ref": return "_ref";
                case "in": return "_in";
                case "string": return "_string";
                case "event": return "_event";
                case "object": return "_object";
                case "base": return "_base";
            }
            //Regex rg = new Regex("\\[[^\\]]*\\]");
            p = Regex.Replace(p, "\\[[^\\]]*\\]", "");
            return p;
        }

        private static string TreatArgsType(string p)
        {
            if (p.Contains("Int"))
            {
            }
            p = p.Replace(" *", "*");
            p = p.Replace("  ", " ");
            
            p = p.Replace("const GLvoid* const*", "ref IntPtr");
            p = p.Replace("const GLvoid*", "IntPtr");
            p = p.Replace("const GLvoid *", "IntPtr");
            p = p.Replace("const GLfloat*", "float[]");
            p = p.Replace("const GLdouble*", "double[]");
            p = p.Replace("const GLubyte*", "byte[]");
            p = p.Replace("const GLubyte", "byte[]");
            p = p.Replace("const GLbyte*", "byte[]");
            p = p.Replace("const GLbyte", "byte[]");
            p = p.Replace("const GLint**", "int[]");
            p = p.Replace("const GLint*", "int[]");
            p = p.Replace("const GLint", "int[]");
            p = p.Replace("const GLuint*", "uint[]");
            p = p.Replace("const GLuint", "uint[]");
            p = p.Replace("const GLchar**", "string[]");
            p = p.Replace("const GLchar **", "string[]");
            p = p.Replace("const GLchar*", "string");           
            
            p = p.Replace("const char*", "string");
            p = p.Replace("const char *", "string");
            p = p.Replace("const GLcharARB*", "string");
            p = p.Replace("const GLcharARB *", "string");

            p = p.Replace("const GLshort*", "short[]");
            p = p.Replace("const GLshort", "short[]");
            p = p.Replace("const GLushort*", "ushort[]");
            p = p.Replace("const GLushort", "ushort[]");
            p = p.Replace("const GLdouble", "double[]");
            p = p.Replace("const GLfloat", "float[]");
            p = p.Replace("const *", "IntPtr");

            p = p.Replace("const GLenum*", "uint[]");
            p = p.Replace("const void*", "IntPtr");
            p = p.Replace("const GLsizei*", "int[]");
            p = p.Replace("const GLsizei *", "int[]");
            p = p.Replace("const GLclampd *", "double[]");
            p = p.Replace("const GLclampd*", "double[]");
            p = p.Replace("const GLclampf*", "float[]");
            p = p.Replace("const GLboolean*", "bool[]");
            p = p.Replace("const GLhalf*", "IntPtr");
            if (p.Contains("const") || p.Contains ("*"))
            {
                p = "IntPtr";
                return p;
            }
            p = p.Replace("GLintptrARB", "IntPtr");
            p = p.Replace("GLhandleARB", "IntPtr");
            p = p.Replace("GLsizeiptrARB", "IntPtr");
            p = p.Replace("GLintptrARB", "IntPtr");
            p = p.Replace("GLsizeiptr", "IntPtr");
            p = p.Replace("GLintptr", "IntPtr");
            p = p.Replace("GLint64EXT", "IntPtr");
            p = p.Replace("GLuint64EXT", "IntPtr");
            p = p.Replace("GLvdpauSurfaceNV", "IntPtr");
            p = p.Replace("GLvoid**", "ref IntPtr");
            p = p.Replace("GLvoid*", "IntPtr");
            p = p.Replace("GLvoid", "IntPtr");
            p = p.Replace("GLenum", "uint");
            p = p.Replace("GLuint", "uint");
        
            p = p.Replace("GLint", "int");
            p = p.Replace("GLfloat", "float");
            p = p.Replace("GLdouble", "double");
            p = p.Replace("GLclampf", "float");
            p = p.Replace("GLsizei", "int");
            p = p.Replace("GLhalf", "int");
            p = p.Replace("GLbyte", "sbyte");
            p = p.Replace("GLubyte", "byte");
            p = p.Replace("GLushort", "ushort");
            p = p.Replace("GLshort", "short");
            p = p.Replace("GLclampd", "double");

                           

            p = p.Replace("GLboolean", "bool");
            p = p.Replace("GLbitfield", "uint");
            p = p.Replace("cl_context", "IntPtr");
            p = p.Replace("cl_event", "IntPtr");
            p = p.Replace("uint64", "long");
            p = p.Replace("GLsync", "IntPtr");

            
           // p = p.Replace("Int ", "int ");
            return p;
        }
    }
}

