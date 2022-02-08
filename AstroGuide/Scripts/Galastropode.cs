using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AstroGuide.Scripts.Planeten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts
{
    public class Galastropode
    {
        public string Name { get; }

        public int Image { get; set; }
        public int Icon { get; set; }

        public string Buff { get; set; }
        
        public Pflanze Food { get; set; }

        public GTerrarium Terrarium { get; set; }


        public Galastropode(string name, int image, Pflanze food,int icon)
        {
            this.Name = name;
            this.Image = image;
            this.Food = food;
            this.Icon = icon;
        }

        public void SetTerrarium(GTerrarium terrarium)
        {
            this.Terrarium = terrarium;
        }

        public void SetBuff(string text)
        {
            this.Buff = text;
        }

    }
}