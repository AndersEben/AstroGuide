using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AstroGuide.Scripts;
using AstroGuide.Scripts.CustViews;
using AstroGuide.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class PlanetActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.planet);

            var plan = PlanetenTest.FindPlanet(Intent.GetStringExtra("Planet"));

            var pName = FindViewById<TextView>(Resource.Id.PlanetName);
            pName.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementM);
            pName.Text = plan.Name;

            FindViewById<ImageView>(Resource.Id.PlanetBild).SetImageResource(plan.Image);
            FindViewById<TextView>(Resource.Id.PlanetType).Text = plan.Typ;
            FindViewById<TextView>(Resource.Id.PlanetSize).Text = plan.Groesse.ToString();
            FindViewById<TextView>(Resource.Id.PlanetDiff).Text = plan.Schwierigkeitsgrad.ToString();

            var penergie = FindViewById<ListView>(Resource.Id.PlanetEnergiequellen);
            penergie.Adapter = new AddEnergie(this, plan.Energieqiellen);
            penergie.LayoutParameters.Height = (plan.Energieqiellen.Count * Einstellungen.ListItemHeight + Einstellungen.PlanetOffset);

            var pressource = FindViewById<ListView>(Resource.Id.PlanetRessourcen);
            pressource.Adapter = new AddRessource(this, plan.Ress);
            pressource.LayoutParameters.Height = (plan.Ress.Count * Einstellungen.ListItemHeight + Einstellungen.PlanetOffset);
            pressource.ItemClick += (o,e) =>
            {
                var item = pressource.Adapter as AddRessource;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", ress.Ress.Name);
                this.StartActivity(intent);
            };

            var ppflanze = FindViewById<ListView>(Resource.Id.PlanetPflanzen);
            ppflanze.Adapter = new AddPflanzen(this, plan.Pflanzen);
            ppflanze.LayoutParameters.Height = (plan.Pflanzen.Count * Einstellungen.ListItemHeight + Einstellungen.PlanetOffset);
            ppflanze.ItemClick += (o, e) =>
            {
                var item = ppflanze.Adapter as AddPflanzen;
                var plant = item[e.Position];

                Intent intent = new Intent(this, typeof(PflanzeActivity));
                intent.PutExtra("Pflanze", plant.Name);
                this.StartActivity(intent);
            };

            var portalRess = FindViewById<TextView>(Resource.Id.PlanetPortalRes);
            portalRess.Text = plan.PortalElement.Name;
            portalRess.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };

            var portalRessImage = FindViewById<ImageView>(Resource.Id.PlanetPortalResImage);
            portalRessImage.SetImageResource(plan.PortalElement.Image);
            portalRessImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", plan.PortalElement.Name);
                this.StartActivity(intent);
            };


            var Galastro = FindViewById<TextView>(Resource.Id.PlanetGalatrop);
            Galastro.Text = plan.Galastro.Name;
            Galastro.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

            var GalastroImage = FindViewById<ImageView>(Resource.Id.PlanetGalatropImage);
            GalastroImage.SetImageResource(plan.Galastro.Icon);
            GalastroImage.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", plan.Galastro.Name);
                this.StartActivity(intent);
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}