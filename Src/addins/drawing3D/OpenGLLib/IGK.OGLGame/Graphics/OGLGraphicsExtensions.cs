

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLGraphicsUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GLGraphicsUtils.cs
*/
using System;
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics
{
    using IGK.GLLib;
    /// <summary>
    /// embed graphics utility fonction
    /// </summary>
    public static class OGLGraphicsExtensions
    {
      
        internal static uint GetGLDataType(this IGK.OGLGame.enuGLDataType data)
        {
            switch (data)
            {
                case IGK.OGLGame.enuGLDataType.Short:
                    return GL.GL_SHORT;                    
                case IGK.OGLGame.enuGLDataType.Int:
                    return GL.GL_INT;
                case IGK.OGLGame.enuGLDataType.Float:
                    return GL.GL_FLOAT;
                case IGK.OGLGame.enuGLDataType.Double:
                    return GL.GL_DOUBLE;
                case IGK.OGLGame.enuGLDataType.UByte:
                    return GL.GL_UNSIGNED_BYTE;
                case IGK.OGLGame.enuGLDataType.Byte:
                    return GL.GL_BYTE;
                case IGK.OGLGame.enuGLDataType.UShort:
                    return GL.GL_UNSIGNED_SHORT;
                case IGK.OGLGame.enuGLDataType.Bool :
                    return 1;
                case IGK.OGLGame.enuGLDataType.UInt :
                    return GL.GL_UNSIGNED_INT;
                default:
                    break;
            }
            return 0;
        }
        internal static uint GetClientStateType(this enuGLVertexUsage usage)
        {
            switch (usage )
            {
                case enuGLVertexUsage.Vertex:
                    return GL.GL_VERTEX_ARRAY;
                case enuGLVertexUsage.Color:
                    return GL.GL_COLOR_ARRAY;
                case enuGLVertexUsage.SecondaryColor:
                    return GL.GL_SECONDARY_COLOR_ARRAY;
                case enuGLVertexUsage.Index:
                    return GL.GL_INDEX_ARRAY;
                case enuGLVertexUsage.Normal:
                    return GL.GL_NORMAL_ARRAY;
                case enuGLVertexUsage.Fog:
                    return GL.GL_FOG_COORD_ARRAY;
                case enuGLVertexUsage.Texture:
                    return GL.GL_TEXTURE_COORD_ARRAY;
                case enuGLVertexUsage.EdgeFlag:
                    return GL.GL_EDGE_FLAG_ARRAY;
                default:
                    break;
            }
            return 0;
        }
        internal static uint GetClientStateType(this enuGLClientState state)
        {
            switch (state)
            {
                case enuGLClientState.Vertex:
                    return GL.GL_VERTEX_ARRAY;                    
                case enuGLClientState.Color:
                    return GL.GL_COLOR_ARRAY;                    
                case enuGLClientState.SecondaryColor:
                    return GL.GL_SECONDARY_COLOR_ARRAY;
                case enuGLClientState.Index:
                    return GL.GL_INDEX_ARRAY;
                case enuGLClientState.Normal:
                    return GL.GL_NORMAL_ARRAY;
                case enuGLClientState.Fog:
                    return GL.GL_FOG_COORD_ARRAY;
                case enuGLClientState.Texture:
                    return GL.GL_TEXTURE_COORD_ARRAY;
                case enuGLClientState.EdgeFlag:
                    return GL.GL_EDGE_FLAG_ARRAY;
                default:
                    break;
            }
            return 0;
        }
        /// <summary>
        /// flip only coord
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="minx"></param>
        /// <param name="maxx"></param>
        /// <param name="miny"></param>
        /// <param name="maxy"></param>
        public static void GetFlipValue(enuGLSpriteEffect effect,
            ref float minx,
            ref float maxx,
            ref float miny,
            ref float maxy)
        {
            switch (effect)
            {
                case enuGLSpriteEffect.None:
                    break;
                case enuGLSpriteEffect.FlipHorizontal:
                    {
                        //swap value
                        float tv = minx;
                        minx = maxx;
                        maxx = tv;
                    }
                    break;
                case enuGLSpriteEffect.FlipVertical:
                    {//swap value
                        float tv = miny;
                        miny = maxy;
                        maxy = tv;
                    }
                    break;
                case enuGLSpriteEffect.FlipVertical | enuGLSpriteEffect.FlipHorizontal:
                    {
                        float tv = minx;
                        minx = maxx;
                        maxx = tv;
                        tv = miny;
                        miny = maxy;
                        maxy = tv;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

