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
    public class Verwendung
    {
        public string Name { get; set; }
        public int Image { get; set; }

        public VerwendungsTyp Typ { get; set; }

        public Verwendung(string name, int image, VerwendungsTyp typ)
        {
            this.Name = name;
            this.Image = image;
            this.Typ = typ;
        }
    }
}