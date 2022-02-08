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
    class PflanzeActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.pflanze);

            var plant = PflanzenTest.FindPLant(Intent.GetStringExtra("Pflanze"));

            FindViewById<TextView>(Resource.Id.PflanzeName).Text = plant.Name;
            FindViewById<ImageView>(Resource.Id.PflanzeBild).SetImageResource(plant.Image);
            FindViewById<TextView>(Resource.Id.PflanzeType).Text = plant.Typ.ToString();
            FindViewById<TextView>(Resource.Id.PflanzeBeschreibung).Text = plant.Beschreibung;

            var planeten = PlanetenTest.Alle_Planeten.FindAll(x => x.Pflanzen.FindAll(y => y.Name == plant.Name).Count > 0);

            var pfPlanetenList = FindViewById<ListView>(Resource.Id.PflanzePlaneten);
            pfPlanetenList.Adapter = new AddVorkommen(this, planeten);
            pfPlanetenList.LayoutParameters.Height = (planeten.Count * Einstellungen.ListItemHeight);
            pfPlanetenList.ItemClick += (o, e) =>
            {
                var item = pfPlanetenList.Adapter as AddVorkommen;
                var plan = item[e.Position];

                Intent intent = new Intent(this, typeof(PlanetActivity));
                intent.PutExtra("Planet", plan.Name);
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