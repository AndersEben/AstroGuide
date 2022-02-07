using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AndroidX.AppCompat.App;

using AstroGuide.Scripts;
using AstroGuide.Scripts.CustViews;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class PlanetenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.planeten);

            var LPlaneten = FindViewById<ListView>(Resource.Id.ListPlaneten);
            LPlaneten.Adapter = new AddPlaneten(this, PlanetenTest.Alle_Planeten);
            LPlaneten.ItemClick += (o, e) =>
            {
                var item = LPlaneten.Adapter as AddPlaneten;
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