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
    public class Ressource
    {
        public string Name { get; set; }
        public int Image { get; set; }
        public ResType Type { get; set; }
        public int Forschungswert { get; set; }

        public string Description { get; set; }


        public List<Ressource> Rezept { get; set; } = new List<Ressource>();
        public Herstellung Hersteller { get; set; }


        public string Tauschwert { get; set; }
        public TauschObjekt TauschObj { get; set; }

        public List<Trade> Tauschen { get; set; } = new List<Trade>();

        public List<int> Images { get; set; }


        public Ressource(string name, int image, ResType type, int wert)
        {
            this.Name = name;
            this.Image = image;
            this.Type = type;
            this.Forschungswert = wert;
            this.Images = new List<int>() { image };
        }

        public void SetDescription(string text)
        {
            this.Description = text;
        }

        public void SetHerstellung(List<Ressource> rezept, Herstellung hersteller)
        {
            this.Rezept = rezept;
            this.Hersteller = hersteller;
        }

        public void AddImage(int image)
        {
            this.Images.Add(image);
        }

        public void SetTauschObjekt(TauschObjekt obj)
        {
            this.TauschObj = obj;
        }

        public void SetTrade(List<Trade> Trade)
        {
            this.Tauschen = Trade;
        }
    }
}