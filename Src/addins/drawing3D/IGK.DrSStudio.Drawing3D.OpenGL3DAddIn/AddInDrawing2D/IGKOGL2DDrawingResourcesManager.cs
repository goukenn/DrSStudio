

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDrawingResourcesManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{

    using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    ﻿using IGK.GLLib;
    using IGK.OGLGame;
    using IGK.OGLGame.Graphics;
    using IGK.OGLGame.Text;

    /// <summary>
    /// OpenGL resources manager
    /// </summary>
    public sealed class IGKOGL2DDrawingResourcesManager
    {

        static Dictionary<ICoreFont, SpriteFont > sm_spriteFonts;
        static Dictionary<Resources3DTextKeyInfo, Font3DResourcesInfo> sm_font3Dlists;
        static Dictionary<ResourcesKeyInfo, ResourcesTextureInfo> sm_textures;
        static Dictionary<ResourcesKeyInfo, QuadricResourcesInfo> sm_quadrics;
        static Dictionary<ResourcesKeyInfo, TessResourcesInfo> sm_tests;
        static Dictionary<ResourcesKeyInfo, ListResourcesInfo> sm_lists;
        
        static CoreWorkingObjectBase sm_BrushOwner;

        static IGKOGL2DDrawingResourcesManager(){ 
            sm_spriteFonts = new Dictionary<ICoreFont,SpriteFont> ();
            sm_font3Dlists = new Dictionary<Resources3DTextKeyInfo, Font3DResourcesInfo>();
            sm_textures = new Dictionary<ResourcesKeyInfo, ResourcesTextureInfo>();
            sm_quadrics = new Dictionary<ResourcesKeyInfo, QuadricResourcesInfo>();
            sm_tests = new Dictionary<ResourcesKeyInfo, TessResourcesInfo>();
            sm_BrushOwner = new RectangleElement();
            sm_lists = new Dictionary<ResourcesKeyInfo, ListResourcesInfo>();
            
        }
       
        
        public static SpriteFont GetSpriteFont(OGLGraphicsDevice device,  ICoreFont font)
        {
            if ((font == null) || (device == null))
                return null;
            //create sprite font from core font
            SpriteFont c = SpriteFont.Create(device, font.FontName, font.FontSize, font.FontStyle,
                GetWidth(font),(int) font.FontSize);
            return c;
        }

        private static int GetWidth(ICoreFont font)
        {
            using (var ft = font.ToGdiFont())
            {
                return TextRenderer.MeasureText("_", ft).Width ;
            }
        }
        public static Texture2D GetTexture(
                OGLGraphicsDevice device, 
                CoreWorkingObjectBase owner, 
                WinCoreBitmap bmp)
            {
                Texture2D text = null;
                ResourcesKeyInfo key = new ResourcesKeyInfo();
                key.m_device = device;
                key.m_owner = owner;
                if (!sm_textures.ContainsKey(key))
                {
                    text = Texture2D.Create(device, bmp.Width, bmp.Height);
                    if (text != null)
                    {
                        try
                        {
                            text.ReplaceTexture(bmp.Bitmap);
                        }
                        catch(Exception ex) {
                            CoreLog.WriteLine(ex.Message);
                        }
                      sm_textures.Add (key,   new ResourcesTextureInfo(device, bmp ,owner, text));
                    }
                }
                else{
                    text = sm_textures[key].Texture;
                    //text.ReplaceTexture(bmp.Bitmap);
                }
                return text;
               
            }

            public  static Texture2D GetTexture(OGLGraphicsDevice device, ICoreBitmap inBmp)
            {
                WinCoreBitmap bmp = inBmp as WinCoreBitmap;
                if (inBmp == null)
                    return null;


                Texture2D text = null;
                ResourcesKeyInfo key = new ResourcesKeyInfo();
                key.m_device = device;
                key.m_owner = bmp;
                if (!sm_textures.ContainsKey(key))
                {
                    text = Texture2D.Create(device, inBmp.Width, inBmp.Height);
                    if (text != null)
                    {
                        text.ReplaceTexture(bmp.Bitmap);
                        sm_textures.Add(key, new ResourcesTextureInfo(device,
                            bmp, sm_BrushOwner, text));
                    }
                }
                else
                {
                    text = sm_textures[key].Texture;
                    //text.ReplaceTexture(bmp.Bitmap);
                }
                return text;
            }

            public static QuadricResourcesInfo GetQuadric(CoreWorkingObjectBase owner, OGLGraphicsDevice device)
            {
                ResourcesKeyInfo key = new ResourcesKeyInfo();
                key.m_device = device;
                key.m_owner = owner;
                if (sm_quadrics.ContainsKey(key))
                {
                    return sm_quadrics[key];
                }
                IntPtr v_ptr = GLU.gluNewQuadric();
                QuadricResourcesInfo r = new QuadricResourcesInfo(v_ptr, owner, device);
                sm_quadrics.Add(key, r);
                return r;
            }


            struct ResourcesKeyInfo
            {
                internal OGLGraphicsDevice m_device;
                internal object m_owner;
            }
            struct Resources3DTextKeyInfo
            {
                internal OGLGraphicsDevice m_device;
                internal object m_owner;
                internal CoreFont m_font;
            }

            class ResourcesTextureInfo : DrawingResourcesInfo
            {
                private WinCoreBitmap m_bmp;
                private Texture2D m_text;

                public ResourcesTextureInfo(OGLGraphicsDevice device,
                    WinCoreBitmap bmp,
                    CoreWorkingObjectBase owner,
                    Texture2D text):base(owner, device)
                {
                    this.m_bmp = bmp;
                    this.m_text = text;                    
                    this.Owner.PropertyChanged += m_owner_PropertyChanged;

       

                }

                void m_owner_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
                {
                    switch ((enu2DPropertyChangedType)e.ID)
                    {
                        case enu2DPropertyChangedType.BitmapChanged:
                            if (this.m_text != null)
                            {
                                WinCoreBitmap bmp = e.GetParam(0) as WinCoreBitmap;
                                this.m_text.ReplaceTexture(bmp.Bitmap);
                                this.m_bmp = bmp;
                            }
                            break;
                    }
                }

                private void _ownerDisposed(object sender, EventArgs e)
                {
                    DisposeRes();
                }

                public override void Dispose()
                {
                    DisposeRes();
                }
                private void DisposeRes()
                {
                    ResourcesKeyInfo key = new ResourcesKeyInfo();
                    key.m_device = this.Device;
                    key.m_owner = this.Owner;
                    if (sm_textures.ContainsKey(key))
                    {
                        sm_textures.Remove(key);
                    }
                    if (this.m_text != null)
                    {
                        this.Device.MakeCurrent();
                        this.m_text.Dispose();
                        this.m_text = null;
                    }
                }

                private void _deviceDisposing(object sender, EventArgs e)
                {
                    DisposeRes();
                }
                public Texture2D Texture { get { return this.m_text; } }
            }

            public class DrawingResourcesInfo : IDisposable
            {
                private OGLGraphicsDevice m_device;
                private CoreWorkingObjectBase m_owner;

                public OGLGraphicsDevice Device { get { return this.m_device; } }
                public CoreWorkingObjectBase Owner { get { return this.m_owner; } }

                public DrawingResourcesInfo(CoreWorkingObjectBase owner, OGLGraphicsDevice device){
                    this.m_device = device;
                    this.m_owner = owner;
                    this.m_owner.Disposed += _ownerDisposed;
                    this.Device.Disposing += _deviceDisposing;
                }

                private void _deviceDisposing(object sender, EventArgs e)
                {
                    this.Dispose();
                }

                private void _ownerDisposed(object sender, EventArgs e)
                {
                    this.Dispose();
                }
                public virtual void Dispose()
                {
       
                    
                }
            }

            public class QuadricResourcesInfo : DrawingResourcesInfo
            {
                private IntPtr quadric;
                /// <summary>
                /// get the quadric inf o resources
                /// </summary>
                public IntPtr Quadric { get { return this.quadric; } }

                public QuadricResourcesInfo(IntPtr quadric, CoreWorkingObjectBase owner, OGLGraphicsDevice device):base(owner, device)
                {
                    this.quadric = quadric;
                }
                public override void Dispose()
                {
                    
                    GLU.gluDeleteQuadric(this.quadric);
                    this.quadric = IntPtr.Zero;
                    ResourcesKeyInfo key = new ResourcesKeyInfo();
                    key.m_device = Device;
                    key.m_owner = Owner;
                    sm_quadrics.Remove(key);
                }
            }


            public class TessResourcesInfo : DrawingResourcesInfo
            {
                private IntPtr m_tessHandle;
                /// <summary>
                /// get the quadric inf o resources
                /// </summary>
                public IntPtr TessHandle{ get { return this.m_tessHandle; } }

                public TessResourcesInfo(IntPtr quadric, CoreWorkingObjectBase owner, OGLGraphicsDevice device)
                    : base(owner, device)
                {
                    this.m_tessHandle = quadric;
                }
                public override void Dispose()
                {
                    if (this.m_tessHandle != IntPtr.Zero)
                    {
                        GLU.gluDeleteTess(this.m_tessHandle);
                        this.m_tessHandle = IntPtr.Zero;
                        ResourcesKeyInfo key = new ResourcesKeyInfo();
                        key.m_device = Device;
                        key.m_owner = Owner;
                        sm_tests.Remove(key);
                    }
                }
            }


            /// <summary>
            /// list 
            /// </summary>
            public class ListResourcesInfo : DrawingResourcesInfo
            {
                private uint m_ListId;
                private int m_Range;

                public int Range
                {
                    get { return m_Range; }
                }

                public uint ListId
                {
                    get { return m_ListId; }
                }
                public ListResourcesInfo(uint listId, int range, OGLGraphicsDevice device,
                    CoreWorkingObjectBase owner):base(owner, device)
                {
                    this.m_ListId = listId;
                    this.m_Range = range;
                }
                public override void Dispose()
                {
                    ResourcesKeyInfo key = new ResourcesKeyInfo();
                    key.m_device = Device;
                    key.m_owner = Owner;
                    if (sm_lists.ContainsKey(key))
                    {
                        GL.glDeleteLists(this.m_ListId, this.m_Range);
                        sm_lists.Remove(key);
                    }
                }
            }

        
            public class Font3DResourcesInfo : DrawingResourcesInfo
            {
                private GL3DFont m_ft;
                private enuGL3DFontFormat m_format;
                private CoreFont m_fontObject;


                public Font3DResourcesInfo(GL3DFont m_ft,
                    CoreFont fontObject,
                    CoreWorkingObjectBase owner, 
                    OGLGraphicsDevice device):base(owner, device)
                {                    
                    this.m_ft = m_ft;
                    this.m_fontObject = fontObject;
                    if (owner is IIGKOGL3DTextElement)
                    { 
                        owner.PropertyChanged += owner_PropertyChanged;
                    }
                }

                private void owner_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
                {
                    if (e.ID == enuPropertyChanged.Definition)
                    {
                        var s = (Owner as IIGKOGL3DTextElement).FontFormat;
                        if (s != m_format)
                        {
                            m_format = s;
                            this.GenerateFont();
                        }
                    }
                    if ((int)e.ID == (int)enu2DPropertyChangedType.FontChanged)
                    {

                        this.GenerateFont();

                    }
                }
                public override void Dispose()
                {
                    base.Dispose();
                }
                private void GenerateFont()
                {
                    this.DisposeFont();
                   var m_ft = IGK.OGLGame.Text.GL3DFont.Load(Device,
                   this.m_fontObject.FontName,
                   enuFontStyle.Regular,
                   this.m_format,
                   1.0f, 1.0f);
                }

                private void DisposeFont()
                {
                    if (this.m_ft != null)
                    {
                        this.m_ft.Dispose();
                    }
                }
                public GL3DFont Font3D { get { return this.m_ft; } }
            }
            /// <summary>
            /// get dessalation resources info
            /// </summary>
            /// <param name="device"></param>
            /// <param name="owner"></param>
            /// <returns></returns>
            public  static TessResourcesInfo GetTess(OGLGraphicsDevice device, CoreWorkingObjectBase owner)
            {
                ResourcesKeyInfo key = new ResourcesKeyInfo();
                key.m_device = device;
                key.m_owner = owner;
                if (sm_tests.ContainsKey(key))
                {
                    return sm_tests[key];
                }
                IntPtr v_ptr = GLU.gluNewTess();
                TessResourcesInfo r = new TessResourcesInfo(v_ptr, owner, device);
                sm_tests.Add(key, r);
                return r;
            }

            public  static IGK.OGLGame.Text.GL3DFont Get3DFont(
                OGLGraphicsDevice device, 
                CoreFont coreFont, 
                CoreWorkingObjectBase owner)
            {

                Resources3DTextKeyInfo key = new Resources3DTextKeyInfo();
                key.m_device = device;
                key.m_owner = owner;
                key.m_font = coreFont;
                if (sm_font3Dlists.ContainsKey(key))
                {
                    return sm_font3Dlists[key].Font3D;
                }
               var m_ft = IGK.OGLGame.Text.GL3DFont.Load(device, 
                   coreFont.FontName,
                   enuFontStyle.Regular, 
                   enuGL3DFontFormat.Line, 
                   1.0f, 1.0f);
               sm_font3Dlists.Add(key, new Font3DResourcesInfo(m_ft,coreFont, owner, device));

               return m_ft;
            }
    }
}
