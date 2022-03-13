using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts.Settings
{
    static class Einstellungen
    {
        public static int ListItemHeight { get; } = 105;
        public static int PlanetOffset { get; } = 35;


        public static int ListSingletHeight { get; set; } = 435;
        public static int ListPlanetHeight { get; set; } = 205;


        public static int ListCraftingHeight { get; } = 205;

        public static int TextSizeListOffset { get; set; }
        public static int DisplayHeight { get; set; }



        public static int TXT_pixel10dip { get; set; }


        public static int RecImageSize { get; set; } = 4;
        public static int TXT_HeaderSize { get; set; } = 10;
        public static int TXT_ElementXL { get; set; } = 13;
        public static int TXT_ElementL { get; set; } = 16;
        public static int TXT_ElementM { get; set; } = 20;
        public static int TXT_ElementS { get; set; } = 28;



        public static int Margin_M { get; set; } = 15;
        public static int TB_Image { get; set; } = 10;



        public static int PageMargin { get; set; } = 30;
        public static int PageMarginTop { get; set; } = 18;


        public static int ListMarginRigth { get; set; } = 43;
        public static int ListMarginRigthText { get; set; } = 9;

        public static int ListMarginLeft { get; set; } = 43;
        public static int ListMarginLeftText { get; set; } = 9;
        public static int ListMarginLeftText2 { get; set; } = 3;

        public static int ListSize { get; set; } = 14;
        public static int ListSpace { get; set; } = 36;

        public static int AdapterSize { get; set; } = 0;
        public static int AdapterImgSize { get; set; } = 0;
        public static int AdapterSpaceCalc { get; set; } = 0;
        public static int AdapterSpace { get; set; } = 4;



        public static int BigListMarginRigth { get; set; } = 43;
        public static int BigListMarginRigthText { get; set; } = 9;
        public static int BigListMarginLeft { get; set; } = 31;
        public static int BigListMarginLeftText { get; set; } = 5;

        public static int BigListSize { get; set; } = 7;
        public static int BigListSpace { get; set; } = 21;

        public static int BigAdapterSize { get; set; } = 0;
        public static int BigAdapterImgSize { get; set; } = 0;
        public static int BigAdapterSpaceCalc { get; set; } = 0;
        public static int BigAdapterSpace { get; set; } = 7;



        public static int LL_E1_margin_left { get; set; } = 0;
        public static int LL_E1_margin_right { get; set; } = 0;
        public static int LL_E1_margin_top { get; set; } = 25;
        public static int LL_E1_margin_bottem { get; set; } = 75;

        public static int LL_AddE_padding_left { get; set; } = 0;
        public static int LL_AddE_padding_right { get; set; } = 0;
        public static int LL_AddE_padding_top { get; set; } = 0;
        public static int LL_AddE_padding_bottem { get; set; } = 45;

    }
}