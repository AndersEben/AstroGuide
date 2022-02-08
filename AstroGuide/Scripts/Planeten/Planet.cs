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
    public class Planet
    {

        public string Name { get; }
        public string Typ { get; }
        public PlanetSize Groesse { get; }
        public Difficulty Schwierigkeitsgrad { get; }

        public List<Energie> Energieqiellen { get; set; }
        public List<Ressourcen> Ress { get; set; }

        public List<Pflanze> Pflanzen { get; set; }

        public Ressource PortalElement { get; set; }

        public Galastropode Galastro { get; set; }

        public int Image { get; set; }

        public Planet(string name, string type, PlanetSize groesse, Difficulty diff, int image,Ressource res)
        {
            this.Name = name;
            this.Typ = type;
            this.Groesse = groesse;
            this.Schwierigkeitsgrad = diff;
            this.Image = image;
            this.PortalElement = res;
        }

        public void SetEnergie(List<Energie> quellen)
        {
            this.Energieqiellen = quellen;
        }

        public void SetPflanzen(List<Pflanze> pflanzen)
        {
            this.Pflanzen = pflanzen;
        }

        public void SetRessourcen(List<Ressourcen> ressourcen)
        {
            this.Ress = ressourcen;
        }

        public void SetGalastropode(Galastropode tropode)
        {
            this.Galastro = tropode;
        }
    }
}