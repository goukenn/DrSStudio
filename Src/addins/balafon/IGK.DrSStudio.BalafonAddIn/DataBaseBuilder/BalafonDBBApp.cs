using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;

    [CoreApplication(Name="BALAFONDB", Title = "Balafon DataBase Builder")]
    public class BalafonDBBApp : CoreSingleApp
    {
        public override string AppAuthor => CoreConstant.AUTHOR;

        public override string AppName => "BALAFONDB";

        public override string Copyright => CoreConstant.COPYRIGHT;
    }
}
