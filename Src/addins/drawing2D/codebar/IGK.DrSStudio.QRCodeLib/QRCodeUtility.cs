

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: QRCodeUtility.cs
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
file:SystemUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.QRCodeLib
{
    /// <summary>
    /// Contains conversion support elements such as classes, interfaces and static methods.
    /// </summary>
    public class QRCodeUtility
    {
        /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceStream">The source Stream to read from.</param>
        /// <param name="target">Contains the array of characteres read from the source Stream.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source Stream.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
        public static System.Int32 ReadInput(System.IO.Stream sourceStream, sbyte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0)
                return 0;
            byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(receiver, start, count);
            // Returns -1 if EOF
            if (bytesRead == 0)
                return -1;
            for (int i = start; i < start + bytesRead; i++)
                target[i] = (sbyte)receiver[i];
            return bytesRead;
        }
        /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceTextReader">The source TextReader to read from</param>
        /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
        public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, short[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0) return 0;
            char[] charArray = new char[target.Length];
            int bytesRead = sourceTextReader.Read(charArray, start, count);
            // Returns -1 if EOF
            if (bytesRead == 0) return -1;
            for (int index = start; index < start + bytesRead; index++)
                target[index] = (short)charArray[index];
            return bytesRead;
        }
        /*******************************/
        /// <summary>
        /// Writes the exception stack trace to the received stream
        /// </summary>
        /// <param name="throwable">Exception to obtain information from</param>
        /// <param name="stream">Output sream used to write to</param>
        public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
        {
            stream.Write(throwable.StackTrace);
            stream.Flush();
        }
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2 << ~bits);
        }
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2L << ~bits);
        }
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }
        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of bytes
        /// </summary>
        /// <param name="sbyteArray">The array of sbytes to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] byteArray = null;
            if (sbyteArray != null)
            {
                byteArray = new byte[sbyteArray.Length];
                for (int index = 0; index < sbyteArray.Length; index++)
                    byteArray[index] = (byte)sbyteArray[index];
            }
            return byteArray;
        }
        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }
        /// <summary>
        /// Converts a array of object-type instances to a byte-type array.
        /// </summary>
        /// <param name="tempObjectArray">Array to convert.</param>
        /// <returns>An array of byte type elements.</returns>
        public static byte[] ToByteArray(System.Object[] tempObjectArray)
        {
            byte[] byteArray = null;
            if (tempObjectArray != null)
            {
                byteArray = new byte[tempObjectArray.Length];
                for (int index = 0; index < tempObjectArray.Length; index++)
                    byteArray[index] = (byte)tempObjectArray[index];
            }
            return byteArray;
        }
        /*******************************/
        /// <summary>
        /// Receives a byte array and returns it transformed in an sbyte array
        /// </summary>
        /// <param name="byteArray">Byte array to process</param>
        /// <returns>The transformed array</returns>
        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] sbyteArray = null;
            if (byteArray != null)
            {
                sbyteArray = new sbyte[byteArray.Length];
                for (int index = 0; index < byteArray.Length; index++)
                    sbyteArray[index] = (sbyte)byteArray[index];
            }
            return sbyteArray;
        }
        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of chars
        /// </summary>
        /// <param name="sByteArray">The array of sbytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(sbyte[] sByteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(ToByteArray(sByteArray));
        }
        /// <summary>
        /// Converts an array of bytes to an array of chars
        /// </summary>
        /// <param name="byteArray">The array of bytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(byte[] byteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
        }

        // Because CLDC1.0 does not support Math.sqrt(), we have to define it manually.
        // faster sqrt (GuoQing Hu's FIX)
        public static int sqrt(int val)
        {
            //		using estimate method from http://www.azillionmonkeys.com/qed/sqroot.html 
            //		Console.out.print(val + ", " + (int)Math.sqrt(val) + ", "); 
            int temp, g = 0, b = 0x8000, bshft = 15;
            do
            {
                if (val >= (temp = (((g << 1) + b) << bshft--)))
                {
                    g += b;
                    val -= temp;
                }
            }
            while ((b >>= 1) > 0);
            return g;
        }
        // for au by KDDI Profile Phase 3.0
        //	public static int[][] parseImage(Image image) {
        //		int width = image.getWidth();
        //		int height = image.getHeight();
        //		Image mutable = Image.createImage(width, height);
        //		Graphics g = mutable.getGraphics();
        //		g.drawImage(image, 0, 0, Graphics.TOP|Graphics.LEFT);
        //		ExtensionGraphics eg = (ExtensionGraphics) g;
        //		int[][] result = new int[width][height];
        //		
        //		for (int x = 0; x < width; x++) {
        //			for (int y = 0; y < height; y++) {
        //				result[x][y] = eg.getPixel(x, y);
        //			}
        //		}
        //		return result;
        //	}
        //	
        //	public static int[][] parseImage(byte[] imageData) {
        //		return parseImage(Image.createImage(imageData, 0, imageData.length));
        //	}
        public static bool IsUniCode(String value)
        {
            byte[] ascii = AsciiStringToByteArray(value);
            byte[] unicode = UnicodeStringToByteArray(value);
            string value1 = FromASCIIByteArray(ascii);
            string value2 = FromUnicodeByteArray(unicode);
            if (value1 != value2)
                return true;
            return false;
        }
        public static bool IsUnicode(byte[] byteData)
        {
            string value1 = FromASCIIByteArray(byteData);
            string value2 = FromUnicodeByteArray(byteData);
            byte[] ascii = AsciiStringToByteArray(value1);
            byte[] unicode = UnicodeStringToByteArray(value2);
            if (ascii[0] != unicode[0])
                return true;
            return false;
        }
        public static String FromASCIIByteArray(byte[] characters)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            String constructedString = encoding.GetString(characters);
            return constructedString;
        }
        public static String FromUnicodeByteArray(byte[] characters)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            String constructedString = encoding.GetString(characters);
            return constructedString;
        }
        public static byte[] AsciiStringToByteArray(String str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        public static byte[] UnicodeStringToByteArray(String str)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            return encoding.GetBytes(str);
        }
    }
}

