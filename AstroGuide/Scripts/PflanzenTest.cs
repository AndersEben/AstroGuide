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
    static class PflanzenTest
    {

        public static Pflanze Knallkoralle = new Pflanze("Knallkoralle", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Dolchwurzel = new Pflanze("Dolchwurzel", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Keuchkraut = new Pflanze("Keuchkraut", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Stachellilie = new Pflanze("Stachellilie", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Peitschenblatt = new Pflanze("Peitschenblatt", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Huepfranke = new Pflanze("Hüpfranke", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Distelgerte = new Pflanze("Distelgerte", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);

        public static Pflanze Zischrebe = new Pflanze("Zischrebe", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Knalloon = new Pflanze("Knalloon", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Katapflanze = new Pflanze("Katapflanze", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Attaktus = new Pflanze("Attaktus", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Spuckblume = new Pflanze("Spuckblume", PlantType.Defensive, Resource.Drawable.Icon_Aluminiumlegierung);

        public static Pflanze FindPLant(string name)
        {
            var fields = typeof(PflanzenTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Pflanze))
                {
                    Pflanze test = (Pflanze)fields[i].GetValue(null);
                    if (test.Name == name)
                    {
                        return test;
                    }
                }

            }

            return Knallkoralle;
        }
    }
}