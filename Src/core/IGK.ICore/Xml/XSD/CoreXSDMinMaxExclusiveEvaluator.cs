using System;

namespace IGK.ICore.Xml.XSD
{
    internal class CoreXSDMinMaxExclusiveEvaluator : ICoreXSDRestrictionEvaluator
    {
        public float? MaxExclude { get; internal set; }
        public float? MinExclude { get; internal set; }
        public bool Match(string v)
        {
            bool r = false ;
            float i = 0.0f;

            if (float.TryParse(v, out i))
            {
                if ((( MaxExclude == null ) || (i <MaxExclude))
                    && 
                    ((MinExclude == null) || (i > MinExclude)))
                    r = true;
                //if ((MaxExclude!=null) && (i<MaxExclude ))
                //    r = true ;
                //if ((MinExclude !=null) && (i > MaxExclude))
                //    r = true;
            }
            return r;

        }
    }
}