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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.CustTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class GalastropodenActivity : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;

            TextView txtv = new TextView(this);
            var param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            param.SetMargins(Einstellungen.LL_E1_margin_left, Einstellungen.LL_E1_margin_top, Einstellungen.LL_E1_margin_right, Einstellungen.LL_E1_margin_bottem);
            txtv.LayoutParameters = param;
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);

            txtv.Text = "Galstropoden";

            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);
            txtv.Gravity = GravityFlags.Center;

            LL.AddView(txtv);

            ScrollView SV = new ScrollView(this);
            LinearLayout.LayoutParams SVparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SV.LayoutParameters = SVparam;

            LL.AddView(SV);

            LinearLayout SVLL = new LinearLayout(this);
            LinearLayout.LayoutParams SVLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SVLL.LayoutParameters = SVLparam;
            SVLL.Orientation = Orientation.Vertical;

            SV.AddView(SVLL);


            SetContentView(LL);

            ListView LGalastro = new ListView(this);
            LGalastro.NestedScrollingEnabled = true;
            LGalastro.LayoutParameters = new LinearLayout.LayoutParams( LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);

            LGalastro.Adapter = new AddGalastro(this, GalastropodenTest.Alle_Galastro);
            LGalastro.ItemClick += (o, e) =>
            {
                var item = LGalastro.Adapter as AddGalastro;
                var gal = item[e.Position];

                Intent intent = new Intent(this, typeof(GalastroActivity));
                intent.PutExtra("Galastropode", gal.Name);
                this.StartActivity(intent);
            };

            SVLL.AddView(LGalastro);
            LGalastro.LayoutParameters.Height = (GalastropodenTest.Alle_Galastro.Count * Einstellungen.ListPlanetHeight);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}