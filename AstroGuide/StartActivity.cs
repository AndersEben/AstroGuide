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

        private void CreateCrafting()
        {
            CraftingTest.rucksack.SetHerstellung(new List<Ressource>() { }, CraftingTest.rucksack);
            CraftingTest.rucksack.SetDescription("folgt noch :)");
            CraftingTest.kleinerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerDrucker.SetDescription("folgt noch :)");
            CraftingTest.mittlerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.mittlerDrucker.SetDescription("folgt noch :)");
            CraftingTest.grosserDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.grosserDrucker.SetDescription("folgt noch :)");


            CraftingTest.verbindungen.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.verbindungen.SetDescription("folgt noch :)");
            CraftingTest.sauerstofffilter.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.sauerstofffilter.SetDescription("folgt noch :)");
            CraftingTest.kleinerKanister.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kleinerKanister.SetDescription("folgt noch :)");
            CraftingTest.leuchtfeuer.SetHerstellung(new List<Ressource>() { MaterialTest.quartz }, CraftingTest.rucksack);
            CraftingTest.leuchtfeuer.SetDescription("folgt noch :)");
            CraftingTest.arbeitslicht.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.arbeitslicht.SetDescription("folgt noch :)");
            CraftingTest.leuchtstab.SetHerstellung(new List<Ressource>() { MaterialTest.organisch }, CraftingTest.rucksack);
            CraftingTest.leuchtstab.SetDescription("folgt noch :)");
            CraftingTest.scheinwerfer.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram }, CraftingTest.rucksack);
            CraftingTest.scheinwerfer.SetDescription("folgt noch :)");
            CraftingTest.verpacker.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.verpacker.SetDescription("folgt noch :)");
            CraftingTest.kleinersauerstofftank.SetHerstellung(new List<Ressource>() { MaterialTest.glas }, CraftingTest.rucksack);
            CraftingTest.kleinersauerstofftank.SetDescription("folgt noch :)");
            CraftingTest.tragbarerSauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung }, CraftingTest.rucksack);
            CraftingTest.tragbarerSauerstoffmacher.SetDescription("folgt noch :)");
            CraftingTest.kleinerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerGenerator.SetDescription("folgt noch :)");
            CraftingTest.kleinesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.kleinesSolarmodul.SetDescription("folgt noch :)");
            CraftingTest.kleineWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.kleineWindturbine.SetDescription("folgt noch :)");
            CraftingTest.energiezellen.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.energiezellen.SetDescription("folgt noch :)");
            CraftingTest.kleineBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.kleineBatterie.SetDescription("folgt noch :)");
            CraftingTest.verlaengerungen.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.verlaengerungen.SetDescription("folgt noch :)");
            CraftingTest.boostModus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.boostModus.SetDescription("folgt noch :)");
            CraftingTest.breiteAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.breiteAnpassung.SetDescription("folgt noch :)");
            CraftingTest.schmaleAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.schmaleAnpassung.SetDescription("folgt noch :)");
            CraftingTest.hemmer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.hemmer.SetDescription("folgt noch :)");
            CraftingTest.ausrichtungsmodus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.ausrichtungsmodus.SetDescription("folgt noch :)");
            CraftingTest.gelaendeAnalysator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.gelaendeAnalysator.SetDescription("folgt noch :)");
            CraftingTest.bohreranpassungSt1.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt1.SetDescription("folgt noch :)");
            CraftingTest.bohreranpassungSt2.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt2.SetDescription("folgt noch :)");
            CraftingTest.bohreranpassungSt3.SetHerstellung(new List<Ressource>() { MaterialTest.diamant }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt3.SetDescription("folgt noch :)");
            CraftingTest.dynamit.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.dynamit.SetDescription("folgt noch :)");
            CraftingTest.feuerwerk.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.feuerwerk.SetDescription("folgt noch :)");
            CraftingTest.kleineKamera.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.kleineKamera.SetDescription("folgt noch :)");
            CraftingTest.hoverboard.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.hoverboard.SetDescription("folgt noch :)");
            CraftingTest.sondenscanner.SetHerstellung(new List<Ressource>() { MaterialTest.stahl }, CraftingTest.rucksack);
            CraftingTest.sondenscanner.SetDescription("folgt noch :)");
            CraftingTest.kTrompetenhupe.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kTrompetenhupe.SetDescription("folgt noch :)");
            CraftingTest.hydrazinJezpack.SetHerstellung(new List<Ressource>() { MaterialTest.titanlegierung }, CraftingTest.rucksack);
            CraftingTest.hydrazinJezpack.SetDescription("folgt noch :)");
            CraftingTest.holografischeFigur.SetHerstellung(new List<Ressource>() { MaterialTest.kunststoff }, CraftingTest.rucksack);
            CraftingTest.holografischeFigur.SetDescription("folgt noch :)");
            CraftingTest.feststoffSprungbooster.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung }, CraftingTest.rucksack);
            CraftingTest.feststoffSprungbooster.SetDescription("folgt noch :)");
            CraftingTest.einebnungsblock.SetHerstellung(new List<Ressource>() { MaterialTest.erde }, CraftingTest.rucksack);
            CraftingTest.einebnungsblock.SetDescription("folgt noch :)");


            CraftingTest.feldunterkunft.SetHerstellung(new List<Ressource>() { MaterialTest.graphen, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.feldunterkunft.SetDescription("folgt noch :)");
            CraftingTest.autoArm.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.autoArm.SetDescription("folgt noch :)");
            CraftingTest.mgKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgKanister.SetDescription("folgt noch :)");
            CraftingTest.mgFlKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgFlKanister.SetDescription("folgt noch :)");
            CraftingTest.mgGaskanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.mgGaskanister.SetDescription("folgt noch :)");
            CraftingTest.energiesensor.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.energiesensor.SetDescription("folgt noch :)");
            CraftingTest.lagersensor.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.lagersensor.SetDescription("folgt noch :)");
            CraftingTest.batteriesensor.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.batteriesensor.SetDescription("folgt noch :)");
            CraftingTest.tastenwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.tastenwiederholer.SetDescription("folgt noch :)");
            CraftingTest.naeherungsinitiator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.naeherungsinitiator.SetDescription("folgt noch :)");
            CraftingTest.verzoegerungswiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.verzoegerungswiederholer.SetDescription("folgt noch :)");
            CraftingTest.zahlwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.zahlwiederholer.SetDescription("folgt noch :)");
            CraftingTest.netzschalter.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.netzschalter.SetDescription("folgt noch :)");
            CraftingTest.mgroßePlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformC.SetDescription("folgt noch :)");
            CraftingTest.hPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hPlatform.SetDescription("folgt noch :)");
            CraftingTest.mgTPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgTPlatform.SetDescription("folgt noch :)");
            CraftingTest.mgroßesSilo.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.titan }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSilo.SetDescription("folgt noch :)");
            CraftingTest.hohesLager.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hohesLager.SetDescription("folgt noch :)");
            CraftingTest.mgroßeHupe.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeHupe.SetDescription("folgt noch :)");
            CraftingTest.planierer.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.aluminiumlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.planierer.SetDescription("folgt noch :)");
            CraftingTest.mgroßesLager.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesLager.SetDescription("folgt noch :)");
            CraftingTest.mgroßePlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformA.SetDescription("folgt noch :)");
            CraftingTest.roverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.roverSitz.SetDescription("folgt noch :)");
            CraftingTest.sauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.sauerstoffmacher.SetDescription("folgt noch :)");
            CraftingTest.mgroßerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.wolfram }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßerGenerator.SetDescription("folgt noch :)");
            CraftingTest.mgroßesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSolarmodul.SetDescription("folgt noch :)");
            CraftingTest.mgroßeWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeWindturbine.SetDescription("folgt noch :)");
            CraftingTest.mgroßeBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeBatterie.SetDescription("Die Mittelgroße Batterie ist ein Energiespeicher und kann mehr Strom aufnehmen als eine kleine Batterie und sorgt für eine gute Energiereserve mitten im Spiel.");
            CraftingTest.rtg.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.nanocarbonlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.rtg.SetDescription("Der RTG oder Radioisotope Thermoelectric Generator ist ein Stromerzeugungselement, das einen konstanten Stromstrom liefert.");
            CraftingTest.verteiler.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.graphit }, CraftingTest.kleinerDrucker);
            CraftingTest.verteiler.SetDescription("folgt noch :)");
            CraftingTest.mgroßePlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformB.SetDescription("folgt noch :)");
            CraftingTest.schredder.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.kleinerDrucker);
            CraftingTest.schredder.SetDescription("Der mittlere Schredder kann alles schreddern, was Tier-1 ist, mit Ausnahme von Dynamit, das explodiert und den Schredder zerstört. Es kann auch verwendet werden, um Trümmer zu zerkleinern, die auf allen Planeten zu finden sind.");
            CraftingTest.traktor.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.traktor.SetDescription("Der Traktor ist eine Art Rover. Mit seinen günstigen Forschungs- und Herstellungskosten und seiner frühen Nützlichkeit wird es meistens als erstes Fahrzeug des Spielers verwendet. Es hat einen T2 - Befestigungsschlitz an der Vorderseite des Fahrzeugs und einen Stromanschluss auf der Rückseite. Der Traktor kann bis zu drei andere Rover ziehen, indem er sie am hinteren Stromanschluss anbringt. Wie andere Rover hat der Traktor keine Kraft, wenn er erstellt wird. Der interne Akku kann aufgeladen werden, indem Sie ihn an eine Basis anschließen oder eine Stromquelle an einen seiner Befestigungssteckplätze anschließen.");
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
            CraftingTest.gLager.SetDescription("Das Große Lager kann bis zu vier Tier-2_Gegenstände lagern.");
            CraftingTest.gPlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformA.SetDescription("folgt noch :)");
            CraftingTest.gPlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformB.SetDescription("folgt noch :)");
            CraftingTest.gPlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.keramik, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformC.SetDescription("folgt noch :)");
            CraftingTest.forschungsKammer.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.forschungsKammer.SetDescription("Die Forschungskammer wird verwendet, um Bytes aus Forschungsgegenständen , Forschungsproben und Ressourcen zu extrahieren . Um die Forschungskammer zu betreiben, muss der Spieler den Gegenstand, den er erforschen möchte, in den Schlitz der Kammer legen und die Kammer mit dem Bedienfeld aktivieren. \n Die Zeit, die zum Erforschen des Gegenstands benötigt wird, hängt vom Gegenstand selbst ab, wobei Gegenstände mit höherem Byte-Wert normalerweise mehr Zeit in Anspruch nehmen als Gegenstände mit niedrigerem Wert. 2 Leistungseinheiten sind erforderlich, um die Kammer mit voller Geschwindigkeit zu betreiben, und funktionieren immer noch bei geringeren Leistungsmengen, aber mit reduzierter Geschwindigkeit, was zu weniger Bytes pro Minute und einer längeren Verarbeitungszeit führt.");
            CraftingTest.schmelzofen.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.schmelzofen.SetDescription("Der Schmelzofen veredelt Rohstoffe, wenn der Spieler den Ofen einschaltet. Nuggets werden aus dem angeschlossenen Lager gezogen, wenn einer der vier Slots auf der Unterseite verfügbar wird, bis alle schmelzbaren Ressourcen veredelt sind. Der Ofen stoppt, wenn es keinen freien Lagerplatz gibt, um den geschmolzenen Ausgangsartikel zu platzieren, und fährt fort, wenn ein Platz frei wird.");
            CraftingTest.erdzentrifuge.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.erdzentrifuge.SetDescription("Die Erdzentrifuge wird verwendet, um Ressourcen aus der Erde zu extrahiert, welche über das Terrain Tool gesammelt wurde.");
            CraftingTest.chemielabor.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram, MaterialTest.glas, MaterialTest.keramik }, CraftingTest.mittlerDrucker);
            CraftingTest.chemielabor.SetDescription("Das Chemielabor wird verwendet, um Ressourcen zu zusammengesetzten Ressourcen zu kombinieren.");
            CraftingTest.atmosphaerenkondensator.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.atmosphaerenkondensator.SetDescription("Der Atmosphärenkondensator kondensiert die Atmosphäre um sich herum zu Gasen, die für die Herstellung im Chemielabor verwendet werden. \n Atmosphärische Kondensatoren laufen, bis alle verfügbaren Speicherplätze auf einer Plattform gefüllt sind, solange der Kondensator auf Wiederholung eingestellt ist.");
            CraftingTest.handelsPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.wolfram, MaterialTest.eisen }, CraftingTest.mittlerDrucker);
            CraftingTest.handelsPlattform.SetDescription("Die Handelsplattform ermöglicht, Schrott und Astronium gegen Ressourcen und verschiedene andere Gegenstände einzutauschen. \n Ein Trade dauert 45 Sekunden.");
            CraftingTest.gSchredder.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.eisen, MaterialTest.wolframkarbid }, CraftingTest.mittlerDrucker);
            CraftingTest.gSchredder.SetDescription("Der Große Schredder kann alles Tier-2 oder darunter schreddern, mit Ausnahme von Dynamit , das beim Schreddern explodiert und den Schredder zerstört. Es kann auch verwendet werden, um Trümmer zu zerkleinern, die auf allen Planeten zu finden sind.");
            CraftingTest.buggy.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.buggy.SetDescription("Der Buggy ist viel kleiner als Rover und eignet sich nicht ideal zum Tragen von Gegenständen oder zum Sammeln von Ressourcen. Aufgrund seiner geringeren Größe und Federung ist er jedoch schneller als Rover. Es ist auch in der Lage, steile, in der Nähe von senkrechten Wänden zu klettern, obwohl die Geschwindigkeit Schwierigkeiten bei der Kontrolle des Buggys bereiten kann, was dazu führt, dass Spieler die Kontrolle verlieren und von Klippen oder in Löcher fahren.");
            CraftingTest.mGrover.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.mGrover.SetDescription("folgt noch :)");
            CraftingTest.gRoverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.gRoverSitz.SetDescription("Großer Rover Sitz bietet Platz für bis zu drei Spieler.");
            CraftingTest.kran.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.silikon, MaterialTest.stahl }, CraftingTest.mittlerDrucker);
            CraftingTest.kran.SetDescription("Der Kran ist ein großes Modul, das auf einem Fahrzeug und bestimmten Plattformen platziert werden kann . In Verbindung mit einem Bohrkopf kann es verwendet werden, um Ressourcen schneller abzubauen als das Terrain Tool und sogar durch harte Sedimente zu graben, die das Terrain Tool nicht durchdringen kann. Der Kran hat einen Tier-2- Befestigungsschlitz am Kopf für den Bohrkopf (obwohl andere Dinge befestigt werden können) und 2 Tier-1- Schlitze an der Seite.");
            CraftingTest.exoPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.exoPlattform.SetDescription("Die EXO-Anforderungsplattform wird verwendet, um Objekte als Gegenleistung für Wiederherstellungspunkte oder Fortschritte in Richtung des Ziels des aktuellen zeitlich begrenzten Ereignisses zu senden. Das Bedienfeld zeigt dem Spieler an, wie viele Punkte er verdient oder wie viele Gegenstände er verschickt hat, sowie freizuschaltende Belohnungen und den Wert der aktuellen Lieferung. Das Versenden einer Sendung kostet 500 Bytes . Auf der Rakete dürfen nur Gegenstände platziert werden, die sich auf das aktuell laufende LTE beziehen. \n Unten links auf der Plattform befindet sich eine Anzeige, die sich je nach der Anzahl der Punkte, die der Spieler verdient hat, füllt. Spieler können weiterhin Objekte versenden, nachdem sie alle Belohnungen freigeschaltet haben, um Pflegepakete mit verschiedenen Gegenständen zu erhalten.");
            CraftingTest.gRessourcenkanister.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung, MaterialTest.titan, MaterialTest.glas }, CraftingTest.mittlerDrucker);
            CraftingTest.gRessourcenkanister.SetDescription("Der große Ressourcenkanister kann bis zu 400 Nuggets eines einzigen Ressourcentyps aufnehmen. Sie können nicht verwendet werden, um atmosphärische Ressourcen zu halten .");
            CraftingTest.gSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gSolarmodul.SetDescription("Das große Solarpanel hat keine Stromausgangskabel und muss daher auf großen oder extragroßen Plattformen platziert werden, um Strom zu erzeugen. Es erzeugt nur Strom, wenn es Sonnenlicht ausgesetzt ist.");
            CraftingTest.gWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gWindturbine.SetDescription("Die Große Windturbine dient in erster Linie der Stromerzeugung. Die Leistung variiert nicht in Abhängigkeit von der Windgeschwindigkeit.");
            CraftingTest.gTPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.mittlerDrucker);
            CraftingTest.gTPlattform.SetDescription("folgt noch :)");
            CraftingTest.gGPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.gGPlattform.SetDescription("folgt noch :)");
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
            CraftingTest.unterkunft.SetDescription("Die Unterkunkt ist ein Basisbauelement in Astroneer und zentraler Punkt permanenter Basen. Wenn Sie ein neues Spiel beginnen und das Landungsschiff verlassen, baut sich ein Unterstand zusammen mit einem nahe gelegenen stationären Landeplatz auf und verwandelt das umgebende Gelände in eine schwarzgraue Erde, die nur mit einem Bohrer oder Bohrer-Mod entfernt werden kann .\n Die Unterkunft kann nicht bewegt oder mit einem Packager verpackt werden, nachdem er platziert wurde.\n Sie wird mit einem dauerhaft angebrachten RTG geliefert, das nicht entfernt werden kann. Es erzeugt jedoch ein Viertel der Leistung im Vergleich zum herstellbaren RTG.\n Hergestellte Unterstände enthalten einen Sauerstoffmacher.");
            CraftingTest.solarstromanlage.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.glas, MaterialTest.kupfer }, CraftingTest.grosserDrucker);
            CraftingTest.solarstromanlage.SetDescription("Das Solar Array ist ein Element zur Stromerzeugung in Astroneer . Es benötigt Sonnenlicht, um Strom zu liefern , und benötigt keine Plattform, um zu funktionieren.");
            CraftingTest.autoExtraktor.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.wolframkarbid, MaterialTest.stahl, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.autoExtraktor.SetDescription("Der Auto Extractor ist ein Automatisierungselement in Astroneer . Es ermöglicht den Spielern, langsam Ressourcen aus Lagerstätten zu extrahieren, ohne Gelände zu entfernen, im Austausch dafür, dass der Spieler die 15-fache Menge an Nuggets erhält, die er normalerweise von Hand ablegen würde.");
            CraftingTest.windturbineXl.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.keramik, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.windturbineXl.SetDescription("Die XL-Windkraftanlage dient in erster Linie der Stromerzeugung. Die Leistung variiert nicht in Abhängigkeit von der Windgeschwindigkeit.");
            CraftingTest.mSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.mSensorbogen.SetDescription("Der mittlere Sensorbogen hat 6 Reaktionsschlitze , die aktiviert werden, wenn der Spieler oder einige Objekte den Ring passieren, Lichter einschalten oder Feuerwerke in den Schlitzen zünden.");
            CraftingTest.xlSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbogen.SetDescription("Der XL Sensor Arch kann mit der Freizeitkugel als eine andere Art von Tor verwendet werden, ähnlich wie der XL-Sensorbaldachin. Es kann auch als Kontrollpunkt für ein Rennen verwendet werden, indem die Reaktionsschlitze verwendet werden, um Lichter oder Feuerwerk zu aktivieren.");
            CraftingTest.xlSensorbaladachin.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbaladachin.SetDescription("Der XL-Sensorbaldachin ist ein Tornetz, das in Kombination mit der Freizeitkugel verwendet werden soll, um jede Art von Sport in einem Multiplayer-Spiel zu spielen. Wenn etwas die blaue Sensorbarriere passiert, wird es jeden Gegenstand auf dem Reaktionsschlitz auf der Rückseite aktivieren oder mit den Zielstiften verbinden.");
            CraftingTest.gSensorring.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorring.SetDescription("Der große Sensorring hat 6 Reaktionsschlitze , die aktiviert werden, wenn der Spieler oder einige Objekte den Ring passieren, Lichter einschalten oder Feuerwerke zünden.");
            CraftingTest.gSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringA.SetDescription("Wenn der Ring von einer Kugel , einer Erholungskugel oder einem Spieler durchquert wird, wird alles innerhalb der Tier-1-Slots um den Ring herum aktiviert.");
            CraftingTest.gSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringB.SetDescription("Wenn der Ring von einer Kugel , einer Erholungskugel oder einem Spieler durchquert wird, wird alles innerhalb der Reaktionsschlitze um den Ring herum aktiviert.");
            CraftingTest.xlSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringA.SetDescription("XL-Sensorring A kann verwendet werden, um zu erkennen, wenn eine Kugel, eine Spielkugel , ein Fahrzeug oder ein Spieler den bläulichen Sensorbereich passiert. Wenn einer erkannt wird, werden die Reaktionsslots aktiviert.\n Dieser Sensor kann insbesondere verwendet werden, um zu erkennen, wenn ein Shuttle auf einem Landeplatz oder einer Landezone landet.");
            CraftingTest.xlSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringB.SetDescription("XL-Sensorring B kann verwendet werden, um zu erkennen, wenn eine Kugel, eine Spielkugel , ein Fahrzeug oder ein Spieler den bläulichen Sensorbereich passiert. Wenn einer erkannt wird, werden die Reaktionsslots aktiviert.\n Dieser Sensor kann insbesondere verwendet werden, um zu erkennen, wenn ein Shuttle auf einem Landeplatz oder einer Landezone landet.");
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
            GalastropodenTest.Sylvie.SetTerrarium(new GTerrarium(MaterialTest.erde,MaterialTest.zink,PflanzenTest.Huepfranke));
            GalastropodenTest.Sylvie.SetBuff("Leuchtet einen Bereich stark aus");
            GalastropodenTest.Usagi.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.wolfram, PflanzenTest.Dolchwurzel));
            GalastropodenTest.Usagi.SetBuff("Verfolgt auf dem aktuellen Planeten die nächstgelegenen wertvollen Kuriositäten auf dem Kompass");
            GalastropodenTest.Stilgar.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.kupfer, PflanzenTest.Keuchkraut));
            GalastropodenTest.Stilgar.SetBuff("Produziert passiv Sauerstoff, der Tanks und Haltenetze füllt");
            GalastropodenTest.Princess.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.lithium, PflanzenTest.Peitschenblatt));
            GalastropodenTest.Princess.SetBuff("Verhindert beim Tragen im Rucksack alle Formen von Schäden außer Ersticken");
            GalastropodenTest.Rogal.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.eisen, PflanzenTest.Distelgerte));
            GalastropodenTest.Rogal.SetBuff("Erzeugt eine beträchtliche Menge an Strom");
            GalastropodenTest.Bestefar.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.argon, PflanzenTest.Knallkoralle));
            GalastropodenTest.Bestefar.SetBuff("Verbessert, während es am Terrain Tool befestigt ist, seine Breite, Verstärkung und Bohrfähigkeit");
            GalastropodenTest.Enoki.SetTerrarium(new GTerrarium(MaterialTest.erde, MaterialTest.helium, PflanzenTest.Stachellilie));
            GalastropodenTest.Enoki.SetBuff("Erhöht beim Tragen im Rucksack die Sprunghöhe, die Sprintgeschwindigkeit und verringert die Bewegungseinbußen beim Tragen schwerer Gegenstände");
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