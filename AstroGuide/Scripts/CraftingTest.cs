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
    static class CraftingTest
    {

        public static Crafter rucksack = new Crafter("Rucksack", Resource.Drawable.Icon_Rucksack);
        public static Crafter kleinerDrucker = new Crafter("Kleiner Drucker", Resource.Drawable.Icon_Kleiner_Drucker);
        public static Crafter mittlerDrucker = new Crafter("Mittelgroßer Drucker", Resource.Drawable.Icon_Mittelgrosser_Drucker);
        public static Crafter grosserDrucker = new Crafter("Großer Drucker", Resource.Drawable.Icon_Grosser_Drucker);

        public static List<Crafter> Alle_crafter = new List<Crafter>() { rucksack, kleinerDrucker, mittlerDrucker, grosserDrucker };


        public static Crafter FindCrafter(string name)
        {
            var fields = typeof(CraftingTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Crafter))
                {
                    Crafter test = (Crafter)fields[i].GetValue(null);
                    if (test.Name == name)
                    {
                        return test;
                    }
                }

            }

            return rucksack;
        }

        public static List<Crafter> AllCrafter()
        {
            var fields = typeof(CraftingTest).GetFields();
            List<Crafter> crafter = new List<Crafter>();
            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Crafter))
                {
                    crafter.Add((Crafter)fields[i].GetValue(null));
                }

            }

            return crafter;
        }

        public static List<Verwendung> AllCrafterToVerwendung()
        {
            var fields = typeof(CraftingTest).GetFields();
            List<Verwendung> verwendung = new List<Verwendung>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Crafter))
                {
                    var file = (Crafter)fields[i].GetValue(null);
                    verwendung.Add(new Verwendung(file.Name, file.Image, VerwendungsTyp.Crafter));
                }

            }

            return verwendung;
        }


        #region Tier1
        public static Craft verbindungen = new Craft("Verbindungen", Resource.Drawable.Icon_Verbindungspaket,0,CraftType.sauerstoffGegenstand,2);
        public static Craft sauerstofffilter = new Craft("Sauerstofffilter", Resource.Drawable.Icon_Verbindungspaket,0, CraftType.sauerstoffGegenstand,2);
        public static Craft kleinerKanister = new Craft("Kleiner Kanister", Resource.Drawable.Icon_Kleiner_Kanister,0, CraftType.werkzeuge,3);
        public static Craft leuchtfeuer = new Craft("Leuchtfeuer", Resource.Drawable.Icon_Leuchtfeuer,0, CraftType.erkennungsGegenstand,3);
        public static Craft arbeitslicht = new Craft("Arbeitslicht", Resource.Drawable.Icon_Arbeitslicht,0, CraftType.beleuchtungsGegenstand,3);
        public static Craft leuchtstab = new Craft("Leuchtstab", Resource.Drawable.Icon_Arbeitslicht,350, CraftType.beleuchtungsGegenstand,3);
        public static Craft scheinwerfer = new Craft("Flutlicht", Resource.Drawable.Icon_Arbeitslicht,2000, CraftType.beleuchtungsGegenstand,3);
        public static Craft verpacker = new Craft("Verpacker", Resource.Drawable.Icon_Packager,1000, CraftType.basisGegenstand,1);
        public static Craft kleinersauerstofftank = new Craft("Sauerstofftank", Resource.Drawable.Icon_Oxygen_Tank,2000, CraftType.sauerstoffGegenstand,2);
        public static Craft tragbarerSauerstoffmacher = new Craft("Tragbarer Sauerstoffmacher", Resource.Drawable.Icon_Oxygenator,10000, CraftType.sauerstoffGegenstand,2);
        public static Craft kleinerGenerator = new Craft("Kleiner Generator", Resource.Drawable.Icon_Generator,0,CraftType.stromGegenstand,4);
        public static Craft kleinesSolarmodul = new Craft("Kleines Solarmodul", Resource.Drawable.Icon_Solar,300,CraftType.stromGegenstand,4);
        public static Craft kleineWindturbine = new Craft("Kleine Windturbine", Resource.Drawable.Icon_Wind_Turbine,300, CraftType.stromGegenstand, 4);
        public static Craft energiezellen = new Craft("Energiezellen", Resource.Drawable.Icon_Power_Cells,800, CraftType.stromGegenstand,4);
        public static Craft kleineBatterie = new Craft("Kleine Batterie", Resource.Drawable.Icon_Battery,2000, CraftType.stromGegenstand, 4);
        public static Craft boostModus = new Craft("Boost Modus", Resource.Drawable.Icon_Augment,1000, CraftType.gelaendewerkzeugErweiterun,5);
        public static Craft breiteAnpassung = new Craft("Breite Anpassung", Resource.Drawable.Icon_Augment,1000,CraftType.gelaendewerkzeugErweiterun,5);
        public static Craft schmaleAnpassung = new Craft("Schmale Anpassung", Resource.Drawable.Icon_Augment,1000,CraftType.gelaendewerkzeugErweiterun,5);
        public static Craft hemmer = new Craft("Hemmer", Resource.Drawable.Icon_Augment,1000, CraftType.gelaendewerkzeugErweiterun,5);
        public static Craft ausrichtungsmodus = new Craft("Ausrichtungsmodus", Resource.Drawable.Icon_Augment,1000,CraftType.gelaendewerkzeugErweiterun,5);
        public static Craft gelaendeAnalysator = new Craft("Gelände Analysator", Resource.Drawable.Icon_Augment,2000,CraftType.gelaendewerkzeugErweiterun,8);
        public static Craft bohreranpassungSt1 = new Craft("Bohreranpassung Stufe 1", Resource.Drawable.Icon_Augment,1000,CraftType.gelaendewerkzeugErweiterun,6);
        public static Craft bohreranpassungSt2 = new Craft("Bohreranpassung Stufe 2", Resource.Drawable.Icon_Augment,2500, CraftType.gelaendewerkzeugErweiterun,6);
        public static Craft bohreranpassungSt3 = new Craft("Bohreranpassung Stufe 3", Resource.Drawable.Icon_Augment,3750, CraftType.gelaendewerkzeugErweiterun, 6);
        public static Craft dynamit = new Craft("Dynamit", Resource.Drawable.Icon_Dynamite,3750,CraftType.nutzgegenstand,7);
        public static Craft feuerwerk = new Craft("Feuerwerk", Resource.Drawable.Icon_Fireworks,3750,CraftType.freizeitGegenstand,7);
        public static Craft kleineKamera = new Craft("Kleine Kamera", Resource.Drawable.Icon_Small_Camera, 2500,CraftType.freizeitGegenstand,8);
        public static Craft hoverboard = new Craft("Hoverboard", Resource.Drawable.Icon_Hoverboard, 0,CraftType.fortbewegungsGegenstand,9);
        public static Craft sondenscanner = new Craft("Sondenscanner", Resource.Drawable.Icon_Astronium, 4000,CraftType.erkennungsGegenstand,8);
        public static Craft kTrompetenhupe = new Craft("Kleine Trompetenhupe", Resource.Drawable.Icon_Astronium, 1000,CraftType.freizeitGegenstand,8);
        public static Craft hydrazinJezpack = new Craft("Hydrazin Jetpack", Resource.Drawable.Icon_Astronium, 15000,CraftType.fortbewegungsGegenstand,8);
        public static Craft holografischeFigur = new Craft("Holografische Figur", Resource.Drawable.Icon_Astronium, 3000,CraftType.freizeitGegenstand,8);
        public static Craft feststoffSprungbooster = new Craft("Feststoff Sprung-Booster", Resource.Drawable.Icon_Astronium, 5000, CraftType.fortbewegungsGegenstand,9);
        public static Craft einebnungsblock = new Craft("Einebnungsblock", Resource.Drawable.Icon_Astronium, 500,CraftType.nutzgegenstand,7);
        #endregion

        #region Tier2
        public static Craft mgroßesLager = new Craft("Mittelgroßes Lager", Resource.Drawable.Icon_Mittelgrosses_Lager,0,CraftType.nutzgegenstand,8);
        public static Craft verlaengerungen = new Craft("Verlängerungen", Resource.Drawable.Icon_Verlaengerungen, 500, CraftType.stromGegenstand,5);
        public static Craft mgroßesSilo = new Craft("Mittelgroßes Silo", Resource.Drawable.Icon_Mittelgrosses_Lager, 3000,CraftType.nutzgegenstand,8);
        public static Craft hohesLager = new Craft("Hohes Lager", Resource.Drawable.Icon_Mittelgrosses_Lager, 400,CraftType.nutzgegenstand,8);
        public static Craft mgroßePlattformA = new Craft("Mittelgroße Plattform A", Resource.Drawable.Icon_Mittelgrosse_Plattform_A,0,CraftType.basisGegenstand,7);
        public static Craft mgroßePlattformC = new Craft("Mittelgroße Plattform C", Resource.Drawable.Icon_Mittelgrosse_Plattform_A,400,CraftType.basisGegenstand,7);
        public static Craft hPlatform = new Craft("Hohe Plattform", Resource.Drawable.Icon_Mittelgrosse_Plattform_A, 750, CraftType.basisGegenstand,7);
        public static Craft mgTPlatform = new Craft("Mittelgroße T-Plattform", Resource.Drawable.Icon_Mittelgrosse_Plattform_A, 400,CraftType.basisGegenstand,7);
        public static Craft roverSitz = new Craft("Rover Sitz", Resource.Drawable.Icon_Rover_Sitz,0,CraftType.fahrzeugGegensatnd,9);
        public static Craft sauerstoffmacher = new Craft("Sauerstoffmacher", Resource.Drawable.Icon_Sauerstoffmacher,1800,CraftType.sauerstoffGegenstand,1);
        public static Craft mgroßerGenerator = new Craft("Mittelgroßer Generator", Resource.Drawable.Icon_Generator,2000,CraftType.stromGegenstand,6);
        public static Craft mgroßesSolarmodul = new Craft("Mittelgroßes Solarmodul", Resource.Drawable.Icon_Mittelgrosses_Solarmodul,2000,CraftType.stromGegenstand,6);
        public static Craft mgroßeWindturbine = new Craft("Mittelgroße Windturbine", Resource.Drawable.Icon_Mittelgrosse_Windturbine,2500,CraftType.stromGegenstand,6);
        public static Craft mgroßeBatterie = new Craft("Mittelgroße Batterie", Resource.Drawable.Icon_Mittelgrosse_Batterie,3750,CraftType.stromGegenstand,6);
        public static Craft rtg = new Craft("RTG", Resource.Drawable.Icon_Generator,12500,CraftType.stromGegenstand,6);
        public static Craft verteiler = new Craft("Verteiler", Resource.Drawable.Icon_Verteiler,1000,CraftType.stromGegenstand,5);
        public static Craft mgroßePlattformB = new Craft("Mittelgroße Plattform B", Resource.Drawable.Icon_Mittelgrosse_Plattform_B,250,CraftType.basisGegenstand,7);
        public static Craft schredder = new Craft("Mittlerer Schredder", Resource.Drawable.Icon_Schredder,1250,CraftType.modul,1);
        public static Craft traktor = new Craft("Traktor", Resource.Drawable.Icon_Traktor,1000,CraftType.fahrzeugGegensatnd,9);
        public static Craft haenger = new Craft("Hänger", Resource.Drawable.Icon_Haenger,1500,CraftType.fahrzeugGegensatnd,9);
        public static Craft bohrstaerke1 = new Craft("Bohrstärke 1", Resource.Drawable.Icon_Bohrstaerke_1,2500,CraftType.fahrzeugGegensatnd,10);
        public static Craft bohrstaerke2 = new Craft("Bohrstärke 2", Resource.Drawable.Icon_Bohrstaerke_2,5000,CraftType.fahrzeugGegensatnd,10);
        public static Craft bohrstaerke3 = new Craft("Bohrstärke 3", Resource.Drawable.Icon_Bohrstaerke_3,7500,CraftType.fahrzeugGegensatnd,10);
        public static Craft feststoffSchubduese = new Craft("Feststoff-Schubdüse", Resource.Drawable.Icon_Feststoff_Schubduese,500,CraftType.fahrzeugGegensatnd,11);
        public static Craft hydrazinSchubduese = new Craft("Hydrazin-Schubdüse", Resource.Drawable.Icon_Hydrazin_Schubduese,3750,CraftType.fahrzeugGegensatnd,11);
        public static Craft winde = new Craft("Winde", Resource.Drawable.Icon_Winde,3750, CraftType.fahrzeugGegensatnd,9);
        public static Craft autoArm = new Craft("Auto-Arm", Resource.Drawable.Icon_Astronium, 1500, CraftType.aktivierungsObjekt,2);
        public static Craft feldunterkunft = new Craft("Feldunterkunft", Resource.Drawable.Icon_Astronium, 8000,CraftType.basisGegenstand,1);
        public static Craft mgKanister = new Craft("Mittlerer Ressourcenkanister", Resource.Drawable.Icon_Astronium, 2000,CraftType.werkzeuge,2);
        public static Craft mgFlKanister = new Craft("Mittlerer Flüssig/Erdkanister", Resource.Drawable.Icon_Astronium, 2500,CraftType.werkzeuge,2);
        public static Craft mgGaskanister = new Craft("Mittlerer Gaskanister", Resource.Drawable.Icon_Astronium, 4000,CraftType.werkzeuge,2);
        public static Craft energiesensor = new Craft("Energiesensor", Resource.Drawable.Icon_Astronium, 500,CraftType.aktivierungsObjekt,3);
        public static Craft lagersensor = new Craft("Lagersensor", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,3);
        public static Craft batteriesensor = new Craft("Batteriesensor", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,3);
        public static Craft tastenwiederholer = new Craft("Tastenwiederholer", Resource.Drawable.Icon_Astronium, 300,CraftType.aktivierungsObjekt,4);
        public static Craft naeherungsinitiator = new Craft("Näherungsinitiator", Resource.Drawable.Icon_Astronium, 700,CraftType.aktivierungsObjekt,4);
        public static Craft verzoegerungswiederholer = new Craft("Verzögerungswiederholer", Resource.Drawable.Icon_Astronium, 1000,CraftType.aktivierungsObjekt,4);
        public static Craft zahlwiederholer = new Craft("Zahlwiederholer", Resource.Drawable.Icon_Astronium, 1000,CraftType.aktivierungsObjekt,4);
        public static Craft netzschalter = new Craft("Netzschalter", Resource.Drawable.Icon_Astronium, 750,CraftType.stromGegenstand,5);
        public static Craft mgroßeHupe = new Craft("Mittelgroße Hupe", Resource.Drawable.Icon_Astronium, 2000,CraftType.fahrzeugGegensatnd,9);
        public static Craft planierer = new Craft("Planierer", Resource.Drawable.Icon_Astronium, 5000,CraftType.fahrzeugGegensatnd,10);
        #endregion

        #region Tier3
        public static Craft gLager = new Craft("Großes Lager", Resource.Drawable.Icon_Grosses_Lager,2000,CraftType.nutzgegenstand,5);
        public static Craft gPlattformA = new Craft("Große Plattform A", Resource.Drawable.Icon_Grosse_Plattform_A,0,CraftType.basisGegenstand,4);
        public static Craft gPlattformB = new Craft("Große Plattform B", Resource.Drawable.Icon_Grosse_Plattform_B,500,CraftType.basisGegenstand,4);
        public static Craft gPlattformC = new Craft("Große Plattform B", Resource.Drawable.Icon_Grosse_Plattform_C,1000, CraftType.basisGegenstand,4);
        //public static Craft landePlattform = new Craft("Landeplattform", Resource.Drawable.Icon_Landeplattform,750);
        public static Craft forschungsKammer = new Craft("Forschungskammer", Resource.Drawable.Icon_Forschungskammer,0,CraftType.modul,2);
        public static Craft schmelzofen = new Craft("Schmelzofen", Resource.Drawable.Icon_Schmelzofen,250,CraftType.anfertigungsGegenstand,1);
        public static Craft erdzentrifuge = new Craft("Erdzentrifuge", Resource.Drawable.Icon_Erdzentrifuge,750,CraftType.anfertigungsGegenstand,1);
        public static Craft chemielabor = new Craft("Chemielabor", Resource.Drawable.Icon_Chemielabor,1600,CraftType.anfertigungsGegenstand,1);
        public static Craft atmosphaerenkondensator = new Craft("Atmosphärenkondensator", Resource.Drawable.Icon_Atmosphaerenkondensator,2200,CraftType.anfertigungsGegenstand,1);
        public static Craft handelsPlattform = new Craft("Handelsplattform", Resource.Drawable.Icon_Handelsplattform,2500,CraftType.modul,2);
        public static Craft gSchredder = new Craft("Großer Schredder", Resource.Drawable.Icon_Grosser_Schredder,2500,CraftType.modul,2);
        public static Craft buggy = new Craft("Buggy", Resource.Drawable.Icon_Rover,1500,CraftType.fahrzeugGegensatnd,6);
        public static Craft mGrover = new Craft("Mittelgroßer Rover", Resource.Drawable.Icon_Rover,3750,CraftType.fahrzeugGegensatnd,6);
        public static Craft gRoverSitz = new Craft("Großer Rover Sitz", Resource.Drawable.Icon_Grosser_Rover_Sitz,2000,CraftType.fahrzeugGegensatnd,6);
        public static Craft kran = new Craft("Kran", Resource.Drawable.Icon_Kran,4500,CraftType.modul,6);
        public static Craft exoPlattform = new Craft("EXO Auftrags-Plattform", Resource.Drawable.Icon_Astronium, 0,CraftType.modul,2);
        public static Craft gRessourcenkanister = new Craft("Großer Ressourcenkanister", Resource.Drawable.Icon_Astronium, 5000,CraftType.werkzeuge,5);
        public static Craft gSolarmodul = new Craft("Großes Solarmodul", Resource.Drawable.Icon_Astronium, 4000,CraftType.stromGegenstand,3);
        public static Craft gWindturbine = new Craft("Große Windturbine", Resource.Drawable.Icon_Astronium, 3500, CraftType.stromGegenstand,3);
        public static Craft gTPlattform = new Craft("Große T-Plattform", Resource.Drawable.Icon_Astronium, 1000,CraftType.basisGegenstand,4);
        public static Craft gGPlattform = new Craft("Große Gewölbte Plattform", Resource.Drawable.Icon_Astronium, 1000,CraftType.basisGegenstand,4);
        public static Craft gSiloA = new Craft("Großes Silo A", Resource.Drawable.Icon_Astronium, 5000,CraftType.nutzgegenstand,5);
        public static Craft gSiloB = new Craft("Großes Silo B", Resource.Drawable.Icon_Astronium, 7500, CraftType.nutzgegenstand,5);
        public static Craft gEPlattform = new Craft("Große Erweiterte Plattform", Resource.Drawable.Icon_Astronium, 500,CraftType.basisGegenstand,4);
        public static Craft gASpeicher = new Craft("Großer Aktiver Speicher", Resource.Drawable.Icon_Astronium, 2000,CraftType.nutzgegenstand,5);
        public static Craft vtol = new Craft("VTOL", Resource.Drawable.Icon_Astronium, 0,CraftType.fahrzeugGegensatnd,6);
        public static Craft gNebelhorn = new Craft("Großes Nebelhorn", Resource.Drawable.Icon_Astronium, 4000,CraftType.fahrzeugGegensatnd,6);
        public static Craft freizeitKugel = new Craft("Freizeit-Kugel", Resource.Drawable.Icon_Astronium, 4500,CraftType.freizeitGegenstand,7);

        #endregion

        #region Tier4
        public static Craft egPlattformA = new Craft("Extra Große Plattform A", Resource.Drawable.Icon_Extragrosse_Plattform_A,2000,CraftType.basisGegenstand,5);
        public static Craft egPlattformB = new Craft("Extra Große Plattform B", Resource.Drawable.Icon_Extragrosse_Plattform_B,3000,CraftType.basisGegenstand,5);
        public static Craft egPlattformC = new Craft("Extra Große Plattform C", Resource.Drawable.Icon_Extragrosse_Plattform_C,2000,CraftType.basisGegenstand,5);
        public static Craft egLager = new Craft("Extra Großes Lager", Resource.Drawable.Icon_Extragrosses_Lager,2000, CraftType.nutzgegenstand,6);
        public static Craft egSchredder = new Craft("Extra Großer Schredder", Resource.Drawable.Icon_Extragrosser_Schredder,5000,CraftType.modul,1);
        public static Craft gRover = new Craft("Großer Rover", Resource.Drawable.Icon_Grosser_Rover,5000,CraftType.fahrzeugGegensatnd,7);
        public static Craft kShuttle = new Craft("Kleines Shuttle", Resource.Drawable.Icon_Kleines_Shuttle,1500,CraftType.fahrzeugGegensatnd,7);
        public static Craft mShuttle = new Craft("Mittleres Shuttle", Resource.Drawable.Icon_Mittleres_Shuttle,3750,CraftType.fahrzeugGegensatnd,7);
        public static Craft gShuttle = new Craft("Großes Shuttle", Resource.Drawable.Icon_Grosses_Shuttle,5000,CraftType.fahrzeugGegensatnd,7);
        public static Craft unterkunft = new Craft("Unterkunft", Resource.Drawable.Icon_Unterkunft,0,CraftType.basisGegenstand,1);
        public static Craft solarstromanlage = new Craft("Solarstromanlage", Resource.Drawable.Icon_Solarstromanlage,6000,CraftType.stromGegenstand,2);
        public static Craft autoExtraktor = new Craft("Auto-Extraktor", Resource.Drawable.Icon_Astronium, 7500,CraftType.aktivierungsObjekt,1);
        public static Craft windturbineXl = new Craft("Windturbine XL", Resource.Drawable.Icon_Astronium, 4500,CraftType.stromGegenstand,2);
        public static Craft mSensorbogen = new Craft("Mittlerer Sensorbogen", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,3);
        public static Craft xlSensorbogen = new Craft("XL-Sensorbogen", Resource.Drawable.Icon_Astronium, 1000,CraftType.aktivierungsObjekt,3);
        public static Craft xlSensorbaladachin = new Craft("XL-Sensorbaladachin", Resource.Drawable.Icon_Astronium, 1000,CraftType.aktivierungsObjekt,3);
        public static Craft gSensorring = new Craft("Großer Sensorring", Resource.Drawable.Icon_Astronium, 500,CraftType.aktivierungsObjekt,4);
        public static Craft gSensorringA = new Craft("Großer Sensorring A", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,4);
        public static Craft gSensorringB = new Craft("Großer Sensorring B", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,4);
        public static Craft xlSensorringA = new Craft("XL-Sensorring A", Resource.Drawable.Icon_Astronium, 750,CraftType.aktivierungsObjekt,4);
        public static Craft xlSensorringB = new Craft("XL-Sensorring B", Resource.Drawable.Icon_Astronium, 1000,CraftType.aktivierungsObjekt,4);
        public static Craft egGPlattform = new Craft("Extra große Gewölbte Plattform", Resource.Drawable.Icon_Astronium, 2000, CraftType.basisGegenstand,5);
        public static Craft xlEPlattform = new Craft("XL Erweiterte Plattform", Resource.Drawable.Icon_Astronium, 750,CraftType.basisGegenstand,5);
        public static Craft fPlattform = new Craft("Figuren-Plattform", Resource.Drawable.Icon_Astronium, 3000,CraftType.basisGegenstand,6);
        public static Craft landeplattform = new Craft("Landeplattform", Resource.Drawable.Icon_Astronium, 750,CraftType.basisGegenstand,7);
        //public static Craft KUnterkunkt = new Craft("Kürbis-Unterkunft", Resource.Drawable.Icon_Astronium, 0);
        #endregion


        public static List<Craft> Alle_craft = new List<Craft>() { verbindungen, sauerstofffilter, kleinerKanister, leuchtfeuer, arbeitslicht, leuchtstab, 
            scheinwerfer, verpacker, kleinersauerstofftank, tragbarerSauerstoffmacher, kleinerGenerator, kleinesSolarmodul, kleineWindturbine, energiezellen,
            kleineBatterie, verlaengerungen, boostModus, breiteAnpassung, schmaleAnpassung, hemmer, ausrichtungsmodus, gelaendeAnalysator, bohreranpassungSt1, 
            bohreranpassungSt2, bohreranpassungSt3, dynamit, feuerwerk, mgroßesLager, mgroßePlattformA, roverSitz, sauerstoffmacher, mgroßerGenerator, 
            mgroßesSolarmodul, mgroßeWindturbine, mgroßeBatterie, rtg, verteiler, mgroßePlattformB, schredder, traktor, haenger, bohrstaerke1, bohrstaerke2, 
            bohrstaerke3, feststoffSchubduese, hydrazinSchubduese, winde, gLager, gPlattformA, gPlattformB, gPlattformC, forschungsKammer,
            schmelzofen, erdzentrifuge, chemielabor, atmosphaerenkondensator, handelsPlattform, gSchredder, buggy, mGrover, gRoverSitz, kran, egPlattformA,
            egPlattformB, egPlattformC, egLager, egSchredder, gRover, kShuttle, mShuttle, gShuttle, unterkunft, solarstromanlage, kleineKamera, hoverboard, 
            autoArm, feldunterkunft, mgKanister, mgFlKanister, mgGaskanister, energiesensor, lagersensor, batteriesensor, tastenwiederholer, naeherungsinitiator, 
            verzoegerungswiederholer,zahlwiederholer, netzschalter, mgroßeHupe, mgroßePlattformC, hPlatform, mgTPlatform, planierer, exoPlattform, gRessourcenkanister, 
            gSolarmodul, gWindturbine, gTPlattform, gGPlattform, gSiloA, gSiloB, gEPlattform, gASpeicher, vtol, gNebelhorn, freizeitKugel, autoExtraktor, windturbineXl, 
            mSensorbogen, xlSensorbogen, xlSensorbaladachin, gSensorring, gSensorringA, gSensorringB, xlSensorringA, xlSensorringB, egGPlattform, xlEPlattform, fPlattform, 
            landeplattform, hohesLager, mgroßesSilo, sondenscanner ,kTrompetenhupe, hydrazinJezpack, holografischeFigur, feststoffSprungbooster, einebnungsblock };


        public static Craft FindCraft(string name)
        {
            var fields = typeof(CraftingTest).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Craft))
                {
                    Craft test = (Craft)fields[i].GetValue(null);
                    if (test.Name.Contains(name))
                    {
                        return test;
                    }
                }

            }

            return verbindungen;
        }

        public static List<Craft> AllCraft()
        {
            var fields = typeof(CraftingTest).GetFields();
            List<Craft> craft = new List<Craft>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Craft))
                {
                    craft.Add((Craft)fields[i].GetValue(null));
                }

            }

            return craft;
        }

        public static List<Verwendung> AllCraftToVerwendung()
        {
            var fields = typeof(CraftingTest).GetFields();
            List<Verwendung> verwendung = new List<Verwendung>();

            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (type == typeof(Craft))
                {
                    var file = (Craft)fields[i].GetValue(null);
                    verwendung.Add(new Verwendung(file.Name,file.Image,VerwendungsTyp.Craft));
                }

            }

            return verwendung;
        }
    }
}