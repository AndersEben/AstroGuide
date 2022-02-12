using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

using AstroGuide.Scripts;
using AstroGuide.Scripts.CustViews;
using AstroGuide.Scripts.Planeten;
using AstroGuide.Scripts.Settings;
using System.Collections.Generic;

namespace AstroGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            SetContentView(Resource.Layout.start);


            var b1 = FindViewById<Button>(Resource.Id.SPlaneten);
            b1.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset/ Einstellungen.TXT_HeaderSize);
            b1.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(PlanetenActivity));
                this.StartActivity(intent);
            };

            var b2 = FindViewById<Button>(Resource.Id.SRessourcen);
            b2.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset/ Einstellungen.TXT_HeaderSize);
            b2.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(RessourcenActivity));
                this.StartActivity(intent);
            };

            var b3 = FindViewById<Button>(Resource.Id.SCrafting);
            b3.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset/ Einstellungen.TXT_HeaderSize);
            b3.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(CraftingActivity));
                this.StartActivity(intent);
            };

            var LL = FindViewById<LinearLayout>(Resource.Id.MainTestLayout);
            var buttonParam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            buttonParam.Gravity = Android.Views.GravityFlags.Center;
            buttonParam.TopMargin = 75;

            var pflanzenButton = new Button(this);
            pflanzenButton.LayoutParameters = buttonParam;
            pflanzenButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            pflanzenButton.Gravity = Android.Views.GravityFlags.Center;
            pflanzenButton.Text = "Pflanzen";
            pflanzenButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(PflanzenActivity));
                this.StartActivity(intent);
            };

            var galastroButton = new Button(this);
            galastroButton.LayoutParameters = buttonParam;
            galastroButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            galastroButton.Gravity = Android.Views.GravityFlags.Center;
            galastroButton.Text = "Galastropoden";
            galastroButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(CraftingActivity));
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