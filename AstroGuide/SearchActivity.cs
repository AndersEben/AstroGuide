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
    class SearchActivity : AppCompatActivity
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



            FrameLayout.LayoutParams ImageparamLeft = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageView TBImageLeft = new ImageView(this);
            ImageparamLeft.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
            TBImageLeft.LayoutParameters = ImageparamLeft;
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
            TBText.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            TBFrameLayout.AddView(TBText);

            FrameLayout.LayoutParams ImageparamRight = new FrameLayout.LayoutParams(Einstellungen.TextSizeListOffset / Einstellungen.TB_Image, Einstellungen.TextSizeListOffset / Einstellungen.TB_Image);
            ImageView TBImageRight = new ImageView(this);
            ImageparamRight.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            TBImageRight.LayoutParameters = ImageparamRight;
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
            txtv.Text = "Suchen";
            TBText.Text = "Suchen";

            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);
            txtv.Gravity = GravityFlags.Center;

            LL.AddView(txtv);

            ScrollView SV = new ScrollView(this);
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


            ListView LSearch = new ListView(this);
            LSearch.NestedScrollingEnabled = true;
            LSearch.LayoutParameters = new LinearLayout.LayoutParams( LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);

            LSearch.Adapter = new AddVerwendungen(this, new List<Verwendung>());
            LSearch.ItemClick += (o, e) =>
            {
                var item = LSearch.Adapter as AddVerwendungen;
                var file = item[e.Position];

                switch (file.Typ)
                {
                    case VerwendungsTyp.Ressource:
                        Intent intent = new Intent(this, typeof(ResActivity));
                        intent.PutExtra("Ressource", file.Name);
                        this.StartActivity(intent);
                        break;
                    case VerwendungsTyp.Crafter:
                        Intent intent2 = new Intent(this, typeof(CrafterActivity));
                        intent2.PutExtra("Crafter", file.Name);
                        this.StartActivity(intent2);
                        break;
                    case VerwendungsTyp.Craft:
                        Intent intent3 = new Intent(this, typeof(CraftActivity));
                        intent3.PutExtra("Craft", file.Name);
                        this.StartActivity(intent3);
                        break;
                    case VerwendungsTyp.Pflanze:
                        Intent intent4 = new Intent(this, typeof(PflanzeActivity));
                        intent4.PutExtra("Pflanze", file.Name);
                        this.StartActivity(intent4);
                        break;
                    case VerwendungsTyp.Galastropode:
                        Intent intent5 = new Intent(this, typeof(GalastroActivity));
                        intent5.PutExtra("Galastropode", file.Name);
                        this.StartActivity(intent5);
                        break;
                    case VerwendungsTyp.Planet:
                        Intent intent6 = new Intent(this, typeof(PlanetActivity));
                        intent6.PutExtra("Planet", file.Name);
                        this.StartActivity(intent6);
                        break;
                }
            };

            SearchView searchView = new SearchView(this);
            searchView.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);
            searchView.SetIconifiedByDefault(false);
            searchView.QueryTextChange += (o, e) =>
            {
                List<Verwendung> gefunden = new List<Verwendung>();

                if (e.NewText == "" || e.NewText == null)
                {
                    LSearch.Adapter = new AddVerwendungen(this, gefunden);
                    LSearch.LayoutParameters.Height = (gefunden.Count * Einstellungen.ListPlanetHeight);
                }
                else
                {
                    gefunden = VerwendungTest.AlleElemente.FindAll(x => x.Name.ToLower().Contains(e.NewText.ToLower()));

                    LSearch.Adapter = new AddVerwendungen(this, gefunden);
                    LSearch.LayoutParameters.Height = (gefunden.Count * Einstellungen.ListPlanetHeight);
                }
            };

            SVLL.AddView(searchView);
            SVLL.AddView(LSearch);

            LSearch.LayoutParameters.Height = (0 * Einstellungen.ListPlanetHeight);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}