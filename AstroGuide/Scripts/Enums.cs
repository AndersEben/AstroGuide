using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts
{
    public enum Difficulty
    {
        leicht,
        mittel,
        schwer,
        overkill
    }

    public enum PlanetSize
    {
        klein,
        mittel,
        groß
    }

    public enum Strength
    {
        niedrig,
        mittel,
        hoch,
        overkill
    }

    public enum Herstellung
    {
        Schmelzofen,
        Chemielabor,
        Atmosphärenkondensator,
        Schredder,
        None,
        Handelsplattform
    }

    public enum VerwendungsTyp
    {
        Ressource,
        Crafter,
        Craft,
        Pflanze,
        Galastropode,
        Planet
    }

    public enum TauschObjekt
    {
        Schrott,
        Astronium
    }

    public enum CraftTool
    {
        [Description("Rucksack")]
        rucksack,
        [Description("Kleiner Drucker")]
        kleinerDrucker,
        [Description("Mittelgroßer Drucker")]
        mittlererDrucker,
        [Description("Großer Drucker")]
        grosserDrucker
    }

    public enum CraftType
    {
        [Description("Beleuchtungsgegenstand")]
        beleuchtungsGegenstand,
        [Description("Anfertigungsgegenstand")]
        anfertigungsGegenstand,
        [Description("Geländewerkzeug-Erweiterung")]
        gelaendewerkzeugErweiterun,
        [Description("Aktivierungsobjekt")]
        aktivierungsObjekt,
        [Description("Fahrzeuggegenstand")]
        fahrzeugGegensatnd,
        [Description("Nutzgegenstand")]
        nutzgegenstand,
        [Description("Stromgegenstand")]
        stromGegenstand,
        [Description("Modul")]
        modul,
        [Description("Basisbaugegenstand")]
        basisGegenstand,
        [Description("Fortbewegungsgegenstand")]
        fortbewegungsGegenstand,
        [Description("Freizeitgegenstand")]
        freizeitGegenstand,
        [Description("Werkzeuge")]
        werkzeuge,
        [Description("Sauerstoffgegenstand")]
        sauerstoffGegenstand,
        [Description("Erkennungsgegenstand")]
        erkennungsGegenstand
    }

    public enum ResType
    {
        [Description("Natürliche Ressource")]
        naturalResource,
        [Description("Natürliches Mineral")]
        naturalMineral,
        [Description("Raffinierte Ressource")]
        verfeinerteResource,
        [Description("Komposite Ressource")]
        kompositRessource,
        [Description("Atmosp. Ressource")]
        atmoRessource,
        [Description("Sonstige Ressourcen")]
        sonstigeRessource
    }

    public enum PlantType
    {
        [Description("Defensive")]
        Defensive,
        [Description("Offensive")]
        Offensive
    }

    public enum ResArt
    {
        Primär,
        Sekundär,
        Universale,
        Gase
    }

    static class Funktionen
    {
        public static string ShowEnumLabel(ResType val)
        {
            Type type = val.GetType();
            string name = Enum.GetName(type, val);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return "PlaceHolder";
        }

        public static string ShowEnumLabel(CraftType val)
        {
            Type type = val.GetType();
            string name = Enum.GetName(type, val);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return "PlaceHolder";
        }

        public static string ShowEnumLabel(PlantType val)
        {
            Type type = val.GetType();
            string name = Enum.GetName(type, val);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return "PlaceHolder";
        }
    }
}