using System;

namespace IGK.ICore.Xml.XSD
{
    internal class CoreXSDMinMaxInclusiveEvaluator : ICoreXSDRestrictionEvaluator
    {
        public float? MaxInclude { get; set; }
        public float? MinInclude { get; set; }

        public CoreXSDMinMaxInclusiveEvaluator()
        {

        }

        public bool Match(string v)
        {
            float i = 0;
            bool r = false;

            if (float.TryParse(v, out i))
            {                
                r =( ((MinInclude ==null) || (i >= MinInclude)) 
                    && ((MaxInclude==null) || (i <= MaxInclude)));
            }
            return r ;
        }
    }
}