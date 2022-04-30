

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuKeys.cs
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
file:enuKeys.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    [Flags ()]
    public enum enuKeys : int
    {
        // Résumé :
        //     Le masque de bits pour extraire les modificateurs à partir d'une valeur de
        //     touche.
        Modifiers = -65536,
        //
        // Résumé :
        //     Aucune touche enfoncée.
        None = 0,
        //
        // Résumé :
        //     Bouton gauche de la souris.
        LButton = 1,
        //
        // Résumé :
        //     Bouton droit de la souris.
        RButton = 2,
        //
        // Résumé :
        //     La touche ANNULER.
        Cancel = 3,
        //
        // Résumé :
        //     Le bouton central de la souris (souris à trois boutons).
        MButton = 4,
        //
        // Résumé :
        //     Le premier bouton x de la souris (souris à cinq boutons).
        XButton1 = 5,
        //
        // Résumé :
        //     Le second bouton x de la souris (souris à cinq boutons).
        XButton2 = 6,
        //
        // Résumé :
        //     Touche RET. ARR (RETOUR ARRIÈRE).
        Back = 8,
        //
        // Résumé :
        //     Touche TAB (TABULATION).
        Tab = 9,
        //
        // Résumé :
        //     La touche SAUT DE LIGNE.
        LineFeed = 10,
        //
        // Résumé :
        //     La touche EFFACER.
        Clear = 12,
        //
        // Résumé :
        //     Touche ENTRÉE.
        Enter = 13,
        //
        // Résumé :
        //     La touche RETOUR.
        Return = 13,
        //
        // Résumé :
        //     Touche MAJ (MAJUSCULE).
        ShiftKey = 16,
        //
        // Résumé :
        //     Touche CTRL.
        ControlKey = 17,
        //
        // Résumé :
        //     Touche ALT.
        Menu = 18,
        //
        // Résumé :
        //     Touche PAUSE.
        Pause = 19,
        //
        // Résumé :
        //     La touche CAPS LOCK.
        CapsLock = 20,
        //
        // Résumé :
        //     La touche CAPS LOCK.
        Capital = 20,
        //
        // Résumé :
        //     La touche mode Kana IME.
        KanaMode = 21,
        //
        // Résumé :
        //     La touche mode Hangul IME (conservée pour la compatibilité ; utilisez HangulMode).
        HanguelMode = 21,
        //
        // Résumé :
        //     La touche mode Hangul IME.
        HangulMode = 21,
        //
        // Résumé :
        //     La touche mode Junja IME.
        JunjaMode = 23,
        //
        // Résumé :
        //     La touche mode final IME.
        FinalMode = 24,
        //
        // Résumé :
        //     La touche mode Kanji IME.
        KanjiMode = 25,
        //
        // Résumé :
        //     La touche mode Hanja IME.
        HanjaMode = 25,
        //
        // Résumé :
        //     Touche ESC.
        Escape = 27,
        //
        // Résumé :
        //     La touche de conversion IME.
        IMEConvert = 28,
        //
        // Résumé :
        //     La touche Nonconvert IME.
        IMENonconvert = 29,
        //
        // Résumé :
        //     La touche Accepter IME. Obsolète, utilisez plutôt System.Windows.Forms.Keys.IMEAccept.
        IMEAceept = 30,
        //
        // Résumé :
        //     La touche Accepter IME remplace System.Windows.Forms.Keys.IMEAceept.
        IMEAccept = 30,
        //
        // Résumé :
        //     La touche de modification de mode IME.
        IMEModeChange = 31,
        //
        // Résumé :
        //     Touche ESPACE.
        Space = 32,
        //
        // Résumé :
        //     Touche PAGE PRÉCÉDENTE.
        Prior = 33,
        //
        // Résumé :
        //     Touche PAGE PRÉCÉDENTE.
        PageUp = 33,
        //
        // Résumé :
        //     La touche PAGE DOWN.
        Next = 34,
        //
        // Résumé :
        //     La touche PAGE DOWN.
        PageDown = 34,
        //
        // Résumé :
        //     Touche FIN.
        End = 35,
        //
        // Résumé :
        //     Touche DÉBUT.
        Home = 36,
        //
        // Résumé :
        //     La touche GAUCHE.
        Left = 37,
        //
        // Résumé :
        //     Touche HAUT.
        Up = 38,
        //
        // Résumé :
        //     Touche DROITE.
        Right = 39,
        //
        // Résumé :
        //     Touche BAS.
        Down = 40,
        //
        // Résumé :
        //     Touche SÉLECTION.
        Select = 41,
        //
        // Résumé :
        //     Touche IMPRIMER.
        Print = 42,
        //
        // Résumé :
        //     Touche EXÉCUTER.
        Execute = 43,
        //
        // Résumé :
        //     Touche IMPRESSION ÉCRAN.
        PrintScreen = 44,
        //
        // Résumé :
        //     Touche IMPRESSION ÉCRAN.
        Snapshot = 44,
        //
        // Résumé :
        //     Touche INS.
        Insert = 45,
        //
        // Résumé :
        //     Touche DEL.
        Delete = 46,
        //
        // Résumé :
        //     Touche HELP.
        Help = 47,
        //
        // Résumé :
        //     Touche 0.
        D0 = 48,
        //
        // Résumé :
        //     Touche 1.
        D1 = 49,
        //
        // Résumé :
        //     Touche 2.
        D2 = 50,
        //
        // Résumé :
        //     Touche 3.
        D3 = 51,
        //
        // Résumé :
        //     Touche 4.
        D4 = 52,
        //
        // Résumé :
        //     Touche 5.
        D5 = 53,
        //
        // Résumé :
        //     Touche 6.
        D6 = 54,
        //
        // Résumé :
        //     Touche 7.
        D7 = 55,
        //
        // Résumé :
        //     Touche 8.
        D8 = 56,
        //
        // Résumé :
        //     Touche 9.
        D9 = 57,
        //
        // Résumé :
        //     Touche A.
        A = 65,
        //
        // Résumé :
        //     Touche B.
        B = 66,
        //
        // Résumé :
        //     Touche C.
        C = 67,
        //
        // Résumé :
        //     Touche D.
        D = 68,
        //
        // Résumé :
        //     Touche E.
        E = 69,
        //
        // Résumé :
        //     Touche F.
        F = 70,
        //
        // Résumé :
        //     Touche G.
        G = 71,
        //
        // Résumé :
        //     Touche H.
        H = 72,
        //
        // Résumé :
        //     Touche I.
        I = 73,
        //
        // Résumé :
        //     Touche J.
        J = 74,
        //
        // Résumé :
        //     Touche K.
        K = 75,
        //
        // Résumé :
        //     Touche L.
        L = 76,
        //
        // Résumé :
        //     Touche M.
        M = 77,
        //
        // Résumé :
        //     Touche N.
        N = 78,
        //
        // Résumé :
        //     Touche O.
        O = 79,
        //
        // Résumé :
        //     Touche P.
        P = 80,
        //
        // Résumé :
        //     Touche Q.
        Q = 81,
        //
        // Résumé :
        //     Touche R.
        R = 82,
        //
        // Résumé :
        //     Touche S.
        S = 83,
        //
        // Résumé :
        //     Touche T.
        T = 84,
        //
        // Résumé :
        //     Touche U.
        U = 85,
        //
        // Résumé :
        //     Touche V.
        V = 86,
        //
        // Résumé :
        //     Touche W.
        W = 87,
        //
        // Résumé :
        //     Touche X.
        X = 88,
        //
        // Résumé :
        //     Touche Y.
        Y = 89,
        //
        // Résumé :
        //     Touche Z.
        Z = 90,
        //
        // Résumé :
        //     La touche du logo Windows de gauche (clavier Microsoft Natural Keyboard).
        LWin = 91,
        //
        // Résumé :
        //     La touche du logo Windows de droite (clavier Microsoft Natural Keyboard).
        RWin = 92,
        //
        // Résumé :
        //     La touche CoreApplicationManager.Instance (clavier Microsoft Natural Keyboard).
        Apps = 93,
        //
        // Résumé :
        //     La touche de mise en veille de l'ordinateur.
        Sleep = 95,
        //
        // Résumé :
        //     La touche 0 du pavé numérique.
        NumPad0 = 96,
        //
        // Résumé :
        //     La touche 1 du pavé numérique.
        NumPad1 = 97,
        //
        // Résumé :
        //     La touche 2 du pavé numérique.
        NumPad2 = 98,
        //
        // Résumé :
        //     La touche 3 du pavé numérique.
        NumPad3 = 99,
        //
        // Résumé :
        //     La touche 4 du pavé numérique.
        NumPad4 = 100,
        //
        // Résumé :
        //     La touche 5 du pavé numérique.
        NumPad5 = 101,
        //
        // Résumé :
        //     La touche 6 du pavé numérique.
        NumPad6 = 102,
        //
        // Résumé :
        //     La touche 7 du pavé numérique.
        NumPad7 = 103,
        //
        // Résumé :
        //     La touche 8 du pavé numérique.
        NumPad8 = 104,
        //
        // Résumé :
        //     La touche 9 du pavé numérique.
        NumPad9 = 105,
        //
        // Résumé :
        //     La touche de multiplication.
        Multiply = 106,
        //
        // Résumé :
        //     La touche Ajouter.
        Add = 107,
        //
        // Résumé :
        //     La touche du séparateur.
        Separator = 108,
        //
        // Résumé :
        //     La touche de soustraction.
        Subtract = 109,
        //
        // Résumé :
        //     La touche de décimale.
        Decimal = 110,
        //
        // Résumé :
        //     La touche de division.
        Divide = 111,
        //
        // Résumé :
        //     Touche F1.
        F1 = 112,
        //
        // Résumé :
        //     Touche F2.
        F2 = 113,
        //
        // Résumé :
        //     Touche F3.
        F3 = 114,
        //
        // Résumé :
        //     La touche F4.
        F4 = 115,
        //
        // Résumé :
        //     La touche F5.
        F5 = 116,
        //
        // Résumé :
        //     La touche F6.
        F6 = 117,
        //
        // Résumé :
        //     La touche F7.
        F7 = 118,
        //
        // Résumé :
        //     Touche F8.
        F8 = 119,
        //
        // Résumé :
        //     Touche F9.
        F9 = 120,
        //
        // Résumé :
        //     Touche F10.
        F10 = 121,
        //
        // Résumé :
        //     Touche F11.
        F11 = 122,
        //
        // Résumé :
        //     Touche F12.
        F12 = 123,
        //
        // Résumé :
        //     Touche F13.
        F13 = 124,
        //
        // Résumé :
        //     Touche F14.
        F14 = 125,
        //
        // Résumé :
        //     Touche F15.
        F15 = 126,
        //
        // Résumé :
        //     Touche F16.
        F16 = 127,
        //
        // Résumé :
        //     Touche F17.
        F17 = 128,
        //
        // Résumé :
        //     Touche F18.
        F18 = 129,
        //
        // Résumé :
        //     Touche F19.
        F19 = 130,
        //
        // Résumé :
        //     Touche F20.
        F20 = 131,
        //
        // Résumé :
        //     Touche F21.
        F21 = 132,
        //
        // Résumé :
        //     Touche F22.
        F22 = 133,
        //
        // Résumé :
        //     Touche F23.
        F23 = 134,
        //
        // Résumé :
        //     Touche F24.
        F24 = 135,
        //
        // Résumé :
        //     La touche NUM LOCK.
        NumLock = 144,
        //
        // Résumé :
        //     La touche ARRÊT DÉFILEMENT.
        Scroll = 145,
        //
        // Résumé :
        //     La touche MAJ de gauche.
        LShiftKey = 160,
        //
        // Résumé :
        //     La touche MAJ de droite.
        RShiftKey = 161,
        //
        // Résumé :
        //     La touche CTRL de gauche.
        LControlKey = 162,
        //
        // Résumé :
        //     La touche CTRL de droite.
        RControlKey = 163,
        //
        // Résumé :
        //     La touche ALT de gauche.
        LMenu = 164,
        //
        // Résumé :
        //     La touche ALT de droite.
        RMenu = 165,
        //
        // Résumé :
        //     La touche Précédente du navigateur (Windows 2000 ou version ultérieure).
        BrowserBack = 166,
        //
        // Résumé :
        //     La touche Suivante du navigateur (Windows 2000 ou version ultérieure).
        BrowserForward = 167,
        //
        // Résumé :
        //     La touche Actualiser du navigateur (Windows 2000 ou version ultérieure).
        BrowserRefresh = 168,
        //
        // Résumé :
        //     La touche Arrêter du navigateur (Windows 2000 ou version ultérieure).
        BrowserStop = 169,
        //
        // Résumé :
        //     La touche Rechercher du navigateur (Windows 2000 ou version ultérieure).
        BrowserSearch = 170,
        //
        // Résumé :
        //     La touche Favoris du navigateur (Windows 2000 ou version ultérieure).
        BrowserFavorites = 171,
        //
        // Résumé :
        //     La touche Démarrage du navigateur (Windows 2000 ou version ultérieure).
        BrowserHome = 172,
        //
        // Résumé :
        //     La touche Volume muet (Windows 2000 ou version ultérieure).
        VolumeMute = 173,
        //
        // Résumé :
        //     La touche Descendre le volume (Windows 2000 ou version ultérieure).
        VolumeDown = 174,
        //
        // Résumé :
        //     La touche Monter le volume (Windows 2000 ou version ultérieure).
        VolumeUp = 175,
        //
        // Résumé :
        //     La touche Piste suivante du média (Windows 2000 ou version ultérieure).
        MediaNextTrack = 176,
        //
        // Résumé :
        //     La touche Piste précédente du média (Windows 2000 ou version ultérieure).
        MediaPreviousTrack = 177,
        //
        // Résumé :
        //     La touche Arrêter du média (Windows 2000 ou version ultérieure).
        MediaStop = 178,
        //
        // Résumé :
        //     La touche Lecture/Pause du média (Windows 2000 ou version ultérieure).
        MediaPlayPause = 179,
        //
        // Résumé :
        //     La touche Démarrer la messagerie (Windows 2000 ou version ultérieure).
        LaunchMail = 180,
        //
        // Résumé :
        //     La touche Sélectionner le média (Windows 2000 ou version ultérieure).
        SelectMedia = 181,
        //
        // Résumé :
        //     La touche Démarrer l'application 1 (Windows 2000 ou version ultérieure).
        LaunchApplication1 = 182,
        //
        // Résumé :
        //     La touche Démarrer l'application 2 (Windows 2000 ou version ultérieure).
        LaunchApplication2 = 183,
        //
        // Résumé :
        //     Clé 1 OEM.
        Oem1 = 186,
        //
        // Résumé :
        //     La touche OEM du point-virgule sur un clavier standard américain (Windows 2000
        //     ou version ultérieure).
        OemSemicolon = 186,
        //
        // Résumé :
        //     La touche OEM d'addition sur un clavier régional (Windows 2000 ou version
        //     ultérieure).
        Oemplus = 187,
        //
        // Résumé :
        //     La touche OEM de virgule sur un clavier régional (Windows 2000 ou version
        //     ultérieure).
        Oemcomma = 188,
        //
        // Résumé :
        //     La touche OEM de soustraction sur un clavier régional (Windows 2000 ou version
        //     ultérieure).
        OemMinus = 189,
        //
        // Résumé :
        //     La touche OEM de point sur un clavier régional (Windows 2000 ou version ultérieure).
        OemPeriod = 190,
        //
        // Résumé :
        //     La touche OEM du point d'interrogation sur un clavier standard américain
        //     (Windows 2000 ou version ultérieure).
        OemQuestion = 191,
        //
        // Résumé :
        //     Clé 2 OEM.
        Oem2 = 191,
        //
        // Résumé :
        //     La touche OEM du tilde sur un clavier standard américain (Windows 2000 ou
        //     version ultérieure).
        Oemtilde = 192,
        //
        // Résumé :
        //     Clé 3 OEM.
        Oem3 = 192,
        //
        // Résumé :
        //     Clé 4 OEM.
        Oem4 = 219,
        //
        // Résumé :
        //     La touche OEM de crochet ouvrant sur un clavier standard américain (Windows 2000
        //     ou version ultérieure).
        OemOpenBrackets = 219,
        //
        // Résumé :
        //     La touche OEM du signe | sur un clavier standard américain (Windows 2000
        //     ou version ultérieure).
        OemPipe = 220,
        //
        // Résumé :
        //     Clé 5 OEM.
        Oem5 = 220,
        //
        // Résumé :
        //     Clé 6 OEM.
        Oem6 = 221,
        //
        // Résumé :
        //     La touche OEM de crochet fermant sur un clavier standard américain (Windows 2000
        //     ou version ultérieure).
        OemCloseBrackets = 221,
        //
        // Résumé :
        //     Clé 7 OEM.
        Oem7 = 222,
        //
        // Résumé :
        //     La touche OEM des guillemets simples et doubles sur un clavier standard américain
        //     (Windows 2000 ou version ultérieure).
        OemQuotes = 222,
        //
        // Résumé :
        //     Clé 8 OEM.
        Oem8 = 223,
        //
        // Résumé :
        //     Clé 102 OEM.
        Oem102 = 226,
        //
        // Résumé :
        //     La touche OEM de guillemets ou de barre oblique inverse sur le clavier RT
        //     de 102 touches (Windows 2000 ou version ultérieure).
        OemBackslash = 226,
        //
        // Résumé :
        //     La touche PROCESS KEY.
        ProcessKey = 229,
        //
        // Résumé :
        //     Permet de passer des caractères Unicode comme s'il s'agissait de séquences
        //     de touches. La valeur de la touche Paquet est le mot inférieur d'une valeur
        //     de clé virtuelle 32 bits utilisée pour les méthodes d'entrée autres qu'au
        //     clavier.
        Packet = 231,
        //
        // Résumé :
        //     Touche ATTN.
        Attn = 246,
        //
        // Résumé :
        //     La touche CRSEL.
        Crsel = 247,
        //
        // Résumé :
        //     La touche EXSEL.
        Exsel = 248,
        //
        // Résumé :
        //     Touche EOF d'effacement.
        EraseEof = 249,
        //
        // Résumé :
        //     Touche PLAY.
        Play = 250,
        //
        // Résumé :
        //     Touche ZOOM.
        Zoom = 251,
        //
        // Résumé :
        //     Une constante réservée à un usage futur.
        NoName = 252,
        //
        // Résumé :
        //     Touche PA1.
        Pa1 = 253,
        //
        // Résumé :
        //     La touche EFFACER.
        OemClear = 254,
        //
        // Résumé :
        //     Le masque de bits pour extraire un code de touche à partir d'une valeur de
        //     touche.
        KeyCode = 65535,
        //
        // Résumé :
        //     La touche de modification MAJ.
        Shift = 65536,
        //
        // Résumé :
        //     La touche de modification Ctrl.
        Control = 131072,
        //
        // Résumé :
        //     La touche de modification Alt.
        Alt = 262144,
    }
}

