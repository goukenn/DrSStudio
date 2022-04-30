using IGK.DrSStudio.WebGLEngine.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGLGameEngineApp
{
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.Xml;

    public partial class MainForm : Form, IWebGLGameListener
    {
        /// <summary>
        /// .ctr
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var s = new WebGLDesignSurface()
            {
                Dock = DockStyle.Fill
            };
            s.BackColor = Color.Red;
            s.SetGameListener(this);
            this.Controls.Add(s);
        }

        public void InitGame(CoreXmlWebDocument document, CoreXmlElement webGLGameTag)
        {
            webGLGameTag["igk-webgl-game-attr-listener"] = "DrSStudio.WebGLGameBuilder.Debug";
            var sc = document.Head.Add("script");
            sc["type"] = "text/javascript";
            sc.Content = " igk.ready (function(){DrSStudio.WebGLGameBuilder.setDebugModeName('webGLGameApp'); });";

        }
    }
}
