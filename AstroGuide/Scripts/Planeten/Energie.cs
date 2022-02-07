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

namespace AstroGuide.Scripts.Planeten
{
    public class Energie
    {
        public string Name { get; set; }

        public Strength Staerke { get; set; }

        public Energie(string name, Strength staerke)
        {
            this.Name = name;
            this.Staerke = staerke;
        }
    }
}