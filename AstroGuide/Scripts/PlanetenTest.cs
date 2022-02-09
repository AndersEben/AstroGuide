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
    static class PlanetenTest
    {

        public static Planet Sylva = new Planet("Sylva", "Erdähnlicher Planet", PlanetSize.mittel, Difficulty.leicht, Resource.Drawable.Icon_Sylva,MaterialTest.quartz);
        public static Planet Desolo = new Planet("Desolo", "Erdähnlicher Mond", PlanetSize.klein, Difficulty.leicht, Resource.Drawable.Icon_Desolo,MaterialTest.zink);
        public static Planet Vesania = new Planet("Vesania", "Exotischer Planet", PlanetSize.groß, Difficulty.mittel, Resource.Drawable.Icon_Vesania,MaterialTest.graphen);
        public static Planet Novus = new Planet("Novus", "Exotischer Mond", PlanetSize.klein, Difficulty.mittel, Resource.Drawable.Icon_Novus,MaterialTest.silikon);
        public static Planet Calidor = new Planet("Calidor", "Wüstenhafter Planet", PlanetSize.klein, Difficulty.mittel, Resource.Drawable.Icon_Calidor,MaterialTest.sprengpulver);
        public static Planet Glacio = new Planet("Glacio", "Tundra Planet", PlanetSize.klein, Difficulty.schwer, Resource.Drawable.Icon_Glacio,MaterialTest.diamant);
        public static Planet Atrox = new Planet("Atrox", "Verstrahlter Planet", PlanetSize.klein, Difficulty.overkill, Resource.Drawable.Icon_Atrox,MaterialTest.wasserstoff);


        public static List<Planet> Alle_Planeten = new List<Planet>() { Sylva, Desolo, Vesania, Novus, Calidor, Glacio, Atrox };

        public static Planet FindPlanet(string name)
        {
            var fields = typeof(PlanetenTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Planet))
                {
                    Planet test = (Planet)fields[i].GetValue(null);
                    if (test.Name == name)
                    {
                        return test;
                    }
                }

            }

            return Sylva;
        }
    }
}