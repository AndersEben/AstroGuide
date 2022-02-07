using Android.App;
using Android.Content;
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


        public static int ListPlanetHeight { get; set; } = 205;


        public static int ListCraftingHeight { get; } = 205;

        public static int TextSizeListOffset { get; set; }
    }
}