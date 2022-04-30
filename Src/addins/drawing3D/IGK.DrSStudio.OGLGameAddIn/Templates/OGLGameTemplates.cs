

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLGameTemplates.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;
using IGK.DrSStudio.OGLGame;
using IGK.DrSStudio.OGLGame.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * TEMPLATE TO CREATE A OPENGL GAME ARCHITECTURE
 * 
 * */

[assembly: CoreWorkingProjectTemplate(
    Name = OGLGameConstant.PROJECTNAME)]
[assembly: CoreWorkingProjectItemTemplate(OGLGameConstant.PROJECTNAME + ".AndroidGame")]
[assembly: CoreWorkingProjectItemTemplate(OGLGameConstant.PROJECTNAME + ".WindowsGame")]
[assembly: CoreWorkingProjectItemTemplate(OGLGameConstant.PROJECTNAME + ".PS3Game")]
[assembly: CoreWorkingProjectItemTemplate(OGLGameConstant.PROJECTNAME + ".IOSGame")]



