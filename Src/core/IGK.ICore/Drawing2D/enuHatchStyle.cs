

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuHatchStyle.cs
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
file:enuHatchStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enuHatchStyle
    {
        //
        // Résumé :
        //     Motif de lignes horizontales.
        Horizontal = 0,
        //
        // Résumé :
        //     Motif de lignes verticales.
        Vertical = 1,
        //
        // Résumé :
        //     Motif de lignes sur une diagonale allant du coin supérieur gauche au coin
        //     inférieur droit.
        ForwardDiagonal = 2,
        //
        // Résumé :
        //     Motif de lignes sur une diagonale allant du coin supérieur droit au coin
        //     inférieur gauche.
        BackwardDiagonal = 3,
        //
        // Résumé :
        //     Spécifie des lignes horizontales et verticales qui se croisent.
        Cross = 4,
        //
        // Résumé :
        //     Motif de lignes diagonales entrecroisées.
        DiagonalCross = 5,
        //
        // Résumé :
        //     Spécifie un hachurage de 5 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 5:95.
        Percent05 = 6,
        //
        // Résumé :
        //     Spécifie un hachurage de 10 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 10:90.
        Percent10 = 7,
        //
        // Résumé :
        //     Spécifie un hachurage de 20 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 20:80.
        Percent20 = 8,
        //
        // Résumé :
        //     Spécifie un hachurage de 25 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 25:75.
        Percent25 = 9,
        //
        // Résumé :
        //     Spécifie un hachurage de 30 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 30:70.
        Percent30 = 10,
        //
        // Résumé :
        //     Spécifie un hachurage de 40 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 40:60.
        Percent40 = 11,
        //
        // Résumé :
        //     Spécifie un hachurage de 50 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 50:50.
        Percent50 = 12,
        //
        // Résumé :
        //     Spécifie un hachurage de 60 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 60:40.
        Percent60 = 13,
        //
        // Résumé :
        //     Spécifie un hachurage de 70 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 70:30.
        Percent70 = 14,
        //
        // Résumé :
        //     Spécifie un hachurage de 75 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 75:25.
        Percent75 = 15,
        //
        // Résumé :
        //     Spécifie un hachurage de 80 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 80:100.
        Percent80 = 16,
        //
        // Résumé :
        //     Spécifie un hachurage de 90 pour cent. Le rapport entre la couleur de premier
        //     plan et la couleur d'arrière-plan est de 90:10.
        Percent90 = 17,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la droite, allant des points
        //     supérieurs aux points inférieurs, espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal,
        //     mais qui sont crénelées.
        LightDownwardDiagonal = 18,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la gauche, allant des points
        //     supérieurs aux points inférieurs, espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,
        //     mais qui sont crénelées.
        LightUpwardDiagonal = 19,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la droite, allant des points
        //     supérieurs aux points inférieurs, espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal
        //     et du double de sa largeur. Ce motif de hachurage est crénelé.
        DarkDownwardDiagonal = 20,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la gauche, allant des points
        //     supérieurs aux points inférieurs, espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal
        //     et du double de sa largeur. Cependant, les lignes sont crénelées.
        DarkUpwardDiagonal = 21,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la droite, allant des points
        //     supérieurs aux points inférieurs, ayant le même espacement que le style de
        //     hachurage System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal et le triple
        //     de sa largeur, mais qui sont crénelées.
        WideDownwardDiagonal = 22,
        //
        // Résumé :
        //     Spécifie des lignes diagonales inclinées vers la gauche, allant des points
        //     supérieurs aux points inférieurs, ayant le même espacement que le style de
        //     hachurage System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal et le triple
        //     de sa largeur, mais qui sont crénelées.
        WideUpwardDiagonal = 23,
        //
        // Résumé :
        //     Spécifie des lignes verticales espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.Vertical.
        LightVertical = 24,
        //
        // Résumé :
        //     Spécifie des lignes horizontales espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.Horizontal
        //     et du double de sa largeur.
        LightHorizontal = 25,
        //
        // Résumé :
        //     Spécifie des lignes verticales espacées de 75 pour cent de moins que le style
        //     de hachurage System.Drawing.Drawing2D.HatchStyle.Vertical (ou 25 pour cent
        //     plus proches que System.Drawing.Drawing2D.HatchStyle.LightVertical).
        NarrowVertical = 26,
        //
        // Résumé :
        //     Spécifie des lignes horizontales espacées de 75 pour cent de moins que le
        //     style de hachurage System.Drawing.Drawing2D.HatchStyle.Horizontal (ou 25 pour
        //     cent plus proches que System.Drawing.Drawing2D.HatchStyle.LightHorizontal).
        NarrowHorizontal = 27,
        //
        // Résumé :
        //     Spécifie des lignes verticales espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.Vertical
        //     et du double de sa largeur.
        DarkVertical = 28,
        //
        // Résumé :
        //     Spécifie des lignes horizontales espacées de 50 pour cent de moins que System.Drawing.Drawing2D.HatchStyle.Horizontal
        //     et du double de la largeur de System.Drawing.Drawing2D.HatchStyle.Horizontal.
        DarkHorizontal = 29,
        //
        // Résumé :
        //     Spécifie des lignes diagonales discontinues inclinées vers la droite et allant
        //     des points supérieurs aux points inférieurs.
        DashedDownwardDiagonal = 30,
        //
        // Résumé :
        //     Spécifie des lignes diagonales discontinues inclinées vers la gauche et allant
        //     des points supérieurs aux points inférieurs.
        DashedUpwardDiagonal = 31,
        //
        // Résumé :
        //     Spécifie des lignes horizontales discontinues.
        DashedHorizontal = 32,
        //
        // Résumé :
        //     Spécifie des lignes verticales discontinues.
        DashedVertical = 33,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de confettis.
        SmallConfetti = 34,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de confettis et composé de pièces plus
        //     grandes que System.Drawing.Drawing2D.HatchStyle.SmallConfetti.
        LargeConfetti = 35,
        //
        // Résumé :
        //     Spécifie des lignes horizontales composées de zigzags.
        ZigZag = 36,
        //
        // Résumé :
        //     Spécifie des lignes horizontales composées de tildes.
        Wave = 37,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de briques superposées, incliné vers
        //     la gauche et allant des points supérieurs aux points inférieurs.
        DiagonalBrick = 38,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de briques superposées horizontalement.
        HorizontalBrick = 39,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un tapis tissé.
        Weave = 40,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un tissu écossais.
        Plaid = 41,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de sillages.
        Divot = 42,
        //
        // Résumé :
        //     Spécifie des lignes horizontales et verticales, chacune composée de points,
        //     qui se croisent.
        DottedGrid = 43,
        //
        // Résumé :
        //     Spécifie des lignes diagonales vers l'avant et vers l'arrière, chacune composée
        //     de points, qui se croisent.
        DottedDiamond = 44,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de tuiles disposées en couches diagonales,
        //     incliné vers la droite et allant des points supérieurs aux points inférieurs.
        Shingle = 45,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un treillis.
        Trellis = 46,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect de sphères adjacentes les unes aux autres.
        Sphere = 47,
        //
        // Résumé :
        //     Spécifie des lignes horizontales et verticales qui se croisent et sont espacées
        //     de 50 pour cent de moins que le style de hachurage System.Drawing.Drawing2D.HatchStyle.Cross.
        SmallGrid = 48,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un damier.
        SmallCheckerBoard = 49,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un damier avec des carrés d'une taille
        //     deux fois supérieure à System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard.
        LargeCheckerBoard = 50,
        //
        // Résumé :
        //     Spécifie des lignes diagonales vers l'avant et vers l'arrière qui se croisent,
        //     mais qui sont crénelées.
        OutlinedDiamond = 51,
        //
        // Résumé :
        //     Spécifie un hachurage ayant l'aspect d'un damier disposé en diagonale.
        SolidDiamond = 52,
    }
}

