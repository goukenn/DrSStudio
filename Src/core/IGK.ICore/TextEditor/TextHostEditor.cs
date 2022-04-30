using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.TextEditor.Actions;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.TextEditor
{
    public class TextHostEditor : ITextEditorListener, IDisposable 
    {
        const char LINESEP = '\n';

        public virtual void CancelEdition()
        {
            
        }

        private ITextEditorHost m_host;
        private ICoreCaret m_Caret; // represent the caret
        private CoreFont m_Font;
        private ActionCollections m_actions;
        private int m_SelectionStart;
        private int m_SelectionLength;
        private StringBuilder m_Buffer;        
        private int m_Line;
        private int m_Column;
       // private float m_halfmind;
        private VirtualLineCollections m_virtualLines;
        private  bool m_Visible;
        private enuTextEditorSelectionMode m_SelectionMode;
        private bool m_configuring;
        protected StringBuilder Buffer {
            get { return this.m_Buffer; }
        }
        public enuTextEditorSelectionMode SelectionMode
        {
            get { return m_SelectionMode; }
            set
            {
                if (m_SelectionMode != value)
                {
                    m_SelectionMode = value;
                }
            }
        }

        public ActionCollections Actions { get { return this.m_actions; } }
        internal  int NewLineLength {
            get { return Environment.NewLine.Length; }
        }
        /// <summary>
        /// get the default font
        /// </summary>
        public ICoreFont Font {
            get
            {
                return this.m_Font;
            }
        }
        /// <summary>
        /// get the test in buffer
        /// </summary>
        public string Text {
            get {
                return this.m_Buffer.ToString();
            }
        }

        public void Refresh() {
            if ((this.m_host != null) && this.Visible )
            {
                this.m_Caret.SetSize(1, (int)(this.GetCaretHeight() * this.m_host.ZoomY));
                _updateCaret();
                this.m_host.Invalidate();
            }
        }
        public VirtualLineCollections VirtualLines {
            get {
                return this.m_virtualLines;
            }
        }
        public int CurrentLineLength {
            get {
                if (this.CurrentLine!=null)
                    return this.CurrentLine.TextLength; 
                return 0;
            }
        }
        /// <summary>
        /// Get the selection start in buffer list
        /// </summary>
        public int SelectionStart
        {
            get { return m_SelectionStart; }
            set {
                this.m_SelectionStart = value;
            }
        }
        public int SelectionLength {
            get {
                return this.m_SelectionLength;
            }
            set {
                this.m_SelectionLength = value;
            }
        }

        public  class TextEditorCollectionBase
        {
           private TextHostEditor m_editor;
           public TextHostEditor Editor { get { return this.m_editor; } }
           public TextEditorCollectionBase(TextHostEditor editor)
            {
                this.m_editor = editor;
            
            }
        }
        public class ActionCollections : TextEditorCollectionBase
        {
            private Dictionary<enuKeys, TextEditorActionBase> m_actions;
            
            public ActionCollections(TextHostEditor editor):base(editor)
            {
                this.m_actions = new Dictionary<enuKeys, TextEditorActionBase>();

            }
            public int Count { get { return this.m_actions.Count; } }
            public override string ToString()
            {
                return string.Format("TextEditorActions:[count:{0}]", this.Count);
            }
            /// <summary>
            /// add or replace action
            /// </summary>
            /// <param name="key"></param>
            /// <param name="action"></param>
            public void Add(enuKeys key, TextEditorActionBase action)
            {
                if (this.m_actions.ContainsKey(key) == false)
                {
                    action.m_editor = this.Editor;
                    this.m_actions.Add(key, action);
                }
                else if (action !=null)
                {
                    action.m_editor = this.Editor;
                    this.m_actions[key] = action;
                }
            }

            public bool Contains(enuKeys enuKeys)
            {
                return this.m_actions.ContainsKey(enuKeys);
            }
            
            public TextEditorActionBase this[enuKeys key] {
                get {
                    if (Contains (key))
                        return this.m_actions[key];
                    return null;
                }
            }

            
        }
        public  class VirtualLineCollections : TextEditorCollectionBase , IEnumerable 
        {
            List<TextEditorVirtualLine> m_lines;
            public override string ToString()
            {
                return string.Format("VirtualLines[Count:{0}]", this.Count);
            }
            public TextEditorVirtualLine this[int index]{
                get{
                    if (this.m_lines.IndexExists (index))
                        return this.m_lines [index];
                    return null;
                }
            }

            public TextEditorVirtualLine GetLineAt(int pos)
            {
                foreach (var item in this.m_lines)
                {
                    if ((pos>=item.StartIndex ) && (pos <  
                     (item.StartIndex + item.TextLength + Editor.NewLineLength )))
                    {
                        return item;
                    }
                }
                return null;
            }
            public VirtualLineCollections(TextHostEditor editor):base(editor )
            {
                this.m_lines = new List<TextEditorVirtualLine> ();                
            }
        
public IEnumerator GetEnumerator()
{
 	return this.m_lines.GetEnumerator ();
}
            

public int Count{
get{return this.m_lines.Count ;}
}
            public void Clear(){
                this.m_lines.Clear();
            }
            public void Add(TextEditorVirtualLine lineInfo){
                if (this.m_lines.Contains(lineInfo) ==false )
                {
                    lineInfo.Editor = this.Editor;
                    this.m_lines.Add (lineInfo );
                }
            }
             public void Remove(TextEditorVirtualLine lineInfo){
                if (this.m_lines.Contains(lineInfo)  )
                {
                    //remove and update 
                    float y = lineInfo.Top;
                    float v_startIndex = lineInfo.StartIndex;
                    int  v_i = IndexOf(lineInfo);
                    int b = lineInfo.TextLength;
                    int ln = b + Environment.NewLine.Length;

                    var s = lineInfo.Text;               
                    this.Editor.m_Buffer.Remove(lineInfo.StartIndex, lineInfo.TextLength +(
                    lineInfo.IsLastLine? 0:
                        Environment.NewLine.Length));

                    lineInfo.Editor = null;
                    this.m_lines.Remove (lineInfo );                   
                }
            }

             internal int IndexOf(TextEditorVirtualLine line)
             {
                 return this.m_lines.IndexOf(line);
             }

             internal void Insert(int index, TextEditorVirtualLine virtualline)
             {
                 if (!this.m_lines.Contains(virtualline))
                 {
                     virtualline.Editor = this.Editor;
                     this.m_lines.Insert(index, virtualline);

                     //update other inf
                 }
             }
        }

        public bool Visible { get { return this.m_Visible; } set {
            this.m_Visible = value;
        } }
        public int Line { get { return this.m_Line; } internal set { this.m_Line = value; } }
        public int Column { get { return this.m_Column; } internal set {
            this.m_Column = value;
        } }
        public int CharCount { get { return this.m_Buffer.Length;  } }

        public Size2f Measure(string g)
        {
            return Measure(g, 0, this.Column);
        }
        public Size2f Measure(string g, int start, int length)
        {
            if (string.IsNullOrEmpty(g) || getHostBound().IsEmpty )
                return Size2f.Empty;
            g = g.Replace(' ', '_');
            var v_cs = CoreApplicationManager.Application.ResourcesManager.MeasureString(
                //measure text
                     g,
                     this.m_Font,
                     getHostBound(),
                     start, length
                     );
            return v_cs.Size;
        }
        private Rectanglef getHostBound() {
            if (this.m_host != null)
                return this.m_host.TextBound;
            return Rectanglef.Empty;
        }

        public Size2f Measure() {
            string[] s = this.m_Buffer.ToString().Split(LINESEP);
            string g = s[Line];
            return Measure(g);
          
        }
        public TextHostEditor()
        {
            this.m_Font = "FontName:courier; size:16pt";
            this.m_Buffer = new StringBuilder();
            this.m_virtualLines = new VirtualLineCollections(this);
            this.m_actions = new ActionCollections(this);
            this.m_Visible = false;
            this.SelectionFillColor = Colorf.DarkGray;
            this.SelectionForeColor  = Colorf.WhiteSmoke;
            //
            this.InitActions(this.m_actions);
            this.m_Font.FontDefinitionChanged += _FontDefinitionChanged;
        }

        void _FontDefinitionChanged(object sender, EventArgs e)
        {
            if (m_configuring || (this.m_host == null)|| !this.Visible )
                return;
            m_configuring = true;
            //update lines 
            foreach (TextEditorVirtualLine  c in VirtualLines)
            {
                c.Update();
            }
            this.m_Caret = this.m_host.CreateCaret(1, (int)(this.GetCaretHeight() * this.m_host.ZoomY));
            //update caret
            this.Update(false);
            this.m_host.OnFontDefinitionChanged(this.m_Font.GetDefinition());
            m_configuring = false;
        }

        protected  virtual void InitActions(ActionCollections actions)
        {
            actions.Add(enuKeys.Escape, new EscapeAction());
            actions.Add(enuKeys.Left, new MoveLeft());
            actions.Add(enuKeys.Right, new MoveRight());
            actions.Add(enuKeys.Up, new MoveUp());
            actions.Add(enuKeys.Down, new MoveDown());
            actions.Add(enuKeys.Enter , new Enter());
            actions.Add(enuKeys.Back, new MoveBack());
            actions.Add(enuKeys.Home, new MoveHome());
            actions.Add(enuKeys.End, new MoveEnd());
            actions.Add(enuKeys.Delete, new DeleteAction());
            actions.Add(enuKeys.Control | enuKeys.A, new IGK.ICore.TextEditor.Actions.SelectAllAction());
            actions.Add(enuKeys.Control | enuKeys.C, new IGK.ICore.TextEditor.Actions.CopySelecionAction());
            actions.Add(enuKeys.Control | enuKeys.X, new IGK.ICore.TextEditor.Actions.CutSelectionAction());
            actions.Add(enuKeys.Control | enuKeys.V, new IGK.ICore.TextEditor.Actions.PasteAction());


            


            actions.Add(enuKeys.Alt | enuKeys.W, new IGK.ICore.TextEditor.Actions.ChangeWrapModeAction());
            actions.Add(enuKeys.Alt | enuKeys.L, new IGK.ICore.TextEditor.Actions.ChangeTextHorizontalOrientation(enuStringAlignment.Near));
            actions.Add(enuKeys.Alt | enuKeys.C, new IGK.ICore.TextEditor.Actions.ChangeTextHorizontalOrientation(enuStringAlignment.Center));
            actions.Add(enuKeys.Alt | enuKeys.R, new IGK.ICore.TextEditor.Actions.ChangeTextHorizontalOrientation(enuStringAlignment.Far));

        }

        /// <summary>
        /// render setiong 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rectangle"></param>
        /// <param name="fillColor"></param>
        /// <param name="foreColor"></param>
        protected void RenderSelection(ICoreGraphics graphics, Rectanglef rectangle, Colorf fillColor, Colorf foreColor)
        {
            if (this.SelectionLength > 0)
            {
                Rectanglef v_rc = Rectanglef.Empty;
                switch (this.SelectionMode)
                {
                    case enuTextEditorSelectionMode.Multiline:
                        int x = this.SelectionStart;
                        int len = this.SelectionLength;
                        var l1 = this.VirtualLines.GetLineAt(x);
                        while ((l1 != null) && (len > 0))
                        {
                            string t = l1.Text;//get line length
                            int i = l1.StartIndex;
                            int k = Math.Min(t.Length - (x - i), len);

                            var rc = CoreApplicationManager.Application.ResourcesManager.GetStringRangeBounds(
                           t,
                            this.Font,
                            rectangle, x - i, k);
                            graphics.FillRectangle(fillColor, rc.X, rectangle.Y + l1.Top, rc.Width, rc.Height);
                            // graphics.SetClip (rc);
                            graphics.SmoothingMode = enuSmoothingMode.AntiAliazed;
                            graphics.TextRenderingMode = enuTextRenderingMode.AntiAliazed;
                            graphics.DrawString(t, (CoreFont)this.Font,
                                foreColor, new Rectanglef(
                                rectangle.X, rectangle.Y + l1.Top, rectangle.Width, rc.Height + 5
                                ));
                            l1 = this.VirtualLines[l1.Index + 1];
                            len -= k + 2;
                            x += k + 2;
                        }


                        break;
                    case enuTextEditorSelectionMode.Block:
                        break;
                }
            }
        }
        public virtual void Bind(ITextEditorHost item){

            if (item == null)
            {
                if (this.m_host != null)
                {
                    this.m_host.RegisterListener(null);
                    this.m_host = null;
                    this.m_Caret.Dispose();
                    this.m_Caret = null;
                }
                return ;
            }
            this.m_configuring = true;
            this.m_host = item;
            this.m_Font.CopyDefinition(item.TextFontDefinition);
            this.m_host.RegisterListener(this);
            this.m_Column = 0;
            this.m_Line = 0;
            this.m_SelectionStart = 0;
            this.m_SelectionLength = 0;
            _loadInfo(this.m_host.Text);
            //get caret height
            if (this.m_Caret != null)
                this.m_Caret.Dispose();
            this.m_Caret = this.m_host.CreateCaret(1, (int)(this.GetCaretHeight() * this.m_host.ZoomY));
            //this.m_Caret.SetSize(1, (int)(this.GetCaretHeight() * this.m_host.ZoomY));
            this.m_Caret.Show();
            this._updateCaret();
            this.m_configuring = false;
           
        }

      

        private int GetCaretHeight()
        {
            var ft = this.m_Font;
            float v_height = ft.FontSize * ft.GetLineSpacing() / ft.GetEmHeight();
            return (int)v_height;// (int)Math.Ceiling(this.m_Font.FontSize);
        }
        /// <summary>
        /// get the current virtual line
        /// </summary>
        public TextEditorVirtualLine  CurrentLine {
            get {
                return this.m_virtualLines[this.Line];
            }
        }
        private void _loadInfo(string text)
        {
            this.m_Buffer.Clear();            
            this.m_virtualLines.Clear();
            float y = 0.0f;
            int x = 0;
            if (string.IsNullOrEmpty(text))
            {
                this.m_virtualLines.Add(new TextEditorVirtualLine()
                {
                    TextLength =0                  
                 
                });
            }
            else
            {
                StringReader sr = new StringReader(text ?? string.Empty);
                string item = null;
                int r = 0;
                while ((item = sr.ReadLine()) != null)
                {
                    if (r > 0)
                        m_Buffer.AppendLine();
                    //item = item.Replace("\r", "");
                    this.m_virtualLines.Add(new TextEditorVirtualLine()
                    {
                        TextLength = item.Length                        
                    });
                    y += Measure(item, 0, item.Length).Height;
                    x = item.Length + ((r > 0) ? NewLineLength : 0);
                    m_Buffer.Append(item);
                    r = 1;
                }
                sr.Close();
            }
        }
       // System.Timers.Timer th = null;
        public void flush() {
            if (m_host == null)
                return;
            //update virtual line text
     
            //if (th == null)
            //{
            //    th = new System.Timers.Timer();
            //    th.BeginInit();
            //    th.Interval = 700;
            //    th.Elapsed += (o, e) =>
            //   {
            //       CoreMethodInvoker m = () =>
            //       {
                       var k = this.m_Buffer.ToString();
                       this.m_host.Text = k;
                       this.m_host.Invalidate();
                     //  this.th.Enabled = false;
                //   };
                //   m.BeginInvoke(null, null);
                //};
            //    th.Enabled = true;
            //    th.EndInit();
            //}
            //else 
            //    th.Enabled =  true;
                
            
        }
        
        public void OnKeyPress(int KeyCode, bool isChar)
        {
           
            if (!isChar && this.m_actions.Contains((enuKeys)KeyCode))
            {
                this.m_actions[(enuKeys)KeyCode].DoAction();
            }
            else {
                if (isChar)
                {
                    char ch = (char)KeyCode;
                    if (!char.IsControl(ch))
                    {
                        AppendCharAction(ch);
                    }
                }
            }
        }

        protected virtual void AppendCharAction(char ch)
        {
            if (this.SelectionLength > 0)
            {
                //replace the char
                if (this.SelectionLength == this.CharCount)
                {
                    string t = this.Text.Remove(this.SelectionStart, this.SelectionLength);
                    t = t.Insert(this.SelectionStart, ch.ToString());
                    this.SelectionStart = 0;
                    this.SelectionLength = 0;
                    this.Column = 0;
                    this.Line = 0;
                    this._loadInfo(t);
                    this.Column = t.Length;
                    this.Update();
                }
                else { 

                }
            }
            else
            {
                if (this.SelectionStart >= this.CharCount)
                {
                    this.m_Buffer.Append(ch);
                }
                else
                {
                    this.m_Buffer.Insert(this.SelectionStart, ch);
                }
                this.CurrentLine.TextLength++;
                this.m_Column++;
                this.SelectionStart = this.CurrentLine.StartIndex + this.m_Column;
                this.Update();
            }
        }

        private void _updateCaret()
        {
            if ((this.m_host == null) || !this.Visible)
                return;
            var rc = this.getHostBound();
            //var rs = this.Measure();
            var t =this.CurrentLine.Text ;
            float x = rc.X;
            //float zx = this.m_host.ZoomX;
            //float zy = this.m_host.ZoomY;
            int v_c = this.Column;
            var lineTop = rc.Top + ( this.VirtualLines[this.Line].Top);
            Vector2f v_loc = Vector2f.Zero;
            if (!string.IsNullOrEmpty(t))
            {
                if (!this.m_Font.WordWrap)
                {
                    t = t.Replace(" ", "_");
                }
                if (v_c > 0)
                {
                    Rectanglef v_trc = rc;
                    v_trc.Y = 0;
                    Rectanglef brs = CoreApplicationManager.Application.ResourcesManager.GetStringRangeBounds(t, this.m_Font,
                 v_trc, v_c-1, 1);
                    v_loc = m_host.GetScreenLocation(new Vector2f(brs.Right, lineTop + brs.Y ));
                }
                else
                {
                    Rectanglef brs = CoreApplicationManager.Application.ResourcesManager.GetStringRangeBounds(t, this.m_Font,
              rc, 0, 1);
                    v_loc = m_host.GetScreenLocation(new Vector2f(brs.X, lineTop));// brs.Right;
                }
            }
            else {
                switch (this.m_Font.HorizontalAlignment)
                {
                    case enuStringAlignment.Center :
                        v_loc = m_host.GetScreenLocation (new Vector2f (rc.MiddleTop.X , lineTop ));
                        break ;
                    case enuStringAlignment.Far :
                        v_loc = m_host.GetScreenLocation (new Vector2f (rc.Right , lineTop ));
                        break ;
                    case enuStringAlignment.Near :
                        v_loc = m_host.GetScreenLocation(new Vector2f(rc.X, lineTop));
                        break;
                }
            }
            x = v_loc.X;
            lineTop = v_loc.Y;
            this.m_Caret.SetPosition((int)Math.Ceiling (x), (int)lineTop);          
            this.m_Caret.Show();
        }

        public void OnGotFocus()
        {
            this._updateCaret();
            this.m_Caret.Show();
          
        }

        public void OnLostFocus()
        {
            this.m_Caret.Hide();
        }


        public virtual void Render(ICoreGraphics graphics, Rectanglef rectangle)
        {
     //       //using (CoreGraphicsPath p = new CoreGraphicsPath())
     //       //{
     //       //    p.AddText(this.m_host.Text,
     //       //        rectangle,
     //       //        this.m_Font);
     //       //    graphics.DrawPath(Colorf.Black, p);
     //       var t =  this.m_host.Text;
     //       if (string.IsNullOrEmpty(t)) 
     //           return;
     //       //graphics.DrawString(
     //       //    this.m_host.Text,
     //       //    this.m_Font,
     //       //    Colorf.Indigo,
     //       //    rectangle);
     //       var vs = this.VirtualLines[this.Line].Text;
     //       var lineTop = rectangle.Top + (this.VirtualLines[this.Line].Top);
     //       var rc = CoreApplicationManager.Instance.ResourcesManager.GetStringRangeBounds(vs, this.m_Font,
     //            new Rectanglef(rectangle.X, 0, rectangle.Width, rectangle.Height), 0, this.Column);
     //       graphics.DrawRectangle(Colorf.Red, rc.X, lineTop, rc.Width, rc.Height);

     //       CoreGraphicsPath p = new CoreGraphicsPath();
     //       p.AddText(t, rectangle, this.m_Font);

     //       rc = p.GetBounds();
     ////       rc = CoreApplicationManager.Instance.ResourcesManager.GetStringRangeBounds(t, this.m_Font,
     ////rectangle, 0, this.Column);
     //       graphics.DrawRectangle(Colorf.Blue, rc.X, rc.Y, rc.Width, rc.Height);

     //       //get char range
            
     //           //graphics.DrawString(
     //           //    this.m_Caret.Location .ToString(),
     //           //    this.m_Font,
     //           //    Colorf.Black,
     //           //    rectangle);



     //      // }
            RenderSelection(graphics, rectangle, this.SelectionFillColor, this.SelectionForeColor);
        }

        internal void RemoveAt(int index, int length, bool updateLine=true, bool updateView = true )
        {
            var v = this.m_virtualLines[this.m_Line];
            int i = v.StartIndex + index;
                if (i+length <= this.CharCount)
            { 
                this.m_Buffer.Remove(v.StartIndex + index, length);
                this.m_Column = index;
                if (updateLine)
                {
                    v.TextLength -= length;
                }
                if (updateView )
                this.Update();
            }
           
        }

        internal void AddLine()
        {//add a new line or insert a options
            this.m_Buffer.AppendLine();
            this.m_Column = 0;
        
            var v = this.m_virtualLines[this.m_Line];
            float y = v.Top + Measure(v.Text , 0,v.Text.Length).Height;
            int i = v.StartIndex + v.TextLength + Environment.NewLine.Length;
            this.m_virtualLines.Add(new TextEditorVirtualLine()
            {
                TextLength = 0,             
            });
            this.SelectionStart = this.m_Buffer.Length;
            this.m_Line++;
            this.Update();
        }
        internal void InsertLine()
        {
            var line = this.CurrentLine;
            var text = line.Text;
            var p = line.StartIndex;
            int c = this.Column ;
            if (c > text.Length)
                return;
            var s = line.Text .Substring (0, c);
            var nlength = line.TextLength - c;
            var top = this.CurrentLine.Top + this.Measure(s, 0, s.Length).Height; 
            
            this.m_Buffer.Insert(p + c, Environment.NewLine);
            line.TextLength = c;
            this.m_virtualLines.Insert(line.Index+1, new TextEditorVirtualLine()
            {
                TextLength = nlength 
                
            });
            this.m_Line++;      
            this.m_Column = 0;
            this.Update();
        }
        //udapte the editor
        internal void Update(bool flush=true)
        {
            this.SelectionStart = this.CurrentLine.StartIndex + this.Column;
            this.SelectionLength = 0;
            this._updateCaret();
            if (flush)
            {
                this.flush();
            }
            else {
                this.m_host?.Invalidate();
            }
        }

        public int LineCount { get { return this.m_virtualLines.Count; } }


        public void Reset(){
            this.m_Column = 0;
            this.m_Line = 0;
            this.m_SelectionStart = 0;
            this.m_SelectionLength = 0;
            this._loadInfo(string.Empty);
            this.Update();
        }

        public string GetText(int startindex, int length)
        {
            if (length < 0)
                return string.Empty;
            if (m_Buffer.Length >= (startindex + length))
                return m_Buffer.ToString().Substring(startindex, length);
            return string.Empty;
        }
        /// <summary>
        /// insert at the current index
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        internal void Insert(int columnIndex, string str)
        {
            var t = this.CurrentLine;

            StringReader sr = new StringReader(str ?? string.Empty);
            string item = null;
            int r = 0;
            StringBuilder sb = new StringBuilder();
            float y = t.Top;
            int x = 0;
            int v_index = t.Index;
            int c = 0;

            while ((item = sr.ReadLine()) != null)
            {
                c = item.Length;
                if (r > 0)
                {
                    sb.AppendLine();
                    this.m_virtualLines.Insert (v_index+r, new TextEditorVirtualLine()
                    {
                        TextLength = c
                    });
                    y += Measure(item, 0, item.Length).Height;
                    x = item.Length + ((r > 0) ? NewLineLength : 0);
                    sb.Append(item);
                    r++;
                }
                else {
                    sb.Append(item);
                    t.TextLength +=c;
                    c += Column;
                    r = 1;
                }
            }
            sr.Close();

            this.m_Buffer.Insert(t.StartIndex + columnIndex, sb.ToString());
            this.Column = c;
            this.Update();
        }
        
        public virtual  void Dispose()
        {
            if (this.m_Font != null)
            {
                this.m_Font.Dispose();
                this.m_Font = null;
            }
            if (this.m_Caret != null) {
                this.m_Caret.Dispose();
                this.m_Caret = null;
            }

        }

        internal void ClearBuffer()
        {
            this.Column = 0;
            this.Line = 0;
            _loadInfo(string.Empty);
            this.Update();
        }

        internal void ReplaceText(string text)
        {
            this.Column = 0;
            this._loadInfo(text);
            this.Update();
        }

        public Colorf SelectionFillColor { get; set; }

        public Colorf SelectionForeColor { get; set; }
    }
}
