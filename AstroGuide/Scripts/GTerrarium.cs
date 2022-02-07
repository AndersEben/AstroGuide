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
    public class GTerrarium
    {
        public Ressource Element1 { get; set; }

        public Ressource Element2 { get; set; }

        public Pflanze Samen { get; set; }

        public GTerrarium(Ressource element1, Ressource element2, Pflanze samen)
        {
            this.Element1 = element1;
            this.Element2 = element2;
            this.Samen = samen;
        }

    }
}