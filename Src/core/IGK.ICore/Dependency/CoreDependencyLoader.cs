using IGK.ICore.JSon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Dependency
{
    /// <summary>
    /// class code pendendency loader 
    /// </summary>
    public class CoreDependencyLoader : CoreWorkingObjectBase, ICoreWorkingObject 
    {
        private CoreDependencyInfo m_propertyInfo;

        public CoreDependencyLoader(CoreDependencyInfo propertyInfo)
        {
            this.m_propertyInfo = propertyInfo;
        }
        public override string ToString()
        {
            return base.ToString();
        }
        protected override void ReadAttributes(Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        public override bool AttributeExist(string name)
        {
            return base.AttributeExist(name);
        }
        /// <summary>
        /// load value of this property item
        /// </summary>
        /// <param name="item">item that is cibling proprerties</param>
        /// <param name="valueOrExpression">expression to evaluete</param>
        public virtual void LoadValue(ICoreWorkingObject item, string valueOrExpression)
        {

            var s = item as CoreDependencyObject;
            if (s == null)
                return;
            CoreDependencyObject dp = null;
            if (m_propertyInfo.Parent == null)
            {
                 //for root dependent
           
                dp = CoreDependencyObject.GetDenpendency(m_propertyInfo.DeclaringType);
                string jsonexpression = IGK.ICore.JSon.CoreJSon.ExpressionRegex;
               
                if (System.Text.RegularExpressions.Regex.IsMatch(valueOrExpression.Trim(), jsonexpression))
                {
                    CoreJSonDependencyLoader v_loader = new CoreJSonDependencyLoader(s, dp);
                    new CoreJSon().Evaluate(item, v_loader, valueOrExpression);
                    return;
                }
              
            }
            var sdp = CoreDependencyObject.GetRegisterProperty(m_propertyInfo);
            s.SetValue(sdp, valueOrExpression);

        }
    }
}
