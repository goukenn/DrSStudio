using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent a min max length evaluator 
    /// </summary>
    class CoreXSDMinMaxEvaluator : ICoreXSDRestrictionEvaluator
    {
        public int Min { get;set;}
        public int Max { get;set;}

        public CoreXSDMinMaxEvaluator()
        {
            this.Min = -1; //unbounded
            this.Max = -1; //unbounded
        }
        public bool Match(string v) {

            var v_t = v.Split (' ');
            bool r = true;
            if (((this.Min > 0) && (v_t.Length < this.Min) ) || 
                    ((this.Max > 0) && (v_t.Length > this.Max))){
                r = false ;
            }

            return r ;

        }
    }
}
