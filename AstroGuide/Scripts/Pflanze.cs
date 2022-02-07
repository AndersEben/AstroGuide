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

namespace AstroGuide.Scripts
{
    public class Pflanze
    {
        public string Name { get; }
        public PlantType Typ { get; }
        public int Image { get; set; }

        public string Beschreibung { get; set; }

        public Pflanze(string name, PlantType type, int image)
        {
            this.Name = name;
            this.Typ = type;
            this.Image = image;
        }

        public void SetDescription(string text)
        {
            this.Beschreibung = text;
        }
    }
}