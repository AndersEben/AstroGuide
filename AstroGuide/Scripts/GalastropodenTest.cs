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
    static class GalastropodenTest
    {

        public static Galastropode Sylvie = new Galastropode("Sylvie", Resource.Drawable.Galas_Sylvie, PflanzenTest.Zischrebe,Resource.Drawable.Icon_Sylvie);
        public static Galastropode Usagi = new Galastropode("Usagi", Resource.Drawable.Galas_Usagi, PflanzenTest.Zischrebe,Resource.Drawable.Icon_Usagi);
        public static Galastropode Stilgar = new Galastropode("Stilgar", Resource.Drawable.Galas_Stilgar, PflanzenTest.Attaktus,Resource.Drawable.Icon_Stilgar);
        public static Galastropode Princess = new Galastropode("Princess", Resource.Drawable.Galas_Princess, PflanzenTest.Katapflanze,Resource.Drawable.Icon_Princess);
        public static Galastropode Rogal = new Galastropode("Rogal", Resource.Drawable.Galas_Rogal, PflanzenTest.Katapflanze,Resource.Drawable.Icon_Rogal);
        public static Galastropode Bestefar = new Galastropode("Bestefar", Resource.Drawable.Galas_Bestefar, PflanzenTest.Knalloon,Resource.Drawable.Icon_Bestefar);
        public static Galastropode Enoki = new Galastropode("Enoki", Resource.Drawable.Galas_Enoki, PflanzenTest.Spuckblume,Resource.Drawable.Icon_Enoki);

        public static List<Galastropode> Alle_Galastro = new List<Galastropode>() { Sylvie,Usagi,Stilgar,Princess,Rogal,Bestefar,Enoki };
        
        public static Galastropode FindGalast(string name)
        {
            var fields = typeof(GalastropodenTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Galastropode))
                {
                    Galastropode test = (Galastropode)fields[i].GetValue(null);
                    if (test.Name == name)
                    {
                        return test;
                    }
                }

            }

            return Sylvie;
        }

        public static List<Galastropode> AllGalastro()
        {
            var fields = typeof(GalastropodenTest).GetFields();
            List<Galastropode> galastro = new List<Galastropode>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Galastropode))
                {
                    galastro.Add((Galastropode)fields[i].GetValue(null));
                }

            }

            return galastro;
        }

        public static List<Verwendung> AllGalastroToVerwendung()
        {
            var fields = typeof(GalastropodenTest).GetFields();
            List<Verwendung> verwendung = new List<Verwendung>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Galastropode))
                {
                    var file = (Galastropode)fields[i].GetValue(null);
                    verwendung.Add(new Verwendung(file.Name, file.Image, VerwendungsTyp.Galastropode));
                }

            }

            return verwendung;
        }
    }
}