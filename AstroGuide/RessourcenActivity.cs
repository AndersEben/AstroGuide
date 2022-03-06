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
    class RessourcenActivity : AppCompatActivity
    {
        private RelativeLayout SetRelativeLayout(string name)
        {

            RelativeLayout RL = new RelativeLayout(this);
            RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent,
                                                                                RelativeLayout.LayoutParams.WrapContent);
            RL.LayoutParameters = param;

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Horizontal;

            TextView txtv = new TextView(this);
            txtv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                ViewGroup.LayoutParams.WrapContent);
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_ElementXL);
            txtv.Text = name;
            txtv.SetPadding(Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip, Einstellungen.TXT_pixel10dip);

            LL.AddView(txtv);
            RL.AddView(LL);

            ImageView imgv = new ImageView(this);
            RelativeLayout.LayoutParams parm;
            parm = new RelativeLayout.LayoutParams(50, 50);
            parm.AddRule(LayoutRules.AlignParentRight);
            parm.AddRule(LayoutRules.CenterVertical);
            imgv.LayoutParameters = parm;
            imgv.SetImageResource(Resource.Drawable.plus);

            RL.AddView(imgv);

            return RL;
        }

        private Tuple<LinearLayout,ListView> SetLinearLayout(RelativeLayout RL, List<Ressource> test)
        {
            int pixel = (int)Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            LinearLayout LL = new LinearLayout(this);
            LinearLayout.LayoutParams Lparam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                                LinearLayout.LayoutParams.WrapContent);
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            LL.SetPadding(Einstellungen.LL_AddE_padding_left, Einstellungen.LL_AddE_padding_top, Einstellungen.LL_AddE_padding_right, Einstellungen.LL_AddE_padding_bottem);

            ListView lv = new ListView(this);
            lv.Adapter = new AddRessourcen(this, test);

            lv.NestedScrollingEnabled = true;
            lv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                LinearLayout.LayoutParams.WrapContent);

            LL.AddView(lv);
            lv.ItemClick += (o, e) =>
            {
                var item = lv.Adapter as AddRessourcen;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", ress.Name);
                this.StartActivity(intent);
            };

            lv.LayoutParameters.Height = test.Count * Einstellungen.BigAdapterSpaceCalc;
            lv.Visibility = ViewStates.Gone;

            RL.Click += (o, e) =>
            {
                for (int i = 0; i < RL.ChildCount; i++)
                {
                    if (RL.GetChildAt(i).GetType() == typeof(ImageView))
                    {
                        var image = (ImageView)RL.GetChildAt(i);

                        if (lv.Visibility == ViewStates.Gone)
                        {
                            image.SetImageResource(Resource.Drawable.minus);
                            lv.Visibility = ViewStates.Visible;
                        }
                        else
                        {
                            image.SetImageResource(Resource.Drawable.plus);
                            lv.Visibility = ViewStates.Gone;
                        }

                        break;
                    }
                }
                
                
            };

            return Tuple.Create(LL,lv);
        }


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
            LL.LayoutParameters = Lparam;
            LL.Orientation = Orientation.Vertical;
            TBLL.AddView(LL);

            TextView txtv = new TextView(this);
            var param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            param.SetMargins(Einstellungen.LL_E1_margin_left, Einstellungen.LL_E1_margin_top, Einstellungen.LL_E1_margin_right, Einstellungen.LL_E1_margin_bottem);
            txtv.LayoutParameters = param;
            txtv.SetTextSize(Android.Util.ComplexUnitType.Px, Einstellungen.TextSizeListOffset / Einstellungen.TXT_HeaderSize);
            txtv.Visibility = ViewStates.Gone;
            txtv.Text = "Ressourcen";
            TBText.Text = "Ressourcen";

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



            ListView searchlv = new ListView(this);
            searchlv.Adapter = new AddRessourcen(this, new List<Ressource>());

            searchlv.NestedScrollingEnabled = true;
            searchlv.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);
            
            searchlv.ItemClick += (o, e) =>
            {
                var item = searchlv.Adapter as AddRessourcen;
                var ress = item[e.Position];

                Intent intent = new Intent(this, typeof(ResActivity));
                intent.PutExtra("Ressource", ress.Name);
                this.StartActivity(intent);
            };

            SearchView searchView = new SearchView(this);
            var LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
                                                                        LinearLayout.LayoutParams.WrapContent);
            LayoutParameters.SetMargins(0, 0, 0, 45);
            searchView.LayoutParameters = LayoutParameters;
            searchView.SetIconifiedByDefault(false);

            SVLL.AddView(searchView);
            SVLL.AddView(searchlv);
            searchlv.LayoutParameters.Height = (0 * Einstellungen.ListPlanetHeight);




            List<ListView> LvList = new List<ListView>();
            List<RelativeLayout> LayoutsR = new List<RelativeLayout>();

            foreach (string item in Enum.GetNames(typeof(ResType)))
            {
                var selCrafts = MaterialTest.Alle_Ressourcen.FindAll(x => x.Type.ToString() == item);
                var Rlayout = SetRelativeLayout(Funktionen.ShowEnumLabel((ResType)System.Enum.Parse(typeof(ResType), item)));
                var layout = SetLinearLayout(Rlayout, selCrafts);

                SVLL.AddView(Rlayout);
                SVLL.AddView(layout.Item1);

                LvList.Add(layout.Item2);
                LayoutsR.Add(Rlayout);
            }


            searchView.QueryTextChange += (o, e) =>
            {

                List<Ressource> gefunden = new List<Ressource>();

                if (e.NewText == "" || e.NewText == null)
                {
                    searchlv.Adapter = new AddRessourcen(this, gefunden);
                    searchlv.LayoutParameters.Height = gefunden.Count * Einstellungen.BigAdapterSpaceCalc;

                    foreach (var item in LayoutsR)
                    {
                        item.Visibility = ViewStates.Visible;
                    }

                }
                else
                {
                    gefunden = MaterialTest.AllRessource().FindAll(x => x.Name.ToLower().Contains(e.NewText.ToLower()));

                    searchlv.Adapter = new AddRessourcen(this, gefunden);
                    searchlv.LayoutParameters.Height = gefunden.Count * Einstellungen.BigAdapterSpaceCalc;


                    foreach (var item in LvList)
                    {
                        item.Visibility = ViewStates.Gone;
                    }
                    foreach (var item in LayoutsR)
                    {
                        item.Visibility = ViewStates.Gone;
                    }
                }
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}