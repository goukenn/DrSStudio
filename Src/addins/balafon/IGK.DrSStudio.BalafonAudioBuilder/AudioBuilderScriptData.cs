using System.Runtime.InteropServices;

namespace IGK.DRSStudio.BalafonAudioBuilder
{
    [ComVisible(true)]
    public  class AudioBuilderScriptData
    {

        public AudioBuilderScriptData()
        {
        }
        /// <summary>
        /// get the data from component
        /// </summary>
        public string Data { get; private set; }

        public void loadComponent(string data) {
            this.Data = data;
        }
    }
}