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
    static class MaterialTest
    {
        #region NaturalRessource
        public static Ressource gemisch = new Ressource("Gemisch", Resource.Drawable.Icon_Compound, ResType.naturalResource, 50);
        public static Ressource harz = new Ressource("Harz", Resource.Drawable.Icon_Resin, ResType.naturalResource, 50);
        public static Ressource organisch = new Ressource("Organisch", Resource.Drawable.Icon_Organisch, ResType.naturalResource, 50);
        public static Ressource graphit = new Ressource("Graphit", Resource.Drawable.Icon_Graphite, ResType.naturalResource, 100);
        public static Ressource ammonium = new Ressource("Ammonium", Resource.Drawable.Icon_Ammonium, ResType.naturalResource, 100);
        public static Ressource astronium = new Ressource("Astronium", Resource.Drawable.Icon_Astronium, ResType.naturalResource, 1000);

        public static Ressource lehm = new Ressource("Lehm", Resource.Drawable.Icon_Lehm, ResType.naturalResource, 100);
        public static Ressource laterit = new Ressource("Laterit", Resource.Drawable.Icon_Laterit, ResType.naturalResource, 100);
        public static Ressource quartz = new Ressource("Quartz", Resource.Drawable.Icon_Quartz, ResType.naturalResource, 100);
        #endregion

        #region NaturalMineral
        public static Ressource malachit = new Ressource("Malachit", Resource.Drawable.Icon_Malachit, ResType.naturalMineral, 201);
        public static Ressource zinkblende = new Ressource("Zinkblende", Resource.Drawable.Icon_Zinkblende, ResType.naturalMineral, 201);
        public static Ressource hämatit = new Ressource("Hämatit", Resource.Drawable.Icon_Haematit, ResType.naturalMineral, 300);
        public static Ressource wolframit = new Ressource("Wolframit", Resource.Drawable.Icon_Wolframit, ResType.naturalMineral, 201);
        public static Ressource titanit = new Ressource("Titanit", Resource.Drawable.Icon_Titanit, ResType.naturalMineral, 300);
        public static Ressource lithium = new Ressource("Lithium", Resource.Drawable.Icon_Lithium, ResType.naturalMineral, 300);
        #endregion

        #region VerfeinerteRessource
        public static Ressource kohlenstoff = new Ressource("Kohlenstoff", Resource.Drawable.Icon_Kohlenstoff, ResType.verfeinerteResource, 75);
        public static Ressource keramik = new Ressource("Keramik", Resource.Drawable.Icon_Keramik, ResType.verfeinerteResource, 150);
        public static Ressource glas = new Ressource("Glas", Resource.Drawable.Icon_Glas, ResType.verfeinerteResource, 150);
        public static Ressource aluminium = new Ressource("Aluminium", Resource.Drawable.Icon_Aluminium, ResType.verfeinerteResource, 150);
        public static Ressource kupfer = new Ressource("Kupfer", Resource.Drawable.Icon_Kupfer, ResType.verfeinerteResource, 275);
        public static Ressource zink = new Ressource("Zink", Resource.Drawable.Icon_Zink, ResType.verfeinerteResource, 275);
        public static Ressource wolfram = new Ressource("Wolfram", Resource.Drawable.Icon_Wolfram, ResType.verfeinerteResource, 275);
        public static Ressource eisen = new Ressource("Eisen", Resource.Drawable.Icon_Eisen, ResType.verfeinerteResource, 425);
        public static Ressource titan = new Ressource("Titan", Resource.Drawable.Icon_Titan, ResType.verfeinerteResource, 425);
        #endregion

        #region Kompositressource
        public static Ressource gummi = new Ressource("Gummi", Resource.Drawable.Icon_Gummi, ResType.kompositRessource, 0);
        public static Ressource kunststoff = new Ressource("Kunststoff", Resource.Drawable.Icon_Kunststoff, ResType.kompositRessource, 0);
        public static Ressource aluminiumlegierung = new Ressource("Aluminiumlegierung", Resource.Drawable.Icon_Aluminiumlegierung, ResType.kompositRessource, 0);
        public static Ressource wolframkarbid = new Ressource("Wolframkarbid", Resource.Drawable.Icon_Wolframkarbid, ResType.kompositRessource, 0);
        public static Ressource graphen = new Ressource("Graphen", Resource.Drawable.Icon_Graphen, ResType.kompositRessource, 0);
        public static Ressource diamant = new Ressource("Diamant", Resource.Drawable.Icon_Diamant, ResType.kompositRessource, 0);
        public static Ressource hydrazin = new Ressource("Hydrazin", Resource.Drawable.Icon_Hydrazin, ResType.kompositRessource, 0);
        public static Ressource silikon = new Ressource("Silikon", Resource.Drawable.Icon_Silikon, ResType.kompositRessource, 0);
        public static Ressource sprengpulver = new Ressource("Sprengpulver", Resource.Drawable.Icon_Sprengpulver, ResType.kompositRessource, 0);
        public static Ressource stahl = new Ressource("Stahl", Resource.Drawable.Icon_Stahl, ResType.kompositRessource, 0);
        public static Ressource titanlegierung = new Ressource("Titanlegierung", Resource.Drawable.Icon_Titanlegierung, ResType.kompositRessource, 0);
        public static Ressource nanocarbonlegierung = new Ressource("Nanocarbonlegierung", Resource.Drawable.Icon_Nanocarbonlegierung, ResType.kompositRessource, 0);
        #endregion

        #region Gase
        public static Ressource argon = new Ressource("Argon", Resource.Drawable.Icon_Argon, ResType.atmoRessource, 0);
        public static Ressource helium = new Ressource("Helium", Resource.Drawable.Icon_Helium, ResType.atmoRessource, 0);
        public static Ressource methan = new Ressource("Methan", Resource.Drawable.Icon_Methan, ResType.atmoRessource, 0);
        public static Ressource schwefel = new Ressource("Schwefel", Resource.Drawable.Icon_Schwefel, ResType.atmoRessource, 0);
        public static Ressource stickstoff = new Ressource("Stickstoff", Resource.Drawable.Icon_Stickstoff, ResType.atmoRessource, 0);
        public static Ressource wasserstoff = new Ressource("Wasserstoff", Resource.Drawable.Icon_Wasserstoff, ResType.atmoRessource, 0);
        #endregion

        #region Sonstiges
        public static Ressource schrott = new Ressource("Schrott", Resource.Drawable.Icon_Schrott, ResType.sonstigeRessource, 275);
        public static Ressource erde = new Ressource("Erde", Resource.Drawable.Icon_Soil, ResType.sonstigeRessource, 0);
        public static Ressource sauerstoff = new Ressource("Sauerstoff", Resource.Drawable.Icon_Sauerstoff, ResType.sonstigeRessource, 0);
        public static Ressource energie = new Ressource("Energie", Resource.Drawable.Icon_Energie, ResType.sonstigeRessource, 0);
        public static Ressource exo_chip = new Ressource("EXO Chip", Resource.Drawable.Icon_EXOChip, ResType.sonstigeRessource, 0);
        #endregion


        public static List<Ressource> Alle_nat_Ressourcen = new List<Ressource>() { gemisch, harz, organisch, graphit, ammonium, astronium, lehm, laterit, quartz, malachit, zinkblende, hämatit, wolframit, titanit, lithium };
        public static List<Ressource> Alle_raff_Ressourcen = new List<Ressource>() { kohlenstoff, keramik, glas, aluminium, kupfer, zink, wolfram, eisen, titan };
        public static List<Ressource> Alle_komp_Ressourcen = new List<Ressource>() { gummi, kunststoff, aluminiumlegierung, wolframkarbid, graphen, diamant, hydrazin, silikon, sprengpulver, stahl, titanlegierung, nanocarbonlegierung };
        public static List<Ressource> Alle_gase_Ressourcen = new List<Ressource>() { argon, helium, methan, schwefel, stickstoff, wasserstoff };
        public static List<Ressource> Alle_sonstigen_Ressourcen = new List<Ressource>() { schrott, erde, sauerstoff, energie, exo_chip };

        public static List<Ressource> Alle_Ressourcen = new List<Ressource>() { gemisch, harz, organisch, graphit, ammonium, astronium, lehm, laterit, quartz, malachit, zinkblende, hämatit, wolframit, titanit, lithium, kohlenstoff, keramik, glas, aluminium, kupfer, zink, wolfram, eisen, titan, gummi, kunststoff, aluminiumlegierung, wolframkarbid, graphen, diamant, hydrazin, silikon, sprengpulver, stahl, titanlegierung, nanocarbonlegierung, argon, helium, methan, schwefel, stickstoff, wasserstoff, schrott, erde, sauerstoff, energie, exo_chip };

        public static Ressource FindRessource(string name)
        {
            var fields = typeof(MaterialTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if(type == typeof(Ressource))
                {
                    Ressource test = (Ressource)fields[i].GetValue(null);
                    if(test.Name == name)
                    {
                        return test;
                    }
                }

            }

            return quartz;
        }

        public static List<Ressource> AllRessource()
        {
            var fields = typeof(MaterialTest).GetFields();
            List<Ressource> ressour = new List<Ressource>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Ressource))
                {
                    ressour.Add((Ressource)fields[i].GetValue(null));
                }

            }

            return ressour;
        }

        public static List<Verwendung> AllRessourceToVerwendung()
        {
            var fields = typeof(MaterialTest).GetFields();
            List<Verwendung> verwendung = new List<Verwendung>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Ressource))
                {
                    var file = (Ressource)fields[i].GetValue(null);
                    verwendung.Add(new Verwendung(file.Name,file.Image,VerwendungsTyp.Ressource));
                }
            }

            return verwendung;
        }
    }
}