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
    public class Craft
    {
        public string Name { get; set; }
        public int Image { get; set; }

        public List<Ressource> Rezept { get; set; }
        public Crafter Hersteller { get; set; }
        public CraftType Typ { get; set; }
        public int Forschungsebene { get; set; }
        public string Description { get; set; }

        public int Forschungskosten { get; set; }

        public Craft(string name, int image, int forschungskosten)
        {
            this.Name = name;
            this.Image = image;
            this.Forschungskosten = forschungskosten;
            
        }

        public Craft(string name, int image, int forschungskosten,CraftType typ,int ebene)
        {
            this.Name = name;
            this.Image = image;
            this.Forschungskosten = forschungskosten;
            this.Typ = typ;
            this.Forschungsebene = ebene;
        }

        public void SetDescription(string text)
        {
            this.Description = text;
        }

        public void SetHerstellung(List<Ressource> rezept, Crafter hersteller)
        {
            this.Rezept = rezept;
            this.Hersteller = hersteller;
        }
    }
}