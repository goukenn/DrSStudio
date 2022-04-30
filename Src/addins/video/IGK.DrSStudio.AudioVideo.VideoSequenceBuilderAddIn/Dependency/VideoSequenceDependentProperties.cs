using IGK.ICore;
using IGK.ICore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Dependency
{

    /*
     * <Drawing2DDocument VideoSequence.Duration="2" VideoSequence.FX="{Name=InvertColor, Duration=1, ...}" >
     * 
     * </Drawing2DDocument>
     * */
    [CoreDependencyName("VideoSequence")]//register dependent
    public class VideoSequenceDependentProperties : CoreDependencyObject 
    {
        static VideoSequenceDependentProperties() {     
        }
        public virtual bool CanBeApplyTo(ICoreWorkingObject obj)
        {
            return false;
        }
    }
}
