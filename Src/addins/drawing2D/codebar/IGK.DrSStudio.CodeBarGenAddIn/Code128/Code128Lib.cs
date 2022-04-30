/*
 * 
 * source from :HTML Barcode Software Development Kit (SDK) - Open Source.htm
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    
    public class Code128Lib
    {

       
        
        public static string EncodeCode128B(string data)
        {
            var fontOutput = ConnectCode_Encode_Code128B(data);
            var output = "";
            var pattern = "";
            int cr = 0;
            for (int x = 0; x < fontOutput.Length; x++)
            {
                cr = getCode128AutoValue((int)fontOutput.Substring(x, 1)[0]);
                #region "conversion"
                switch (cr)
                {
                    case 0:
                        pattern = "bbwbbwwbbww";
                        break;
                    case 1:
                        pattern = "bbwwbbwbbww";
                        break;
                    case 2:
                        pattern = "bbwwbbwwbbw";
                        break;
                    case 3:
                        pattern = "bwwbwwbbwww";
                        break;
                    case 4:
                        pattern = "bwwbwwwbbww";
                        break;
                    case 5:
                        pattern = "bwwwbwwbbww";
                        break;
                    case 6:
                        pattern = "bwwbbwwbwww";
                        break;
                    case 7:
                        pattern = "bwwbbwwwbww";
                        break;
                    case 8:
                        pattern = "bwwwbbwwbww";
                        break;
                    case 9:
                        pattern = "bbwwbwwbwww";
                        break;
                    case 10:
                        pattern = "bbwwbwwwbww";
                        break;
                    case 11:
                        pattern = "bbwwwbwwbww";
                        break;
                    case 12:
                        pattern = "bwbbwwbbbww";
                        break;
                    case 13:
                        pattern = "bwwbbwbbbww";
                        break;
                    case 14:
                        pattern = "bwwbbwwbbbw";
                        break;
                    case 15:
                        pattern = "bwbbbwwbbww";
                        break;
                    case 16:
                        pattern = "bwwbbbwbbww";
                        break;
                    case 17:
                        pattern = "bwwbbbwwbbw";
                        break;
                    case 18:
                        pattern = "bbwwbbbwwbw";
                        break;
                    case 19:
                        pattern = "bbwwbwbbbww";
                        break;
                    case 20:
                        pattern = "bbwwbwwbbbw";
                        break;
                    case 21:
                        pattern = "bbwbbbwwbww";
                        break;
                    case 22:
                        pattern = "bbwwbbbwbww";
                        break;
                    case 23:
                        pattern = "bbbwbbwbbbw";
                        break;
                    case 24:
                        pattern = "bbbwbwwbbww";
                        break;
                    case 25:
                        pattern = "bbbwwbwbbww";
                        break;
                    case 26:
                        pattern = "bbbwwbwwbbw";
                        break;
                    case 27:
                        pattern = "bbbwbbwwbww";
                        break;
                    case 28:
                        pattern = "bbbwwbbwbww";
                        break;
                    case 29:
                        pattern = "bbbwwbbwwbw";
                        break;
                    case 30:
                        pattern = "bbwbbwbbwww";
                        break;
                    case 31:
                        pattern = "bbwbbwwwbbw";
                        break;
                    case 32:
                        pattern = "bbwwwbbwbbw";
                        break;
                    case 33:
                        pattern = "bwbwwwbbwww";
                        break;
                    case 34:
                        pattern = "bwwwbwbbwww";
                        break;
                    case 35:
                        pattern = "bwwwbwwwbbw";
                        break;
                    case 36:
                        pattern = "bwbbwwwbwww";
                        break;
                    case 37:
                        pattern = "bwwwbbwbwww";
                        break;
                    case 38:
                        pattern = "bwwwbbwwwbw";
                        break;
                    case 39:
                        pattern = "bbwbwwwbwww";
                        break;
                    case 40:
                        pattern = "bbwwwbwbwww";
                        break;
                    case 41:
                        pattern = "bbwwwbwwwbw";
                        break;
                    case 42:
                        pattern = "bwbbwbbbwww";
                        break;
                    case 43:
                        pattern = "bwbbwwwbbbw";
                        break;
                    case 44:
                        pattern = "bwwwbbwbbbw";
                        break;
                    case 45:
                        pattern = "bwbbbwbbwww";
                        break;
                    case 46:
                        pattern = "bwbbbwwwbbw";
                        break;
                    case 47:
                        pattern = "bwwwbbbwbbw";
                        break;
                    case 48:
                        pattern = "bbbwbbbwbbw";
                        break;
                    case 49:
                        pattern = "bbwbwwwbbbw";
                        break;
                    case 50:
                        pattern = "bbwwwbwbbbw";
                        break;
                    case 51:
                        pattern = "bbwbbbwbwww";
                        break;
                    case 52:
                        pattern = "bbwbbbwwwbw";
                        break;
                    case 53:
                        pattern = "bbwbbbwbbbw";
                        break;
                    case 54:
                        pattern = "bbbwbwbbwww";
                        break;
                    case 55:
                        pattern = "bbbwbwwwbbw";
                        break;
                    case 56:
                        pattern = "bbbwwwbwbbw";
                        break;
                    case 57:
                        pattern = "bbbwbbwbwww";
                        break;
                    case 58:
                        pattern = "bbbwbbwwwbw";
                        break;
                    case 59:
                        pattern = "bbbwwwbbwbw";
                        break;
                    case 60:
                        pattern = "bbbwbbbbwbw";
                        break;
                    case 61:
                        pattern = "bbwwbwwwwbw";
                        break;
                    case 62:
                        pattern = "bbbbwwwbwbw";
                        break;
                    case 63:
                        pattern = "bwbwwbbwwww";
                        break;
                    case 64:
                        pattern = "bwbwwwwbbww";
                        break;
                    case 65:
                        pattern = "bwwbwbbwwww";
                        break;
                    case 66:
                        pattern = "bwwbwwwwbbw";
                        break;
                    case 67:
                        pattern = "bwwwwbwbbww";
                        break;
                    case 68:
                        pattern = "bwwwwbwwbbw";
                        break;
                    case 69:
                        pattern = "bwbbwwbwwww";
                        break;
                    case 70:
                        pattern = "bwbbwwwwbww";
                        break;
                    case 71:
                        pattern = "bwwbbwbwwww";
                        break;
                    case 72:
                        pattern = "bwwbbwwwwbw";
                        break;
                    case 73:
                        pattern = "bwwwwbbwbww";
                        break;
                    case 74:
                        pattern = "bwwwwbbwwbw";
                        break;
                    case 75:
                        pattern = "bbwwwwbwwbw";
                        break;
                    case 76:
                        pattern = "bbwwbwbwwww";
                        break;
                    case 77:
                        pattern = "bbbbwbbbwbw";
                        break;
                    case 78:
                        pattern = "bbwwwwbwbww";
                        break;
                    case 79:
                        pattern = "bwwwbbbbwbw";
                        break;
                    case 80:
                        pattern = "bwbwwbbbbww";
                        break;
                    case 81:
                        pattern = "bwwbwbbbbww";
                        break;
                    case 82:
                        pattern = "bwwbwwbbbbw";
                        break;
                    case 83:
                        pattern = "bwbbbbwwbww";
                        break;
                    case 84:
                        pattern = "bwwbbbbwbww";
                        break;
                    case 85:
                        pattern = "bwwbbbbwwbw";
                        break;
                    case 86:
                        pattern = "bbbbwbwwbww";
                        break;
                    case 87:
                        pattern = "bbbbwwbwbww";
                        break;
                    case 88:
                        pattern = "bbbbwwbwwbw";
                        break;
                    case 89:
                        pattern = "bbwbbwbbbbw";
                        break;
                    case 90:
                        pattern = "bbwbbbbwbbw";
                        break;
                    case 91:
                        pattern = "bbbbwbbwbbw";
                        break;
                    case 92:
                        pattern = "bwbwbbbbwww";
                        break;
                    case 93:
                        pattern = "bwbwwwbbbbw";
                        break;
                    case 94:
                        pattern = "bwwwbwbbbbw";
                        break;
                    case 95:
                        pattern = "bwbbbbwbwww";
                        break;
                    case 96:
                        pattern = "bwbbbbwwwbw";
                        break;
                    case 97:
                        pattern = "bbbbwbwbwww";
                        break;
                    case 98:
                        pattern = "bbbbwbwwwbw";
                        break;
                    case 99:
                        pattern = "bwbbbwbbbbw";
                        break;
                    case 100:
                        pattern = "bwbbbbwbbbw";
                        break;
                    case 101:
                        pattern = "bbbwbwbbbbw";
                        break;
                    case 102:
                        pattern = "bbbbwbwbbbw";
                        break;
                    case 103:
                        pattern = "bbwbwwwwbww";
                        break;
                    case 104:
                        pattern = "bbwbwwbwwww";
                        break;
                    case 105:
                        pattern = "bbwbwwbbbww";
                        break;
                    case 106:
                        pattern = "bbwwwbbbwbwbb";
                        break;
                    default: break;
                }
                #endregion
#pragma warning disable IDE0054 // Use compound assignment
                output = output + pattern;
#pragma warning restore IDE0054 // Use compound assignment
                //Console.WriteLine("output ::: " + output + ":::"+cr);
            }
            return output;
        }
        public static string ConnectCode_Encode_Code128B(string data)
        {
            var cd = "";
            var Result = "";
            var filtereddata = filterInput(data);
            var filteredlength = filtereddata.Length;
            int c = 0;

            if (filteredlength > 254)
            {
                filtereddata = filtereddata.Substring(0, 254);
            }
            cd = generateCheckDigitB(filtereddata);

            for (int x = 0; x < filtereddata.Length; x++)
            {
                c = filtereddata[x];
                if (c == 127)
                {
#pragma warning disable IDE0054 // Use compound assignment
                    c = c + 100;
#pragma warning restore IDE0054 // Use compound assignment
                }
                else
                {
                    //  c = c;
                }

#pragma warning disable IDE0054 // Use compound assignment
                Result = Result + ((char)c).ToString();
#pragma warning restore IDE0054 // Use compound assignment

            }

#pragma warning disable IDE0054 // Use compound assignment
            Result = Result + cd;
#pragma warning restore IDE0054 // Use compound assignment

            var startc = 236;
            var stopc = 238;
            Result = ((char)startc).ToString() + Result + ((char)stopc).ToString();
            
            return Result;
        }
        public static string generateCheckDigitB(string data)
        {//128b
            var datalength = 0;
            var Sum = 104;
            var Result = -1;
            var strResult = "";

            datalength = data.Length;

            var x = 0;
            var Weight = 1;
            var num = 0;

            for (x = 0; x < data.Length; x++)
            {
                num = (int)data[x];
#pragma warning disable IDE0054 // Use compound assignment
                Sum = Sum + (getCode128BValue(num) * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment
            }

            Result = Sum % 103;
            strResult = getCode128BCharacter(Result).ToString();
            return strResult;
        }
  
    
        public static char getCode128BCharacter(int inputvalue)
        {

            if ((inputvalue <= 94) && (inputvalue >= 0))
            {
#pragma warning disable IDE0054 // Use compound assignment
                inputvalue = inputvalue + 32;
#pragma warning restore IDE0054 // Use compound assignment
            }
            else if ((inputvalue <= 106) && (inputvalue >= 95))
            {
                inputvalue = inputvalue + 100 + 32;
            }
            else
            {
                inputvalue = -1;
            }

            return (char)inputvalue;

        }

        public static int getCode128BValue(int inputchar)
        {

            var returnvalue = 0;

            if ((inputchar <= 127) && (inputchar >= 32))
            {
                returnvalue = (inputchar - 32);
            }
            else
            {
                returnvalue = -1;
            }

            return returnvalue;

        }



        public static string filterInput(string data)
        {
            var Result = "";
            var datalength = data.Length;
            for (int x = 0; x < datalength; x++)
            {
                if ((data[x] >= 32) && (data[x] <= 127))
                {
#pragma warning disable IDE0054 // Use compound assignment
                    Result = Result + data.Substring(x, 1);
#pragma warning restore IDE0054 // Use compound assignment
                }
            }

            return Result;
        }
        public static int getCode128AutoValue(int inputvalue)
        {
            if (inputvalue <= 94 + 32 && inputvalue >= 0 + 32)
#pragma warning disable IDE0054 // Use compound assignment
                inputvalue = inputvalue - 32;
#pragma warning restore IDE0054 // Use compound assignment
            else if (inputvalue <= 106 + 32 + 100 && inputvalue >= 95 + 32 + 100)
                inputvalue = inputvalue - 32 - 100;
            else
                inputvalue = -1;
            return inputvalue;
        }


      

        #region for 128 auto



    

        //private static int getCode128AutoValue(int inputvalue)
        //{
        //    if (inputvalue <= 94 + 32 && inputvalue >= 0 + 32)
        //        inputvalue = inputvalue - 32;
        //    else if (inputvalue <= 106 + 32 + 100 && inputvalue >= 95 + 32 + 100)
        //        inputvalue = inputvalue - 32 - 100;
        //    else
        //        inputvalue = -1;

        //    return inputvalue;
        //}
        public static string EncodeCode128Auto(string data)
        {
            var fontOutput = ConnectCode_Encode_Code128Auto(data);
            var output = "";
            var pattern = "";
            for (int x = 0; x < fontOutput.Length; x++)
            {
                #region " B "
                switch (getCode128AutoValue(fontOutput.Substring(x, 1)[0]))
                {
                    case 0:
                        pattern = "bbwbbwwbbww";
                        break;
                    case 1:
                        pattern = "bbwwbbwbbww";
                        break;
                    case 2:
                        pattern = "bbwwbbwwbbw";
                        break;
                    case 3:
                        pattern = "bwwbwwbbwww";
                        break;
                    case 4:
                        pattern = "bwwbwwwbbww";
                        break;
                    case 5:
                        pattern = "bwwwbwwbbww";
                        break;
                    case 6:
                        pattern = "bwwbbwwbwww";
                        break;
                    case 7:
                        pattern = "bwwbbwwwbww";
                        break;
                    case 8:
                        pattern = "bwwwbbwwbww";
                        break;
                    case 9:
                        pattern = "bbwwbwwbwww";
                        break;
                    case 10:
                        pattern = "bbwwbwwwbww";
                        break;
                    case 11:
                        pattern = "bbwwwbwwbww";
                        break;
                    case 12:
                        pattern = "bwbbwwbbbww";
                        break;
                    case 13:
                        pattern = "bwwbbwbbbww";
                        break;
                    case 14:
                        pattern = "bwwbbwwbbbw";
                        break;
                    case 15:
                        pattern = "bwbbbwwbbww";
                        break;
                    case 16:
                        pattern = "bwwbbbwbbww";
                        break;
                    case 17:
                        pattern = "bwwbbbwwbbw";
                        break;
                    case 18:
                        pattern = "bbwwbbbwwbw";
                        break;
                    case 19:
                        pattern = "bbwwbwbbbww";
                        break;
                    case 20:
                        pattern = "bbwwbwwbbbw";
                        break;
                    case 21:
                        pattern = "bbwbbbwwbww";
                        break;
                    case 22:
                        pattern = "bbwwbbbwbww";
                        break;
                    case 23:
                        pattern = "bbbwbbwbbbw";
                        break;
                    case 24:
                        pattern = "bbbwbwwbbww";
                        break;
                    case 25:
                        pattern = "bbbwwbwbbww";
                        break;
                    case 26:
                        pattern = "bbbwwbwwbbw";
                        break;
                    case 27:
                        pattern = "bbbwbbwwbww";
                        break;
                    case 28:
                        pattern = "bbbwwbbwbww";
                        break;
                    case 29:
                        pattern = "bbbwwbbwwbw";
                        break;
                    case 30:
                        pattern = "bbwbbwbbwww";
                        break;
                    case 31:
                        pattern = "bbwbbwwwbbw";
                        break;
                    case 32:
                        pattern = "bbwwwbbwbbw";
                        break;
                    case 33:
                        pattern = "bwbwwwbbwww";
                        break;
                    case 34:
                        pattern = "bwwwbwbbwww";
                        break;
                    case 35:
                        pattern = "bwwwbwwwbbw";
                        break;
                    case 36:
                        pattern = "bwbbwwwbwww";
                        break;
                    case 37:
                        pattern = "bwwwbbwbwww";
                        break;
                    case 38:
                        pattern = "bwwwbbwwwbw";
                        break;
                    case 39:
                        pattern = "bbwbwwwbwww";
                        break;
                    case 40:
                        pattern = "bbwwwbwbwww";
                        break;
                    case 41:
                        pattern = "bbwwwbwwwbw";
                        break;
                    case 42:
                        pattern = "bwbbwbbbwww";
                        break;
                    case 43:
                        pattern = "bwbbwwwbbbw";
                        break;
                    case 44:
                        pattern = "bwwwbbwbbbw";
                        break;
                    case 45:
                        pattern = "bwbbbwbbwww";
                        break;
                    case 46:
                        pattern = "bwbbbwwwbbw";
                        break;
                    case 47:
                        pattern = "bwwwbbbwbbw";
                        break;
                    case 48:
                        pattern = "bbbwbbbwbbw";
                        break;
                    case 49:
                        pattern = "bbwbwwwbbbw";
                        break;
                    case 50:
                        pattern = "bbwwwbwbbbw";
                        break;
                    case 51:
                        pattern = "bbwbbbwbwww";
                        break;
                    case 52:
                        pattern = "bbwbbbwwwbw";
                        break;
                    case 53:
                        pattern = "bbwbbbwbbbw";
                        break;
                    case 54:
                        pattern = "bbbwbwbbwww";
                        break;
                    case 55:
                        pattern = "bbbwbwwwbbw";
                        break;
                    case 56:
                        pattern = "bbbwwwbwbbw";
                        break;
                    case 57:
                        pattern = "bbbwbbwbwww";
                        break;
                    case 58:
                        pattern = "bbbwbbwwwbw";
                        break;
                    case 59:
                        pattern = "bbbwwwbbwbw";
                        break;
                    case 60:
                        pattern = "bbbwbbbbwbw";
                        break;
                    case 61:
                        pattern = "bbwwbwwwwbw";
                        break;
                    case 62:
                        pattern = "bbbbwwwbwbw";
                        break;
                    case 63:
                        pattern = "bwbwwbbwwww";
                        break;
                    case 64:
                        pattern = "bwbwwwwbbww";
                        break;
                    case 65:
                        pattern = "bwwbwbbwwww";
                        break;
                    case 66:
                        pattern = "bwwbwwwwbbw";
                        break;
                    case 67:
                        pattern = "bwwwwbwbbww";
                        break;
                    case 68:
                        pattern = "bwwwwbwwbbw";
                        break;
                    case 69:
                        pattern = "bwbbwwbwwww";
                        break;
                    case 70:
                        pattern = "bwbbwwwwbww";
                        break;
                    case 71:
                        pattern = "bwwbbwbwwww";
                        break;
                    case 72:
                        pattern = "bwwbbwwwwbw";
                        break;
                    case 73:
                        pattern = "bwwwwbbwbww";
                        break;
                    case 74:
                        pattern = "bwwwwbbwwbw";
                        break;
                    case 75:
                        pattern = "bbwwwwbwwbw";
                        break;
                    case 76:
                        pattern = "bbwwbwbwwww";
                        break;
                    case 77:
                        pattern = "bbbbwbbbwbw";
                        break;
                    case 78:
                        pattern = "bbwwwwbwbww";
                        break;
                    case 79:
                        pattern = "bwwwbbbbwbw";
                        break;
                    case 80:
                        pattern = "bwbwwbbbbww";
                        break;
                    case 81:
                        pattern = "bwwbwbbbbww";
                        break;
                    case 82:
                        pattern = "bwwbwwbbbbw";
                        break;
                    case 83:
                        pattern = "bwbbbbwwbww";
                        break;
                    case 84:
                        pattern = "bwwbbbbwbww";
                        break;
                    case 85:
                        pattern = "bwwbbbbwwbw";
                        break;
                    case 86:
                        pattern = "bbbbwbwwbww";
                        break;
                    case 87:
                        pattern = "bbbbwwbwbww";
                        break;
                    case 88:
                        pattern = "bbbbwwbwwbw";
                        break;
                    case 89:
                        pattern = "bbwbbwbbbbw";
                        break;
                    case 90:
                        pattern = "bbwbbbbwbbw";
                        break;
                    case 91:
                        pattern = "bbbbwbbwbbw";
                        break;
                    case 92:
                        pattern = "bwbwbbbbwww";
                        break;
                    case 93:
                        pattern = "bwbwwwbbbbw";
                        break;
                    case 94:
                        pattern = "bwwwbwbbbbw";
                        break;
                    case 95:
                        pattern = "bwbbbbwbwww";
                        break;
                    case 96:
                        pattern = "bwbbbbwwwbw";
                        break;
                    case 97:
                        pattern = "bbbbwbwbwww";
                        break;
                    case 98:
                        pattern = "bbbbwbwwwbw";
                        break;
                    case 99:
                        pattern = "bwbbbwbbbbw";
                        break;
                    case 100:
                        pattern = "bwbbbbwbbbw";
                        break;
                    case 101:
                        pattern = "bbbwbwbbbbw";
                        break;
                    case 102:
                        pattern = "bbbbwbwbbbw";
                        break;
                    case 103:
                        pattern = "bbwbwwwwbww";
                        break;
                    case 104:
                        pattern = "bbwbwwbwwww";
                        break;
                    case 105:
                        pattern = "bbwbwwbbbww";
                        break;
                    case 106:
                        pattern = "bbwwwbbbwbwbb";
                        break;
                    default: break;
                }
#pragma warning disable IDE0054 // Use compound assignment
                output = output + pattern;
#pragma warning restore IDE0054 // Use compound assignment
                #endregion
            }

            return output;

        }
        private static string getCode128CCharacterAuto(int inputvalue)
        {

            if ((inputvalue <= 94) && (inputvalue >= 0))
#pragma warning disable IDE0054 // Use compound assignment
                inputvalue = inputvalue + 32;
#pragma warning restore IDE0054 // Use compound assignment
            else if ((inputvalue <= 106) && (inputvalue >= 95))
                inputvalue = inputvalue + 32 + 100;
            else
                inputvalue = -1;


            return ((char)inputvalue).ToString();

        }
        private static string OptimizeNumbers(string data, int x, string strResult, int num)
        {

            var BtoC = ((char)231).ToString();
#pragma warning disable IDE0054 // Use compound assignment
            strResult = strResult + BtoC;
#pragma warning restore IDE0054 // Use compound assignment

            var endpoint = x + num;
            while (x < endpoint)
            {
                var twonum = Convert.ToInt32(data.Substring(x, 2), 10);
#pragma warning disable IDE0054 // Use compound assignment
                strResult = strResult + getCode128CCharacterAuto(twonum);
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                x = x + 2;
#pragma warning restore IDE0054 // Use compound assignment
            }

            var CtoB = ((char)232);
#pragma warning disable IDE0054 // Use compound assignment
            strResult = strResult + CtoB;
#pragma warning restore IDE0054 // Use compound assignment
            return strResult;
        }

        private static int ScanAhead_8orMore_Numbers(string data, int x)
        {
            var numNumbers = 0;
            var exitx = 0;
            while ((x < data.Length) && (exitx == 0))
            {
                var barcodechar = data.Substring(x, 1);
                var barcodevalue = (int)barcodechar[0];
                if (barcodevalue >= 48 && barcodevalue <= 57)
#pragma warning disable IDE0054 // Use compound assignment
                    numNumbers = numNumbers + 1;
#pragma warning restore IDE0054 // Use compound assignment
                else
                    exitx = 1;

#pragma warning disable IDE0054 // Use compound assignment
                x = x + 1;
#pragma warning restore IDE0054 // Use compound assignment

            }
            if (numNumbers > 8)
            {
                if (numNumbers % 2 == 1)
#pragma warning disable IDE0054 // Use compound assignment
                    numNumbers = numNumbers - 1;
#pragma warning restore IDE0054 // Use compound assignment
            }
            return numNumbers;

        }

        private static string getAutoSwitchingAB(string data)
        {

            var datalength = 0;
            var strResult = "";
            var shiftchar = ((char)230).ToString();
            int num = 0;
            datalength = data.Length;
            var barcodechar = "";
            var x = 0;

            for (x = 0; x < datalength; x++)
            {
                barcodechar = data.Substring(x, 1);
                var barcodevalue = barcodechar[0];

                if (barcodevalue == 31)
                {
                    barcodechar = ((char)barcodechar[0] + 96 + 100).ToString();
#pragma warning disable IDE0054 // Use compound assignment
                    strResult = strResult + barcodechar;
#pragma warning restore IDE0054 // Use compound assignment
                }
                else if (barcodevalue == 127)
                {
                    barcodechar = ((char)barcodechar[0] + 100).ToString();
#pragma warning disable IDE0054 // Use compound assignment
                    strResult = strResult + barcodechar;
#pragma warning restore IDE0054 // Use compound assignment
                }
                else
                {
                    num = ScanAhead_8orMore_Numbers(data, x);

                    if (num >= 8)
                    {
                        strResult = OptimizeNumbers(data, x, strResult, num);
#pragma warning disable IDE0054 // Use compound assignment
                        x = x + num;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                        x = x - 1;
#pragma warning restore IDE0054 // Use compound assignment
                    }
                    else
#pragma warning disable IDE0054 // Use compound assignment
                        strResult = strResult + barcodechar;
#pragma warning restore IDE0054 // Use compound assignment
                }

            }
            return strResult;

        }

       

        //private static string filterInput(string data)
        //{
        //    var Result = String.Empty;
        //    var datalength = data.Length;
        //    for (int x = 0; x < datalength; x++)
        //    {
        //        if (data[x] >= 0 && data[x] <= 127)
        //        {
        //            Result = Result + data.Substring(x, 1);
        //        }
        //    }
        //    return Result;
        //}

        private static string ConnectCode_Encode_Code128Auto(string data)
        {

            var cd = "";
            var Result = "";
            int num = 0;
            var filtereddata = filterInput(data);
            var filteredlength = filtereddata.Length;

            if (filteredlength > 254)
            {
                filtereddata = filtereddata.Substring(0, 254);
            }

            if (detectAllNumbers(filtereddata) == 0)
            {
                filtereddata = addShift(filtereddata);
                cd = generateCheckDigit_Code128ABAuto(filtereddata);

                filtereddata = getAutoSwitchingAB(filtereddata);

#pragma warning disable IDE0054 // Use compound assignment
                filtereddata = filtereddata + cd;
#pragma warning restore IDE0054 // Use compound assignment
                Result = filtereddata;

                var startc = 236;
                var stopc = 238;
                Result = ((char)startc) + Result + ((char)stopc);
            }
            else
            {

                cd = generateCheckDigit_Code128CAuto(filtereddata);
                var lenFiltered = filtereddata.Length;

#pragma warning disable IDE0054 // Use compound assignment
                for (int x = 0; x < lenFiltered; x = x + 2)
#pragma warning restore IDE0054 // Use compound assignment
                {
                    var tstr = filtereddata.Substring(x, 2);
                    num = Convert.ToInt32(tstr);//,10);
#pragma warning disable IDE0054 // Use compound assignment
                    Result = Result + getCode128CCharacterAuto(num);
#pragma warning restore IDE0054 // Use compound assignment
                }


#pragma warning disable IDE0054 // Use compound assignment
                Result = Result + cd;
#pragma warning restore IDE0054 // Use compound assignment
                int startc = 237;
                int stopc = 238;
                Result = ((char)startc).ToString() + Result + ((char)stopc).ToString();
                //Result = ((char)startc) + Result + ((char)stopc);

            }
            //Result=html_decode(html_escape(Result));	               
            return Result;
        }


        private static string generateCheckDigit_Code128CAuto(string data)
        {
            var datalength = 0;
            var Sum = 105;
            var Result = -1;
            var strResult = "";

            datalength = data.Length;

            var x = 0;
            var Weight = 1;
            var num = 0;

#pragma warning disable IDE0054 // Use compound assignment
            for (x = 0; x < data.Length; x = x + 2)
#pragma warning restore IDE0054 // Use compound assignment
            {
                num = Convert.ToInt32(data.Substring(x, 2), 10);
#pragma warning disable IDE0054 // Use compound assignment
                Sum = Sum + (num * Weight);
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment
            }

            Result = Sum % 103;
            strResult = getCode128CCharacter(Result);
            return strResult;
        }

        private static string generateCheckDigit_Code128ABAuto(string data)
        {
            var datalength = 0;
            var Sum = 104;
            var Result = -1;
            var strResult = "";
            int endpoint = 0;
            datalength = data.Length;

            var num = 0;
            var Weight = 1;

            var x = 0;
            while (x < data.Length)
            {
                num = ScanAhead_8orMore_Numbers(data, x);
                if (num >= 8)
                {
                    endpoint = x + num;

                    var BtoC = 99;
#pragma warning disable IDE0054 // Use compound assignment
                    Sum = Sum + (BtoC * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment

                    while (x < endpoint)
                    {
                        num = Convert.ToInt32(data.Substring(x, 2));
#pragma warning disable IDE0054 // Use compound assignment
                        Sum = Sum + (num * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                        x = x + 2;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                        Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment

                    }
                    var CtoB = 100;
#pragma warning disable IDE0054 // Use compound assignment
                    Sum = Sum + (CtoB * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment

                }
                else
                {
                    num = (byte)data[x];
#pragma warning disable IDE0054 // Use compound assignment
                    Sum = Sum + (getCode128ABValueAuto(num) * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    x = x + 1;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment

                }
            }
            Result = Sum % 103;
            strResult = getCode128ABCharacterAuto(Result);
            return strResult;
        }

        private static string getCode128ABCharacterAuto(int inputvalue)
        {
            if ((inputvalue <= 94) && (inputvalue >= 0))
#pragma warning disable IDE0054 // Use compound assignment
                inputvalue = inputvalue + 32;
#pragma warning restore IDE0054 // Use compound assignment
            else if ((inputvalue <= 106) && (inputvalue >= 95))
                inputvalue = inputvalue + 100 + 32;
            else
                inputvalue = -1;


            return ((char)inputvalue).ToString();
        }

        private static string getCode128CCharacter(int inputvalue)
        {
            if ((inputvalue <= 94) && (inputvalue >= 0))
#pragma warning disable IDE0054 // Use compound assignment
                inputvalue = inputvalue + 32;
#pragma warning restore IDE0054 // Use compound assignment
            else if ((inputvalue <= 106) && (inputvalue >= 95))
                inputvalue = inputvalue + 32 + 100;
            else
                inputvalue = -1;

            return ((char)inputvalue).ToString();


        }
        private static int getCode128ABValueAuto(int inputchar)
        {

            var returnvalue = 0;

            if ((inputchar <= 31) && (inputchar >= 0))
                returnvalue = (inputchar + 64);
            else if ((inputchar <= 127) && (inputchar >= 32))
                returnvalue = (inputchar - 32);
            else if (inputchar == 230)
                returnvalue = 98;
            else
                returnvalue = -1;

            return returnvalue;

        }

        static int detectAllNumbers(string data)
        {
            var allnumbers = 1;

            var datalength = data.Length;

            if (datalength % 2 == 1)
                allnumbers = 0;
            else
            {
                for (int x = 0; x < datalength; x++)
                {
                    var barcodechar = (int)data[x];
                    if ((barcodechar <= 57) && (barcodechar >= 48))
                    {
                    }
                    else
                        allnumbers = 0;
                }
            }

            return allnumbers;

        }

        private static string addShift(string data)
        {
            var datalength = 0;
            var strResult = "";
            var shiftchar = ((char)230);

            datalength = data.Length;

            for (int x = 0; x < datalength; x++)
            {
                var barcodechar = data.Substring(x, 1);
                var barcodevalue = barcodechar[0];
                if ((barcodevalue <= 31) && (barcodevalue >= 0))
                {

#pragma warning disable IDE0054 // Use compound assignment
                    strResult = strResult + shiftchar;
#pragma warning restore IDE0054 // Use compound assignment
                    barcodechar = (barcodechar[0] + 96).ToString();
#pragma warning disable IDE0054 // Use compound assignment
                    strResult = strResult + barcodechar;
#pragma warning restore IDE0054 // Use compound assignment
                }
                else
#pragma warning disable IDE0054 // Use compound assignment
                    strResult = strResult + barcodechar;
#pragma warning restore IDE0054 // Use compound assignment



            }

            return strResult;

        }


        #endregion

        #region for 128 c

        public static string EncodeCode128C(string data)
        {
            var fontOutput = ConnectCode_Encode_Code128C(data);
            var output = "";
            var pattern = "";
            for (int x = 0; x < fontOutput.Length; x++)
            {
                switch ((int)getCode128AutoValue(fontOutput.Substring(x, 1)[0]))
                {
                    case 0:
                        pattern = "bbwbbwwbbww";
                        break;
                    case 1:
                        pattern = "bbwwbbwbbww";
                        break;
                    case 2:
                        pattern = "bbwwbbwwbbw";
                        break;
                    case 3:
                        pattern = "bwwbwwbbwww";
                        break;
                    case 4:
                        pattern = "bwwbwwwbbww";
                        break;
                    case 5:
                        pattern = "bwwwbwwbbww";
                        break;
                    case 6:
                        pattern = "bwwbbwwbwww";
                        break;
                    case 7:
                        pattern = "bwwbbwwwbww";
                        break;
                    case 8:
                        pattern = "bwwwbbwwbww";
                        break;
                    case 9:
                        pattern = "bbwwbwwbwww";
                        break;
                    case 10:
                        pattern = "bbwwbwwwbww";
                        break;
                    case 11:
                        pattern = "bbwwwbwwbww";
                        break;
                    case 12:
                        pattern = "bwbbwwbbbww";
                        break;
                    case 13:
                        pattern = "bwwbbwbbbww";
                        break;
                    case 14:
                        pattern = "bwwbbwwbbbw";
                        break;
                    case 15:
                        pattern = "bwbbbwwbbww";
                        break;
                    case 16:
                        pattern = "bwwbbbwbbww";
                        break;
                    case 17:
                        pattern = "bwwbbbwwbbw";
                        break;
                    case 18:
                        pattern = "bbwwbbbwwbw";
                        break;
                    case 19:
                        pattern = "bbwwbwbbbww";
                        break;
                    case 20:
                        pattern = "bbwwbwwbbbw";
                        break;
                    case 21:
                        pattern = "bbwbbbwwbww";
                        break;
                    case 22:
                        pattern = "bbwwbbbwbww";
                        break;
                    case 23:
                        pattern = "bbbwbbwbbbw";
                        break;
                    case 24:
                        pattern = "bbbwbwwbbww";
                        break;
                    case 25:
                        pattern = "bbbwwbwbbww";
                        break;
                    case 26:
                        pattern = "bbbwwbwwbbw";
                        break;
                    case 27:
                        pattern = "bbbwbbwwbww";
                        break;
                    case 28:
                        pattern = "bbbwwbbwbww";
                        break;
                    case 29:
                        pattern = "bbbwwbbwwbw";
                        break;
                    case 30:
                        pattern = "bbwbbwbbwww";
                        break;
                    case 31:
                        pattern = "bbwbbwwwbbw";
                        break;
                    case 32:
                        pattern = "bbwwwbbwbbw";
                        break;
                    case 33:
                        pattern = "bwbwwwbbwww";
                        break;
                    case 34:
                        pattern = "bwwwbwbbwww";
                        break;
                    case 35:
                        pattern = "bwwwbwwwbbw";
                        break;
                    case 36:
                        pattern = "bwbbwwwbwww";
                        break;
                    case 37:
                        pattern = "bwwwbbwbwww";
                        break;
                    case 38:
                        pattern = "bwwwbbwwwbw";
                        break;
                    case 39:
                        pattern = "bbwbwwwbwww";
                        break;
                    case 40:
                        pattern = "bbwwwbwbwww";
                        break;
                    case 41:
                        pattern = "bbwwwbwwwbw";
                        break;
                    case 42:
                        pattern = "bwbbwbbbwww";
                        break;
                    case 43:
                        pattern = "bwbbwwwbbbw";
                        break;
                    case 44:
                        pattern = "bwwwbbwbbbw";
                        break;
                    case 45:
                        pattern = "bwbbbwbbwww";
                        break;
                    case 46:
                        pattern = "bwbbbwwwbbw";
                        break;
                    case 47:
                        pattern = "bwwwbbbwbbw";
                        break;
                    case 48:
                        pattern = "bbbwbbbwbbw";
                        break;
                    case 49:
                        pattern = "bbwbwwwbbbw";
                        break;
                    case 50:
                        pattern = "bbwwwbwbbbw";
                        break;
                    case 51:
                        pattern = "bbwbbbwbwww";
                        break;
                    case 52:
                        pattern = "bbwbbbwwwbw";
                        break;
                    case 53:
                        pattern = "bbwbbbwbbbw";
                        break;
                    case 54:
                        pattern = "bbbwbwbbwww";
                        break;
                    case 55:
                        pattern = "bbbwbwwwbbw";
                        break;
                    case 56:
                        pattern = "bbbwwwbwbbw";
                        break;
                    case 57:
                        pattern = "bbbwbbwbwww";
                        break;
                    case 58:
                        pattern = "bbbwbbwwwbw";
                        break;
                    case 59:
                        pattern = "bbbwwwbbwbw";
                        break;
                    case 60:
                        pattern = "bbbwbbbbwbw";
                        break;
                    case 61:
                        pattern = "bbwwbwwwwbw";
                        break;
                    case 62:
                        pattern = "bbbbwwwbwbw";
                        break;
                    case 63:
                        pattern = "bwbwwbbwwww";
                        break;
                    case 64:
                        pattern = "bwbwwwwbbww";
                        break;
                    case 65:
                        pattern = "bwwbwbbwwww";
                        break;
                    case 66:
                        pattern = "bwwbwwwwbbw";
                        break;
                    case 67:
                        pattern = "bwwwwbwbbww";
                        break;
                    case 68:
                        pattern = "bwwwwbwwbbw";
                        break;
                    case 69:
                        pattern = "bwbbwwbwwww";
                        break;
                    case 70:
                        pattern = "bwbbwwwwbww";
                        break;
                    case 71:
                        pattern = "bwwbbwbwwww";
                        break;
                    case 72:
                        pattern = "bwwbbwwwwbw";
                        break;
                    case 73:
                        pattern = "bwwwwbbwbww";
                        break;
                    case 74:
                        pattern = "bwwwwbbwwbw";
                        break;
                    case 75:
                        pattern = "bbwwwwbwwbw";
                        break;
                    case 76:
                        pattern = "bbwwbwbwwww";
                        break;
                    case 77:
                        pattern = "bbbbwbbbwbw";
                        break;
                    case 78:
                        pattern = "bbwwwwbwbww";
                        break;
                    case 79:
                        pattern = "bwwwbbbbwbw";
                        break;
                    case 80:
                        pattern = "bwbwwbbbbww";
                        break;
                    case 81:
                        pattern = "bwwbwbbbbww";
                        break;
                    case 82:
                        pattern = "bwwbwwbbbbw";
                        break;
                    case 83:
                        pattern = "bwbbbbwwbww";
                        break;
                    case 84:
                        pattern = "bwwbbbbwbww";
                        break;
                    case 85:
                        pattern = "bwwbbbbwwbw";
                        break;
                    case 86:
                        pattern = "bbbbwbwwbww";
                        break;
                    case 87:
                        pattern = "bbbbwwbwbww";
                        break;
                    case 88:
                        pattern = "bbbbwwbwwbw";
                        break;
                    case 89:
                        pattern = "bbwbbwbbbbw";
                        break;
                    case 90:
                        pattern = "bbwbbbbwbbw";
                        break;
                    case 91:
                        pattern = "bbbbwbbwbbw";
                        break;
                    case 92:
                        pattern = "bwbwbbbbwww";
                        break;
                    case 93:
                        pattern = "bwbwwwbbbbw";
                        break;
                    case 94:
                        pattern = "bwwwbwbbbbw";
                        break;
                    case 95:
                        pattern = "bwbbbbwbwww";
                        break;
                    case 96:
                        pattern = "bwbbbbwwwbw";
                        break;
                    case 97:
                        pattern = "bbbbwbwbwww";
                        break;
                    case 98:
                        pattern = "bbbbwbwwwbw";
                        break;
                    case 99:
                        pattern = "bwbbbwbbbbw";
                        break;
                    case 100:
                        pattern = "bwbbbbwbbbw";
                        break;
                    case 101:
                        pattern = "bbbwbwbbbbw";
                        break;
                    case 102:
                        pattern = "bbbbwbwbbbw";
                        break;
                    case 103:
                        pattern = "bbwbwwwwbww";
                        break;
                    case 104:
                        pattern = "bbwbwwbwwww";
                        break;
                    case 105:
                        pattern = "bbwbwwbbbww";
                        break;
                    case 106:
                        pattern = "bbwwwbbbwbwbb";
                        break;
                    default: break;
                }
#pragma warning disable IDE0054 // Use compound assignment
                output = output + pattern;
#pragma warning restore IDE0054 // Use compound assignment
            }
            return output;
        }


        private static string ConnectCode_Encode_Code128C(string data)
        {//128c
            var cd = "";
            var Result = "";
            var filtereddata = filterInput(data);
            int filteredLength = filtereddata.Length;
            int num = 0;

            if (filteredLength > 253)
            {
                filtereddata = filtereddata.Substring(0, 253);
            }

            if (filtereddata.Length % 2 == 1)
            {
                filtereddata = "0" + filtereddata;
            }

            cd = generateCheckDigitC(filtereddata);
#pragma warning disable IDE0054 // Use compound assignment
            for (int x = 0; x < filtereddata.Length; x = x + 2)
#pragma warning restore IDE0054 // Use compound assignment
            {
                num = Convert.ToInt32(filtereddata.Substring(x, 2), 10);
#pragma warning disable IDE0054 // Use compound assignment
                Result = Result + getCode128CCharacter(num);
#pragma warning restore IDE0054 // Use compound assignment
            }

#pragma warning disable IDE0054 // Use compound assignment
            Result = Result + cd;
#pragma warning restore IDE0054 // Use compound assignment

            var startc = 237;
            var stopc = 238;
            Result = ((char)startc) + Result + ((char)stopc);

            return Result;
        }
         private static string generateCheckDigitC(string data)
        {//128c
            var dataLength = 0;
            var Sum = 105;
            var Result = -1;
            var strResult = "";

            dataLength = data.Length;

            var Weight = 1;
            var num = 0;

#pragma warning disable IDE0054 // Use compound assignment
            for (int x = 0; x < data.Length; x = x + 2)
#pragma warning restore IDE0054 // Use compound assignment
            {
                num = Convert.ToInt32(data.Substring(x, 2), 10);
#pragma warning disable IDE0054 // Use compound assignment
                Sum = Sum + (num * Weight);
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment
            }

            Result = Sum % 103;
            strResult = getCode128CCharacter(Result);
            return strResult;
        }
        #endregion



         #region A
         public  static string EncodeCode128A(string data)
         {
             var fontOutput = ConnectCode_Encode_Code128A(data);
             var output = "";
             var pattern = "";
             for (int x = 0; x < fontOutput.Length; x++)
             {
                 switch ((int)getCode128AutoValue(fontOutput.Substring(x, 1)[0]))
                 {
                     case 0:
                         pattern = "bbwbbwwbbww";
                         break;
                     case 1:
                         pattern = "bbwwbbwbbww";
                         break;
                     case 2:
                         pattern = "bbwwbbwwbbw";
                         break;
                     case 3:
                         pattern = "bwwbwwbbwww";
                         break;
                     case 4:
                         pattern = "bwwbwwwbbww";
                         break;
                     case 5:
                         pattern = "bwwwbwwbbww";
                         break;
                     case 6:
                         pattern = "bwwbbwwbwww";
                         break;
                     case 7:
                         pattern = "bwwbbwwwbww";
                         break;
                     case 8:
                         pattern = "bwwwbbwwbww";
                         break;
                     case 9:
                         pattern = "bbwwbwwbwww";
                         break;
                     case 10:
                         pattern = "bbwwbwwwbww";
                         break;
                     case 11:
                         pattern = "bbwwwbwwbww";
                         break;
                     case 12:
                         pattern = "bwbbwwbbbww";
                         break;
                     case 13:
                         pattern = "bwwbbwbbbww";
                         break;
                     case 14:
                         pattern = "bwwbbwwbbbw";
                         break;
                     case 15:
                         pattern = "bwbbbwwbbww";
                         break;
                     case 16:
                         pattern = "bwwbbbwbbww";
                         break;
                     case 17:
                         pattern = "bwwbbbwwbbw";
                         break;
                     case 18:
                         pattern = "bbwwbbbwwbw";
                         break;
                     case 19:
                         pattern = "bbwwbwbbbww";
                         break;
                     case 20:
                         pattern = "bbwwbwwbbbw";
                         break;
                     case 21:
                         pattern = "bbwbbbwwbww";
                         break;
                     case 22:
                         pattern = "bbwwbbbwbww";
                         break;
                     case 23:
                         pattern = "bbbwbbwbbbw";
                         break;
                     case 24:
                         pattern = "bbbwbwwbbww";
                         break;
                     case 25:
                         pattern = "bbbwwbwbbww";
                         break;
                     case 26:
                         pattern = "bbbwwbwwbbw";
                         break;
                     case 27:
                         pattern = "bbbwbbwwbww";
                         break;
                     case 28:
                         pattern = "bbbwwbbwbww";
                         break;
                     case 29:
                         pattern = "bbbwwbbwwbw";
                         break;
                     case 30:
                         pattern = "bbwbbwbbwww";
                         break;
                     case 31:
                         pattern = "bbwbbwwwbbw";
                         break;
                     case 32:
                         pattern = "bbwwwbbwbbw";
                         break;
                     case 33:
                         pattern = "bwbwwwbbwww";
                         break;
                     case 34:
                         pattern = "bwwwbwbbwww";
                         break;
                     case 35:
                         pattern = "bwwwbwwwbbw";
                         break;
                     case 36:
                         pattern = "bwbbwwwbwww";
                         break;
                     case 37:
                         pattern = "bwwwbbwbwww";
                         break;
                     case 38:
                         pattern = "bwwwbbwwwbw";
                         break;
                     case 39:
                         pattern = "bbwbwwwbwww";
                         break;
                     case 40:
                         pattern = "bbwwwbwbwww";
                         break;
                     case 41:
                         pattern = "bbwwwbwwwbw";
                         break;
                     case 42:
                         pattern = "bwbbwbbbwww";
                         break;
                     case 43:
                         pattern = "bwbbwwwbbbw";
                         break;
                     case 44:
                         pattern = "bwwwbbwbbbw";
                         break;
                     case 45:
                         pattern = "bwbbbwbbwww";
                         break;
                     case 46:
                         pattern = "bwbbbwwwbbw";
                         break;
                     case 47:
                         pattern = "bwwwbbbwbbw";
                         break;
                     case 48:
                         pattern = "bbbwbbbwbbw";
                         break;
                     case 49:
                         pattern = "bbwbwwwbbbw";
                         break;
                     case 50:
                         pattern = "bbwwwbwbbbw";
                         break;
                     case 51:
                         pattern = "bbwbbbwbwww";
                         break;
                     case 52:
                         pattern = "bbwbbbwwwbw";
                         break;
                     case 53:
                         pattern = "bbwbbbwbbbw";
                         break;
                     case 54:
                         pattern = "bbbwbwbbwww";
                         break;
                     case 55:
                         pattern = "bbbwbwwwbbw";
                         break;
                     case 56:
                         pattern = "bbbwwwbwbbw";
                         break;
                     case 57:
                         pattern = "bbbwbbwbwww";
                         break;
                     case 58:
                         pattern = "bbbwbbwwwbw";
                         break;
                     case 59:
                         pattern = "bbbwwwbbwbw";
                         break;
                     case 60:
                         pattern = "bbbwbbbbwbw";
                         break;
                     case 61:
                         pattern = "bbwwbwwwwbw";
                         break;
                     case 62:
                         pattern = "bbbbwwwbwbw";
                         break;
                     case 63:
                         pattern = "bwbwwbbwwww";
                         break;
                     case 64:
                         pattern = "bwbwwwwbbww";
                         break;
                     case 65:
                         pattern = "bwwbwbbwwww";
                         break;
                     case 66:
                         pattern = "bwwbwwwwbbw";
                         break;
                     case 67:
                         pattern = "bwwwwbwbbww";
                         break;
                     case 68:
                         pattern = "bwwwwbwwbbw";
                         break;
                     case 69:
                         pattern = "bwbbwwbwwww";
                         break;
                     case 70:
                         pattern = "bwbbwwwwbww";
                         break;
                     case 71:
                         pattern = "bwwbbwbwwww";
                         break;
                     case 72:
                         pattern = "bwwbbwwwwbw";
                         break;
                     case 73:
                         pattern = "bwwwwbbwbww";
                         break;
                     case 74:
                         pattern = "bwwwwbbwwbw";
                         break;
                     case 75:
                         pattern = "bbwwwwbwwbw";
                         break;
                     case 76:
                         pattern = "bbwwbwbwwww";
                         break;
                     case 77:
                         pattern = "bbbbwbbbwbw";
                         break;
                     case 78:
                         pattern = "bbwwwwbwbww";
                         break;
                     case 79:
                         pattern = "bwwwbbbbwbw";
                         break;
                     case 80:
                         pattern = "bwbwwbbbbww";
                         break;
                     case 81:
                         pattern = "bwwbwbbbbww";
                         break;
                     case 82:
                         pattern = "bwwbwwbbbbw";
                         break;
                     case 83:
                         pattern = "bwbbbbwwbww";
                         break;
                     case 84:
                         pattern = "bwwbbbbwbww";
                         break;
                     case 85:
                         pattern = "bwwbbbbwwbw";
                         break;
                     case 86:
                         pattern = "bbbbwbwwbww";
                         break;
                     case 87:
                         pattern = "bbbbwwbwbww";
                         break;
                     case 88:
                         pattern = "bbbbwwbwwbw";
                         break;
                     case 89:
                         pattern = "bbwbbwbbbbw";
                         break;
                     case 90:
                         pattern = "bbwbbbbwbbw";
                         break;
                     case 91:
                         pattern = "bbbbwbbwbbw";
                         break;
                     case 92:
                         pattern = "bwbwbbbbwww";
                         break;
                     case 93:
                         pattern = "bwbwwwbbbbw";
                         break;
                     case 94:
                         pattern = "bwwwbwbbbbw";
                         break;
                     case 95:
                         pattern = "bwbbbbwbwww";
                         break;
                     case 96:
                         pattern = "bwbbbbwwwbw";
                         break;
                     case 97:
                         pattern = "bbbbwbwbwww";
                         break;
                     case 98:
                         pattern = "bbbbwbwwwbw";
                         break;
                     case 99:
                         pattern = "bwbbbwbbbbw";
                         break;
                     case 100:
                         pattern = "bwbbbbwbbbw";
                         break;
                     case 101:
                         pattern = "bbbwbwbbbbw";
                         break;
                     case 102:
                         pattern = "bbbbwbwbbbw";
                         break;
                     case 103:
                         pattern = "bbwbwwwwbww";
                         break;
                     case 104:
                         pattern = "bbwbwwbwwww";
                         break;
                     case 105:
                         pattern = "bbwbwwbbbww";
                         break;
                     case 106:
                         pattern = "bbwwwbbbwbwbb";
                         break;
                     default: break;
                 }
#pragma warning disable IDE0054 // Use compound assignment
                 output = output + pattern;
#pragma warning restore IDE0054 // Use compound assignment
             }
             return output;
         }
         private static string translateCharacter(int inputchar)
         {

             var returnvalue = 0;

             if ((inputchar <= 30) && (inputchar >= 0))
             {
                 returnvalue = (inputchar + 96);
             }
             else if (inputchar == 31)
             {
                 returnvalue = (inputchar + 96 + 100);
             }
             else if ((inputchar <= 95) && (inputchar >= 32))
             {
                 returnvalue = inputchar;
             }
             else
             {
                 returnvalue = -1;
             }
             return ((char)returnvalue).ToString();

         }
         private static string ConnectCode_Encode_Code128A(string data)
         {
             var cd = "";
             var Result = "";
             var filtereddata = filterInput(data);
             int filteredLength = filtereddata.Length;

             if (filteredLength > 254)
             {
                 filtereddata = filtereddata.Substring(0, 254);
             }
             cd = generateCheckDigitA(filtereddata);
             for (int x = 0; x < filtereddata.Length; x++)
             {
                 var c = "";
                 c = translateCharacter(filtereddata[x]);
#pragma warning disable IDE0054 // Use compound assignment
                 Result = Result + c;
#pragma warning restore IDE0054 // Use compound assignment
             }

#pragma warning disable IDE0054 // Use compound assignment
             Result = Result + cd;
#pragma warning restore IDE0054 // Use compound assignment

             var startc = 235;
             var stopc = 238;
             Result = ((char)startc).ToString() + Result + ((char)stopc).ToString();

             return Result;
         }

         private static int getCode128AValue(int inputchar)
         {

             var returnvalue = 0;

             if ((inputchar <= 31) && (inputchar >= 0))
             {
                 returnvalue = (inputchar + 64);
             }
             else if ((inputchar <= 95) && (inputchar >= 32))
             {
                 returnvalue = (inputchar - 32);
             }
             else
             {
                 returnvalue = -1;
             }

             return returnvalue;

         }
         public static string generateCheckDigitA(string data)
         {
             var dataLength = 0;
             var Sum = 103;
             var Result = -1;
             var strResult = "";

             dataLength = data.Length;

             var Weight = 1;
             var num = 0;

             for (int x = 0; x < data.Length; x++)
             {
                 num = data[x];
#pragma warning disable IDE0054 // Use compound assignment
                 Sum = Sum + (getCode128AValue(num) * (Weight));
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                 Weight = Weight + 1;
#pragma warning restore IDE0054 // Use compound assignment
             }

             Result = Sum % 103;
             strResult = getCode128ACharacter(Result);
             return strResult;
         }
         private static string getCode128ACharacter(int inputvalue)
         {

             if ((inputvalue <= 94) && (inputvalue >= 0))
             {
#pragma warning disable IDE0054 // Use compound assignment
                 inputvalue = inputvalue + 32;
#pragma warning restore IDE0054 // Use compound assignment
             }
             else if ((inputvalue <= 106) && (inputvalue >= 95))
             {
                 inputvalue = inputvalue + 100 + 32;
             }
             else
             {
                 inputvalue = -1;
             }

             return ((char)inputvalue).ToString();
         }
         #endregion
    }
}