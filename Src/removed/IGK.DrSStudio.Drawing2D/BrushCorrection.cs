

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BrushCorrection.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:BrushCorrection.cs
*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//namespace IGK.DrSStudio.Drawing2D
//{
//    /// <summary>
//    /// using for brush correction
//    /// </summary>
//    public sealed class BrushCorrection : IDisposable
//    {
//        Brush m_br;
//        private PenType m_PenType;
//        private Matrix m_transform;
//        private Matrix m_btransform;
//        private Matrix m_globalTransform;
//        private Matrix m_localTransform;
//        public Brush Brush { get { return this.m_br; } }
//        public PenType PenType
//        {
//            get { return m_PenType; }
//        }
//        /// <summary>
//        /// .ctr
//        /// </summary>
//        private BrushCorrection( Matrix globalTransform, Matrix localTransform)
//        {
//            this.m_localTransform = localTransform;
//            this.m_globalTransform = globalTransform;
//        }
//        /// <summary>
//        /// create a brush correction from texture brush
//        /// </summary>
//        /// <param name="br"></param>
//        /// <returns></returns>
//        public static BrushCorrection Create(TextureBrush br, Matrix globalTransform, Matrix localTransform)
//        {
//            if (br == null) return null;
//            BrushCorrection v_t = new BrushCorrection(globalTransform , localTransform );
//            v_t.m_PenType = PenType.TextureFill;
//            v_t.m_br = br;
//            return v_t;
//        }
//        /// <summary>
//        /// create a brush corrrection from lignear gradient brush
//        /// </summary>
//        /// <param name="br"></param>
//        /// <returns></returns>
//        public static BrushCorrection Create(LinearGradientBrush br,Matrix globalTransform, Matrix localTransform)
//       {
//            if (br == null) return null;
//            BrushCorrection v_t = new BrushCorrection(globalTransform , localTransform );
//            v_t.m_PenType = PenType.LinearGradient;
//            v_t.m_br = br;
//            return v_t;
//        }
//        /// <summary>
//        /// create a brush correction from brush item
//        /// </summary>
//        /// <param name="br"></param>
//        /// <returns></returns>
//        public static BrushCorrection Create(PathGradientBrush br, Matrix globalTransform, Matrix localTransform)
//        {
//            if (br == null) return null;
//            BrushCorrection v_t = new BrushCorrection(globalTransform , localTransform );
//            v_t.m_PenType = PenType.PathGradient;
//            v_t.m_br = br;
//            return v_t;
//        }
//        /// <summary>
//        /// save the brush item
//        /// </summary>
//        public void Save()
//        {
//            //switch (this.PenType)
//            //{
//            //    case PenType.HatchFill:
//            //        break;
//            //    case PenType.LinearGradient:
//            //        this.m_transform = (this.m_br as LinearGradientBrush).Transform.Clone() as Matrix;
//            //        break;
//            //    case PenType.PathGradient:
//            //        this.m_transform = (this.m_br as PathGradientBrush).Transform.Clone() as Matrix;
//            //        break;
//            //    case PenType.SolidColor:
//            //        break;
//            //    case PenType.TextureFill:
//            //        this.m_transform = (this.m_br as TextureBrush).Transform.Clone() as Matrix;
//            //        break;
//            //    default:
//            //        break;
//            //}
//            this.m_btransform = this.m_transform.Clone() as Matrix;
//        }
//        public void Restore()
//        {
//            this.SetMatrix(this.m_btransform);
//        }
//        public static BrushCorrection Correct(Brush br, Matrix globalTransform, Matrix localTransform)
//        {
//            if (br is TextureBrush)
//            {
//                return BrushCorrection.Create(br as TextureBrush, globalTransform , localTransform );
//            }
//            else if (br is PathGradientBrush)
//            {
//                return BrushCorrection.Create(br as PathGradientBrush, globalTransform, localTransform);
//            }
//            else if (br is LinearGradientBrush)
//            {
//                return BrushCorrection.Create(br as LinearGradientBrush, globalTransform, localTransform);
//            }
//            return null;
//        }
//        void SetMatrix(Matrix m)
//        {
//            switch (this.m_PenType)
//            {
//                case PenType.HatchFill:
//                    break;
//                case PenType.LinearGradient:
//                    (this.m_br as LinearGradientBrush).Transform = m_transform;
//                    break;
//                case PenType.PathGradient:
//                    (this.m_br as PathGradientBrush).Transform = m;
//                    break;
//                case PenType.SolidColor:
//                    break;
//                case PenType.TextureFill:
//                    (this.m_br as TextureBrush).Transform = m_transform;
//                    break;
//                default:
//                    break;
//            }
//        }
//        public void InvertAndMult(Matrix mat)
//        {
//            Matrix m = mat.Clone() as Matrix;
//            m.Invert();
//            switch (this.m_PenType)
//            {
//                case PenType.HatchFill:
//                    break;
//                case PenType.LinearGradient:
//                    (this.m_br as LinearGradientBrush).MultiplyTransform(m);
//                    break;
//                case PenType.PathGradient:                  
//                    PathGradientBrush c_br = this.m_br as PathGradientBrush;                    
//                    c_br.MultiplyTransform(m, enuMatrixOrder.Append );//.Prepend );
//                    break;
//                case PenType.SolidColor:
//                    break;
//                case PenType.TextureFill:
//                    (this.m_br as TextureBrush).MultiplyTransform(m, enuMatrixOrder.Append );//.Transform = m_transform;
//                    break;
//                default:
//                    break;
//            }
//            m.Dispose();
//        }
//        public void Dispose()
//        {
//            if (this.m_transform != null) this.m_transform.Dispose();
//            if (this.m_btransform != null) this.m_btransform.Dispose();
//        }
//    }
//}

