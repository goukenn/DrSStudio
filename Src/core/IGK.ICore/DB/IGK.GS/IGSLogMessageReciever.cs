﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.WinUI
{
    public interface IGSLogMessageReciever
    {
        void SendMessage(string message);
    }
}