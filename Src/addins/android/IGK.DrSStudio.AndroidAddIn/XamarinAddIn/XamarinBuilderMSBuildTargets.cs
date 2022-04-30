using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin
{
    public static class XamarinBuilderMSBuildTargets
    {
        public static readonly string Build;
        public static readonly string Run;
        public static readonly string BuildApk;
        public static readonly string Package;
        public static readonly string SignAndroidPackage;
        public static readonly string BuildCompile;

        static XamarinBuilderMSBuildTargets() {
            foreach (var m in typeof(XamarinBuilderMSBuildTargets).GetFields(
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ))
            {
                m.SetValue(null, m.Name);
            }
        }
    }
}
