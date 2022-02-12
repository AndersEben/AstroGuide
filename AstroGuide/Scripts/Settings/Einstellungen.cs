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


        public static int ListSingletHeight { get; set; } = 325;
        public static int ListPlanetHeight { get; set; } = 205;


        public static int ListCraftingHeight { get; } = 205;

        public static int TextSizeListOffset { get; set; }



        public static int TXT_pixel10dip { get; set; }



        public static int TXT_HeaderSize { get; set; } = 10;
        public static int TXT_ElementXL { get; set; } = 13;
        public static int TXT_ElementL { get; set; } = 16;
        public static int TXT_ElementM { get; set; } = 20;
        public static int TXT_ElementS { get; set; } = 28;



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