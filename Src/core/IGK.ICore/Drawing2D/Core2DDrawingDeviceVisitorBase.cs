using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// reprensent abstract drawing 2d object visitor object 
    /// </summary>
    public abstract class Core2DDrawingDeviceVisitorBase 
    {
        private static MethodInfo sm_entryMethod;
        public const string VISIT_METHOD = "Visit";

        /// <summary>
        /// get the visitor method. 
        /// </summary>
        protected static MethodInfo VisitorMethod {
            get { return sm_entryMethod; }
        }
        static Core2DDrawingDeviceVisitorBase()
        {
            sm_entryMethod = MethodInfo.GetCurrentMethod().DeclaringType.GetMethod(VISIT_METHOD,
                new Type[] { typeof(ICoreWorkingObject) });
        }
        /// <summary>
        /// entrie visitor working object
        /// </summary>
        /// <param name="obj"></param>
        public void Visit(ICoreWorkingObject obj)
        {
            if (obj == null)
                return;
            Type t = obj.GetType();
            this.Visit(obj, t);
        }
       
        public virtual void Visit(ICoreWorkingObject obj, Type requestedType)
        {
            if (obj == null)
                return;

            sm_entryMethod.Visit(this, new Type[]{
                    requestedType,
                }, obj);
            
        }

        /// <summary>
        /// get the setup matrix element
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="proportional"></param>
        /// <param name="flipMode"></param>
        /// <returns></returns>
        protected Matrix SetupMatrix(Rectanglef src, Rectanglef dest, bool proportional, enuFlipMode flipMode)
        {

            Matrix m = new Matrix();
            if (src.IsEmpty)
                return m;

            float fx = dest.Width / (float)src.Width;
            float fy = dest.Height / (float)src.Height;


            switch (flipMode)
            {
                case enuFlipMode.None:
                    break;
                case enuFlipMode.FlipVertical:
                    fy *= -1;
                    break;
                case enuFlipMode.FlipHorizontal:
                    fx *= -1;
                    break;
                default:
                    break;
            }


            if (proportional == false)
            {
                m.Scale(fx, fy, enuMatrixOrder.Append);
                m.Translate(dest.X, dest.Y, enuMatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, dest.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(dest.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                fx = Math.Min(fx, fy);
                fy = fx;
                m.Scale(fx, fy, enuMatrixOrder.Append);
                int posx = (int)(((-src.Width * fx) / 2.0f) + (dest.Width / 2.0f));
                int posy = (int)(((-src.Height * fx) / 2.0f) + (dest.Height / 2.0f));
                m.Translate(posx + dest.X, posy + dest.Y, enuMatrixOrder.Append);


                switch (flipMode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, dest.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(dest.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            return m;
        }
     
    }
}
