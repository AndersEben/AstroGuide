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
    public class Crafter
    {
        public string Name { get; set; }
        public int Image { get; set; }
        public List<int> Images { get; set; }

        public List<Ressource> Rezept { get; set; }
        public Crafter Hersteller { get; set; }

        public string Description { get; set; }

        public Crafter(string name, int image)
        {
            this.Name = name;
            this.Image = image;
            this.Images = new List<int>() { image };
        }
        public void AddImage(int image)
        {
            this.Images.Add(image);
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