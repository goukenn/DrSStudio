

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuKeyboardButton.cs
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
file:enuKeyboardButton.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.OGLGame.Input
{
    public enum enuKeyboardButton
    {
        None = Keys.None,
        LButton = Keys.LButton,
        RButton = Keys.RButton,
        Cancel = Keys.Cancel,
        MButton = Keys.MButton,
        XButton1 = Keys.XButton1,
        XButton2 = Keys.XButton2,
        Back = Keys.Back,
        Tab = Keys.Tab,
        LineFeed = Keys.LineFeed,
        Clear = Keys.Clear,
        Return = Keys.Return,
        ShiftKey = Keys.ShiftKey,
        ControlKey = Keys.ControlKey,
        Menu = Keys.Menu,
        Pause = Keys.Pause,
        Capital = Keys.Capital,
        KanaMode = Keys.KanaMode,
        JunjaMode = Keys.JunjaMode,
        FinalMode = Keys.FinalMode,
        HanjaMode = Keys.HanjaMode,
        Escape = Keys.Escape,
        IMEConvert = Keys.IMEConvert,
        IMENonconvert = Keys.IMENonconvert,
        IMEAceept = Keys.IMEAceept,
        IMEModeChange = Keys.IMEModeChange,
        Space = Keys.Space,
        PageUp = Keys.PageUp,
        Next = Keys.Next,
        End = Keys.End,
        Home = Keys.Home,
        Left = Keys.Left,
        Up = Keys.Up,
        Right = Keys.Right,
        Down = Keys.Down,
        Select = Keys.Select,
        Print = Keys.Print,
        Execute = Keys.Execute,
        PrintScreen = Keys.PrintScreen,
        Insert = Keys.Insert,
        Delete = Keys.Delete,
        Help = Keys.Help,
        D0 = Keys.D0,
        D1 = Keys.D1,
        D2 = Keys.D2,
        D3 = Keys.D3,
        D4 = Keys.D4,
        D5 = Keys.D5,
        D6 = Keys.D6,
        D7 = Keys.D7,
        D8 = Keys.D8,
        D9 = Keys.D9,
        A = Keys.A,
        B = Keys.B,
        C = Keys.C,
        D = Keys.D,
        E = Keys.E,
        F = Keys.F,
        G = Keys.G,
        H = Keys.H,
        I = Keys.I,
        J = Keys.J,
        K = Keys.K,
        L = Keys.L,
        M = Keys.M,
        N = Keys.N,
        O = Keys.O,
        P = Keys.P,
        Q = Keys.Q,
        R = Keys.R,
        S = Keys.S,
        T = Keys.T,
        U = Keys.U,
        V = Keys.V,
        W = Keys.W,
        X = Keys.X,
        Y = Keys.Y,
        Z = Keys.Z,
        LWin = Keys.LWin,
        RWin = Keys.RWin,
        Apps = Keys.Apps,
        Sleep = Keys.Sleep,
        NumPad0 = Keys.NumPad0,
        NumPad1 = Keys.NumPad1,
        NumPad2 = Keys.NumPad2,
        NumPad3 = Keys.NumPad3,
        NumPad4 = Keys.NumPad4,
        NumPad5 = Keys.NumPad5,
        NumPad6 = Keys.NumPad6,
        NumPad7 = Keys.NumPad7,
        NumPad8 = Keys.NumPad8,
        NumPad9 = Keys.NumPad9,
        Multiply = Keys.Multiply,
        Add = Keys.Add,
        Separator = Keys.Separator,
        Subtract = Keys.Subtract,
        Decimal = Keys.Decimal,
        Divide = Keys.Divide,
        F1 = Keys.F1,
        F2 = Keys.F2,
        F3 = Keys.F3,
        F4 = Keys.F4,
        F5 = Keys.F5,
        F6 = Keys.F6,
        F7 = Keys.F7,
        F8 = Keys.F8,
        F9 = Keys.F9,
        F10 = Keys.F10,
        F11 = Keys.F11,
        F12 = Keys.F12,
        F13 = Keys.F13,
        F14 = Keys.F14,
        F15 = Keys.F15,
        F16 = Keys.F16,
        F17 = Keys.F17,
        F18 = Keys.F18,
        F19 = Keys.F19,
        F20 = Keys.F20,
        F21 = Keys.F21,
        F22 = Keys.F22,
        F23 = Keys.F23,
        F24 = Keys.F24,
        NumLock = Keys.NumLock,
        Scroll = Keys.Scroll,
        LShiftKey = Keys.LShiftKey,
        RShiftKey = Keys.RShiftKey,
        LControlKey = Keys.LControlKey,
        RControlKey = Keys.RControlKey,
        LMenu = Keys.LMenu,
        RMenu = Keys.RMenu,
        BrowserBack = Keys.BrowserBack,
        BrowserForward = Keys.BrowserForward,
        BrowserRefresh = Keys.BrowserRefresh,
        BrowserStop = Keys.BrowserStop,
        BrowserSearch = Keys.BrowserSearch,
        BrowserFavorites = Keys.BrowserFavorites,
        BrowserHome = Keys.BrowserHome,
        VolumeMute = Keys.VolumeMute,
        VolumeDown = Keys.VolumeDown,
        VolumeUp = Keys.VolumeUp,
        MediaNextTrack = Keys.MediaNextTrack,
        MediaPreviousTrack = Keys.MediaPreviousTrack,
        MediaStop = Keys.MediaStop,
        MediaPlayPause = Keys.MediaPlayPause,
        LaunchMail = Keys.LaunchMail,
        SelectMedia = Keys.SelectMedia,
        LaunchApplication1 = Keys.LaunchApplication1,
        LaunchApplication2 = Keys.LaunchApplication2,
        Oem1 = Keys.Oem1,
        Oemplus = Keys.Oemplus,
        Oemcomma = Keys.Oemcomma,
        OemMinus = Keys.OemMinus,
        OemPeriod = Keys.OemPeriod,
        OemQuestion = Keys.OemQuestion,
        Oemtilde = Keys.Oemtilde,
        OemOpenBrackets = Keys.OemOpenBrackets,
        Oem5 = Keys.Oem5,
        Oem6 = Keys.Oem6,
        Oem7 = Keys.Oem7,
        Oem8 = Keys.Oem8,
        OemBackslash = Keys.OemBackslash,
        ProcessKey = Keys.ProcessKey,
        Packet = Keys.Packet,
        Attn = Keys.Attn,
        Crsel = Keys.Crsel,
        Exsel = Keys.Exsel,
        EraseEof = Keys.EraseEof,
        Play = Keys.Play,
        Zoom = Keys.Zoom,
        NoName = Keys.NoName,
        Pa1 = Keys.Pa1,
        OemClear = Keys.OemClear,
        KeyCode = Keys.KeyCode,
        Shift = Keys.Shift,
        Control = Keys.Control,
        Alt = Keys.Alt,
        Modifiers = Keys.Modifiers,
    }
}
