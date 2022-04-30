using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    public interface IBalafonProject
    {
        IBalafonProjectItem AddFolder(string name);
        IBalafonProjectItem AddFile(string filemane, enuBalafonItemType type);
    }
}
