using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstroGuide.Scripts.Settings;
using AstroGuide.Scripts;
using AstroGuide.Scripts.Planeten;

namespace AstroGuide
{
    [Activity(Theme = "@style/AppTheme.Splash", NoHistory = true, MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class StartActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { Startup(); });
            startupWork.Start();
        }

        async void Startup()
        {

            var display = Resources.DisplayMetrics;
            Einstellungen.TextSizeListOffset = display.WidthPixels;

            Einstellungen.TXT_pixel10dip = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            LoadAll();

            CreateMaterial();
            CreateCrafting();
            CreateTerrarium();
            CreatePlaneten();

            await Task.Delay(2000);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        private void LoadAll()
        {
            MaterialTest.Alle_Ressourcen = MaterialTest.AllRessource();

            CraftingTest.Alle_craft = CraftingTest.AllCraft();
            CraftingTest.Alle_crafter = CraftingTest.AllCrafter();

            PlanetenTest.Alle_Planeten = PlanetenTest.AllPlanet();

            PflanzenTest.Alle_Pflanzen = PflanzenTest.AllPlant();

            GalastropodenTest.Alle_Galastro = GalastropodenTest.AllGalastro();


            foreach (var item in MaterialTest.AllRessourceToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }

            foreach (var item in CraftingTest.AllCraftToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }

            foreach (var item in CraftingTest.AllCrafterToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }

            foreach (var item in PlanetenTest.AllPlanetToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }

            foreach (var item in PflanzenTest.AllPlantToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }

            foreach (var item in GalastropodenTest.AllGalastroToVerwendung())
            {
                VerwendungTest.AlleElemente.Add(item);
            }
        }


        private void CreateMaterial()
        {

            MaterialTest.astronium.SetTauschObjekt(TauschObjekt.Astronium);
            MaterialTest.astronium.SetTrade(new List<Trade>() { new Trade("EXO Chip","3:1", VerwendungsTyp.Ressource), new Trade("Verpacker","1:4", VerwendungsTyp.Craft),
                new Trade("Feststoff-Schubdüse","1:2", VerwendungsTyp.Craft), new Trade("Dynamit","3:4", VerwendungsTyp.Craft), new Trade("Hydrazin","3:4", VerwendungsTyp.Ressource) });

            MaterialTest.schrott.SetTauschObjekt(TauschObjekt.Schrott);
            MaterialTest.schrott.SetTrade(new List<Trade>() { new Trade(MaterialTest.gemisch.Name,"1:2",VerwendungsTyp.Ressource),
                new Trade(MaterialTest.harz.Name,"1:2", VerwendungsTyp.Ressource), new Trade(MaterialTest.organisch.Name,"1:2", VerwendungsTyp.Ressource),
                new Trade(MaterialTest.lehm.Name,"1:2", VerwendungsTyp.Ressource), new Trade(MaterialTest.quartz.Name,"1:2", VerwendungsTyp.Ressource),
                new Trade(MaterialTest.graphit.Name,"1:2", VerwendungsTyp.Ressource), new Trade(MaterialTest.zinkblende.Name,"1:2",VerwendungsTyp.Ressource),
                new Trade(MaterialTest.ammonium.Name,"1:1",VerwendungsTyp.Ressource), new Trade(MaterialTest.laterit.Name,"1:1",VerwendungsTyp.Ressource),
                new Trade(MaterialTest.malachit.Name,"1:1", VerwendungsTyp.Ressource), new Trade(MaterialTest.wolframit.Name,"3:2", VerwendungsTyp.Ressource),
                new Trade(MaterialTest.hämatit.Name,"3:2", VerwendungsTyp.Ressource), new Trade(MaterialTest.titanit.Name,"2:1", VerwendungsTyp.Ressource),
                new Trade(MaterialTest.lithium.Name,"4:1", VerwendungsTyp.Ressource)});


            for (int i = 0; i < MaterialTest.astronium.Tauschen.Count; i++)
            {
                MaterialTest.astronium.Tauschen[i].SetTradeObjekt(MaterialTest.astronium.TauschObj);

                switch (MaterialTest.astronium.Tauschen[i].Typ)
                {
                    case VerwendungsTyp.Ressource:
                        MaterialTest.astronium.Tauschen[i].SetImage(MaterialTest.FindRessource(MaterialTest.astronium.Tauschen[i].Item).Image);
                        break;
                    case VerwendungsTyp.Crafter:
                        MaterialTest.astronium.Tauschen[i].SetImage(CraftingTest.FindCrafter(MaterialTest.astronium.Tauschen[i].Item).Image);
                        break;
                    case VerwendungsTyp.Craft:
                        MaterialTest.astronium.Tauschen[i].SetImage(CraftingTest.FindCraft(MaterialTest.astronium.Tauschen[i].Item).Image);
                        break;
                }
            }

            for (int i = 0; i < MaterialTest.schrott.Tauschen.Count; i++)
            {
                MaterialTest.schrott.Tauschen[i].SetTradeObjekt(MaterialTest.schrott.TauschObj);

                switch (MaterialTest.schrott.Tauschen[i].Typ)
                {
                    case VerwendungsTyp.Ressource:
                        MaterialTest.schrott.Tauschen[i].SetImage(MaterialTest.FindRessource(MaterialTest.schrott.Tauschen[i].Item).Image);
                        break;
                    case VerwendungsTyp.Crafter:
                        MaterialTest.schrott.Tauschen[i].SetImage(CraftingTest.FindCrafter(MaterialTest.schrott.Tauschen[i].Item).Image);
                        break;
                    case VerwendungsTyp.Craft:
                        MaterialTest.schrott.Tauschen[i].SetImage(CraftingTest.FindCraft(MaterialTest.schrott.Tauschen[i].Item).Image);
                        break;
                }
            }

            MaterialTest.gemisch.AddImage(Resource.Drawable.Nugget_Gemisch);
            MaterialTest.harz.AddImage(Resource.Drawable.Nugget_Harz);
            MaterialTest.organisch.AddImage(Resource.Drawable.Nugget_Organisch);
            MaterialTest.graphit.AddImage(Resource.Drawable.Nugget_Graphit);
            MaterialTest.ammonium.AddImage(Resource.Drawable.Nugget_Ammonium);
            MaterialTest.astronium.AddImage(Resource.Drawable.Nugget_Astronium);
            MaterialTest.lehm.AddImage(Resource.Drawable.Nugget_Lehm);
            MaterialTest.laterit.AddImage(Resource.Drawable.Nugget_Laterit);
            MaterialTest.quartz.AddImage(Resource.Drawable.Nugget_Quartz);

            MaterialTest.malachit.AddImage(Resource.Drawable.Nugget_Malachit);
            MaterialTest.zinkblende.AddImage(Resource.Drawable.Nugget_Zinkblende);
            MaterialTest.hämatit.AddImage(Resource.Drawable.Nugget_Haematit);
            MaterialTest.wolframit.AddImage(Resource.Drawable.Nugget_Wolframit);
            MaterialTest.titanit.AddImage(Resource.Drawable.Nugget_Titanit);
            MaterialTest.lithium.AddImage(Resource.Drawable.Nugget_Lithium);

            MaterialTest.kohlenstoff.AddImage(Resource.Drawable.Nugget_Kohlenstoff);
            MaterialTest.keramik.AddImage(Resource.Drawable.Nugget_Keramik);
            MaterialTest.glas.AddImage(Resource.Drawable.Nugget_Glas);
            MaterialTest.aluminium.AddImage(Resource.Drawable.Nugget_Aluminum);
            MaterialTest.kupfer.AddImage(Resource.Drawable.Nugget_Kupfer);
            MaterialTest.zink.AddImage(Resource.Drawable.Nugget_Zink);
            MaterialTest.wolfram.AddImage(Resource.Drawable.Nugget_Wolfram);
            MaterialTest.eisen.AddImage(Resource.Drawable.Nugget_Eisen);
            MaterialTest.titan.AddImage(Resource.Drawable.Nugget_Titan);

            MaterialTest.gummi.AddImage(Resource.Drawable.Nugget_Gummi);
            MaterialTest.kunststoff.AddImage(Resource.Drawable.Nugget_Kunststoff);
            MaterialTest.aluminiumlegierung.AddImage(Resource.Drawable.Nugget_Aluminumlegierung);
            MaterialTest.wolframkarbid.AddImage(Resource.Drawable.Nugget_Wolframkarbid);
            MaterialTest.graphen.AddImage(Resource.Drawable.Nugget_Graphen);
            MaterialTest.diamant.AddImage(Resource.Drawable.Nugget_Diamand);
            MaterialTest.hydrazin.AddImage(Resource.Drawable.Nugget_Hydrazin);
            MaterialTest.silikon.AddImage(Resource.Drawable.Nugget_Silikon);
            MaterialTest.sprengpulver.AddImage(Resource.Drawable.Nugget_Sprengpulver);
            MaterialTest.stahl.AddImage(Resource.Drawable.Nugget_Stahlpng);
            MaterialTest.titanlegierung.AddImage(Resource.Drawable.Nugget_Titanlegierung);
            MaterialTest.nanocarbonlegierung.AddImage(Resource.Drawable.Nugget_Nanocarbonlegierung);

            MaterialTest.argon.AddImage(Resource.Drawable.Nugget_Argon);
            MaterialTest.helium.AddImage(Resource.Drawable.Nugget_Helium);
            MaterialTest.methan.AddImage(Resource.Drawable.Nugget_Methan);
            MaterialTest.schwefel.AddImage(Resource.Drawable.Nugget_Schwefel);
            MaterialTest.stickstoff.AddImage(Resource.Drawable.Nugget_Stickstoff);
            MaterialTest.wasserstoff.AddImage(Resource.Drawable.Nugget_Wasserstoff);

            MaterialTest.erde.AddImage(Resource.Drawable.Nugget_Erde);
            MaterialTest.exo_chip.AddImage(Resource.Drawable.Nugget_EXOChip);

            MaterialTest.kohlenstoff.SetHerstellung(new List<Ressource>() { MaterialTest.organisch }, Herstellung.Schmelzofen);
            MaterialTest.kohlenstoff.SetDescription("");
            MaterialTest.keramik.SetHerstellung(new List<Ressource>() { MaterialTest.lehm }, Herstellung.Schmelzofen);
            MaterialTest.keramik.SetDescription("");
            MaterialTest.glas.SetHerstellung(new List<Ressource>() { MaterialTest.quartz }, Herstellung.Schmelzofen);
            MaterialTest.glas.SetDescription("");
            MaterialTest.aluminium.SetHerstellung(new List<Ressource>() { MaterialTest.laterit }, Herstellung.Schmelzofen);
            MaterialTest.aluminium.SetDescription("");
            MaterialTest.kupfer.SetHerstellung(new List<Ressource>() { MaterialTest.malachit }, Herstellung.Schmelzofen);
            MaterialTest.kupfer.SetDescription("");
            MaterialTest.zink.SetHerstellung(new List<Ressource>() { MaterialTest.zinkblende }, Herstellung.Schmelzofen);
            MaterialTest.zink.SetDescription("");
            MaterialTest.wolfram.SetHerstellung(new List<Ressource>() { MaterialTest.wolframit }, Herstellung.Schmelzofen);
            MaterialTest.wolfram.SetDescription("");
            MaterialTest.eisen.SetHerstellung(new List<Ressource>() { MaterialTest.hämatit }, Herstellung.Schmelzofen);
            MaterialTest.eisen.SetDescription("");
            MaterialTest.titan.SetHerstellung(new List<Ressource>() { MaterialTest.titanit }, Herstellung.Schmelzofen);
            MaterialTest.titan.SetDescription("");


            MaterialTest.gummi.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.organisch }, Herstellung.Chemielabor);
            MaterialTest.gummi.SetDescription("");
            MaterialTest.kunststoff.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.kohlenstoff }, Herstellung.Chemielabor);
            MaterialTest.kunststoff.SetDescription("");
            MaterialTest.aluminiumlegierung.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.kupfer }, Herstellung.Chemielabor);
            MaterialTest.aluminiumlegierung.SetDescription("");
            MaterialTest.wolframkarbid.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram, MaterialTest.kohlenstoff }, Herstellung.Chemielabor);
            MaterialTest.wolframkarbid.SetDescription("");
            MaterialTest.graphen.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.hydrazin }, Herstellung.Chemielabor);
            MaterialTest.graphen.SetDescription("");
            MaterialTest.diamant.SetHerstellung(new List<Ressource>() { MaterialTest.graphen, MaterialTest.graphen }, Herstellung.Chemielabor);
            MaterialTest.diamant.SetDescription("");
            MaterialTest.hydrazin.SetHerstellung(new List<Ressource>() { MaterialTest.ammonium, MaterialTest.ammonium, MaterialTest.wasserstoff }, Herstellung.Chemielabor);
            MaterialTest.hydrazin.SetDescription("");
            MaterialTest.silikon.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.quartz, MaterialTest.methan }, Herstellung.Chemielabor);
            MaterialTest.silikon.SetDescription("");
            MaterialTest.sprengpulver.SetHerstellung(new List<Ressource>() { MaterialTest.kohlenstoff, MaterialTest.kohlenstoff, MaterialTest.schwefel }, Herstellung.Chemielabor);
            MaterialTest.sprengpulver.SetDescription("");
            MaterialTest.stahl.SetHerstellung(new List<Ressource>() { MaterialTest.kohlenstoff, MaterialTest.eisen, MaterialTest.argon }, Herstellung.Chemielabor);
            MaterialTest.stahl.SetDescription("");
            MaterialTest.titanlegierung.SetHerstellung(new List<Ressource>() { MaterialTest.graphen, MaterialTest.titan, MaterialTest.stickstoff }, Herstellung.Chemielabor);
            MaterialTest.titanlegierung.SetDescription("");
            MaterialTest.nanocarbonlegierung.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.titanlegierung, MaterialTest.helium }, Herstellung.Chemielabor);
            MaterialTest.nanocarbonlegierung.SetDescription("");

            MaterialTest.argon.SetHerstellung(new List<Ressource>() { MaterialTest.argon }, Herstellung.Atmosphärenkondensator);
            MaterialTest.argon.SetDescription("");
            MaterialTest.helium.SetHerstellung(new List<Ressource>() { MaterialTest.helium }, Herstellung.Atmosphärenkondensator);
            MaterialTest.helium.SetDescription("");
            MaterialTest.methan.SetHerstellung(new List<Ressource>() { MaterialTest.methan }, Herstellung.Atmosphärenkondensator);
            MaterialTest.methan.SetDescription("");
            MaterialTest.schwefel.SetHerstellung(new List<Ressource>() { MaterialTest.schwefel }, Herstellung.Atmosphärenkondensator);
            MaterialTest.schwefel.SetDescription("");
            MaterialTest.stickstoff.SetHerstellung(new List<Ressource>() { MaterialTest.stickstoff }, Herstellung.Atmosphärenkondensator);
            MaterialTest.stickstoff.SetDescription("");
            MaterialTest.wasserstoff.SetHerstellung(new List<Ressource>() { MaterialTest.wasserstoff }, Herstellung.Atmosphärenkondensator);
            MaterialTest.wasserstoff.SetDescription("");

            MaterialTest.schrott.SetHerstellung(new List<Ressource>() { }, Herstellung.Schredder);
            MaterialTest.schrott.SetDescription("");
            MaterialTest.erde.SetHerstellung(new List<Ressource>() { }, Herstellung.None);
            MaterialTest.erde.SetDescription("");
            MaterialTest.sauerstoff.SetHerstellung(new List<Ressource>() { }, Herstellung.None);
            MaterialTest.sauerstoff.SetDescription("");
            MaterialTest.energie.SetHerstellung(new List<Ressource>() { }, Herstellung.None);
            MaterialTest.energie.SetDescription("");
            MaterialTest.exo_chip.SetHerstellung(new List<Ressource>() { }, Herstellung.Handelsplattform);
            MaterialTest.exo_chip.SetDescription("");

        }
        private void CreatePflanzen()
        {

            PflanzenTest.Knallkoralle.SetDescription("");
            PflanzenTest.Dolchwurzel.SetDescription("");
            PflanzenTest.Keuchkraut.SetDescription("");
            PflanzenTest.Stachellilie.SetDescription("");
            PflanzenTest.Peitschenblatt.SetDescription("");
            PflanzenTest.Huepfranke.SetDescription("");
            PflanzenTest.Distelgerte.SetDescription("");

            PflanzenTest.Zischrebe.SetDescription("");
            PflanzenTest.Knalloon.SetDescription("");
            PflanzenTest.Katapflanze.SetDescription("");
            PflanzenTest.Attaktus.SetDescription("");
            PflanzenTest.Spuckblume.SetDescription("");
            
            /*
            PflanzenTest.Knallkoralle.AddImage();
            PflanzenTest.Dolchwurzel.AddImage();
            PflanzenTest.Keuchkraut.AddImage();
            PflanzenTest.Stachellilie.AddImage();
            PflanzenTest.Peitschenblatt.AddImage();
            PflanzenTest.Huepfranke.AddImage();
            PflanzenTest.Distelgerte.AddImage();

            PflanzenTest.Zischrebe.AddImage();
            PflanzenTest.Knalloon.AddImage();
            PflanzenTest.Katapflanze.AddImage();
            PflanzenTest.Attaktus.AddImage();
            PflanzenTest.Spuckblume.AddImage(); 
            */

        }

        private void CreateCrafting()
        {
            CraftingTest.rucksack.SetHerstellung(new List<Ressource>() { }, CraftingTest.rucksack);
            CraftingTest.rucksack.SetDescription("");
            CraftingTest.kleinerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerDrucker.SetDescription("Druckt kleine bis mittlere Gegenstände aus Ressourcen.");
            CraftingTest.mittlerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.mittlerDrucker.SetDescription("Druckt große Gegenstände aus Ressourcen");
            CraftingTest.grosserDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.grosserDrucker.SetDescription("Druckt extra große Gegenstände aus Ressourcen.");


            CraftingTest.verbindungen.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.verbindungen.SetDescription("Bündel von Verbindungen, das dein Sauerstoffnetzwerk erweitert, wenn an Axygenerator angeschlossen.");
            CraftingTest.sauerstofffilter.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.sauerstofffilter.SetDescription("Bietet eine vorübergehende Erhöhung deiner Sauerstoffkapazität.");
            CraftingTest.kleinerKanister.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kleinerKanister.SetDescription("Nachfüllbarer Erdkanister zur Verwendung mit dem Geländewerkzeug, der Bodenzentrifuge und den Fahrzeugbohrer");
            CraftingTest.leuchtfeuer.SetHerstellung(new List<Ressource>() { MaterialTest.quartz }, CraftingTest.rucksack);
            CraftingTest.leuchtfeuer.SetDescription("Anpassbare Kompassmarkierung zur Unterstützung der Navigation");
            CraftingTest.arbeitslicht.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.arbeitslicht.SetDescription("Bietet kleine Lichtquelle, wenn mit Strom versorgt.");
            CraftingTest.leuchtstab.SetHerstellung(new List<Ressource>() { MaterialTest.organisch }, CraftingTest.rucksack);
            CraftingTest.leuchtstab.SetDescription("Bündel von temporären Lichtquellen zur Unterstützung der Navigation");
            CraftingTest.scheinwerfer.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram }, CraftingTest.rucksack);
            CraftingTest.scheinwerfer.SetDescription("Bietet eine leistungsstarke Lichtquelle, die ein- und ausgeschaltet werde kann");
            CraftingTest.verpacker.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.verpacker.SetDescription("Verpackt Gegenstände zum leichten Tragen und Transport");
            CraftingTest.kleinersauerstofftank.SetHerstellung(new List<Ressource>() { MaterialTest.glas }, CraftingTest.rucksack);
            CraftingTest.kleinersauerstofftank.SetDescription("Nachfüllbarer Behälter, der deine Sauerstoffkapazität erweitert");
            CraftingTest.tragbarerSauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung }, CraftingTest.rucksack);
            CraftingTest.tragbarerSauerstoffmacher.SetDescription("Produziert Sauerstoff, wenn angetrieben.");
            CraftingTest.kleinerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerGenerator.SetDescription("Tragbare Energiequelle, die mit organischen Ressourcen betrieben wird.");
            CraftingTest.kleinesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.kleinesSolarmodul.SetDescription("Wandelt Sonnelicht in Energie um.");
            CraftingTest.kleineWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.kleineWindturbine.SetDescription("Wandelt Wind in Energie um.");
            CraftingTest.energiezellen.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.energiezellen.SetDescription("Verbrauchsgegenstand, der deine Energiekapazität erhöht");
            CraftingTest.kleineBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.kleineBatterie.SetDescription("Wiederaufladbare Stromquelle.");
            CraftingTest.verlaengerungen.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.verlaengerungen.SetDescription("Zum Verlängern von STromkabeln. Die Segmente zeigen die Stromrichtung an");
            CraftingTest.boostModus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.boostModus.SetDescription("Erhöhte Geschwindigkeit der Deformation. Aktivierung durch Anschluss an Geländewerkzeug");
            CraftingTest.breiteAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.breiteAnpassung.SetDescription("Erweitert die Verformung des Geländewerkzeugs. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.schmaleAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.schmaleAnpassung.SetDescription("Reduziert die Verformung des Geländewerkzeugs.  Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.hemmer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.hemmer.SetDescription("Ermöglicht Sammeln von Ressourcen und verhindert gleichzeitig Geländeverformung. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.ausrichtungsmodus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.ausrichtungsmodus.SetDescription("Richtet Geländeverformung an der Krümmung des Planeten aus. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.gelaendeAnalysator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.gelaendeAnalysator.SetDescription("Erkennt und isoliert Terrainfarben. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.bohreranpassungSt1.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt1.SetDescription("Erhöht die Borhstärke, um harte Geländeoberflächen zu bewältigen. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.bohreranpassungSt2.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt2.SetDescription("Erhöht die Borhstärke, um härtere Geländeoberflächen zu bewältigen. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.bohreranpassungSt3.SetHerstellung(new List<Ressource>() { MaterialTest.diamant }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt3.SetDescription("Erhöht die Borhstärke, um alle Geländeoberflächen zu bewältigen. Aktivierung durch Anschluss an Geländewerkzeug.");
            CraftingTest.dynamit.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.dynamit.SetDescription("Starker Sprengstoff für den Bergbau, zur Verformung von Gelände und andere Anwendung.(z.B. öffnen von Exo-Koffern)");
            CraftingTest.feuerwerk.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.feuerwerk.SetDescription("Standartfackel zum Senden von farbenfrohen Signalen.");
            CraftingTest.kleineKamera.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.kleineKamera.SetDescription("Macht ein Foto, wenn aktiviert. Enthält anpassbare Fotofilter.");
            CraftingTest.hoverboard.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.hoverboard.SetDescription("Persönliche, reibungsarmes Mobilitätsgerät. Ausgestattet durch den Rucksack");
            CraftingTest.sondenscanner.SetHerstellung(new List<Ressource>() { MaterialTest.stahl }, CraftingTest.rucksack);
            CraftingTest.sondenscanner.SetDescription("Verwenden sie in Zusatzplatz, um fehlende Sonden zu finden.");
            CraftingTest.kTrompetenhupe.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kTrompetenhupe.SetDescription("Erzeugt bei Aktivierung einen Ton. Benötigt für Galastropoden Missionen.");
            CraftingTest.hydrazinJezpack.SetHerstellung(new List<Ressource>() { MaterialTest.titanlegierung }, CraftingTest.rucksack);
            CraftingTest.hydrazinJezpack.SetDescription("Sorgt für anhaltenden Richtungsflug.Verbraucht Hydrazin als Treibstoff.");
            CraftingTest.holografischeFigur.SetHerstellung(new List<Ressource>() { MaterialTest.kunststoff }, CraftingTest.rucksack);
            CraftingTest.holografischeFigur.SetDescription("Erzeugt bei Aktivierung ein anpassbares Hologramm.");
            CraftingTest.feststoffSprungbooster.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung }, CraftingTest.rucksack);
            CraftingTest.feststoffSprungbooster.SetDescription("Der Feststoff Sprung-Booster wird verwendet, um dem Sprung eines Spielers einen zusätzlichen Schub zu verleihen.");
            CraftingTest.einebnungsblock.SetHerstellung(new List<Ressource>() { MaterialTest.erde }, CraftingTest.rucksack);
            CraftingTest.einebnungsblock.SetDescription("Der Einebnungsblock wird verwendet, um gleichmäßig flache Oberflächen zu erzeugen, die auf das Gitter des Planeten ausgerichtet sind. Auf diese Weise können Spieler Oberflächen erstellen, die auch als True Flat bezeichnet werden und glatt sind, ohne Grate oder Unebenheiten.");


            CraftingTest.feldunterkunft.SetHerstellung(new List<Ressource>() { MaterialTest.graphen, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.feldunterkunft.SetDescription("Der Feldunterkunft ist gut für Spieler, die kleine, temporäre Stützpunkte errichten möchten, während sie die Planeten erkunden. ");
            CraftingTest.autoArm.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.autoArm.SetDescription("Der Auto Arm ermöglicht den Spielern, die Bewegung von Gegenständen um ihre Basen herum zu automatisieren, mit der Möglichkeit, Tier-1 -Gegenstände von einem Steckplatz in einen anderen zu übertragen.");
            CraftingTest.mgKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgKanister.SetDescription("Der Mittlere Ressourcenkanister ist ein Lagergegenstand und ermöglicht Spielern, bis zu 32 Einheiten von einer Ressource zu lagern.");
            CraftingTest.mgFlKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgFlKanister.SetDescription("Der Mittlere Flüssig/Erdkanister dient der Aufbewahrung von Erde und Hydrazin und fasst die gleiche Menge Erde und Hydrazin wie 24 kleine Kanister .");
            CraftingTest.mgGaskanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.mgGaskanister.SetDescription("Der Mittlere Gaskanister ermöglicht den Spielern, mehr von einem Gas zu speichern, als andere Speichergegenstände zulassen.");
            CraftingTest.energiesensor.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.energiesensor.SetDescription("Der Energiesensor ermöglicht Spielern zu erkennen, wenn sich der Stromfluss ändert, und sendet ein Signal an verbundene Gegenstände.");
            CraftingTest.lagersensor.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.lagersensor.SetDescription("Der Lagersensor ermöglicht Spielern zu erkennen, wenn sich der Speicherstatus verbundener Speichergegenstände ändert, und sendet ein Signal an verbundene Gegenstände.");
            CraftingTest.batteriesensor.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.batteriesensor.SetDescription("Der Batteriesensor ermöglicht Spielern zu erkennen, wann eine Batterie aufgeladen ist, und sendet ein Signal an verbundene Gegenstände.");
            CraftingTest.tastenwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.tastenwiederholer.SetDescription("Der Button Repeater wird verwendet, um verschiedene Elemente zu aktivieren, entweder selbst oder durch Sensoren ausgelöst . Es kann in einen beliebigen Slot oder auf den Boden gelegt werden.");
            CraftingTest.naeherungsinitiator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.naeherungsinitiator.SetDescription("folgt noch :)");
            CraftingTest.verzoegerungswiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.verzoegerungswiederholer.SetDescription("Der Verzögerungswiederholer Aktiviert bei Aktivierung durch Sensoren oder Button Repeater auch alle angeschlossenen Module nach einer festgelegten Verzögerung.");
            CraftingTest.zahlwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.zahlwiederholer.SetDescription("Der Zahlwiederholer wird verwendet, um ein Signal zu senden, nachdem er eine festgelegte Anzahl von Malen mit Sensoren oder Button Repeater ausgelöst wurde. Es kann in einen beliebigen Slot oder auf den Boden gelegt werden. Der Spieler kann eine Signalanzahl von 2 bis 8 einstellen. Nachdem die ausgewählte Anzahl erreicht ist, sendet der Repeater ein Signal an verbundene Elemente.");
            CraftingTest.netzschalter.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.netzschalter.SetDescription("Der Netzschalter ist sehr nützlich, um komplexere Schaltungen herzustellen, da er als Transistor dienen kann, wodurch Logikgatter sehr einfach hergestellt werden können.");
            CraftingTest.mgroßePlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformC.SetDescription("Mittelgroße Plattform C ist eines der Hauptbaumodule.");
            CraftingTest.hPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hPlatform.SetDescription("Hohe Plattform ist eines der Hauptbaumodule.");
            CraftingTest.mgTPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgTPlatform.SetDescription("Mittelgroße T-Plattform ist eines der Hauptbaumodule.");
            CraftingTest.mgroßesSilo.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.titan }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSilo.SetDescription("Das Mittelgroße Silo kann bis zu 24 Tier-1- Gegenstände aufnehmen.");
            CraftingTest.hohesLager.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hohesLager.SetDescription("Der hohe Speicher verfügt über drei Tier-1- BefestigungsSlot am Ende einer großen Stange, wobei 2 der Slots nach unten abgewinkelt sind und einer Straßenlaterne ähneln.");
            CraftingTest.mgroßeHupe.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeHupe.SetDescription("Die Mittelgroße Hupe kann verwendet werden, um andere Spieler anzuhupen, während sie auf Rovers herumfahren. Wenn Sie es auf einen der vorderen Steckplätze des Rovers legen und die entsprechenden Kontexttasten drücken, wird die Hupe aktiviert.");
            CraftingTest.planierer.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.aluminiumlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.planierer.SetDescription("Der Planierer ist Bohrern ähnlich, aber anstatt Erde auszuheben, pflastert er eine Straße aus grauer Erde unter dem Rover. Sowohl ein Fertiger als auch ein Bohrer können zusammen verwendet werden, um Wege zu bohren und zu platzieren, sodass die Spieler Gruben und lange Abhänge nicht mehr manuell abdecken müssen. ");
            CraftingTest.mgroßesLager.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesLager.SetDescription("Mittelgroßes Lager ist ein Gebrauchsgegenstand, der bis zu 8 Tier-1- Gegenstände tragen kann.");
            CraftingTest.mgroßePlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformA.SetDescription("Mittelgroße Plattform A ist eines der Hauptbaumodule.");
            CraftingTest.roverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.roverSitz.SetDescription("Der Rover-Sitz ist ein Tier-2-Anbaugerät, das auf Rover und Shuttles gesetzt werden kann. Während sich die Spieler auf dem Sitz befinden, können sie alles bedienen, woran der Sitz befestigt ist. Sie können entweder hergestellt oder in verschiedenen Arten von Wracks auf den Planeten gefunden werden.");
            CraftingTest.sauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.sauerstoffmacher.SetDescription("Der Sauerstoffmacher ist ein Gegenstand zur Sauerstofferzeugung, der es Spielern ermöglicht, ihr Sauerstoff- und Energienetzwerk mithilfe von Fesseln zu erweitern, wenn er auf Fahrzeugen, Plattformen oder dem Startunterstand platziert wird. Es versorgt alle angeschlossenen Tether und alle mit Netzwerkkabeln verbundenen Plattformen mit Sauerstoff und Strom .");
            CraftingTest.mgroßerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.wolfram }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßerGenerator.SetDescription("Der Mittelgroße Generator ist ein Element zur Stromerzeugung und verbraucht  Kohlenstoff, um Strom zu erzeugen .");
            CraftingTest.mgroßesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSolarmodul.SetDescription("Es erzeugt nur Strom, wenn es Sonnenlicht ausgesetzt ist. Die beiden Paneele werden über der Mitte nach oben gefaltet, wenn sie nicht dem Sonnenlicht ausgesetzt ist. Das Medium Solar Panel hat keine Stromausgangskabel und muss daher auf einer Plattform mit 2-Steckplatz-Verbindung platziert werden, um Strom zu erzeugen.");
            CraftingTest.mgroßeWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeWindturbine.SetDescription("Die Mittelgroße Windturbine dient in erster Linie der Stromerzeugung. Die Leistung variiert nicht in Abhängigkeit von der Windgeschwindigkeit.");
            CraftingTest.mgroßeBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeBatterie.SetDescription("Die Mittelgroße Batterie ist ein Energiespeicher und kann mehr Strom aufnehmen als eine kleine Batterie und sorgt für eine gute Energiereserve mitten im Spiel.");
            CraftingTest.rtg.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.nanocarbonlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.rtg.SetDescription("Der RTG oder Radioisotope Thermoelectric Generator ist ein Stromerzeugungselement, das einen konstanten Stromstrom liefert. RTGs können auf Plattformen platziert werden, um jederzeit einen konstanten Stromfluss bereitzustellen.");
            CraftingTest.verteiler.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.graphit }, CraftingTest.kleinerDrucker);
            CraftingTest.verteiler.SetDescription("Der Splitter ist ein Modul, das in der Lage ist, die Ausgangsgeschwindigkeit von Power auf mehrere Module zu ändern. Um es zu verwenden, müssen Sie es an eine Stromquelle anschließen und dann die anderen Kabel an die Module anschließen , zwischen denen Sie die Stromversorgung aufteilen möchten. Sie können das Bedienfeld verwenden, um die durch die Kabel fließende Leistung zu erhöhen oder zu verringern. Mehrere Splitter können für ein größeres Stromsteuernetz miteinander verdrahtet werden. Beachten Sie, dass Strom nur entweder in den Splitter oder aus dem Splitter fließen kann. Der Strom kann nicht über dasselbe Stromkabel in beide Richtungen gehen. Der Splitter hat keine Befestigungspunkte und kann nicht auf einer Plattform gelagert werden.");
            CraftingTest.mgroßePlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformB.SetDescription("Mittelgroße Plattform B ist eines der Hauptbaumodule.");
            CraftingTest.schredder.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.kleinerDrucker);
            CraftingTest.schredder.SetDescription("Der mittlere Schredder kann alles schreddern, was Tier-1 ist, mit Ausnahme von Dynamit, das explodiert und den Schredder zerstört. Es kann auch verwendet werden, um Trümmer zu zerkleinern, die auf allen Planeten zu finden sind.");
            CraftingTest.traktor.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.traktor.SetDescription("Der Traktor ist eine Art Rover. Mit seinen günstigen Forschungs- und Herstellungskosten und seiner frühen Nützlichkeit wird es meistens als erstes Fahrzeug des Spielers verwendet. Es hat einen T2 - BefestigungsSlot an der Vorderseite des Fahrzeugs und einen Stromanschluss auf der Rückseite. Der Traktor kann bis zu drei andere Rover ziehen, indem er sie am hinteren Stromanschluss anbringt. Wie andere Rover hat der Traktor keine Kraft, wenn er erstellt wird. Der interne Akku kann aufgeladen werden, indem Sie ihn an eine Basis anschließen oder eine Stromquelle an einen seiner Befestigungssteckplätze anschließen.");
            CraftingTest.haenger.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.haenger.SetDescription("Der Hänger ist ein kleines Fahrzeug mit einem einzigen Tier-2- Slot.");
            CraftingTest.bohrstaerke1.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke1.SetDescription("Bohrstärke 1 ist ein Anbaugerät für den Traktor, mittleren Rover, großen Rover oder Kran . Nach dem Anbringen kann es zum Ausbohren von Erde verwendet werden und kann härtere Böden durchbrechen, als es das Terrain Tool zulässt.");
            CraftingTest.bohrstaerke2.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid, MaterialTest.titanlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke2.SetDescription("Bohrstärke 2 ist ein Anbaugerät für den Traktor, mittleren Rover, großen Rover oder Kran . Nach dem Anbringen kann es zum Ausbohren von Erde verwendet werden und kann härtere Böden durchbrechen, als es die Bohrstärke 1 zulässt.");
            CraftingTest.bohrstaerke3.SetHerstellung(new List<Ressource>() { MaterialTest.diamant, MaterialTest.titanlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke3.SetDescription("Bohrstärke 3 ist ein Anbaugerät für den Traktor, mittleren Rover, großen Rover oder Kran . Nach dem Anbringen kann es zum Ausbohren von Erde verwendet werden und kann härtere Böden durchbrechen, als es die Bohrstärke 2 zulässt.");
            CraftingTest.feststoffSchubduese.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.ammonium }, CraftingTest.kleinerDrucker);
            CraftingTest.feststoffSchubduese.SetDescription("Die Feststoff-Schubdüse ist ein Tier-2 -Fahrzeuganbaugerät. Sie sind entbehrlich, mit genügend Ladungen, die maximal vier Starts und Planetensprünge ermöglichen, und erfordern mehr gepackte Triebwerke im Rucksack, falls der Spieler längere Reisen unternehmen möchte.");
            CraftingTest.hydrazinSchubduese.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.exo_chip }, CraftingTest.kleinerDrucker);
            CraftingTest.hydrazinSchubduese.SetDescription("Die Hydrazin-Schubdüse ist ein wiederverwendbarer Gegenstand in Astroneer. Hydrazin-Triebwerke werden von Hydrazin angetrieben und werden verwendet, um Fahrzeuge anzutreiben, hauptsächlich die Shuttles, um zu anderen Planeten zu reisen.");
            CraftingTest.winde.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.gummi }, CraftingTest.kleinerDrucker);
            CraftingTest.winde.SetDescription("Die Winde kann auf jedem Rover platziert werden. Ausßerdem kann sie an Shuttles befestigt werden, aber Vorsicht ist geboten, da die befestigten Gegenstände herunterfallen, wenn Sie in den Weltraum fliegen.Die Winde kann jeden Gegenstand im Spiel ziehen.");


            CraftingTest.gLager.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.mittlerDrucker);
            CraftingTest.gLager.SetDescription("Das Große Lager kann bis zu vier Tier-2 Gegenstände lagern.");
            CraftingTest.gPlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformA.SetDescription("Große Plattform A ist eines der Hauptbaumodule.");
            CraftingTest.gPlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformB.SetDescription("Große Plattform B ist eines der Hauptbaumodule.");
            CraftingTest.gPlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.keramik, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformC.SetDescription("Große Plattform C ist eines der Hauptbaumodule.");
            CraftingTest.forschungsKammer.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.forschungsKammer.SetDescription("Die Forschungskammer wird verwendet, um Bytes aus Forschungsgegenständen , Forschungsproben und Ressourcen zu extrahieren . Um die Forschungskammer zu betreiben, muss der Spieler den Gegenstand, den er erforschen möchte, in den Slot der Kammer legen und die Kammer mit dem Bedienfeld aktivieren. \nDie Zeit, die zum Erforschen des Gegenstands benötigt wird, hängt vom Gegenstand selbst ab, wobei Gegenstände mit höherem Byte-Wert normalerweise mehr Zeit in Anspruch nehmen als Gegenstände mit niedrigerem Wert. 2 Leistungseinheiten sind erforderlich, um die Kammer mit voller Geschwindigkeit zu betreiben, und funktionieren immer noch bei geringeren Leistungsmengen, aber mit reduzierter Geschwindigkeit, was zu weniger Bytes pro Minute und einer längeren Verarbeitungszeit führt.");
            CraftingTest.schmelzofen.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.schmelzofen.SetDescription("Der Schmelzofen veredelt Rohstoffe, wenn der Spieler den Ofen einschaltet. Nuggets werden aus dem angeschlossenen Lager gezogen, wenn einer der vier Slots auf der Unterseite verfügbar wird, bis alle schmelzbaren Ressourcen veredelt sind. Der Ofen stoppt, wenn es keinen freien Lagerplatz gibt, um den geschmolzenen Ausgangsartikel zu platzieren, und fährt fort, wenn ein Platz frei wird.");
            CraftingTest.erdzentrifuge.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.erdzentrifuge.SetDescription("Die Erdzentrifuge wird verwendet, um Ressourcen aus der Erde zu extrahiert, welche über das Terrain Tool gesammelt wurde.");
            CraftingTest.chemielabor.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram, MaterialTest.glas, MaterialTest.keramik }, CraftingTest.mittlerDrucker);
            CraftingTest.chemielabor.SetDescription("Das Chemielabor wird verwendet, um Ressourcen zu zusammengesetzten Ressourcen zu kombinieren.");
            CraftingTest.atmosphaerenkondensator.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.atmosphaerenkondensator.SetDescription("Der Atmosphärenkondensator kondensiert die Atmosphäre um sich herum zu Gasen, die für die Herstellung im Chemielabor verwendet werden. \nAtmosphärische Kondensatoren laufen, bis alle verfügbaren Speicherplätze auf einer Plattform gefüllt sind, solange der Kondensator auf Wiederholung eingestellt ist.");
            CraftingTest.handelsPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.wolfram, MaterialTest.eisen }, CraftingTest.mittlerDrucker);
            CraftingTest.handelsPlattform.SetDescription("Die Handelsplattform ermöglicht, Schrott und Astronium gegen Ressourcen und verschiedene andere Gegenstände einzutauschen. \nEin Trade dauert 45 Sekunden.");
            CraftingTest.gSchredder.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.eisen, MaterialTest.wolframkarbid }, CraftingTest.mittlerDrucker);
            CraftingTest.gSchredder.SetDescription("Der Große Schredder kann alles Tier-2 oder darunter schreddern, mit Ausnahme von Dynamit , das beim Schreddern explodiert und den Schredder zerstört. Es kann auch verwendet werden, um Trümmer zu zerkleinern, die auf allen Planeten zu finden sind.");
            CraftingTest.buggy.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.buggy.SetDescription("Der Buggy ist viel kleiner als Rover und eignet sich nicht ideal zum Tragen von Gegenständen oder zum Sammeln von Ressourcen. Aufgrund seiner geringeren Größe und Federung ist er jedoch schneller als Rover. Es ist auch in der Lage, steile, in der Nähe von senkrechten Wänden zu klettern, obwohl die Geschwindigkeit Schwierigkeiten bei der Kontrolle des Buggys bereiten kann, was dazu führt, dass Spieler die Kontrolle verlieren und von Klippen oder in Löcher fahren.");
            CraftingTest.mGrover.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.mGrover.SetDescription("folgt noch :)");
            CraftingTest.gRoverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.gRoverSitz.SetDescription("Großer Rover Sitz bietet Platz für bis zu drei Spieler.");
            CraftingTest.kran.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.silikon, MaterialTest.stahl }, CraftingTest.mittlerDrucker);
            CraftingTest.kran.SetDescription("Der Kran ist ein großes Modul, das auf einem Fahrzeug und bestimmten Plattformen platziert werden kann . In Verbindung mit einem Bohrkopf kann es verwendet werden, um Ressourcen schneller abzubauen als das Terrain Tool und sogar durch harte Sedimente zu graben, die das Terrain Tool nicht durchdringen kann. Der Kran hat einen Tier-2- BefestigungsSlot am Kopf für den Bohrkopf (obwohl andere Dinge befestigt werden können) und 2 Tier-1-Slots an der Seite.");
            CraftingTest.exoPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.exoPlattform.SetDescription("Die EXO-Anforderungsplattform wird verwendet, um Objekte als Gegenleistung für Wiederherstellungspunkte oder Fortschritte in Richtung des Ziels des aktuellen zeitlich begrenzten Ereignisses zu senden. Das Bedienfeld zeigt dem Spieler an, wie viele Punkte er verdient oder wie viele Gegenstände er verschickt hat, sowie freizuschaltende Belohnungen und den Wert der aktuellen Lieferung. Das Versenden einer Sendung kostet 500 Bytes . Auf der Rakete dürfen nur Gegenstände platziert werden, die sich auf das aktuell laufende LTE beziehen. \nUnten links auf der Plattform befindet sich eine Anzeige, die sich je nach der Anzahl der Punkte, die der Spieler verdient hat, füllt. Spieler können weiterhin Objekte versenden, nachdem sie alle Belohnungen freigeschaltet haben, um Pflegepakete mit verschiedenen Gegenständen zu erhalten.");
            CraftingTest.gRessourcenkanister.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung, MaterialTest.titan, MaterialTest.glas }, CraftingTest.mittlerDrucker);
            CraftingTest.gRessourcenkanister.SetDescription("Der große Ressourcenkanister kann bis zu 400 Nuggets eines einzigen Ressourcentyps aufnehmen. Sie können nicht verwendet werden, um atmosphärische Ressourcen zu halten .");
            CraftingTest.gSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gSolarmodul.SetDescription("Das große Solarpanel hat keine Stromausgangskabel und muss daher auf großen oder extragroßen Plattformen platziert werden, um Strom zu erzeugen. Es erzeugt nur Strom, wenn es Sonnenlicht ausgesetzt ist.");
            CraftingTest.gWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gWindturbine.SetDescription("Die Große Windturbine dient in erster Linie der Stromerzeugung. Die Leistung variiert nicht in Abhängigkeit von der Windgeschwindigkeit.");
            CraftingTest.gTPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.mittlerDrucker);
            CraftingTest.gTPlattform.SetDescription("Große T-Plattform ist eines der Hauptbaumodule.");
            CraftingTest.gGPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.gGPlattform.SetDescription("Große Gewölbte Plattform ist eines der Hauptbaumodule.");
            CraftingTest.gSiloA.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.mittlerDrucker);
            CraftingTest.gSiloA.SetDescription("Das Große Silo A hat  acht Tier-2- Anbauplätzen.");
            CraftingTest.gSiloB.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.stahl, MaterialTest.stahl }, CraftingTest.mittlerDrucker);
            CraftingTest.gSiloB.SetDescription("Das Große Silo B hat zwölf Tier-2- Anbauplätzen, was der dreifachen Kapazität des großen Lagers entspricht. Es kann 24 Tier-1-Elemente enthalten.");
            CraftingTest.gEPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gEPlattform.SetDescription("Große Erweiterte Plattform ist für die Verwendung mit Auto Arms gedacht , da sie die ideale Länge für den Transport von Gegenständen haben.");
            CraftingTest.gASpeicher.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.aluminium, MaterialTest.zink }, CraftingTest.mittlerDrucker);
            CraftingTest.gASpeicher.SetDescription("Der große aktive Speicher kann mit Automatisierungselementen verwendet werden, um sich bewegende und animierte Schilder zu erstellen, oder als allgemeiner Speicher verwendet werden .");
            CraftingTest.vtol.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.wolframkarbid, MaterialTest.exo_chip }, CraftingTest.mittlerDrucker);
            CraftingTest.vtol.SetDescription("Das VTOL, oder Vertical Take-Off and Landing, ist ein leichtes Flugzeug in Astroneer, das es dem Spieler ermöglicht, lange Strecken über die Planeten zu reisen, indem es über die Oberfläche des Planeten fliegt.");
            CraftingTest.gNebelhorn.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.gummi, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.gNebelhorn.SetDescription("Das große Nebelhorn kann verwendet werden, um andere Spieler anzuhupen, während sie auf Rovers herumfahren . Wenn Sie es auf einen der vorderen Steckplätze des Rovers legen und die entsprechenden Kontexttasten drücken, wird die Hupe aktiviert.");
            CraftingTest.freizeitKugel.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.freizeitKugel.SetDescription("Die Erholungskugel ist ein Erholungsgegenstand in Astroneer .");


            CraftingTest.egPlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformA.SetDescription("Die Extragroße Plattform A wird am besten verwendet, um Tier-4- Module zu halten.");
            CraftingTest.egPlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformB.SetDescription("Die Extra Large Platform B hat ein einzigartiges zweischichtiges Design für einfachen Zugriff auf gespeicherte Module und Ressourcen .");
            CraftingTest.egPlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.harz, MaterialTest.harz }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformC.SetDescription("Zwei Tier-3- und ein Tier-4- Befestigungssteckplätze. Nützlich in großen Fabriken, da es eine Produktionskette von vier großen Modulen aufnehmen kann.");
            CraftingTest.egLager.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egLager.SetDescription("Das Extragroße Lager kann bis zu 31 Tier-1-Gegenstände lagern.");
            CraftingTest.egSchredder.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.wolframkarbid, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.egSchredder.SetDescription("Der Extra Large Shredder ist in der Lage, alles Tier-3 oder darunter zu schreddern, sowie Trümmer, die Schrott produzieren. Tier-4- Gegenstände können nicht geschreddert werden, können aber mit Dynamite und anschließendem Schreddern der Überreste zerstört werden.");
            CraftingTest.gRover.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.aluminiumlegierung, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.gRover.SetDescription("Große Rover sind die vierte Stufe der Landfahrzeuge in Astroneer. Um einen großen Rover zu bauen, muss der Bauplan durch Forschung freigeschaltet werden. Obwohl sie über eine eingebaute Batterie verfügen, benötigen Große Rover Strom, um angetrieben zu werden, und fungieren als unbegrenzte Sauerstoffversorgung, wenn der Spieler darin sitzt oder mit ihnen verbunden ist.");
            CraftingTest.kShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.grosserDrucker);
            CraftingTest.kShuttle.SetDescription("Es wird verwendet, um schnell durch eine Welt oder zu anderen Planeten zu reisen .");
            CraftingTest.mShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.aluminiumlegierung, MaterialTest.aluminiumlegierung }, CraftingTest.grosserDrucker);
            CraftingTest.mShuttle.SetDescription("Es wird verwendet, um schnell durch eine Welt oder zu anderen Planeten zu reisen .");
            CraftingTest.gShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.titanlegierung, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.gShuttle.SetDescription("Es ist das größte Shuttle und kann mehr Gegenstände aufnehmen, einschließlich großer Module, als andere Shuttle. Es kann verwendet werden, um schnell durch eine Welt oder einfacher zu anderen Planeten zu reisen.");
            CraftingTest.unterkunft.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.silikon, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.grosserDrucker);
            CraftingTest.unterkunft.SetDescription("Die Unterkunkt ist ein Basisbauelement in Astroneer und zentraler Punkt permanenter Basen. Wenn Sie ein neues Spiel beginnen und das Landungsschiff verlassen, baut sich ein Unterstand zusammen mit einem nahe gelegenen stationären Landeplatz auf und verwandelt das umgebende Gelände in eine schwarzgraue Erde, die nur mit einem Bohrer oder Bohrer-Mod entfernt werden kann .\n Die Unterkunft kann nicht bewegt oder mit einem Packager verpackt werden, nachdem er platziert wurde.\nSie wird mit einem dauerhaft angebrachten RTG geliefert, das nicht entfernt werden kann. Es erzeugt jedoch ein Viertel der Leistung im Vergleich zum herstellbaren RTG.\nHergestellte Unterstände enthalten einen Sauerstoffmacher.");
            CraftingTest.solarstromanlage.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.glas, MaterialTest.kupfer }, CraftingTest.grosserDrucker);
            CraftingTest.solarstromanlage.SetDescription("Das Solar Array ist ein Element zur Stromerzeugung in Astroneer . Es benötigt Sonnenlicht, um Strom zu liefern , und benötigt keine Plattform, um zu funktionieren.");
            CraftingTest.autoExtraktor.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.wolframkarbid, MaterialTest.stahl, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.autoExtraktor.SetDescription("Der Auto Extractor ist ein Automatisierungselement in Astroneer . Es ermöglicht den Spielern, langsam Ressourcen aus Lagerstätten zu extrahieren, ohne Gelände zu entfernen, im Austausch dafür, dass der Spieler die 15-fache Menge an Nuggets erhält, die er normalerweise von Hand ablegen würde.");
            CraftingTest.windturbineXl.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.keramik, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.windturbineXl.SetDescription("Die XL-Windkraftanlage dient in erster Linie der Stromerzeugung. Die Leistung variiert nicht in Abhängigkeit von der Windgeschwindigkeit.");
            CraftingTest.mSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.mSensorbogen.SetDescription("Der mittlere Sensorbogen hat 6 ReaktionsSlot , die aktiviert werden, wenn der Spieler oder einige Objekte den Ring passieren, Lichter einschalten oder Feuerwerke in den Slots zünden.");
            CraftingTest.xlSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbogen.SetDescription("Der XL Sensor Arch kann mit der Freizeitkugel als eine andere Art von Tor verwendet werden, ähnlich wie der XL-Sensorbaldachin. Es kann auch als Kontrollpunkt für ein Rennen verwendet werden, indem die ReaktionsSlot verwendet werden, um Lichter oder Feuerwerk zu aktivieren.");
            CraftingTest.xlSensorbaladachin.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbaladachin.SetDescription("Der XL-Sensorbaldachin ist ein Tornetz, das in Kombination mit der Freizeitkugel verwendet werden soll, um jede Art von Sport in einem Multiplayer-Spiel zu spielen. Wenn etwas die blaue Sensorbarriere passiert, wird es jeden Gegenstand auf dem ReaktionsSlot auf der Rückseite aktivieren oder mit den Zielstiften verbinden.");
            CraftingTest.gSensorring.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorring.SetDescription("Der große Sensorring hat 6 ReaktionsSlot , die aktiviert werden, wenn der Spieler oder einige Objekte den Ring passieren, Lichter einschalten oder Feuerwerke zünden.");
            CraftingTest.gSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringA.SetDescription("Wenn der Ring von einer Kugel , einer Erholungskugel oder einem Spieler durchquert wird, wird alles innerhalb der Tier-1-Slots um den Ring herum aktiviert.");
            CraftingTest.gSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringB.SetDescription("Wenn der Ring von einer Kugel , einer Erholungskugel oder einem Spieler durchquert wird, wird alles innerhalb der ReaktionsSlot um den Ring herum aktiviert.");
            CraftingTest.xlSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringA.SetDescription("XL-Sensorring A kann verwendet werden, um zu erkennen, wenn eine Kugel, eine Spielkugel , ein Fahrzeug oder ein Spieler den bläulichen Sensorbereich passiert. Wenn einer erkannt wird, werden die Reaktionsslots aktiviert.\nDieser Sensor kann insbesondere verwendet werden, um zu erkennen, wenn ein Shuttle auf einem Landeplatz oder einer Landezone landet.");
            CraftingTest.xlSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringB.SetDescription("XL-Sensorring B kann verwendet werden, um zu erkennen, wenn eine Kugel, eine Spielkugel , ein Fahrzeug oder ein Spieler den bläulichen Sensorbereich passiert. Wenn einer erkannt wird, werden die Reaktionsslots aktiviert.\nDieser Sensor kann insbesondere verwendet werden, um zu erkennen, wenn ein Shuttle auf einem Landeplatz oder einer Landezone landet.");
            CraftingTest.egGPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egGPlattform.SetDescription("folgt noch :)");
            CraftingTest.xlEPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.harz }, CraftingTest.grosserDrucker);
            CraftingTest.xlEPlattform.SetDescription("Die XL Extended Platform ist für die Verwendung mit Auto Arms gedacht , da sie die ideale Länge für den Transport von Gegenständen haben. Die XL Extended Platform bietet Platz für zwei Auto Arms an den Seiten und ein Lager- oder Crafting-Modul in der Mitte.");
            CraftingTest.fPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.fPlattform.SetDescription("Die Figurenplattform kann als allgemeine Plattform zum Lagern von Gegenständen verwendet werden, jedoch sind alle 64 Slots kleine Slots und können keine größeren Lager aufnehmen. Eine gute Verwendung für die Plattform kann sein, Geländeanalysatoren oder kleine Batterien zu halten.");
            CraftingTest.landeplattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.aluminium }, CraftingTest.grosserDrucker);
            CraftingTest.landeplattform.SetDescription("Gedruckte Plattformen können überall platziert werden, wo der Boden gerade ist. Ist der Punkt in der Mitte der Plattform orange, ist der Boden nicht eben genug. Ist er grün, kann man die Plattform erweitern. Ab diesem Zeitpunkt entsteht ein neuer Landepunkt, der vom Orbit aus ausgewählt werden kann.");

        }

        private void CreateTerrarium()
        {
            GalastropodenTest.Sylvie.AddImage(Resource.Drawable.Galas_Sylva_Shell);
            GalastropodenTest.Sylvie.AddImage(Resource.Drawable.Icon_Sylvie);
            GalastropodenTest.Usagi.AddImage(Resource.Drawable.Galas_Desolo_Shell);
            GalastropodenTest.Usagi.AddImage(Resource.Drawable.Icon_Usagi);
            GalastropodenTest.Stilgar.AddImage(Resource.Drawable.Galas_Calidor_Shell);
            GalastropodenTest.Stilgar.AddImage(Resource.Drawable.Icon_Stilgar);
            GalastropodenTest.Princess.AddImage(Resource.Drawable.Galas_Vesania_Shell);
            GalastropodenTest.Princess.AddImage(Resource.Drawable.Icon_Princess);
            GalastropodenTest.Rogal.AddImage(Resource.Drawable.Galas_Novus_Shell);
            GalastropodenTest.Rogal.AddImage(Resource.Drawable.Icon_Rogal);
            GalastropodenTest.Bestefar.AddImage(Resource.Drawable.Galas_Glacio_Shell);
            GalastropodenTest.Bestefar.AddImage(Resource.Drawable.Icon_Bestefar);
            GalastropodenTest.Enoki.AddImage(Resource.Drawable.Galas_Atrox_Shell);
            GalastropodenTest.Enoki.AddImage(Resource.Drawable.Icon_Enoki);

            GalastropodenTest.Sylvie.SetTerrarium(new GTerrarium(MaterialTest.erde,MaterialTest.zink,PflanzenTest.Huepfranke));
            GalastropodenTest.Sylvie.SetBuff("Leuchtet einen Bereich stark aus");
            GalastropodenTest.Sylvie.SetDescription("Ein mutiger und neugieriger Gefährte, der ein helles Licht von seinem Körper ausstrahlt, wenn er gut ernährt ist. Sylvie erleuchtet die dunkelsten Orte, egal wo sie sich aufhalten.");
            GalastropodenTest.Usagi.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.wolfram, PflanzenTest.Dolchwurzel));
            GalastropodenTest.Usagi.SetBuff("Verfolgt auf dem aktuellen Planeten die nächstgelegenen wertvollen Kuriositäten auf dem Kompass");
            GalastropodenTest.Usagi.SetDescription("Eine abenteuerlustige und verspielte Gefährtin, die Expertin darin ist, glänzende Dinge zu finden, wenn sie gut genährt ist. Usagi hilft Ihnen gerne dabei, auf jedem Planeten, auf dem Sie sich befinden, Orte zu finden, an denen Sie nachsehen können, egal wo sie wohnt.");
            GalastropodenTest.Stilgar.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.kupfer, PflanzenTest.Keuchkraut));
            GalastropodenTest.Stilgar.SetBuff("Produziert passiv Sauerstoff, der Tanks und Haltenetze füllt");
            GalastropodenTest.Stilgar.SetDescription("Ein widerstandsfähiger Begleiter, der dich bei jedem Abenteuer gut durchatmen lässt. Stilgar ist eine würzige Ergänzung für jeden Rucksack: THE AIR MUST FLOW!");
            GalastropodenTest.Princess.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.lithium, PflanzenTest.Peitschenblatt));
            GalastropodenTest.Princess.SetBuff("Verhindert beim Tragen im Rucksack alle Formen von Schäden außer Ersticken");
            GalastropodenTest.Princess.SetDescription("folgt noch :)");
            GalastropodenTest.Rogal.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.eisen, PflanzenTest.Distelgerte));
            GalastropodenTest.Rogal.SetBuff("Erzeugt eine beträchtliche Menge an Strom");
            GalastropodenTest.Sylvie.SetDescription("folgt noch :)");
            GalastropodenTest.Bestefar.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.argon, PflanzenTest.Knallkoralle));
            GalastropodenTest.Bestefar.SetBuff("Verbessert, während es am Terrain Tool befestigt ist, seine Breite, Verstärkung und Bohrfähigkeit");
            GalastropodenTest.Sylvie.SetDescription("folgt noch :)");
            GalastropodenTest.Enoki.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.helium, PflanzenTest.Stachellilie));
            GalastropodenTest.Enoki.SetBuff("Erhöht beim Tragen im Rucksack die Sprunghöhe, die Sprintgeschwindigkeit und verringert die Bewegungseinbußen beim Tragen schwerer Gegenstände");
            GalastropodenTest.Sylvie.SetDescription("folgt noch :)");
        }


        private void CreatePlaneten()
        {

            #region Sylva
            Energie SonneSylva = new Energie("Sonneneinstrahlung", Strength.mittel);
            Energie WindSylva = new Energie("Windgeschwindigkeit", Strength.mittel);

            PlanetenTest.Sylva.SetEnergie(new List<Energie>() { SonneSylva, WindSylva });

            Ressourcen Res1Sylva = new Ressourcen(MaterialTest.zinkblende, ResArt.Primär, "Oft");
            Ressourcen Res2Sylva = new Ressourcen(MaterialTest.malachit, ResArt.Sekundär, "Oft");
            Ressourcen Res3Sylva = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res4Sylva = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res5Sylva = new Ressourcen(MaterialTest.organisch, ResArt.Universale, "Oft");
            Ressourcen Res6Sylva = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res7Sylva = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res8Sylva = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res9Sylva = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res10Sylva = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res11Sylva = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res12Sylva = new Ressourcen(MaterialTest.wasserstoff, ResArt.Gase, "75ppu");
            Ressourcen Res13Sylva = new Ressourcen(MaterialTest.stickstoff, ResArt.Gase, "100ppu");

            PlanetenTest.Sylva.SetGalastropode(GalastropodenTest.Sylvie);

            PlanetenTest.Sylva.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Huepfranke, PflanzenTest.Dolchwurzel, PflanzenTest.Zischrebe });

            PlanetenTest.Sylva.SetRessourcen(new List<Ressourcen>() { Res1Sylva, Res2Sylva, Res3Sylva, Res4Sylva, Res5Sylva, Res6Sylva, Res7Sylva, Res8Sylva, Res9Sylva, Res10Sylva, Res11Sylva, Res12Sylva, Res13Sylva });
            #endregion

            #region Desolo
            Energie SonneDesolo = new Energie("Sonneneinstrahlung", Strength.hoch);
            Energie WindDesolo = new Energie("Windgeschwindigkeit", Strength.niedrig);

            PlanetenTest.Desolo.SetEnergie(new List<Energie>() { SonneDesolo, WindDesolo });

            Ressourcen Res1Desolo = new Ressourcen(MaterialTest.wolframit, ResArt.Primär, "Oft");
            Ressourcen Res2Desolo = new Ressourcen(MaterialTest.zinkblende, ResArt.Sekundär, "Oft");
            Ressourcen Res3Desolo = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res4Desolo = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res5Desolo = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res6Desolo = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res7Desolo = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res8Desolo = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res9Desolo = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res10Desolo = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            PlanetenTest.Desolo.SetGalastropode(GalastropodenTest.Usagi);

            PlanetenTest.Desolo.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Dolchwurzel, PflanzenTest.Zischrebe });

            PlanetenTest.Desolo.SetRessourcen(new List<Ressourcen>() { Res1Desolo, Res2Desolo, Res3Desolo, Res4Desolo, Res5Desolo, Res6Desolo, Res7Desolo, Res8Desolo, Res9Desolo, Res10Desolo });
            #endregion

            #region Vesania
            Energie SonneVesania = new Energie("Sonneneinstrahlung", Strength.niedrig);
            Energie WindVesania = new Energie("Windgeschwindigkeit", Strength.hoch);

            PlanetenTest.Vesania.SetEnergie(new List<Energie>() { SonneVesania, WindVesania });

            Ressourcen Res1Vesania = new Ressourcen(MaterialTest.lithium, ResArt.Primär, "Oft");
            Ressourcen Res2Vesania = new Ressourcen(MaterialTest.titanit, ResArt.Sekundär, "Oft");
            Ressourcen Res3Vesania = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res4Vesania = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res5Vesania = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res6Vesania = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res7Vesania = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res8Vesania = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res9Vesania = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res13Vesania = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res10Vesania = new Ressourcen(MaterialTest.wasserstoff, ResArt.Gase, "100ppu");
            Ressourcen Res11Vesania = new Ressourcen(MaterialTest.argon, ResArt.Gase, "50ppu");
            Ressourcen Res12Vesania = new Ressourcen(MaterialTest.stickstoff, ResArt.Gase, "75ppu");

            PlanetenTest.Vesania.SetGalastropode(GalastropodenTest.Princess);

            PlanetenTest.Vesania.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Peitschenblatt, PflanzenTest.Distelgerte, PflanzenTest.Zischrebe, PflanzenTest.Katapflanze });

            PlanetenTest.Vesania.SetRessourcen(new List<Ressourcen>() { Res1Vesania, Res2Vesania, Res3Vesania, Res4Vesania, Res5Vesania, Res6Vesania, Res7Vesania, Res8Vesania, Res9Vesania, Res13Vesania, Res10Vesania, Res11Vesania, Res12Vesania });
            #endregion

            #region Novus
            Energie SonneNovus = new Energie("Sonneneinstrahlung", Strength.hoch);
            Energie WindNovus = new Energie("Windgeschwindigkeit", Strength.hoch);

            PlanetenTest.Novus.SetEnergie(new List<Energie>() { SonneNovus, WindNovus });

            Ressourcen Res1Novus = new Ressourcen(MaterialTest.hämatit, ResArt.Primär, "Oft");
            Ressourcen Res2Novus = new Ressourcen(MaterialTest.lithium, ResArt.Sekundär, "Oft");
            Ressourcen Res3Novus = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res4Novus = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res5Novus = new Ressourcen(MaterialTest.organisch, ResArt.Universale, "Oft");
            Ressourcen Res6Novus = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res7Novus = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res8Novus = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res9Novus = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res10Novus = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res11Novus = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res12Novus = new Ressourcen(MaterialTest.wasserstoff, ResArt.Gase, "25ppu");
            Ressourcen Res13Novus = new Ressourcen(MaterialTest.methan, ResArt.Gase, "75ppu");

            PlanetenTest.Novus.SetGalastropode(GalastropodenTest.Rogal);

            PlanetenTest.Novus.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Peitschenblatt, PflanzenTest.Distelgerte, PflanzenTest.Zischrebe, PflanzenTest.Katapflanze });

            PlanetenTest.Novus.SetRessourcen(new List<Ressourcen>() { Res1Novus, Res2Novus, Res3Novus, Res4Novus, Res5Novus, Res6Novus, Res7Novus, Res8Novus, Res9Novus, Res10Novus, Res11Novus, Res12Novus, Res13Novus });
            #endregion

            #region Calidor
            Energie SonneCalidor = new Energie("Sonneneinstrahlung", Strength.overkill);
            Energie WindCalidor = new Energie("Windgeschwindigkeit", Strength.niedrig);

            PlanetenTest.Calidor.SetEnergie(new List<Energie>() { SonneCalidor, WindCalidor });

            Ressourcen Res1Calidor = new Ressourcen(MaterialTest.malachit, ResArt.Primär, "Oft");
            Ressourcen Res2Calidor = new Ressourcen(MaterialTest.wolframit, ResArt.Sekundär, "Oft");
            Ressourcen Res3Calidor = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res4Calidor = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res5Calidor = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res6Calidor = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res7Calidor = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res8Calidor = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res9Calidor = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res12Calidor = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res10Calidor = new Ressourcen(MaterialTest.wasserstoff, ResArt.Gase, "50ppu");
            Ressourcen Res11Calidor = new Ressourcen(MaterialTest.schwefel, ResArt.Gase, "100ppu");

            PlanetenTest.Calidor.SetGalastropode(GalastropodenTest.Stilgar);

            PlanetenTest.Calidor.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Keuchkraut, PflanzenTest.Stachellilie, PflanzenTest.Zischrebe, PflanzenTest.Attaktus });

            PlanetenTest.Calidor.SetRessourcen(new List<Ressourcen>() { Res1Calidor, Res2Calidor, Res3Calidor, Res4Calidor, Res5Calidor, Res6Calidor, Res7Calidor, Res8Calidor, Res9Calidor, Res12Calidor, Res10Calidor, Res11Calidor });
            #endregion

            #region Glacio
            Energie SonneGlacio = new Energie("Sonneneinstrahlung", Strength.niedrig);
            Energie WindGlacio = new Energie("Windgeschwindigkeit", Strength.overkill);

            PlanetenTest.Glacio.SetEnergie(new List<Energie>() { SonneGlacio, WindGlacio });

            Ressourcen Res1Glacio = new Ressourcen(MaterialTest.titanit, ResArt.Primär, "Oft");
            Ressourcen Res2Glacio = new Ressourcen(MaterialTest.hämatit, ResArt.Sekundär, "Oft");
            Ressourcen Res3Glacio = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res4Glacio = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res5Glacio = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res6Glacio = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res7Glacio = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res8Glacio = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res9Glacio = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res11Glacio = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res10Glacio = new Ressourcen(MaterialTest.argon, ResArt.Gase, "100ppu");

            PlanetenTest.Glacio.SetGalastropode(GalastropodenTest.Bestefar);

            PlanetenTest.Glacio.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Zischrebe, PflanzenTest.Knalloon, PflanzenTest.Katapflanze });

            PlanetenTest.Glacio.SetRessourcen(new List<Ressourcen>() { Res1Glacio, Res2Glacio, Res3Glacio, Res4Glacio, Res5Glacio, Res6Glacio, Res7Glacio, Res8Glacio, Res9Glacio, Res11Glacio, Res10Glacio });
            #endregion

            #region Atrox
            Energie SonneAtrox = new Energie("Sonneneinstrahlung", Strength.niedrig);
            Energie WindAtrox = new Energie("Windgeschwindigkeit", Strength.niedrig);

            PlanetenTest.Atrox.SetEnergie(new List<Energie>() { SonneAtrox, WindAtrox });

            Ressourcen Res1Atrox = new Ressourcen(MaterialTest.gemisch, ResArt.Universale, "Oft");
            Ressourcen Res2Atrox = new Ressourcen(MaterialTest.organisch, ResArt.Universale, "Oft");
            Ressourcen Res3Atrox = new Ressourcen(MaterialTest.harz, ResArt.Universale, "Oft");
            Ressourcen Res4Atrox = new Ressourcen(MaterialTest.ammonium, ResArt.Universale, "Oft");
            Ressourcen Res5Atrox = new Ressourcen(MaterialTest.lehm, ResArt.Universale, "Oft");
            Ressourcen Res6Atrox = new Ressourcen(MaterialTest.graphit, ResArt.Universale, "Oft");
            Ressourcen Res7Atrox = new Ressourcen(MaterialTest.laterit, ResArt.Universale, "Oft");
            Ressourcen Res8Atrox = new Ressourcen(MaterialTest.quartz, ResArt.Universale, "Oft");
            Ressourcen Res13Atrox = new Ressourcen(MaterialTest.astronium, ResArt.Universale, "Oft");

            Ressourcen Res9Atrox = new Ressourcen(MaterialTest.helium, ResArt.Gase, "25ppu");
            Ressourcen Res10Atrox = new Ressourcen(MaterialTest.methan, ResArt.Gase, "100ppu");
            Ressourcen Res11Atrox = new Ressourcen(MaterialTest.stickstoff, ResArt.Gase, "50ppu");
            Ressourcen Res12Atrox = new Ressourcen(MaterialTest.schwefel, ResArt.Gase, "75ppu");

            PlanetenTest.Atrox.SetGalastropode(GalastropodenTest.Enoki);

            PlanetenTest.Atrox.SetPflanzen(new List<Pflanze>() { PflanzenTest.Knallkoralle, PflanzenTest.Keuchkraut, PflanzenTest.Stachellilie, PflanzenTest.Dolchwurzel, PflanzenTest.Zischrebe, PflanzenTest.Katapflanze, PflanzenTest.Attaktus, PflanzenTest.Spuckblume });

            PlanetenTest.Atrox.SetRessourcen(new List<Ressourcen>() { Res1Atrox, Res2Atrox, Res3Atrox, Res4Atrox, Res5Atrox, Res6Atrox, Res7Atrox, Res8Atrox, Res13Atrox, Res9Atrox, Res10Atrox, Res11Atrox, Res12Atrox });
            #endregion

        }
    }
}