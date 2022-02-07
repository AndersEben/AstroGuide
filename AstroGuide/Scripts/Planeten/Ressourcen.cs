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
    public class Ressourcen
    {
        public Ressource Ress { get; set; }
        public ResArt Art { get; set; }
        public string Haeufigkeit { get; set; }

        public Ressourcen(Ressource res, ResArt art, string anzahl)
        {
            this.Ress = res;
            this.Art = art;
            this.Haeufigkeit = anzahl;
        }
    }
}