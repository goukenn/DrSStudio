using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuidTool
{
    class Program
    {
        [STAThread()]
        static void Main(string[] args)
        {
            System.Windows.Forms.Clipboard.SetData(
                System.Windows.Forms.DataFormats.Text,
                Guid.NewGuid().ToString().ToUpper());
        }
    }
}
