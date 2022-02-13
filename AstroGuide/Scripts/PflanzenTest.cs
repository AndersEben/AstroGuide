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

        public static Pflanze Zischrebe = new Pflanze("Zischrebe", PlantType.Offensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Knalloon = new Pflanze("Knalloon", PlantType.Offensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Katapflanze = new Pflanze("Katapflanze", PlantType.Offensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Attaktus = new Pflanze("Attaktus", PlantType.Offensive, Resource.Drawable.Icon_Aluminiumlegierung);
        public static Pflanze Spuckblume = new Pflanze("Spuckblume", PlantType.Offensive, Resource.Drawable.Icon_Aluminiumlegierung);


        public static List<Pflanze> Alle_Pflanzen = new List<Pflanze>() { Knallkoralle,Dolchwurzel,Keuchkraut,Stachellilie,Peitschenblatt,Huepfranke,Distelgerte,Zischrebe,Knallkoralle,Katapflanze,Attaktus,Spuckblume};


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

        public static List<Pflanze> AllPlant()
        {
            var fields = typeof(PflanzenTest).GetFields();
            List<Pflanze> plant = new List<Pflanze>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Pflanze))
                {
                    plant.Add((Pflanze)fields[i].GetValue(null));
                }

            }

            return plant;
        }

        public static List<Verwendung> AllPlantToVerwendung()
        {
            var fields = typeof(PflanzenTest).GetFields();
            List<Verwendung> verwendung = new List<Verwendung>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Pflanze))
                {
                    var file = (Pflanze)fields[i].GetValue(null);
                    verwendung.Add(new Verwendung(file.Name,file.Image,VerwendungsTyp.Pflanze));
                }

            }

            return verwendung;
        }
    }
}