using System;
using System.Collections;
using System.Collections.Generic;

namespace IGK.ICore.IO
{

    public enum enuWOFFRangeType
    {
        Level1,
        Level2,
        Level3,
        Level4
    }

    public interface IRangeList
    {
        int From { get; set; }
        int To { get; set; }
    }

    ///<summary>represent the range defintion</summary>
    public struct WOFFRangeDefinition
    {

        public int From { get; set; }
        public int To { get; set; }
        public enuWOFFRangeType RangeType { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }

        public static readonly WOFFRangeDefinition Empty;

        static WOFFRangeDefinition()
        {
            Empty = new WOFFRangeDefinition();
        }
        public override string ToString()
        {
            return $"{nameof(WOFFRangeDefinition)}-[{RangeType}] - {Name}- {From} to {To}";
        }
    }


    public class WOFFRangeUtility
    {
        //store range defintion
        static Dictionary<int, WOFFRangeDefinition> sm_ranges;


        static WOFFRangeUtility()
        {
            sm_ranges = new Dictionary<int, WOFFRangeDefinition>();

            //sm_ranges.Add(0, new WOFFRangeDefinition(){
            //	From = 0x0,
            //	To = 0x7F,
            //	Value=1,
            //	RangeType = enuWOFFRangeType.Level1
            //});


            // Generate the Police OS/2 c# definion

            LoadFontDef("Basic Latin", 0x0000, 0x007F, enuWOFFRangeType.Level1, 1);
            LoadFontDef("Latin-1 Supplement", 0x0080, 0x00FF, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Latin Extended-A", 0x0100, 0x017F, enuWOFFRangeType.Level1, 3);
            LoadFontDef("Latin Extended-B", 0x0180, 0x024F, enuWOFFRangeType.Level1, 4);
            LoadFontDef("IPA Extensions", 0x0250, 0x02AF, enuWOFFRangeType.Level1, 5);
            LoadFontDef("Phonetic Extensions", 0x1D00, 0x1D7F, enuWOFFRangeType.Level1, 5);
            LoadFontDef("Phonetic Extensions Supplement", 0x1D80, 0x1DBF, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Spacing Modifier Letters", 0x02B0, 0x02FF, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Modifier Tone Letters", 0xA700, 0xA71F, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Combining Diacritical Marks", 0x0300, 0x036F, enuWOFFRangeType.Level1, 7);
            LoadFontDef("Combining Diacritical Marks Supplement", 0x1DC0, 0x1DFF, enuWOFFRangeType.Level1, 7);
            LoadFontDef("Greek and Coptic", 0x0370, 0x03FF, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Coptic", 0x2C80, 0x2CFF, enuWOFFRangeType.Level1, 1);
            LoadFontDef("Cyrillic", 0x0400, 0x04FF, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Cyrillic Supplement", 0x0500, 0x052F, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Cyrillic Extended-A", 0x2DE0, 0x2DFF, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Cyrillic Extended-B", 0xA640, 0xA69F, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Armenian", 0x0530, 0x058F, enuWOFFRangeType.Level1, 3);
            LoadFontDef("Hebrew", 0x0590, 0x05FF, enuWOFFRangeType.Level1, 4);
            LoadFontDef("Vai", 0xA500, 0xA63F, enuWOFFRangeType.Level1, 5);
            LoadFontDef("Arabic", 0x0600, 0x06FF, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Arabic Supplement", 0x0750, 0x077F, enuWOFFRangeType.Level1, 6);
            LoadFontDef("NKo", 0x07C0, 0x07FF, enuWOFFRangeType.Level1, 7);
            LoadFontDef("Devanagari", 0x0900, 0x097F, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Bengali", 0x0980, 0x09FF, enuWOFFRangeType.Level1, 1);
            LoadFontDef("Gurmukhi", 0x0A00, 0x0A7F, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Gujarati", 0x0A80, 0x0AFF, enuWOFFRangeType.Level1, 3);
            LoadFontDef("Oriya", 0x0B00, 0x0B7F, enuWOFFRangeType.Level1, 4);
            LoadFontDef("Tamil", 0x0B80, 0x0BFF, enuWOFFRangeType.Level1, 5);
            LoadFontDef("Telugu", 0x0C00, 0x0C7F, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Kannada", 0x0C80, 0x0CFF, enuWOFFRangeType.Level1, 7);
            LoadFontDef("Malayalam", 0x0D00, 0x0D7F, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Thai", 0x0E00, 0x0E7F, enuWOFFRangeType.Level1, 1);
            LoadFontDef("Lao", 0x0E80, 0x0EFF, enuWOFFRangeType.Level1, 2);
            LoadFontDef("Georgian", 0x10A0, 0x10FF, enuWOFFRangeType.Level1, 3);
            LoadFontDef("Georgian Supplement", 0x2D00, 0x2D2F, enuWOFFRangeType.Level1, 3);
            LoadFontDef("Balinese", 0x1B00, 0x1B7F, enuWOFFRangeType.Level1, 4);
            LoadFontDef("Hangul Jamo", 0x1100, 0x11FF, enuWOFFRangeType.Level1, 5);
            LoadFontDef("Latin Extended Additional", 0x1E00, 0x1EFF, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Latin Extended-C", 0x2C60, 0x2C7F, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Latin Extended-D", 0xA720, 0xA7FF, enuWOFFRangeType.Level1, 6);
            LoadFontDef("Greek Extended", 0x1F00, 0x1FFF, enuWOFFRangeType.Level1, 7);
            LoadFontDef("General Punctuation", 0x2000, 0x206F, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Supplemental Punctuation", 0x2E00, 0x2E7F, enuWOFFRangeType.Level1, 8);
            LoadFontDef("Superscripts And Subscripts", 0x2070, 0x209F, enuWOFFRangeType.Level2, 1);
            LoadFontDef("Currency Symbols", 0x20A0, 0x20CF, enuWOFFRangeType.Level2, 2);
            LoadFontDef("Combining Diacritical Marks For Symbols", 0x20D0, 0x20FF, enuWOFFRangeType.Level2, 3);
            LoadFontDef("Letterlike Symbols", 0x2100, 0x214F, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Number Forms", 0x2150, 0x218F, enuWOFFRangeType.Level2, 5);
            LoadFontDef("Arrows", 0x2190, 0x21FF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Supplemental Arrows-A", 0x27F0, 0x27FF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Supplemental Arrows-B", 0x2900, 0x297F, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Miscellaneous Symbols and Arrows", 0x2B00, 0x2BFF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Mathematical Operators", 0x2200, 0x22FF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Supplemental Mathematical Operators", 0x2A00, 0x2AFF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Miscellaneous Mathematical Symbols-A", 0x27C0, 0x27EF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Miscellaneous Mathematical Symbols-B", 0x2980, 0x29FF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Miscellaneous Technical", 0x2300, 0x23FF, enuWOFFRangeType.Level2, 8);
            LoadFontDef("Control Pictures", 0x2400, 0x243F, enuWOFFRangeType.Level2, 1);
            LoadFontDef("Optical Character Recognition", 0x2440, 0x245F, enuWOFFRangeType.Level2, 2);
            LoadFontDef("Enclosed Alphanumerics", 0x2460, 0x24FF, enuWOFFRangeType.Level2, 3);
            LoadFontDef("Box Drawing", 0x2500, 0x257F, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Block Elements", 0x2580, 0x259F, enuWOFFRangeType.Level2, 5);
            LoadFontDef("Geometric Shapes", 0x25A0, 0x25FF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Miscellaneous Symbols", 0x2600, 0x26FF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Dingbats", 0x2700, 0x27BF, enuWOFFRangeType.Level2, 8);
            LoadFontDef("CJK Symbols And Punctuation", 0x3000, 0x303F, enuWOFFRangeType.Level2, 1);
            LoadFontDef("Hiragana", 0x3040, 0x309F, enuWOFFRangeType.Level2, 2);
            LoadFontDef("Katakana", 0x30A0, 0x30FF, enuWOFFRangeType.Level2, 3);
            LoadFontDef("Katakana Phonetic Extensions", 0x31F0, 0x31FF, enuWOFFRangeType.Level2, 3);
            LoadFontDef("Bopomofo", 0x3100, 0x312F, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Bopomofo Extended", 0x31A0, 0x31BF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Hangul Compatibility Jamo", 0x3130, 0x318F, enuWOFFRangeType.Level2, 5);
            LoadFontDef("Phags-pa", 0xA840, 0xA87F, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Enclosed CJK Letters And Months", 0x3200, 0x32FF, enuWOFFRangeType.Level2, 7);
            LoadFontDef("CJK Compatibility", 0x3300, 0x33FF, enuWOFFRangeType.Level2, 8);
            LoadFontDef("Hangul Syllables", 0xAC00, 0xD7AF, enuWOFFRangeType.Level2, 1);
            LoadFontDef("Non-Plane 0 *", 0xD800, 0xDFFF, enuWOFFRangeType.Level2, 2);
            LoadFontDef("Phoenician", 0x10900, 0x1091F, enuWOFFRangeType.Level2, 3);
            LoadFontDef("CJK Unified Ideographs", 0x4E00, 0x9FFF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("CJK Radicals Supplement", 0x2E80, 0x2EFF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Kangxi Radicals", 0x2F00, 0x2FDF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Ideographic Description Characters", 0x2FF0, 0x2FFF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("CJK Unified Ideographs Extension A", 0x3400, 0x4DBF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("CJK Unified Ideographs Extension B", 0x20000, 0x2A6DF, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Kanbun", 0x3190, 0x319F, enuWOFFRangeType.Level2, 4);
            LoadFontDef("Private Use Area (plane 0)", 0xE000, 0xF8FF, enuWOFFRangeType.Level2, 5);
            LoadFontDef("CJK Strokes", 0x31C0, 0x31EF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("CJK Compatibility Ideographs", 0xF900, 0xFAFF, enuWOFFRangeType.Level2, 6);
            LoadFontDef("CJK Compatibility Ideographs Supplement", 0x2F800, 0x2FA1F, enuWOFFRangeType.Level2, 6);
            LoadFontDef("Alphabetic Presentation Forms", 0xFB00, 0xFB4F, enuWOFFRangeType.Level2, 7);
            LoadFontDef("Arabic Presentation Forms-A", 0xFB50, 0xFDFF, enuWOFFRangeType.Level2, 8);
            LoadFontDef("Combining Half Marks", 0xFE20, 0xFE2F, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Vertical Forms", 0xFE10, 0xFE1F, enuWOFFRangeType.Level3, 2);
            LoadFontDef("CJK Compatibility Forms", 0xFE30, 0xFE4F, enuWOFFRangeType.Level3, 2);
            LoadFontDef("Small Form Variants", 0xFE50, 0xFE6F, enuWOFFRangeType.Level3, 3);
            LoadFontDef("Arabic Presentation Forms-B", 0xFE70, 0xFEFF, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Halfwidth And Fullwidth Forms", 0xFF00, 0xFFEF, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Specials", 0xFFF0, 0xFFFF, enuWOFFRangeType.Level3, 6);
            LoadFontDef("Tibetan", 0x0F00, 0x0FFF, enuWOFFRangeType.Level3, 7);
            LoadFontDef("Syriac", 0x0700, 0x074F, enuWOFFRangeType.Level3, 8);
            LoadFontDef("Thaana", 0x0780, 0x07BF, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Sinhala", 0x0D80, 0x0DFF, enuWOFFRangeType.Level3, 2);
            LoadFontDef("Myanmar", 0x1000, 0x109F, enuWOFFRangeType.Level3, 3);
            LoadFontDef("Ethiopic", 0x1200, 0x137F, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Ethiopic Supplement", 0x1380, 0x139F, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Ethiopic Extended", 0x2D80, 0x2DDF, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Cherokee", 0x13A0, 0x13FF, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Unified Canadian Aboriginal Syllabics", 0x1400, 0x167F, enuWOFFRangeType.Level3, 6);
            LoadFontDef("Ogham", 0x1680, 0x169F, enuWOFFRangeType.Level3, 7);
            LoadFontDef("Runic", 0x16A0, 0x16FF, enuWOFFRangeType.Level3, 8);
            LoadFontDef("Khmer", 0x1780, 0x17FF, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Khmer Symbols", 0x19E0, 0x19FF, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Mongolian", 0x1800, 0x18AF, enuWOFFRangeType.Level3, 2);
            LoadFontDef("Braille Patterns", 0x2800, 0x28FF, enuWOFFRangeType.Level3, 3);
            LoadFontDef("Yi Syllables", 0xA000, 0xA48F, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Yi Radicals", 0xA490, 0xA4CF, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Tagalog", 0x1700, 0x171F, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Hanunoo", 0x1720, 0x173F, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Buhid", 0x1740, 0x175F, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Tagbanwa", 0x1760, 0x177F, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Old Italic", 0x10300, 0x1032F, enuWOFFRangeType.Level3, 6);
            LoadFontDef("Gothic", 0x10330, 0x1034F, enuWOFFRangeType.Level3, 7);
            LoadFontDef("Deseret", 0x10400, 0x1044F, enuWOFFRangeType.Level3, 8);
            LoadFontDef("Byzantine Musical Symbols", 0x1D000, 0x1D0FF, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Musical Symbols", 0x1D100, 0x1D1FF, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Ancient Greek Musical Notation", 0x1D200, 0x1D24F, enuWOFFRangeType.Level3, 1);
            LoadFontDef("Mathematical Alphanumeric Symbols", 0x1D400, 0x1D7FF, enuWOFFRangeType.Level3, 2);
            LoadFontDef("Private Use (plane 15)", 0xF0000, 0xFFFFD, enuWOFFRangeType.Level3, 3);
            LoadFontDef("Private Use (plane 16)", 0x100000, 0x10FFFD, enuWOFFRangeType.Level3, 3);
            LoadFontDef("Variation Selectors", 0xFE00, 0xFE0F, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Variation Selectors Supplement", 0xE0100, 0xE01EF, enuWOFFRangeType.Level3, 4);
            LoadFontDef("Tags", 0xE0000, 0xE007F, enuWOFFRangeType.Level3, 5);
            LoadFontDef("Limbu", 0x1900, 0x194F, enuWOFFRangeType.Level3, 6);
            LoadFontDef("Tai Le", 0x1950, 0x197F, enuWOFFRangeType.Level3, 7);
            LoadFontDef("New Tai Lue", 0x1980, 0x19DF, enuWOFFRangeType.Level3, 8);
            LoadFontDef("Buginese", 0x1A00, 0x1A1F, enuWOFFRangeType.Level4, 1);
            LoadFontDef("Glagolitic", 0x2C00, 0x2C5F, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Tifinagh", 0x2D30, 0x2D7F, enuWOFFRangeType.Level4, 3);
            LoadFontDef("Yijing Hexagram Symbols", 0x4DC0, 0x4DFF, enuWOFFRangeType.Level4, 4);
            LoadFontDef("Syloti Nagri", 0xA800, 0xA82F, enuWOFFRangeType.Level4, 5);
            LoadFontDef("Linear B Syllabary", 0x10000, 0x1007F, enuWOFFRangeType.Level4, 6);
            LoadFontDef("Linear B Ideograms", 0x10080, 0x100FF, enuWOFFRangeType.Level4, 6);
            LoadFontDef("Aegean Numbers", 0x10100, 0x1013F, enuWOFFRangeType.Level4, 6);
            LoadFontDef("Ancient Greek Numbers", 0x10140, 0x1018F, enuWOFFRangeType.Level4, 7);
            LoadFontDef("Ugaritic", 0x10380, 0x1039F, enuWOFFRangeType.Level4, 8);
            LoadFontDef("Old Persian", 0x103A0, 0x103DF, enuWOFFRangeType.Level4, 1);
            LoadFontDef("Shavian", 0x10450, 0x1047F, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Osmanya", 0x10480, 0x104AF, enuWOFFRangeType.Level4, 3);
            LoadFontDef("Cypriot Syllabary", 0x10800, 0x1083F, enuWOFFRangeType.Level4, 4);
            LoadFontDef("Kharoshthi", 0x10A00, 0x10A5F, enuWOFFRangeType.Level4, 5);
            LoadFontDef("Tai Xuan Jing Symbols", 0x1D300, 0x1D35F, enuWOFFRangeType.Level4, 6);
            LoadFontDef("Cuneiform", 0x12000, 0x123FF, enuWOFFRangeType.Level4, 7);
            LoadFontDef("Cuneiform Numbers and Punctuation", 0x12400, 0x1247F, enuWOFFRangeType.Level4, 7);
            LoadFontDef("Counting Rod Numerals", 0x1D360, 0x1D37F, enuWOFFRangeType.Level4, 8);
            LoadFontDef("Sundanese", 0x1B80, 0x1BBF, enuWOFFRangeType.Level4, 1);
            LoadFontDef("Lepcha", 0x1C00, 0x1C4F, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Ol Chiki", 0x1C50, 0x1C7F, enuWOFFRangeType.Level4, 3);
            LoadFontDef("Saurashtra", 0xA880, 0xA8DF, enuWOFFRangeType.Level4, 4);
            LoadFontDef("Kayah Li", 0xA900, 0xA92F, enuWOFFRangeType.Level4, 5);
            LoadFontDef("Rejang", 0xA930, 0xA95F, enuWOFFRangeType.Level4, 6);
            LoadFontDef("Cham", 0xAA00, 0xAA5F, enuWOFFRangeType.Level4, 7);
            LoadFontDef("Ancient Symbols", 0x10190, 0x101CF, enuWOFFRangeType.Level4, 8);
            LoadFontDef("Phaistos Disc", 0x101D0, 0x101FF, enuWOFFRangeType.Level4, 1);
            LoadFontDef("Carian", 0x102A0, 0x102DF, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Lycian", 0x10280, 0x1029F, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Lydian", 0x10920, 0x1093F, enuWOFFRangeType.Level4, 2);
            LoadFontDef("Domino Tiles", 0x1F030, 0x1F09F, enuWOFFRangeType.Level4, 3);
            LoadFontDef("Mahjong Tiles", 0x1F000, 0x1F02F, enuWOFFRangeType.Level4, 3);
            LoadFontDef("Reserved for process-internal usage", 0x0, 0x0, enuWOFFRangeType.Level4, 4);
            LoadFontDef("", 0x0, 0x0, enuWOFFRangeType.Level4, 5);
            LoadFontDef("", 0x0, 0x0, enuWOFFRangeType.Level4, 6);
            LoadFontDef("", 0x0, 0x0, enuWOFFRangeType.Level4, 7);
            LoadFontDef("", 0x0, 0x0, enuWOFFRangeType.Level4, 8);

            // Powered by IGKDEV 

        }
        internal static void LoadFontDef(string name, int @from, int @to, enuWOFFRangeType level, int value)
        {
            if ((@from == 0) && (@to == 0))
                return;
            var m = new WOFFRangeDefinition()
            {
                Name = name,
                From = @from,
                To = to,
                Value = value,
                RangeType = level
            };
            for (int i = @from; i <= @to; i++)
            {
                if (sm_ranges.ContainsKey(i))
                {
                    var d = sm_ranges[i];
                }
                else
                {
                    sm_ranges.Add(i, m);
                }
            }
        }
        public static bool CheckValue(int @char, ref WOFFRangeDefinition def)
        {
            //get mininimu value
            if (sm_ranges.ContainsKey(@char))
            {
                def = sm_ranges[@char];
                return true;
            }
            return false;
        }


        public static int[] GetUnicodeRangeDefintion(IEnumerable<IRangeList> list)
        {
            int[] tab = new int[4];
            WOFFRangeDefinition def = WOFFRangeDefinition.Empty;
            foreach (var i in list)
            {
                for (int j = i.From; j <= i.To; j++)
                {
                    if (WOFFRangeUtility.CheckValue(j, ref def))
                    {
                        switch (def.RangeType)
                        {
                            case enuWOFFRangeType.Level1:
                                tab[0] |= def.Value;
                                break;
                            case enuWOFFRangeType.Level2:
                                tab[1] |= def.Value;
                                break;
                            case enuWOFFRangeType.Level3:
                                tab[2] |= def.Value;
                                break;
                            case enuWOFFRangeType.Level4:
                                tab[3] |= def.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return tab;

        }


    }
}
