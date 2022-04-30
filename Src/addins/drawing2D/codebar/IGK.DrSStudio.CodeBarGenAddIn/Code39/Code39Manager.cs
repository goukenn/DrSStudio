

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Code39Manager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Code39Manager.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    static class Core39Manager
    {
        static Dictionary<char, string> sm_values;
        static Core39Manager()
        {
            sm_values = new Dictionary<char, string>();
            //form wikipedia
            //http://fr.wikipedia.org/wiki/Code_39
sm_values.Add ('A',"100001001");
sm_values.Add ('B',"001001001");
sm_values.Add ('C',"101001000");
sm_values.Add ('D',"000011001");
sm_values.Add ('E',"100011000");
sm_values.Add ('F',"001011000");
sm_values.Add ('G',"000001101");
sm_values.Add ('H',"100001100");
sm_values.Add ('I',"001001100");
sm_values.Add ('J',"000011100");
sm_values.Add ('K',"100000011");
sm_values.Add ('L',"001000011");
sm_values.Add ('M',"101000010");
sm_values.Add ('N',"000010011");
sm_values.Add ('O',"100010010");
sm_values.Add ('P',"001010010");
sm_values.Add ('Q',"000000111");
sm_values.Add ('R',"100000110");
sm_values.Add ('S',"001000110");
sm_values.Add ('T',"000010110");
sm_values.Add ('U',"110000001");
sm_values.Add ('V',"011000001");
sm_values.Add ('W',"111000000");
sm_values.Add ('X',"010010001");
sm_values.Add ('Y',"110010000");
sm_values.Add ('Z',"011010000");
sm_values.Add ('0',"000110100");
sm_values.Add ('1',"100100001");
sm_values.Add ('2',"001100001");
sm_values.Add ('3',"101100000");
sm_values.Add ('4',"000110001");
sm_values.Add ('5',"100110000");
sm_values.Add ('6',"001110000");
sm_values.Add ('7',"000100101");
sm_values.Add ('8',"100100100");
sm_values.Add ('9',"001100100");
sm_values.Add (' ',"011000100");
sm_values.Add ('-',"010000101");
sm_values.Add ('$',"010101000");
sm_values.Add ('%',"000101010");
sm_values.Add ('.',"110000100");
sm_values.Add ('/',"010100010");
sm_values.Add('+', "010001010");
sm_values.Add('*', "010010100");
        }
        internal static string GetValue(char p)
        {
            if (sm_values.ContainsKey(p))
                return sm_values[p];
            return null;
        }
    }
}

