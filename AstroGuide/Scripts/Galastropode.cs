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

        public string Beschreibung { get; set; }
        
        public Pflanze Food { get; set; }

        public GTerrarium Terrarium { get; set; }

        public List<int> Images { get; set; }


        public Galastropode(string name, int image, Pflanze food,int icon)
        {
            this.Name = name;
            this.Image = image;
            this.Food = food;
            this.Icon = icon;
            this.Images = new List<int>() { image };
        }

        public void AddImage(int image)
        {
            this.Images.Add(image);
        }

        public void SetTerrarium(GTerrarium terrarium)
        {
            this.Terrarium = terrarium;
        }

        public void SetDescription(string beschreibung)
        {
            this.Beschreibung = beschreibung;
        }

        public void SetBuff(string text)
        {
            this.Buff = text;
        }

    }
}