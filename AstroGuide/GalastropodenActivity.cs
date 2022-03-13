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

            LinearLayout TBLL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            TBLL.LayoutParameters = Lparam;
            TBLL.Orientation = Orientation.Vertical;

            Toolbar ToolBar = new Toolbar(this);
            var toolbarparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ToolBar.Background = Resources.GetDrawable(Resource.Color.colorPrimary);
            ToolBar.LayoutParameters = toolbarparam;
            ToolBar.SetContentInsetsAbsolute(10, 10);
            ToolBar.ContentInsetStartWithNavigation = 0;

            TBLL.AddView(ToolBar);

            FrameLayout TBFrameLayout = new FrameLayout(this);
            var FrameLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            TBFrameLayout.LayoutParameters = FrameLparam;
            ToolBar.AddView(TBFrameLayout);



            FrameLayout.LayoutParams ImageparamLeft = new FrameLayout.LayoutParams(100, 100);
            ImageView TBImageLeft = new ImageView(this);
            ImageparamLeft.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
            TBImageLeft.LayoutParameters = ImageparamLeft;
            TBImageLeft.SetPadding(0, 0, 25, 0);
            TBImageLeft.SetImageResource(Resource.Drawable.backbutton_white);
            TBFrameLayout.AddView(TBImageLeft);
            TBImageLeft.Click += (o, e) =>
            {
                base.OnBackPressed();
            };

            FrameLayout.LayoutParams Textparam = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WrapContent, FrameLayout.LayoutParams.MatchParent);
            TextView TBText = new TextView(this);
            Textparam.Gravity = GravityFlags.Center;
            TBText.LayoutParameters = Textparam;
            TBText.SetTextColor(Android.Graphics.Color.White);
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, 85);
            TBFrameLayout.AddView(TBText);

            FrameLayout.LayoutParams ImageparamRight = new FrameLayout.LayoutParams(100, 100);
            ImageView TBImageRight = new ImageView(this);
            ImageparamRight.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            TBImageRight.LayoutParameters = ImageparamRight;
            TBImageRight.SetPadding(0, 0, 25, 0);
            TBImageRight.SetImageResource(Resource.Drawable.homebutton_white);
            TBFrameLayout.AddView(TBImageRight);
            TBImageRight.Click += (o, e) =>
            {
                FinishAffinity();
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            };

            LinearLayout LL = new LinearLayout(this);
            //LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            TBLL.AddView(LL);

            TextView txtv = new TextView(this);
            var param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            param.SetMargins(Einstellungen.LL_E1_margin_left, Einstellungen.LL_E1_margin_top, Einstellungen.LL_E1_margin_right, Einstellungen.LL_E1_margin_bottem);
            txtv.LayoutParameters = param;
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            txtv.Visibility = ViewStates.Gone;
            txtv.Text = "Galstropoden";
            TBText.Text = "Galstropoden";

            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);
            txtv.Gravity = GravityFlags.Center;

            LL.AddView(txtv);

            AndroidX.Core.Widget.NestedScrollView SV = new AndroidX.Core.Widget.NestedScrollView(this);
            LinearLayout.LayoutParams SVparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SV.LayoutParameters = SVparam;

            LL.AddView(SV);

            LinearLayout SVLL = new LinearLayout(this);
            LinearLayout.LayoutParams SVLparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            SVLparam.TopMargin = Einstellungen.TextSizeListOffset / Einstellungen.Margin_M;
            SVLL.LayoutParameters = SVLparam;
            SVLL.Orientation = Orientation.Vertical;

            SV.AddView(SVLL);


            SetContentView(TBLL);

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