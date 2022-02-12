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


            //SetContentView(Resource.Layout.start);


            //var LL = FindViewById<LinearLayout>(Resource.Id.MainTestLayout);

            LinearLayout LL = new LinearLayout(this);
            LL.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            LL.Orientation = Orientation.Vertical;



            var buttonParam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            buttonParam.Gravity = Android.Views.GravityFlags.Center;
            buttonParam.TopMargin = 75;

            var PlanetenButton = new Button(this);
            PlanetenButton.LayoutParameters = buttonParam;
            PlanetenButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            PlanetenButton.Gravity = Android.Views.GravityFlags.Center;
            PlanetenButton.Text = "Planeten";
            //pflanzenButton.SetBackgroundColor(new Android.Graphics.Color(-16115236));
            PlanetenButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(PlanetenActivity));
                this.StartActivity(intent);
            };

            var ressourcenButton = new Button(this);
            ressourcenButton.LayoutParameters = buttonParam;
            ressourcenButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            ressourcenButton.Gravity = Android.Views.GravityFlags.Center;
            ressourcenButton.Text = "Ressourcen";
            //pflanzenButton.SetBackgroundColor(new Android.Graphics.Color(-16115236));
            ressourcenButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(RessourcenActivity));
                this.StartActivity(intent);
            };

            var craftingButton = new Button(this);
            craftingButton.LayoutParameters = buttonParam;
            craftingButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            craftingButton.Gravity = Android.Views.GravityFlags.Center;
            craftingButton.Text = "Crafting";
            //pflanzenButton.SetBackgroundColor(new Android.Graphics.Color(-16115236));
            craftingButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(CraftingActivity));
                this.StartActivity(intent);
            };

            var pflanzenButton = new Button(this);
            pflanzenButton.LayoutParameters = buttonParam;
            pflanzenButton.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            pflanzenButton.Gravity = Android.Views.GravityFlags.Center;
            pflanzenButton.Text = "Pflanzen";
            //pflanzenButton.SetBackgroundColor(new Android.Graphics.Color(-16115236));
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
            //galastroButton.SetBackgroundColor(new Android.Graphics.Color(-16115236));
            galastroButton.Click += (o, e) =>
            {
                Intent intent = new Intent(this, typeof(GalastropodenActivity));
                this.StartActivity(intent);
            };

            LL.AddView(PlanetenButton);
            LL.AddView(ressourcenButton);
            LL.AddView(craftingButton);
            LL.AddView(pflanzenButton);
            LL.AddView(galastroButton);

            SetContentView(LL);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}