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

            CreateMaterial();
            CreateCrafting();
            CreatePlaneten();

            await Task.Delay(2000);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
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
            CraftingTest.rucksack.SetDescription("");
            CraftingTest.kleinerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerDrucker.SetDescription("");
            CraftingTest.mittlerDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.mittlerDrucker.SetDescription("");
            CraftingTest.grosserDrucker.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.grosserDrucker.SetDescription("");


            CraftingTest.verbindungen.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.verbindungen.SetDescription("");
            CraftingTest.sauerstofffilter.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.sauerstofffilter.SetDescription("");
            CraftingTest.kleinerKanister.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kleinerKanister.SetDescription("");
            CraftingTest.leuchtfeuer.SetHerstellung(new List<Ressource>() { MaterialTest.quartz }, CraftingTest.rucksack);
            CraftingTest.leuchtfeuer.SetDescription("");
            CraftingTest.arbeitslicht.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.arbeitslicht.SetDescription("");
            CraftingTest.leuchtstab.SetHerstellung(new List<Ressource>() { MaterialTest.organisch }, CraftingTest.rucksack);
            CraftingTest.leuchtstab.SetDescription("");
            CraftingTest.scheinwerfer.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram }, CraftingTest.rucksack);
            CraftingTest.scheinwerfer.SetDescription("");
            CraftingTest.verpacker.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.verpacker.SetDescription("");
            CraftingTest.kleinersauerstofftank.SetHerstellung(new List<Ressource>() { MaterialTest.glas }, CraftingTest.rucksack);
            CraftingTest.kleinersauerstofftank.SetDescription("");
            CraftingTest.tragbarerSauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung }, CraftingTest.rucksack);
            CraftingTest.tragbarerSauerstoffmacher.SetDescription("");
            CraftingTest.kleinerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch }, CraftingTest.rucksack);
            CraftingTest.kleinerGenerator.SetDescription("");
            CraftingTest.kleinesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.rucksack);
            CraftingTest.kleinesSolarmodul.SetDescription("");
            CraftingTest.kleineWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.kleineWindturbine.SetDescription("");
            CraftingTest.energiezellen.SetHerstellung(new List<Ressource>() { MaterialTest.graphit }, CraftingTest.rucksack);
            CraftingTest.energiezellen.SetDescription("");
            CraftingTest.kleineBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.kleineBatterie.SetDescription("");
            CraftingTest.verlaengerungen.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.verlaengerungen.SetDescription("");
            CraftingTest.boostModus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.boostModus.SetDescription("");
            CraftingTest.breiteAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.breiteAnpassung.SetDescription("");
            CraftingTest.schmaleAnpassung.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.schmaleAnpassung.SetDescription("");
            CraftingTest.hemmer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.hemmer.SetDescription("");
            CraftingTest.ausrichtungsmodus.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.ausrichtungsmodus.SetDescription("");
            CraftingTest.gelaendeAnalysator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.rucksack);
            CraftingTest.gelaendeAnalysator.SetDescription("");
            CraftingTest.bohreranpassungSt1.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt1.SetDescription("");
            CraftingTest.bohreranpassungSt2.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt2.SetDescription("");
            CraftingTest.bohreranpassungSt3.SetHerstellung(new List<Ressource>() { MaterialTest.diamant }, CraftingTest.rucksack);
            CraftingTest.bohreranpassungSt3.SetDescription("");
            CraftingTest.dynamit.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.dynamit.SetDescription("");
            CraftingTest.feuerwerk.SetHerstellung(new List<Ressource>() { MaterialTest.sprengpulver }, CraftingTest.rucksack);
            CraftingTest.feuerwerk.SetDescription("");
            CraftingTest.kleineKamera.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.kleineKamera.SetDescription("");
            CraftingTest.hoverboard.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip }, CraftingTest.rucksack);
            CraftingTest.hoverboard.SetDescription("");
            CraftingTest.sondenscanner.SetHerstellung(new List<Ressource>() { MaterialTest.stahl }, CraftingTest.rucksack);
            CraftingTest.sondenscanner.SetDescription("");
            CraftingTest.kTrompetenhupe.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.rucksack);
            CraftingTest.kTrompetenhupe.SetDescription("");
            CraftingTest.hydrazinJezpack.SetHerstellung(new List<Ressource>() { MaterialTest.titanlegierung }, CraftingTest.rucksack);
            CraftingTest.hydrazinJezpack.SetDescription("");
            CraftingTest.holografischeFigur.SetHerstellung(new List<Ressource>() { MaterialTest.kunststoff }, CraftingTest.rucksack);
            CraftingTest.holografischeFigur.SetDescription("");
            CraftingTest.feststoffSprungbooster.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung }, CraftingTest.rucksack);
            CraftingTest.feststoffSprungbooster.SetDescription("");
            CraftingTest.einebnungsblock.SetHerstellung(new List<Ressource>() { MaterialTest.erde }, CraftingTest.rucksack);
            CraftingTest.einebnungsblock.SetDescription("");


            CraftingTest.feldunterkunft.SetHerstellung(new List<Ressource>() { MaterialTest.graphen, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.feldunterkunft.SetDescription("");
            CraftingTest.autoArm.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.autoArm.SetDescription("");
            CraftingTest.mgKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgKanister.SetDescription("");
            CraftingTest.mgFlKanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgFlKanister.SetDescription("");
            CraftingTest.mgGaskanister.SetHerstellung(new List<Ressource>() { MaterialTest.glas, MaterialTest.silikon }, CraftingTest.kleinerDrucker);
            CraftingTest.mgGaskanister.SetDescription("");
            CraftingTest.energiesensor.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.energiesensor.SetDescription("");
            CraftingTest.lagersensor.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.lagersensor.SetDescription("");
            CraftingTest.batteriesensor.SetHerstellung(new List<Ressource>() { MaterialTest.graphit, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.batteriesensor.SetDescription("");
            CraftingTest.tastenwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.tastenwiederholer.SetDescription("");
            CraftingTest.naeherungsinitiator.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.naeherungsinitiator.SetDescription("");
            CraftingTest.verzoegerungswiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.verzoegerungswiederholer.SetDescription("");
            CraftingTest.zahlwiederholer.SetHerstellung(new List<Ressource>() { MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.zahlwiederholer.SetDescription("");
            CraftingTest.netzschalter.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer }, CraftingTest.kleinerDrucker);
            CraftingTest.netzschalter.SetDescription("");
            CraftingTest.mgroßePlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformC.SetDescription("");
            CraftingTest.hPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hPlatform.SetDescription("");
            CraftingTest.mgTPlatform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgTPlatform.SetDescription("");
            CraftingTest.mgroßesSilo.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.titan }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSilo.SetDescription("");
            CraftingTest.hohesLager.SetHerstellung(new List<Ressource>() { MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.hohesLager.SetDescription("");
            CraftingTest.mgroßeHupe.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeHupe.SetDescription("");
            CraftingTest.planierer.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.aluminiumlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.planierer.SetDescription("");
            CraftingTest.mgroßesLager.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesLager.SetDescription("Lager, welches 8 Tier-1 Items/Module halten kann.");
            CraftingTest.mgroßePlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformA.SetDescription("Plattform mit einem einzigen Tier-2 Slot.");
            CraftingTest.roverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.roverSitz.SetDescription("Rover-Sitz für Fahrzeuge.");
            CraftingTest.sauerstoffmacher.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.sauerstoffmacher.SetDescription("Erlaubt den Verbindungen Sauerstoff zu transportieren.");
            CraftingTest.mgroßerGenerator.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.wolfram }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßerGenerator.SetDescription("Verbrennt Kohlenstoff und produziert Energie.");
            CraftingTest.mgroßesSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßesSolarmodul.SetDescription("Macht aus Sonnenlicht Strom.");
            CraftingTest.mgroßeWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeWindturbine.SetDescription("Macht aus Wind Strom.");
            CraftingTest.mgroßeBatterie.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.zink }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßeBatterie.SetDescription("Speichert 16 Balken Energie.");
            CraftingTest.rtg.SetHerstellung(new List<Ressource>() { MaterialTest.lithium, MaterialTest.nanocarbonlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.rtg.SetDescription("Erzeugt konstante 4A Energie.");
            CraftingTest.verteiler.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.graphit }, CraftingTest.kleinerDrucker);
            CraftingTest.verteiler.SetDescription("Verteilt und kontrolliert Energie.");
            CraftingTest.mgroßePlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.kleinerDrucker);
            CraftingTest.mgroßePlattformB.SetDescription("Mittelgroße Plattform mit einem Tier-2 Konnektor.");
            CraftingTest.schredder.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.kleinerDrucker);
            CraftingTest.schredder.SetDescription("Schreddert Schutt zu Schrott.");
            CraftingTest.traktor.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.kleinerDrucker);
            CraftingTest.traktor.SetDescription("Ein kleines Fahrzeug, zum frühen schnellen Transport.");
            CraftingTest.haenger.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch }, CraftingTest.kleinerDrucker);
            CraftingTest.haenger.SetDescription("Eine bewegbare Plattform mit einem Tier-2 Platz. Toll in Verbindung mit dem Traktor.");
            CraftingTest.bohrstaerke1.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid, MaterialTest.keramik }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke1.SetDescription("Wird benutzt um Erde und Ressourcen zu bekommen.");
            CraftingTest.bohrstaerke2.SetHerstellung(new List<Ressource>() { MaterialTest.wolframkarbid, MaterialTest.titanlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke2.SetDescription("Kann härteres Gestein als Stärke 1 graben");
            CraftingTest.bohrstaerke3.SetHerstellung(new List<Ressource>() { MaterialTest.diamant, MaterialTest.titanlegierung }, CraftingTest.kleinerDrucker);
            CraftingTest.bohrstaerke3.SetDescription("Kann durch das härteste Gestein graben.");
            CraftingTest.feststoffSchubduese.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.ammonium }, CraftingTest.kleinerDrucker);
            CraftingTest.feststoffSchubduese.SetDescription("Super für eine Reise zu einem Planeten und wieder runter.");
            CraftingTest.hydrazinSchubduese.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.exo_chip }, CraftingTest.kleinerDrucker);
            CraftingTest.hydrazinSchubduese.SetDescription("Nachfüllbare Schubdüse, angetrieben durch Hydrazin.");
            CraftingTest.winde.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.gummi }, CraftingTest.kleinerDrucker);
            CraftingTest.winde.SetDescription("Kann benutzt werden, um Objekte zu bewegen.");


            CraftingTest.gLager.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.mittlerDrucker);
            CraftingTest.gLager.SetDescription("besitzt 4 Tier-2 Steckplätze");
            CraftingTest.gPlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformA.SetDescription("eine Plattform");
            CraftingTest.gPlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformB.SetDescription("eine Plattform");
            CraftingTest.gPlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.keramik, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gPlattformC.SetDescription("eine Plattform");
            CraftingTest.forschungsKammer.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.forschungsKammer.SetDescription("generiert Bytes durch Erforschen");
            CraftingTest.schmelzofen.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.schmelzofen.SetDescription("verfeinert, schmilzt oder verbrennt Ressourcen");
            CraftingTest.erdzentrifuge.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.erdzentrifuge.SetDescription("extrahiert aus Boden bestimmte Ressourcen");
            CraftingTest.chemielabor.SetHerstellung(new List<Ressource>() { MaterialTest.wolfram, MaterialTest.glas, MaterialTest.keramik }, CraftingTest.mittlerDrucker);
            CraftingTest.chemielabor.SetDescription("kombiniert Ressourcen");
            CraftingTest.atmosphaerenkondensator.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.glas, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.atmosphaerenkondensator.SetDescription("sammelt sich in der Atmosphäre befindenden Gase ein");
            CraftingTest.handelsPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.wolfram, MaterialTest.eisen }, CraftingTest.mittlerDrucker);
            CraftingTest.handelsPlattform.SetDescription("handelt mit der Orbitalstation");
            CraftingTest.gSchredder.SetHerstellung(new List<Ressource>() { MaterialTest.exo_chip, MaterialTest.eisen, MaterialTest.wolframkarbid }, CraftingTest.mittlerDrucker);
            CraftingTest.gSchredder.SetDescription("schreddert Schutt zu Schrott");
            CraftingTest.buggy.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.buggy.SetDescription("kleines Fahrzeug");
            CraftingTest.mGrover.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.mGrover.SetDescription("mittelgroßes Fahrzeug");
            CraftingTest.gRoverSitz.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.gRoverSitz.SetDescription("ein Sitz mit 3 Plätzen");
            CraftingTest.kran.SetHerstellung(new List<Ressource>() { MaterialTest.titan, MaterialTest.silikon, MaterialTest.stahl }, CraftingTest.mittlerDrucker);
            CraftingTest.kran.SetDescription("steuerbarer Arm am Fahrzeug");
            CraftingTest.exoPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.exoPlattform.SetDescription("");
            CraftingTest.gRessourcenkanister.SetHerstellung(new List<Ressource>() { MaterialTest.nanocarbonlegierung, MaterialTest.titan, MaterialTest.glas }, CraftingTest.mittlerDrucker);
            CraftingTest.gRessourcenkanister.SetDescription("");
            CraftingTest.gSolarmodul.SetHerstellung(new List<Ressource>() { MaterialTest.kupfer, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gSolarmodul.SetDescription("");
            CraftingTest.gWindturbine.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.glas, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.gWindturbine.SetDescription("");
            CraftingTest.gTPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.mittlerDrucker);
            CraftingTest.gTPlattform.SetDescription("");
            CraftingTest.gGPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.gemisch }, CraftingTest.mittlerDrucker);
            CraftingTest.gGPlattform.SetDescription("");
            CraftingTest.gSiloA.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.mittlerDrucker);
            CraftingTest.gSiloA.SetDescription("");
            CraftingTest.gSiloB.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.stahl, MaterialTest.stahl }, CraftingTest.mittlerDrucker);
            CraftingTest.gSiloB.SetDescription("");
            CraftingTest.gEPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz }, CraftingTest.mittlerDrucker);
            CraftingTest.gEPlattform.SetDescription("");
            CraftingTest.gASpeicher.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.aluminium, MaterialTest.zink }, CraftingTest.mittlerDrucker);
            CraftingTest.gASpeicher.SetDescription("");
            CraftingTest.vtol.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.wolframkarbid, MaterialTest.exo_chip }, CraftingTest.mittlerDrucker);
            CraftingTest.vtol.SetDescription("");
            CraftingTest.gNebelhorn.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.gummi, MaterialTest.kunststoff }, CraftingTest.mittlerDrucker);
            CraftingTest.gNebelhorn.SetDescription("");
            CraftingTest.freizeitKugel.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.aluminiumlegierung }, CraftingTest.mittlerDrucker);
            CraftingTest.freizeitKugel.SetDescription("");


            CraftingTest.egPlattformA.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformA.SetDescription("");
            CraftingTest.egPlattformB.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformB.SetDescription("");
            CraftingTest.egPlattformC.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.harz, MaterialTest.harz }, CraftingTest.grosserDrucker);
            CraftingTest.egPlattformC.SetDescription("");
            CraftingTest.egLager.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egLager.SetDescription("Das Extragroße Lager kann bis zu 31 Tier-1-Gegenstände lagern.");
            CraftingTest.egSchredder.SetHerstellung(new List<Ressource>() { MaterialTest.stahl, MaterialTest.wolframkarbid, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.egSchredder.SetDescription("");
            CraftingTest.gRover.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.aluminiumlegierung, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.gRover.SetDescription("");
            CraftingTest.kShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.aluminium, MaterialTest.aluminium }, CraftingTest.grosserDrucker);
            CraftingTest.kShuttle.SetDescription("");
            CraftingTest.mShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.aluminiumlegierung, MaterialTest.aluminiumlegierung }, CraftingTest.grosserDrucker);
            CraftingTest.mShuttle.SetDescription("");
            CraftingTest.gShuttle.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.titanlegierung, MaterialTest.exo_chip, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.gShuttle.SetDescription("");
            CraftingTest.unterkunft.SetHerstellung(new List<Ressource>() { MaterialTest.silikon, MaterialTest.silikon, MaterialTest.kunststoff, MaterialTest.kunststoff }, CraftingTest.grosserDrucker);
            CraftingTest.unterkunft.SetDescription("");
            CraftingTest.solarstromanlage.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.glas, MaterialTest.kupfer }, CraftingTest.grosserDrucker);
            CraftingTest.solarstromanlage.SetDescription("");
            CraftingTest.autoExtraktor.SetHerstellung(new List<Ressource>() { MaterialTest.gummi, MaterialTest.wolframkarbid, MaterialTest.stahl, MaterialTest.exo_chip }, CraftingTest.grosserDrucker);
            CraftingTest.autoExtraktor.SetDescription("");
            CraftingTest.windturbineXl.SetHerstellung(new List<Ressource>() { MaterialTest.aluminiumlegierung, MaterialTest.graphen, MaterialTest.keramik, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.windturbineXl.SetDescription("");
            CraftingTest.mSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.mSensorbogen.SetDescription("");
            CraftingTest.xlSensorbogen.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbogen.SetDescription("");
            CraftingTest.xlSensorbaladachin.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorbaladachin.SetDescription("");
            CraftingTest.gSensorring.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorring.SetDescription("");
            CraftingTest.gSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringA.SetDescription("");
            CraftingTest.gSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.gSensorringB.SetDescription("");
            CraftingTest.xlSensorringA.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringA.SetDescription("");
            CraftingTest.xlSensorringB.SetHerstellung(new List<Ressource>() { MaterialTest.quartz, MaterialTest.zink, MaterialTest.zink, MaterialTest.zink }, CraftingTest.grosserDrucker);
            CraftingTest.xlSensorringB.SetDescription("");
            CraftingTest.egGPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.gemisch, MaterialTest.gemisch, MaterialTest.keramik, MaterialTest.keramik }, CraftingTest.grosserDrucker);
            CraftingTest.egGPlattform.SetDescription("");
            CraftingTest.xlEPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.harz, MaterialTest.harz, MaterialTest.harz }, CraftingTest.grosserDrucker);
            CraftingTest.xlEPlattform.SetDescription("");
            CraftingTest.fPlattform.SetHerstellung(new List<Ressource>() { MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen, MaterialTest.eisen }, CraftingTest.grosserDrucker);
            CraftingTest.fPlattform.SetDescription("");
            CraftingTest.landeplattform.SetHerstellung(new List<Ressource>() { MaterialTest.keramik, MaterialTest.keramik, MaterialTest.aluminium }, CraftingTest.grosserDrucker);
            CraftingTest.landeplattform.SetDescription("");

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